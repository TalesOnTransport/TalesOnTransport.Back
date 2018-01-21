using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TalesOnTransport.Back.Migrations.Book
{
    public partial class renameTimesScanned : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "numTimesScanned",
                table: "Book",
                newName: "TimesScanned");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TimesScanned",
                table: "Book",
                newName: "numTimesScanned");
        }
    }
}
