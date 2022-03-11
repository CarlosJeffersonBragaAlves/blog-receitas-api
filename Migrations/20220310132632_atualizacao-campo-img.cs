using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace blog_receitas_api.Migrations
{
    public partial class atualizacaocampoimg : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "img",
                table: "Receitas");

            migrationBuilder.AddColumn<string>(
                name: "UrlImg",
                table: "Receitas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UrlImg",
                table: "Receitas");

            migrationBuilder.AddColumn<byte[]>(
                name: "img",
                table: "Receitas",
                type: "longblob",
                nullable: true);
        }
    }
}
