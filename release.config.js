module.exports = {
  prepare: [
    // https://github.com/semantic-release/changelog
    // Set of semantic-release plugins for creating or updating a changelog file.
    '@semantic-release/changelog'
  ],
  generateNotes: [
    // https://github.com/semantic-release/release-notes-generator
    // Set of semantic-release plugins for creating or updating a release notes file.
    '@semantic-release/release-notes-generator'
  ],
  publish: [
    // https://github.com/semantic-release/git
    // '@semantic-release/git',
    // Exec plugin uses to call dotnet nuget push to push the packages from
    // the artifacts folder to NuGet
    {
      path: '@semantic-release/exec',
      // cmd: `dotnet nuget push .artifacts/**/*.nupkg -k ${process.env.NUGET_API_KEY} -s ${process.env.NUGET_SOURCE}`,
      cmd: 'uname -a'
    }
  ],
  verifyConditions: [
    () => {
      if (!process.env.NUGET_API_KEY) {
        throw new SemanticReleaseErrorc(
          'The environment variable NUGET_API_KEY is required.',
          'ENOAPMTOKEN'
        )
      }
    }
    // https://github.com/semantic-release/changelog
    // Set of semantic-release plugins for creating or updating a changelog file.
    // '@semantic-release/changelog',
  ],
  debug: false,
  repositoryUrl: 'ssh://git@bitbucket.org/plsos2/plx.git',
  fail: [],
  success: [],
  verifyRelease: []
}
