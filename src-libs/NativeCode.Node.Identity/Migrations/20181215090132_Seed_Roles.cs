namespace NativeCode.Node.Identity.Migrations
{
    using System;
    using Microsoft.EntityFrameworkCore.Migrations;

    public partial class Seed_Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                "AspNetRoles",
                new[] {"Id", "ConcurrencyStamp", "Name", "NormalizedName"},
                new object[,]
                {
                    {
                        new Guid("ac5051df-8b61-4d6f-b079-d6d180984d9b"), "c0f6d982-a37a-41b6-97c2-a61be4693997", "Administrator",
                        "ADMINISTRATOR"
                    },
                    {new Guid("b765372f-49b7-4f34-acd3-461bb1562a3f"), "320d9ebd-1af3-4b19-b0eb-3c1c118a1db5", "Service", "SERVICE"},
                    {new Guid("223194a3-a3b0-4d95-a78c-aeaac63f4761"), "b0d56e55-afe0-435e-8e3a-552165faa54a", "System", "SYSTEM"},
                    {new Guid("c2a0a2ac-1202-4ae9-9210-d4419f8f8cb9"), "0fb3505a-3fe9-4d2b-b655-d9ff200ed495", "User", "USER"},
                    {new Guid("60b140ee-10ce-432e-ab6b-66b364d960cf"), "e630904f-7598-4bd3-b8cd-dad653a966e8", null, null},
                    {new Guid("7384f518-bb39-43b7-a858-78f66988eddf"), "1a45dcaa-b305-4cb8-b9b0-672ba868499f", null, null},
                    {new Guid("06abe995-b89c-4d2a-9067-0d571a0c9d8d"), "82a729ad-2b38-4a5d-a215-3defd482087a", null, null},
                    {new Guid("2e643172-b07e-48aa-9588-c20ccd226117"), "95f3f40a-72b8-482f-8401-e7c6261f5b01", null, null},
                    {new Guid("c541c65a-27a4-4937-b5e4-f6b14c2ddad9"), "b4565c7f-f591-4db1-aa32-50515c9e8a3f", null, null},
                    {new Guid("ad8146c0-9c2a-45e9-895f-2921de685448"), "11413e47-68b4-4dbd-8948-a274b8b50a56", null, null},
                    {new Guid("79537994-9d9f-452e-9aa7-e9a5d7004468"), "30b6a168-7f15-4d11-a1f7-75d2ac7f6d93", null, null},
                    {new Guid("c9442fa0-97ce-4210-af7a-1d8745ac2c19"), "1003ab05-5a1a-46bc-bfcc-01d41d6c7f26", null, null}
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                "AspNetRoles",
                "Id",
                new Guid("06abe995-b89c-4d2a-9067-0d571a0c9d8d"));

            migrationBuilder.DeleteData(
                "AspNetRoles",
                "Id",
                new Guid("223194a3-a3b0-4d95-a78c-aeaac63f4761"));

            migrationBuilder.DeleteData(
                "AspNetRoles",
                "Id",
                new Guid("2e643172-b07e-48aa-9588-c20ccd226117"));

            migrationBuilder.DeleteData(
                "AspNetRoles",
                "Id",
                new Guid("60b140ee-10ce-432e-ab6b-66b364d960cf"));

            migrationBuilder.DeleteData(
                "AspNetRoles",
                "Id",
                new Guid("7384f518-bb39-43b7-a858-78f66988eddf"));

            migrationBuilder.DeleteData(
                "AspNetRoles",
                "Id",
                new Guid("79537994-9d9f-452e-9aa7-e9a5d7004468"));

            migrationBuilder.DeleteData(
                "AspNetRoles",
                "Id",
                new Guid("ac5051df-8b61-4d6f-b079-d6d180984d9b"));

            migrationBuilder.DeleteData(
                "AspNetRoles",
                "Id",
                new Guid("ad8146c0-9c2a-45e9-895f-2921de685448"));

            migrationBuilder.DeleteData(
                "AspNetRoles",
                "Id",
                new Guid("b765372f-49b7-4f34-acd3-461bb1562a3f"));

            migrationBuilder.DeleteData(
                "AspNetRoles",
                "Id",
                new Guid("c2a0a2ac-1202-4ae9-9210-d4419f8f8cb9"));

            migrationBuilder.DeleteData(
                "AspNetRoles",
                "Id",
                new Guid("c541c65a-27a4-4937-b5e4-f6b14c2ddad9"));

            migrationBuilder.DeleteData(
                "AspNetRoles",
                "Id",
                new Guid("c9442fa0-97ce-4210-af7a-1d8745ac2c19"));
        }
    }
}
