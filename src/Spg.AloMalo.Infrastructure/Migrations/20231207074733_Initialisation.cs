using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Spg.AloMalo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initialisation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    NickName = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Username_Address = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Photographers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    StudioAddress_StreetNumber = table.Column<string>(type: "TEXT", nullable: false),
                    StudioAddress_ZipCode = table.Column<string>(type: "TEXT", nullable: false),
                    StudioAddress_City = table.Column<string>(type: "TEXT", nullable: false),
                    StudioAddress_Country = table.Column<string>(type: "TEXT", nullable: false),
                    StateName = table.Column<string>(type: "TEXT", nullable: false),
                    MobilePhoneNumber_CountryCode = table.Column<int>(type: "INTEGER", nullable: false),
                    MobilePhoneNumber_AreaCode = table.Column<int>(type: "INTEGER", nullable: false),
                    MobilePhoneNumber_SerialNumber = table.Column<string>(type: "TEXT", nullable: false),
                    BusinessPhoneNumber_CountryCode = table.Column<int>(type: "INTEGER", nullable: false),
                    BusinessPhoneNumber_AreaCode = table.Column<int>(type: "INTEGER", nullable: false),
                    BusinessPhoneNumber_SerialNumber = table.Column<string>(type: "TEXT", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", nullable: false),
                    LastName = table.Column<string>(type: "TEXT", nullable: false),
                    Username_Address = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photographers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Albums",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreationTimeStamp = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    Private = table.Column<bool>(type: "INTEGER", nullable: false),
                    OwnerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Albums", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Albums_Photographers_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Photographers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Photographers_EMails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Address = table.Column<string>(type: "TEXT", nullable: false),
                    PhotographerId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photographers_EMails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photographers_EMails_Photographers_PhotographerId",
                        column: x => x.PhotographerId,
                        principalTable: "Photographers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EntityList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    CreationTimeStamp = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    ImageType = table.Column<int>(type: "INTEGER", nullable: false),
                    Location_Longitude = table.Column<double>(type: "REAL", nullable: false),
                    Location_Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Width = table.Column<int>(type: "INTEGER", nullable: false),
                    Height = table.Column<int>(type: "INTEGER", nullable: false),
                    Orientation = table.Column<int>(type: "INTEGER", nullable: false),
                    AiGenerated = table.Column<bool>(type: "INTEGER", nullable: false),
                    PhotographerNavigationId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Photos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Photos_Photographers_PhotographerNavigationId",
                        column: x => x.PhotographerNavigationId,
                        principalTable: "Photographers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AlbumPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false),
                    AlbumNavigationId = table.Column<int>(type: "INTEGER", nullable: false),
                    PhotoNavigationId = table.Column<int>(type: "INTEGER", nullable: false),
                    Position = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlbumPhoto_Albums_AlbumNavigationId",
                        column: x => x.AlbumNavigationId,
                        principalTable: "Albums",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AlbumPhoto_Photos_PhotoNavigationId",
                        column: x => x.PhotoNavigationId,
                        principalTable: "EntityList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumPhoto_AlbumNavigationId",
                table: "AlbumPhoto",
                column: "AlbumNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_AlbumPhoto_PhotoNavigationId",
                table: "AlbumPhoto",
                column: "PhotoNavigationId");

            migrationBuilder.CreateIndex(
                name: "IX_Albums_OwnerId",
                table: "Albums",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Photographers_EMails_PhotographerId",
                table: "Photographers_EMails",
                column: "PhotographerId");

            migrationBuilder.CreateIndex(
                name: "IX_Photos_PhotographerNavigationId",
                table: "EntityList",
                column: "PhotographerNavigationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumPhoto");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Photographers_EMails");

            migrationBuilder.DropTable(
                name: "Albums");

            migrationBuilder.DropTable(
                name: "EntityList");

            migrationBuilder.DropTable(
                name: "Photographers");
        }
    }
}
