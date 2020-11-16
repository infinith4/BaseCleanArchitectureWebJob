using Microsoft.EntityFrameworkCore.Migrations;

namespace ConsoleApp.Infrastructure.Migrations
{
    public partial class IntializeDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MST_AppSetting",
                columns: table => new
                {
                    ApiKey = table.Column<string>(type: "varchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MST_AppSetting", x => x.ApiKey);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MST_AppSetting");
        }
    }
}
