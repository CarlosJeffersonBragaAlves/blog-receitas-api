using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace blog_receitas_api.Migrations
{
    public partial class atualizacaoreceitas : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusId",
                table: "Receitas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "destaque",
                table: "Receitas",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "img",
                table: "Receitas",
                type: "longblob",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Status",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Desc = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Status", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Receitas_StatusId",
                table: "Receitas",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receitas_Status_StatusId",
                table: "Receitas",
                column: "StatusId",
                principalTable: "Status",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receitas_Status_StatusId",
                table: "Receitas");

            migrationBuilder.DropTable(
                name: "Status");

            migrationBuilder.DropIndex(
                name: "IX_Receitas_StatusId",
                table: "Receitas");

            migrationBuilder.DropColumn(
                name: "StatusId",
                table: "Receitas");

            migrationBuilder.DropColumn(
                name: "destaque",
                table: "Receitas");

            migrationBuilder.DropColumn(
                name: "img",
                table: "Receitas");
        }
    }
}
