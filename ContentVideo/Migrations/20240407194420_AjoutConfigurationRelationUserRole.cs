using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ContentVideo.Migrations
{
    /// <inheritdoc />
    public partial class AjoutConfigurationRelationUserRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("0644dcd7-037d-4223-bded-bf19e212d503"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("524ea902-77a9-416d-8f67-73625b9011b4"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("a6ee77a0-1121-4e53-a5af-c67f0d60a91e"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("b437cf3c-5f8c-477c-8269-16fb08441d55"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
                    { new Guid("3263a024-822f-491a-9357-91ef9cffc0cd"), "Accès aux API et aux fonctionnalités de développement.", "Developer" },
                    { new Guid("6a3759ce-7a6f-4f65-9d1e-c7b429c50155"), "Accès complet à toutes les fonctionnalités.", "Admin" },
                    { new Guid("87e93d15-9a6a-4e4f-961e-d6edc4731044"), "Accès limité, principalement en lecture seule.", "Guest" },
                    { new Guid("9b66d107-020a-4387-8361-a04e09c3886b"), "Accès de base pour interagir avec l'application.", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("3263a024-822f-491a-9357-91ef9cffc0cd"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("6a3759ce-7a6f-4f65-9d1e-c7b429c50155"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("87e93d15-9a6a-4e4f-961e-d6edc4731044"));

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: new Guid("9b66d107-020a-4387-8361-a04e09c3886b"));

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
                    { new Guid("0644dcd7-037d-4223-bded-bf19e212d503"), "Accès limité, principalement en lecture seule.", "Guest" },
                    { new Guid("524ea902-77a9-416d-8f67-73625b9011b4"), "Accès aux API et aux fonctionnalités de développement.", "Developer" },
                    { new Guid("a6ee77a0-1121-4e53-a5af-c67f0d60a91e"), "Accès de base pour interagir avec l'application.", "User" },
                    { new Guid("b437cf3c-5f8c-477c-8269-16fb08441d55"), "Accès complet à toutes les fonctionnalités.", "Admin" }
                });
        }
    }
}
