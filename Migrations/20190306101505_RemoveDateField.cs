using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace socialbrothersquotesapi.Migrations {
    public partial class RemoveDateField : Migration {
        protected override void Up(MigrationBuilder migrationBuilder) {
            migrationBuilder.DropColumn(
                "Date",
                "Quotes");
        }

        protected override void Down(MigrationBuilder migrationBuilder) {
            migrationBuilder.AddColumn<DateTime>(
                "Date",
                "Quotes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}