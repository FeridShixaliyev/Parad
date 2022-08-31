using Microsoft.EntityFrameworkCore.Migrations;

namespace Parad.Migrations
{
    public partial class ProfileImagIdOfUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PofileImagId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PofileImagId",
                table: "AspNetUsers");
        }
    }
}
