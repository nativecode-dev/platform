namespace NativeCode.Node.Identity.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Metadata;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "ApiResources",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Enabled = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true),
                    NonEditable = table.Column<bool>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_ApiResources", x => x.Id); });

            migrationBuilder.CreateTable(
                "AspNetRoles",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_AspNetRoles", x => x.Id); });

            migrationBuilder.CreateTable(
                "AspNetUsers",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_AspNetUsers", x => x.Id); });

            migrationBuilder.CreateTable(
                "Clients",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Enabled = table.Column<bool>(nullable: false),
                    ClientId = table.Column<string>(maxLength: 200, nullable: false),
                    ProtocolType = table.Column<string>(maxLength: 200, nullable: false),
                    RequireClientSecret = table.Column<bool>(nullable: false),
                    ClientName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    ClientUri = table.Column<string>(maxLength: 2000, nullable: true),
                    LogoUri = table.Column<string>(maxLength: 2000, nullable: true),
                    RequireConsent = table.Column<bool>(nullable: false),
                    AllowRememberConsent = table.Column<bool>(nullable: false),
                    AlwaysIncludeUserClaimsInIdToken = table.Column<bool>(nullable: false),
                    RequirePkce = table.Column<bool>(nullable: false),
                    AllowPlainTextPkce = table.Column<bool>(nullable: false),
                    AllowAccessTokensViaBrowser = table.Column<bool>(nullable: false),
                    FrontChannelLogoutUri = table.Column<string>(maxLength: 2000, nullable: true),
                    FrontChannelLogoutSessionRequired = table.Column<bool>(nullable: false),
                    BackChannelLogoutUri = table.Column<string>(maxLength: 2000, nullable: true),
                    BackChannelLogoutSessionRequired = table.Column<bool>(nullable: false),
                    AllowOfflineAccess = table.Column<bool>(nullable: false),
                    IdentityTokenLifetime = table.Column<int>(nullable: false),
                    AccessTokenLifetime = table.Column<int>(nullable: false),
                    AuthorizationCodeLifetime = table.Column<int>(nullable: false),
                    ConsentLifetime = table.Column<int>(nullable: true),
                    AbsoluteRefreshTokenLifetime = table.Column<int>(nullable: false),
                    SlidingRefreshTokenLifetime = table.Column<int>(nullable: false),
                    RefreshTokenUsage = table.Column<int>(nullable: false),
                    UpdateAccessTokenClaimsOnRefresh = table.Column<bool>(nullable: false),
                    RefreshTokenExpiration = table.Column<int>(nullable: false),
                    AccessTokenType = table.Column<int>(nullable: false),
                    EnableLocalLogin = table.Column<bool>(nullable: false),
                    IncludeJwtId = table.Column<bool>(nullable: false),
                    AlwaysSendClientClaims = table.Column<bool>(nullable: false),
                    ClientClaimsPrefix = table.Column<string>(maxLength: 200, nullable: true),
                    PairWiseSubjectSalt = table.Column<string>(maxLength: 200, nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    LastAccessed = table.Column<DateTime>(nullable: true),
                    UserSsoLifetime = table.Column<int>(nullable: true),
                    UserCodeType = table.Column<string>(maxLength: 100, nullable: true),
                    DeviceCodeLifetime = table.Column<int>(nullable: false),
                    NonEditable = table.Column<bool>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Clients", x => x.Id); });

            migrationBuilder.CreateTable(
                "DataProtectionKeys",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FriendlyName = table.Column<string>(nullable: true),
                    Xml = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_DataProtectionKeys", x => x.Id); });

            migrationBuilder.CreateTable(
                "IdentityResources",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Enabled = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Required = table.Column<bool>(nullable: false),
                    Emphasize = table.Column<bool>(nullable: false),
                    ShowInDiscoveryDocument = table.Column<bool>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Updated = table.Column<DateTime>(nullable: true),
                    NonEditable = table.Column<bool>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_IdentityResources", x => x.Id); });

            migrationBuilder.CreateTable(
                "ApiClaims",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 200, nullable: false),
                    ApiResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiClaims", x => x.Id);
                    table.ForeignKey(
                        "FK_ApiClaims_ApiResources_ApiResourceId",
                        x => x.ApiResourceId,
                        "ApiResources",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "ApiProperties",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(maxLength: 250, nullable: false),
                    Value = table.Column<string>(maxLength: 2000, nullable: false),
                    ApiResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiProperties", x => x.Id);
                    table.ForeignKey(
                        "FK_ApiProperties_ApiResources_ApiResourceId",
                        x => x.ApiResourceId,
                        "ApiResources",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "ApiScopes",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    DisplayName = table.Column<string>(maxLength: 200, nullable: true),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Required = table.Column<bool>(nullable: false),
                    Emphasize = table.Column<bool>(nullable: false),
                    ShowInDiscoveryDocument = table.Column<bool>(nullable: false),
                    ApiResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiScopes", x => x.Id);
                    table.ForeignKey(
                        "FK_ApiScopes_ApiResources_ApiResourceId",
                        x => x.ApiResourceId,
                        "ApiResources",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "ApiSecrets",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 1000, nullable: true),
                    Value = table.Column<string>(maxLength: 4000, nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(maxLength: 250, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ApiResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiSecrets", x => x.Id);
                    table.ForeignKey(
                        "FK_ApiSecrets_ApiResources_ApiResourceId",
                        x => x.ApiResourceId,
                        "ApiResources",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AspNetRoleClaims",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        x => x.RoleId,
                        "AspNetRoles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AspNetUserClaims",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        "FK_AspNetUserClaims_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AspNetUserLogins",
                table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new {x.LoginProvider, x.ProviderKey});
                    table.ForeignKey(
                        "FK_AspNetUserLogins_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AspNetUserRoles",
                table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new {x.UserId, x.RoleId});
                    table.ForeignKey(
                        "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        x => x.RoleId,
                        "AspNetRoles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_AspNetUserRoles_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AspNetUserTokens",
                table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new {x.UserId, x.LoginProvider, x.Name});
                    table.ForeignKey(
                        "FK_AspNetUserTokens_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "ClientClaims",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 250, nullable: false),
                    Value = table.Column<string>(maxLength: 250, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientClaims", x => x.Id);
                    table.ForeignKey(
                        "FK_ClientClaims_Clients_ClientId",
                        x => x.ClientId,
                        "Clients",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "ClientCorsOrigins",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Origin = table.Column<string>(maxLength: 150, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientCorsOrigins", x => x.Id);
                    table.ForeignKey(
                        "FK_ClientCorsOrigins_Clients_ClientId",
                        x => x.ClientId,
                        "Clients",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "ClientGrantTypes",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    GrantType = table.Column<string>(maxLength: 250, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientGrantTypes", x => x.Id);
                    table.ForeignKey(
                        "FK_ClientGrantTypes_Clients_ClientId",
                        x => x.ClientId,
                        "Clients",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "ClientIdPRestrictions",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Provider = table.Column<string>(maxLength: 200, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientIdPRestrictions", x => x.Id);
                    table.ForeignKey(
                        "FK_ClientIdPRestrictions_Clients_ClientId",
                        x => x.ClientId,
                        "Clients",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "ClientPostLogoutRedirectUris",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PostLogoutRedirectUri = table.Column<string>(maxLength: 2000, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientPostLogoutRedirectUris", x => x.Id);
                    table.ForeignKey(
                        "FK_ClientPostLogoutRedirectUris_Clients_ClientId",
                        x => x.ClientId,
                        "Clients",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "ClientProperties",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(maxLength: 250, nullable: false),
                    Value = table.Column<string>(maxLength: 2000, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientProperties", x => x.Id);
                    table.ForeignKey(
                        "FK_ClientProperties_Clients_ClientId",
                        x => x.ClientId,
                        "Clients",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "ClientRedirectUris",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RedirectUri = table.Column<string>(maxLength: 2000, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientRedirectUris", x => x.Id);
                    table.ForeignKey(
                        "FK_ClientRedirectUris_Clients_ClientId",
                        x => x.ClientId,
                        "Clients",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "ClientScopes",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Scope = table.Column<string>(maxLength: 200, nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientScopes", x => x.Id);
                    table.ForeignKey(
                        "FK_ClientScopes_Clients_ClientId",
                        x => x.ClientId,
                        "Clients",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "ClientSecrets",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(maxLength: 2000, nullable: true),
                    Value = table.Column<string>(maxLength: 4000, nullable: false),
                    Expiration = table.Column<DateTime>(nullable: true),
                    Type = table.Column<string>(maxLength: 250, nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ClientId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientSecrets", x => x.Id);
                    table.ForeignKey(
                        "FK_ClientSecrets_Clients_ClientId",
                        x => x.ClientId,
                        "Clients",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "IdentityClaims",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 200, nullable: false),
                    IdentityResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityClaims", x => x.Id);
                    table.ForeignKey(
                        "FK_IdentityClaims_IdentityResources_IdentityResourceId",
                        x => x.IdentityResourceId,
                        "IdentityResources",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "IdentityProperties",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Key = table.Column<string>(maxLength: 250, nullable: false),
                    Value = table.Column<string>(maxLength: 2000, nullable: false),
                    IdentityResourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IdentityProperties", x => x.Id);
                    table.ForeignKey(
                        "FK_IdentityProperties_IdentityResources_IdentityResourceId",
                        x => x.IdentityResourceId,
                        "IdentityResources",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "ApiScopeClaims",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(maxLength: 200, nullable: false),
                    ApiScopeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApiScopeClaims", x => x.Id);
                    table.ForeignKey(
                        "FK_ApiScopeClaims_ApiScopes_ApiScopeId",
                        x => x.ApiScopeId,
                        "ApiScopes",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp", "Name", "NormalizedName"},
                new object[,]
                {
                    {
                        new Guid("ac5051df-8b61-4d6f-b079-d6d180984d9b"), "76510cf9-6d5a-43af-978f-edc796febe5f", "Administrator",
                        "ADMINISTRATOR"
                    },
                    {new Guid("b765372f-49b7-4f34-acd3-461bb1562a3f"), "7ac7019f-33c7-4bf1-b75c-e52e79a9e52b", "Service", "SERVICE"},
                    {new Guid("223194a3-a3b0-4d95-a78c-aeaac63f4761"), "ed69ec81-60ea-4aad-a235-f404ce8c9a1a", "System", "SYSTEM"},
                    {new Guid("c2a0a2ac-1202-4ae9-9210-d4419f8f8cb9"), "e4f087f4-4db6-4435-bcaf-6429cfda51ba", "User", "USER"},
                    {new Guid("60b140ee-10ce-432e-ab6b-66b364d960cf"), "00e9f56b-62c9-4553-b1df-591c6a511386", null, null},
                    {new Guid("7384f518-bb39-43b7-a858-78f66988eddf"), "d8a360b3-2959-4573-96eb-d6eeda4b3ef5", null, null},
                    {new Guid("06abe995-b89c-4d2a-9067-0d571a0c9d8d"), "c8a77f43-8e4d-4252-b35e-0286ef7ebadc", null, null},
                    {new Guid("2e643172-b07e-48aa-9588-c20ccd226117"), "49ed10af-aad7-477d-9c5d-4fa73d6b4b21", null, null},
                    {new Guid("c541c65a-27a4-4937-b5e4-f6b14c2ddad9"), "7854ef46-d31e-4784-be1d-784c5b3a036f", null, null},
                    {new Guid("ad8146c0-9c2a-45e9-895f-2921de685448"), "f1902271-0461-46a7-99c1-59ee149a64fb", null, null},
                    {new Guid("79537994-9d9f-452e-9aa7-e9a5d7004468"), "3ba074df-ea87-4897-a3da-128b60748afa", null, null},
                    {new Guid("c9442fa0-97ce-4210-af7a-1d8745ac2c19"), "2694f006-751e-4f22-9076-cf1ee7d8d9ec", null, null}
                });

            migrationBuilder.CreateIndex(
                "IX_ApiClaims_ApiResourceId",
                "ApiClaims",
                "ApiResourceId");

            migrationBuilder.CreateIndex(
                "IX_ApiProperties_ApiResourceId",
                "ApiProperties",
                "ApiResourceId");

            migrationBuilder.CreateIndex(
                "IX_ApiResources_Name",
                "ApiResources",
                "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_ApiScopeClaims_ApiScopeId",
                "ApiScopeClaims",
                "ApiScopeId");

            migrationBuilder.CreateIndex(
                "IX_ApiScopes_ApiResourceId",
                "ApiScopes",
                "ApiResourceId");

            migrationBuilder.CreateIndex(
                "IX_ApiScopes_Name",
                "ApiScopes",
                "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_ApiSecrets_ApiResourceId",
                "ApiSecrets",
                "ApiResourceId");

            migrationBuilder.CreateIndex(
                "IX_AspNetRoleClaims_RoleId",
                "AspNetRoleClaims",
                "RoleId");

            migrationBuilder.CreateIndex(
                "RoleNameIndex",
                "AspNetRoles",
                "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_AspNetUserClaims_UserId",
                "AspNetUserClaims",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_AspNetUserLogins_UserId",
                "AspNetUserLogins",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_AspNetUserRoles_RoleId",
                "AspNetUserRoles",
                "RoleId");

            migrationBuilder.CreateIndex(
                "EmailIndex",
                "AspNetUsers",
                "NormalizedEmail");

            migrationBuilder.CreateIndex(
                "UserNameIndex",
                "AspNetUsers",
                "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                "IX_ClientClaims_ClientId",
                "ClientClaims",
                "ClientId");

            migrationBuilder.CreateIndex(
                "IX_ClientCorsOrigins_ClientId",
                "ClientCorsOrigins",
                "ClientId");

            migrationBuilder.CreateIndex(
                "IX_ClientGrantTypes_ClientId",
                "ClientGrantTypes",
                "ClientId");

            migrationBuilder.CreateIndex(
                "IX_ClientIdPRestrictions_ClientId",
                "ClientIdPRestrictions",
                "ClientId");

            migrationBuilder.CreateIndex(
                "IX_ClientPostLogoutRedirectUris_ClientId",
                "ClientPostLogoutRedirectUris",
                "ClientId");

            migrationBuilder.CreateIndex(
                "IX_ClientProperties_ClientId",
                "ClientProperties",
                "ClientId");

            migrationBuilder.CreateIndex(
                "IX_ClientRedirectUris_ClientId",
                "ClientRedirectUris",
                "ClientId");

            migrationBuilder.CreateIndex(
                "IX_Clients_ClientId",
                "Clients",
                "ClientId",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_ClientScopes_ClientId",
                "ClientScopes",
                "ClientId");

            migrationBuilder.CreateIndex(
                "IX_ClientSecrets_ClientId",
                "ClientSecrets",
                "ClientId");

            migrationBuilder.CreateIndex(
                "IX_IdentityClaims_IdentityResourceId",
                "IdentityClaims",
                "IdentityResourceId");

            migrationBuilder.CreateIndex(
                "IX_IdentityProperties_IdentityResourceId",
                "IdentityProperties",
                "IdentityResourceId");

            migrationBuilder.CreateIndex(
                "IX_IdentityResources_Name",
                "IdentityResources",
                "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "ApiClaims");

            migrationBuilder.DropTable(
                "ApiProperties");

            migrationBuilder.DropTable(
                "ApiScopeClaims");

            migrationBuilder.DropTable(
                "ApiSecrets");

            migrationBuilder.DropTable(
                "AspNetRoleClaims");

            migrationBuilder.DropTable(
                "AspNetUserClaims");

            migrationBuilder.DropTable(
                "AspNetUserLogins");

            migrationBuilder.DropTable(
                "AspNetUserRoles");

            migrationBuilder.DropTable(
                "AspNetUserTokens");

            migrationBuilder.DropTable(
                "ClientClaims");

            migrationBuilder.DropTable(
                "ClientCorsOrigins");

            migrationBuilder.DropTable(
                "ClientGrantTypes");

            migrationBuilder.DropTable(
                "ClientIdPRestrictions");

            migrationBuilder.DropTable(
                "ClientPostLogoutRedirectUris");

            migrationBuilder.DropTable(
                "ClientProperties");

            migrationBuilder.DropTable(
                "ClientRedirectUris");

            migrationBuilder.DropTable(
                "ClientScopes");

            migrationBuilder.DropTable(
                "ClientSecrets");

            migrationBuilder.DropTable(
                "DataProtectionKeys");

            migrationBuilder.DropTable(
                "IdentityClaims");

            migrationBuilder.DropTable(
                "IdentityProperties");

            migrationBuilder.DropTable(
                "ApiScopes");

            migrationBuilder.DropTable(
                "AspNetRoles");

            migrationBuilder.DropTable(
                "AspNetUsers");

            migrationBuilder.DropTable(
                "Clients");

            migrationBuilder.DropTable(
                "IdentityResources");

            migrationBuilder.DropTable(
                "ApiResources");
        }
    }
}
