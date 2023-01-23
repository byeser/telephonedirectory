using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace report.persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedInculude : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "ContactInfo",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddColumn<Guid>(
                name: "PersonsUUID",
                table: "ContactInfo",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContactInfo_PersonsUUID",
                table: "ContactInfo",
                column: "PersonsUUID");

            migrationBuilder.AddForeignKey(
                name: "FK_ContactInfo_Persons_PersonsUUID",
                table: "ContactInfo",
                column: "PersonsUUID",
                principalTable: "Persons",
                principalColumn: "UUID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContactInfo_Persons_PersonsUUID",
                table: "ContactInfo");

            migrationBuilder.DropIndex(
                name: "IX_ContactInfo_PersonsUUID",
                table: "ContactInfo");

            migrationBuilder.DropColumn(
                name: "PersonsUUID",
                table: "ContactInfo");

            migrationBuilder.AlterColumn<DateTime>(
                name: "Created",
                table: "ContactInfo",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");
        }
    }
}
