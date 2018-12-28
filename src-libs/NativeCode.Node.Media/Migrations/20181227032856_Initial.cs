namespace NativeCode.Node.Media.Migrations
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using Microsoft.EntityFrameworkCore.Migrations;

    [SuppressMessage("Microsoft.Performance", "CA1814")]
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "Credential",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Login = table.Column<string>(maxLength: 256, nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Password = table.Column<string>(maxLength: 256, nullable: true),
                    SshPrivateKey = table.Column<byte[]>(nullable: true),
                    SshPublicKey = table.Column<byte[]>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Credential", x => x.Id); });

            migrationBuilder.CreateTable(
                "MovieCollections",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SortTitle = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_MovieCollections", x => x.Id); });

            migrationBuilder.CreateTable(
                "PlexSources",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_PlexSources", x => x.Id); });

            migrationBuilder.CreateTable(
                "ReleaseInfo",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true),
                    AnnounceDate = table.Column<DateTimeOffset>(nullable: true),
                    ReleaseDate = table.Column<DateTimeOffset>(nullable: true),
                    PublishDate = table.Column<DateTimeOffset>(nullable: true),
                    StreamDate = table.Column<DateTimeOffset>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_ReleaseInfo", x => x.Id); });

            migrationBuilder.CreateTable(
                "Mounts",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true),
                    CredentialId = table.Column<Guid>(nullable: true),
                    Host = table.Column<string>(maxLength: 256, nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    MountType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Mounts", x => x.Id);
                    table.ForeignKey(
                        "FK_Mounts_Credential_CredentialId",
                        x => x.CredentialId,
                        "Credential",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "PlexServerInfo",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true),
                    Host = table.Column<string>(nullable: false),
                    Login = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Port = table.Column<int>(nullable: false),
                    Token = table.Column<string>(nullable: true),
                    PlexLibrarySourceId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlexServerInfo", x => x.Id);
                    table.ForeignKey(
                        "FK_PlexServerInfo_PlexSources_PlexLibrarySourceId",
                        x => x.PlexLibrarySourceId,
                        "PlexSources",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Movies",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SortTitle = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    MovieCollectionId = table.Column<Guid>(nullable: true),
                    ReleaseInfoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                    table.ForeignKey(
                        "FK_Movies_MovieCollections_MovieCollectionId",
                        x => x.MovieCollectionId,
                        "MovieCollections",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Movies_ReleaseInfo_ReleaseInfoId",
                        x => x.ReleaseInfoId,
                        "ReleaseInfo",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "MountPath",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true),
                    MountId = table.Column<Guid>(nullable: false),
                    Path = table.Column<string>(maxLength: 4096, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MountPath", x => x.Id);
                    table.ForeignKey(
                        "FK_MountPath_Mounts_MountId",
                        x => x.MountId,
                        "Mounts",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "SourceMovies",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true),
                    MovieId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceMovies", x => x.Id);
                    table.ForeignKey(
                        "FK_SourceMovies_Movies_MovieId",
                        x => x.MovieId,
                        "Movies",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Image",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true),
                    DisplayHeight = table.Column<int>(nullable: false),
                    DisplayWidth = table.Column<int>(nullable: false),
                    ImageType = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    SourceHeight = table.Column<int>(nullable: false),
                    SourceWidth = table.Column<int>(nullable: false),
                    Text = table.Column<string>(maxLength: 4096, nullable: true),
                    EpisodeId = table.Column<Guid>(nullable: true),
                    MovieCollectionId = table.Column<Guid>(nullable: true),
                    MovieId = table.Column<Guid>(nullable: true),
                    SeasonId = table.Column<Guid>(nullable: true),
                    SeriesId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Image", x => x.Id);
                    table.ForeignKey(
                        "FK_Image_MovieCollections_MovieCollectionId",
                        x => x.MovieCollectionId,
                        "MovieCollections",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Image_Movies_MovieId",
                        x => x.MovieId,
                        "Movies",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "MediaProperty",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true),
                    Key = table.Column<string>(maxLength: 256, nullable: false),
                    Value = table.Column<string>(nullable: false),
                    ValueType = table.Column<string>(maxLength: 1024, nullable: false),
                    EpisodeId = table.Column<Guid>(nullable: true),
                    MovieCollectionId = table.Column<Guid>(nullable: true),
                    MovieId = table.Column<Guid>(nullable: true),
                    SeasonId = table.Column<Guid>(nullable: true),
                    SeriesId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MediaProperty", x => x.Id);
                    table.ForeignKey(
                        "FK_MediaProperty_MovieCollections_MovieCollectionId",
                        x => x.MovieCollectionId,
                        "MovieCollections",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_MediaProperty_Movies_MovieId",
                        x => x.MovieId,
                        "Movies",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "MetadataSource",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true),
                    Agent = table.Column<string>(nullable: false),
                    CacheUrl = table.Column<string>(nullable: true),
                    Provider = table.Column<string>(nullable: false),
                    SourceUrl = table.Column<string>(nullable: false),
                    EpisodeId = table.Column<Guid>(nullable: true),
                    MovieId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetadataSource", x => x.Id);
                    table.ForeignKey(
                        "FK_MetadataSource_Movies_MovieId",
                        x => x.MovieId,
                        "Movies",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Series",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SortTitle = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    ReleaseInfoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.Id);
                    table.ForeignKey(
                        "FK_Series_ReleaseInfo_ReleaseInfoId",
                        x => x.ReleaseInfoId,
                        "ReleaseInfo",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "Seasons",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SortTitle = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    SeriesId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                    table.ForeignKey(
                        "FK_Seasons_Series_SeriesId",
                        x => x.SeriesId,
                        "Series",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Episodes",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    SortTitle = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    SeasonId = table.Column<Guid>(nullable: false),
                    ReleaseInfoId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                    table.ForeignKey(
                        "FK_Episodes_ReleaseInfo_ReleaseInfoId",
                        x => x.ReleaseInfoId,
                        "ReleaseInfo",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Episodes_Seasons_SeasonId",
                        x => x.SeasonId,
                        "Seasons",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "SourceEpisodes",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true),
                    EpisodeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SourceEpisodes", x => x.Id);
                    table.ForeignKey(
                        "FK_SourceEpisodes_Episodes_EpisodeId",
                        x => x.EpisodeId,
                        "Episodes",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Tag",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    EpisodeId = table.Column<Guid>(nullable: true),
                    MovieCollectionId = table.Column<Guid>(nullable: true),
                    MovieId = table.Column<Guid>(nullable: true),
                    SeasonId = table.Column<Guid>(nullable: true),
                    SeriesId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tag", x => x.Id);
                    table.ForeignKey(
                        "FK_Tag_Episodes_EpisodeId",
                        x => x.EpisodeId,
                        "Episodes",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Tag_MovieCollections_MovieCollectionId",
                        x => x.MovieCollectionId,
                        "MovieCollections",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Tag_Movies_MovieId",
                        x => x.MovieId,
                        "Movies",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Tag_Seasons_SeasonId",
                        x => x.SeasonId,
                        "Seasons",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_Tag_Series_SeriesId",
                        x => x.SeriesId,
                        "Series",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                "MountPathFile",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTimeOffset>(nullable: false),
                    DateModified = table.Column<DateTimeOffset>(nullable: true),
                    UserCreated = table.Column<string>(nullable: true),
                    UserModified = table.Column<string>(nullable: true),
                    FileName = table.Column<string>(maxLength: 1024, nullable: false),
                    FilePath = table.Column<string>(maxLength: 4096, nullable: false),
                    Hash = table.Column<byte[]>(nullable: false),
                    MountPathId = table.Column<Guid>(nullable: false),
                    Size = table.Column<long>(nullable: false),
                    MediaSourceEpisodeId = table.Column<Guid>(nullable: true),
                    MediaSourceMovieId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MountPathFile", x => x.Id);
                    table.ForeignKey(
                        "FK_MountPathFile_SourceEpisodes_MediaSourceEpisodeId",
                        x => x.MediaSourceEpisodeId,
                        "SourceEpisodes",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_MountPathFile_SourceMovies_MediaSourceMovieId",
                        x => x.MediaSourceMovieId,
                        "SourceMovies",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        "FK_MountPathFile_MountPath_MountPathId",
                        x => x.MountPathId,
                        "MountPath",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                "Mounts",
                new[] {"Id", "CredentialId", "DateCreated", "DateModified", "Host", "MountType", "Name", "UserCreated", "UserModified"},
                new object[]
                {
                    new Guid("dfb9a8fb-320a-4d09-a70f-68184cd10723"), null,
                    new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null,
                    "nas01.in.nativecode.com", 2, "NAS01", null, null
                });

            migrationBuilder.InsertData(
                "Mounts",
                new[] {"Id", "CredentialId", "DateCreated", "DateModified", "Host", "MountType", "Name", "UserCreated", "UserModified"},
                new object[]
                {
                    new Guid("cd7cebdb-bbb5-4133-a7c4-fa3c700d110c"), null,
                    new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null,
                    "qnap.in.nativecode.com", 2, "QNAP", null, null
                });

            migrationBuilder.InsertData(
                "Mounts",
                new[] {"Id", "CredentialId", "DateCreated", "DateModified", "Host", "MountType", "Name", "UserCreated", "UserModified"},
                new object[]
                {
                    new Guid("4db231d2-a895-4128-a1a6-295c0fcfed91"), null,
                    new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null,
                    "storage.in.nativecode.com", 2, "STORAGE", null, null
                });

            migrationBuilder.InsertData(
                "MountPath",
                new[] {"Id", "DateCreated", "DateModified", "MountId", "Path", "UserCreated", "UserModified"},
                new object[,]
                {
                    {
                        new Guid("1adc9bd6-ea41-44db-88d6-525550f69dec"),
                        new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null,
                        new Guid("dfb9a8fb-320a-4d09-a70f-68184cd10723"), "/", null, null
                    },
                    {
                        new Guid("826a7e0a-7b2b-4fa2-b10f-f2b94cd4783e"),
                        new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null,
                        new Guid("dfb9a8fb-320a-4d09-a70f-68184cd10723"), "/Movies", null, null
                    },
                    {
                        new Guid("23f44e25-37f2-4e37-8a99-4584c390ef46"),
                        new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null,
                        new Guid("cd7cebdb-bbb5-4133-a7c4-fa3c700d110c"), "/", null, null
                    },
                    {
                        new Guid("772ef0c4-decc-4977-8c50-21851398cf1f"),
                        new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null,
                        new Guid("cd7cebdb-bbb5-4133-a7c4-fa3c700d110c"), "/Media/Other", null, null
                    },
                    {
                        new Guid("16f76922-a237-46b6-8f58-351c19353da1"),
                        new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null,
                        new Guid("4db231d2-a895-4128-a1a6-295c0fcfed91"), "/", null, null
                    },
                    {
                        new Guid("742c9390-2b9c-4c18-a150-08fc18166263"),
                        new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), null,
                        new Guid("4db231d2-a895-4128-a1a6-295c0fcfed91"), "/Media/Shows", null, null
                    }
                });

            migrationBuilder.CreateIndex(
                "IX_Episodes_ReleaseInfoId",
                "Episodes",
                "ReleaseInfoId");

            migrationBuilder.CreateIndex(
                "IX_Episodes_SeasonId",
                "Episodes",
                "SeasonId");

            migrationBuilder.CreateIndex(
                "IX_Image_EpisodeId",
                "Image",
                "EpisodeId");

            migrationBuilder.CreateIndex(
                "IX_Image_MovieCollectionId",
                "Image",
                "MovieCollectionId");

            migrationBuilder.CreateIndex(
                "IX_Image_MovieId",
                "Image",
                "MovieId");

            migrationBuilder.CreateIndex(
                "IX_Image_SeasonId",
                "Image",
                "SeasonId");

            migrationBuilder.CreateIndex(
                "IX_Image_SeriesId",
                "Image",
                "SeriesId");

            migrationBuilder.CreateIndex(
                "IX_MediaProperty_EpisodeId",
                "MediaProperty",
                "EpisodeId");

            migrationBuilder.CreateIndex(
                "IX_MediaProperty_MovieCollectionId",
                "MediaProperty",
                "MovieCollectionId");

            migrationBuilder.CreateIndex(
                "IX_MediaProperty_MovieId",
                "MediaProperty",
                "MovieId");

            migrationBuilder.CreateIndex(
                "IX_MediaProperty_SeasonId",
                "MediaProperty",
                "SeasonId");

            migrationBuilder.CreateIndex(
                "IX_MediaProperty_SeriesId",
                "MediaProperty",
                "SeriesId");

            migrationBuilder.CreateIndex(
                "IX_MetadataSource_EpisodeId",
                "MetadataSource",
                "EpisodeId");

            migrationBuilder.CreateIndex(
                "IX_MetadataSource_MovieId",
                "MetadataSource",
                "MovieId");

            migrationBuilder.CreateIndex(
                "IX_MountPath_MountId",
                "MountPath",
                "MountId");

            migrationBuilder.CreateIndex(
                "IX_MountPathFile_MediaSourceEpisodeId",
                "MountPathFile",
                "MediaSourceEpisodeId");

            migrationBuilder.CreateIndex(
                "IX_MountPathFile_MediaSourceMovieId",
                "MountPathFile",
                "MediaSourceMovieId");

            migrationBuilder.CreateIndex(
                "IX_MountPathFile_MountPathId",
                "MountPathFile",
                "MountPathId");

            migrationBuilder.CreateIndex(
                "IX_Mounts_CredentialId",
                "Mounts",
                "CredentialId");

            migrationBuilder.CreateIndex(
                "IX_Movies_MovieCollectionId",
                "Movies",
                "MovieCollectionId");

            migrationBuilder.CreateIndex(
                "IX_Movies_ReleaseInfoId",
                "Movies",
                "ReleaseInfoId");

            migrationBuilder.CreateIndex(
                "IX_PlexServerInfo_PlexLibrarySourceId",
                "PlexServerInfo",
                "PlexLibrarySourceId");

            migrationBuilder.CreateIndex(
                "IX_Seasons_SeriesId",
                "Seasons",
                "SeriesId");

            migrationBuilder.CreateIndex(
                "IX_Series_ReleaseInfoId",
                "Series",
                "ReleaseInfoId");

            migrationBuilder.CreateIndex(
                "IX_SourceEpisodes_EpisodeId",
                "SourceEpisodes",
                "EpisodeId");

            migrationBuilder.CreateIndex(
                "IX_SourceMovies_MovieId",
                "SourceMovies",
                "MovieId");

            migrationBuilder.CreateIndex(
                "IX_Tag_EpisodeId",
                "Tag",
                "EpisodeId");

            migrationBuilder.CreateIndex(
                "IX_Tag_MovieCollectionId",
                "Tag",
                "MovieCollectionId");

            migrationBuilder.CreateIndex(
                "IX_Tag_MovieId",
                "Tag",
                "MovieId");

            migrationBuilder.CreateIndex(
                "IX_Tag_SeasonId",
                "Tag",
                "SeasonId");

            migrationBuilder.CreateIndex(
                "IX_Tag_SeriesId",
                "Tag",
                "SeriesId");

            migrationBuilder.AddForeignKey(
                "FK_Image_Seasons_SeasonId",
                "Image",
                "SeasonId",
                "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Image_Episodes_EpisodeId",
                "Image",
                "EpisodeId",
                "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Image_Series_SeriesId",
                "Image",
                "SeriesId",
                "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_MediaProperty_Seasons_SeasonId",
                "MediaProperty",
                "SeasonId",
                "Seasons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_MediaProperty_Episodes_EpisodeId",
                "MediaProperty",
                "EpisodeId",
                "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_MediaProperty_Series_SeriesId",
                "MediaProperty",
                "SeriesId",
                "Series",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_MetadataSource_Episodes_EpisodeId",
                "MetadataSource",
                "EpisodeId",
                "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                "FK_Series_Episodes_Id",
                "Series",
                "Id",
                "Episodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Episodes_ReleaseInfo_ReleaseInfoId",
                "Episodes");

            migrationBuilder.DropForeignKey(
                "FK_Series_ReleaseInfo_ReleaseInfoId",
                "Series");

            migrationBuilder.DropForeignKey(
                "FK_Episodes_Seasons_SeasonId",
                "Episodes");

            migrationBuilder.DropTable(
                "Image");

            migrationBuilder.DropTable(
                "MediaProperty");

            migrationBuilder.DropTable(
                "MetadataSource");

            migrationBuilder.DropTable(
                "MountPathFile");

            migrationBuilder.DropTable(
                "PlexServerInfo");

            migrationBuilder.DropTable(
                "Tag");

            migrationBuilder.DropTable(
                "SourceEpisodes");

            migrationBuilder.DropTable(
                "SourceMovies");

            migrationBuilder.DropTable(
                "MountPath");

            migrationBuilder.DropTable(
                "PlexSources");

            migrationBuilder.DropTable(
                "Movies");

            migrationBuilder.DropTable(
                "Mounts");

            migrationBuilder.DropTable(
                "MovieCollections");

            migrationBuilder.DropTable(
                "Credential");

            migrationBuilder.DropTable(
                "ReleaseInfo");

            migrationBuilder.DropTable(
                "Seasons");

            migrationBuilder.DropTable(
                "Series");

            migrationBuilder.DropTable(
                "Episodes");
        }
    }
}
