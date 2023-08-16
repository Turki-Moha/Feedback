using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Feedback.Migrations
{
    public partial class AddImageUrl2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageUrlNew",
                table: "Requests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageUrlNew",
                table: "Requests");
        }
    }
}
