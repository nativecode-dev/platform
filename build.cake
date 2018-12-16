#addin nuget:?package=Cake.Figlet
#addin nuget:?package=Cake.FileHelpers
#addin nuget:?package=Cake.Npx

// See: https://medium.com/@michael.wolfenden/simplified-versioning-and-publishing-for-net-libraries-a28e5e740fa6

///////////////////////////////////////////////////////////////////////////////
// ARGUMENTS
///////////////////////////////////////////////////////////////////////////////

var target = Argument<string>("target", "Default");
var configuration = Argument<string>("configuration", "Release");

///////////////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
///////////////////////////////////////////////////////////////////////////////

var projectName = "Cake.Npx";
var releaseVersion = "0.0.0";

var verbosity = DotNetCoreVerbosity.Minimal;

var packagesDir = Directory("./.artifacts/packages");
var publishedDir = Directory("./.artifacts/published");
var symbolsDir = Directory("./.artifacts/symbols");

var branchName = EnvironmentVariable("DRONE_BRANCH");
var isLocalBuild = HasEnvironmentVariable("CI") == false;
var isMasterBranch = StringComparer.OrdinalIgnoreCase.Equals("master", branchName);

var changesDetectedSinceLastRelease = false;
var shouldRelease = !isLocalBuild && isMasterBranch;

Action<NpxSettings> requiredSemanticVersionPackages = settings => settings
    .AddPackage("semantic-release@15.10.5")
    .AddPackage("@semantic-release/changelog@3.0.1")
    .AddPackage("@semantic-release/exec@3.3.0")
    ;

//////////////////////////////////////////////////////////////////////
// TASK VARIABLES
//////////////////////////////////////////////////////////////////////

const string SOLUTION = "**/*.sln";
const string SOLUTION_APPS = "src-tools/**/*.csproj";
const string SOLUTION_NUGET = "src/**/*.csproj;src-libs/**/*.csproj";
const string SOLUTION_TESTS = "src-tests/**/*.csproj";

///////////////////////////////////////////////////////////////////////////////
// SETUP / TEARDOWN
///////////////////////////////////////////////////////////////////////////////

Setup(context =>
{
    Information(Figlet(projectName));
    Information("Local build {0}", isLocalBuild);
    Information("Running on master branch {0}", isMasterBranch);
    Information("Should release {0}", shouldRelease);
});

Teardown(context =>
{
    Information("✔ Finished running tasks");
});

//////////////////////////////////////////////////////////////////////
// DEFAULT TASK
//////////////////////////////////////////////////////////////////////

Task("Default")
    .IsDependentOn("Banner")
    .IsDependentOn("Clean")
    .IsDependentOn("Version")
    .IsDependentOn("Build")
    // .IsDependentOn("Tests")
    .IsDependentOn("Publish")
    .IsDependentOn("Package")
    .IsDependentOn("Release")
    ;

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Banner").Does(() =>
{
    Information("dotnet --info");

    StartProcess("dotnet", new ProcessSettings { Arguments = "--info" });
});

Task("Clean").Does(() =>
{
    Information("🛈 Cleaning {0}, {1}, bin, and obj folders", packagesDir, symbolsDir);

    CleanDirectory(packagesDir);
    CleanDirectory(publishedDir);
    CleanDirectory(symbolsDir);
    CleanDirectories("./**/bin");
    CleanDirectories("./**/obj");
});

Task("Version")
    .WithCriteria(shouldRelease || target == "Version")
    .Does(() =>
{
    Information("🛈 Running semantic-release in dry run mode to extract next semantic version number");

    string[] semanticReleaseOutput;
    Npx("semantic-release", "--dry-run", requiredSemanticVersionPackages, out semanticReleaseOutput);
    Information(string.Join(Environment.NewLine, semanticReleaseOutput));

    var nextSemanticVersionNumber = ExtractNextSemanticVersionNumber(semanticReleaseOutput);

    if (nextSemanticVersionNumber == null) {
        Warning("There are no relevant changes, skipping release");
    } else {
        Information("🛈 Next semantic version number is {0}", nextSemanticVersionNumber);
        releaseVersion = nextSemanticVersionNumber;
        changesDetectedSinceLastRelease = true;
    }
})
.OnError(exception =>
{
    var logFiles = GetFiles("/root/.npm/_logs/*.log");
    foreach (var logFile in logFiles)
    {
        Information(FileReadText(logFile));
    }

    Information(exception.Message);
    Information(exception.StackTrace);

    throw exception;
});

Task("Build").Does(() =>
{
    var solutions = GetFiles(SOLUTION);

    foreach (var solution in solutions)
    {
        Information("🛈 Building solution {0} v{1}", solution.GetFilenameWithoutExtension(), releaseVersion);

        var assemblyVersion = $"{releaseVersion}.0";
        DotNetCoreBuild(solution.FullPath, new DotNetCoreBuildSettings()
        {
            Configuration = configuration,
            Verbosity = verbosity,
            MSBuildSettings = new DotNetCoreMSBuildSettings()
                .WithProperty("AssemblyVersion", assemblyVersion)
                .WithProperty("FileVersion", assemblyVersion)
                .WithProperty("Version", assemblyVersion)
                .SetMaxCpuCount(0)
        });
    }
});

Task("Tests").Does(() =>
{
    var testProjects = GetFiles(SOLUTION_TESTS);

    foreach (var testProject in testProjects)
    {
        if (HasEnvironmentVariable("CI") && testProject.FullPath.Contains("Tests.Integration.csproj"))
        {
            Information("✗ Skipping test project {0}", testProject.GetFilenameWithoutExtension());

            continue;
        }

        Information("🛈 Testing project {0}", testProject.GetFilenameWithoutExtension());

        DotNetCoreTest(testProject.FullPath, new DotNetCoreTestSettings {
            Configuration = configuration,
            NoBuild = true,
            NoRestore = true,
            Verbosity = verbosity,
        });
    }
});

Task("Publish").Does(() =>
{
    var projects = GetFiles(SOLUTION_APPS);

    foreach (var project in projects)
    {
        var projectDirectory = project.GetDirectory().FullPath;
        var projectName = project.GetDirectory().GetDirectoryName();

        Information("🛈 Publishing project artifacts {0} v{1}", project.GetFilenameWithoutExtension(), releaseVersion);

        var assemblyVersion = $"{releaseVersion}.0";
        DotNetCorePublish(project.FullPath, new DotNetCorePublishSettings {
            Configuration = configuration,
            OutputDirectory = publishedDir.Path.Combine(projectName),
            NoBuild = true,
            NoRestore = true,
            Verbosity = verbosity,
            MSBuildSettings = new DotNetCoreMSBuildSettings()
                .WithProperty("AssemblyVersion", assemblyVersion)
                .WithProperty("FileVersion", assemblyVersion)
                .WithProperty("Version", assemblyVersion)
        });
    }
});

Task("Package")
    .WithCriteria(shouldRelease)
    .WithCriteria(() => changesDetectedSinceLastRelease)
    .Does(() =>
{
    var projects = GetFiles(SOLUTION_NUGET);

    foreach (var project in projects)
    {
        var projectDirectory = project.GetDirectory().FullPath;

        Information("🛈 Packaging project {0} v{1}", project.GetFilenameWithoutExtension(), releaseVersion);

        var assemblyVersion = $"{releaseVersion}.0";
        DotNetCorePack(project.FullPath, new DotNetCorePackSettings {
            Configuration = configuration,
            OutputDirectory = packagesDir,
            NoBuild = true,
            NoRestore = true,
            Verbosity = verbosity,
            MSBuildSettings = new DotNetCoreMSBuildSettings()
                .WithProperty("AssemblyVersion", assemblyVersion)
                .WithProperty("FileVersion", assemblyVersion)
                .WithProperty("Version", assemblyVersion)
        });
    }
});

Task("Release")
    .WithCriteria(shouldRelease)
    .WithCriteria(() => changesDetectedSinceLastRelease)
    .Does(() =>
{
    Information("Releasing v{0}", releaseVersion);
    Information("Updating CHANGELOG.md");
    Information("Creating release");
    Information("Pushing to NuGet");

    Npx("semantic-release", requiredSemanticVersionPackages);

    var settings = new DotNetCoreNuGetPushSettings
    {
        ApiKey = EnvironmentVariable("NUGET_API_KEY"),
        IgnoreSymbols = true,
        Source = EnvironmentVariable("NUGET_SOURCE"),
        Verbosity = verbosity,
    };

    var packageFiles = GetFiles($"{packagesDir}/*.nupkg");
    foreach (var packageFile in packageFiles)
    {
        Information("🛈 Releasing package {0}", packageFile);
        DotNetCoreNuGetPush(packageFile.FullPath, settings);
    }
})
.OnError(exception =>
{
    var logFiles = GetFiles("/root/.npm/_logs/*.log");
    foreach (var logFile in logFiles)
    {
        Information(FileReadText(logFile));
    }

    Information(exception.Message);
    Information(exception.StackTrace);

    throw exception;
});


///////////////////////////////////////////////////////////////////////////////
// EXECUTION
///////////////////////////////////////////////////////////////////////////////

RunTarget(target);

///////////////////////////////////////////////////////////////////////////////
// Helpers
///////////////////////////////////////////////////////////////////////////////

string ExtractNextSemanticVersionNumber(string[] semanticReleaseOutput)
{
    var extractRegEx = new System.Text.RegularExpressions.Regex("^.+next release version is (?<SemanticVersionNumber>.*)$");

    return semanticReleaseOutput
        .Select(line => extractRegEx.Match(line).Groups["SemanticVersionNumber"].Value)
        .Where(line => !string.IsNullOrWhiteSpace(line))
        .SingleOrDefault();
}
