using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PearAdmin.AbpTemplate.Migrations
{
    public partial class AddProfilePicture : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ProfilePictureId",
                table: "AbpUsers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePictureId",
                table: "AbpUsers");
        }
    }
}
