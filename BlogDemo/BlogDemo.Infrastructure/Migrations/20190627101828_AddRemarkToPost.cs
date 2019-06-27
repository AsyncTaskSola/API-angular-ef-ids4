using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogDemo.Infrastructure.Migrations
{
    public partial class AddRemarkToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Remark",
                table: "Posts",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Remark",
                table: "Posts");
        }
    }
}
