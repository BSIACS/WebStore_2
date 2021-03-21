using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.DAL.Migrations
{
    public partial class Initial_Update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Section",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Section",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
