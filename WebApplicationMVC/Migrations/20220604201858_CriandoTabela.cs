using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApplicationMVC.Migrations
{
    public partial class CriandoTabela : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "Conta",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nome",
                table: "Conta");
        }
    }
}
