using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaTown.Data.Migrations
{
    public partial class UpdateDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable("Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
