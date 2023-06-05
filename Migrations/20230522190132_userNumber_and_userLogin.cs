using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace uwierzytelnianieClaimsIdentity.Migrations
{
    public partial class userNumber_and_userLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "userNumber",
                table: "Search",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "userLogin",
                table: "Search",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "userLogin",
                table: "Search");

            migrationBuilder.AlterColumn<int>(
                name: "userNumber",
                table: "Search",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
