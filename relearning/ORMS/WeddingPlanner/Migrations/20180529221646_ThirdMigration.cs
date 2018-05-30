using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WeddingPlanner.Migrations
{
    public partial class ThirdMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Weddings_Users_UserId",
                table: "Weddings");

            migrationBuilder.DropIndex(
                name: "IX_Weddings_UserId",
                table: "Weddings");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Weddings");

            migrationBuilder.CreateTable(
                name: "Atendees",
                columns: table => new
                {
                    AtendeesId = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<int>(nullable: false),
                    WeddingsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Atendees", x => x.AtendeesId);
                    table.ForeignKey(
                        name: "FK_Atendees_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Atendees_Weddings_WeddingsId",
                        column: x => x.WeddingsId,
                        principalTable: "Weddings",
                        principalColumn: "WeddingsId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Atendees_UserId",
                table: "Atendees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Atendees_WeddingsId",
                table: "Atendees",
                column: "WeddingsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Atendees");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Weddings",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Weddings_UserId",
                table: "Weddings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Weddings_Users_UserId",
                table: "Weddings",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
