using Microsoft.EntityFrameworkCore.Migrations;

namespace blog_receitas_api.Migrations
{
    public partial class addcamporeceita : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "difficulty",
                table: "Receitas",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "difficulty",
                table: "Receitas");
        }
    }
}
