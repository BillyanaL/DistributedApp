using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PizzaTown.Data.Migrations
{
    public partial class removeRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropForeignKey(
            //    name: "FK_Users_Roles_RoleId1",
            //    table: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            //migrationBuilder.DropIndex(
            //    name: "IX_Users_RoleId1",
            //    table: "Users");

            //migrationBuilder.DropColumn(
            //    name: "RoleId",
            //    table: "Users");

            //migrationBuilder.DropColumn(
            //    name: "RoleId1",
            //    table: "Users");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<Guid>(
            //    name: "RoleId",
            //    table: "Users",
            //    type: "uniqueidentifier",
            //    nullable: false,
            //    defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            //migrationBuilder.AddColumn<string>(
            //    name: "RoleId1",
            //    table: "Users",
            //    type: "nvarchar(450)",
            //    nullable: false,
            //    defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            //migrationBuilder.CreateIndex(
            //    name: "IX_Users_RoleId1",
            //    table: "Users",
            //    column: "RoleId1");

            //migrationBuilder.AddForeignKey(
            //    name: "FK_Users_Roles_RoleId1",
            //    table: "Users",
            //    column: "RoleId1",
            //    principalTable: "Roles",
            //    principalColumn: "Id",
            //    onDelete: ReferentialAction.Cascade);
        }
    }
}
