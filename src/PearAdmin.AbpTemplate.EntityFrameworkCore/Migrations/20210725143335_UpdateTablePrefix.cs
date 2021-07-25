using Microsoft.EntityFrameworkCore.Migrations;

namespace PearAdmin.AbpTemplate.EntityFrameworkCore.Migrations
{
    public partial class UpdateTablePrefix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AppFriendships",
                table: "AppFriendships");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppChatMessages",
                table: "AppChatMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AppBinaryObjects",
                table: "AppBinaryObjects");

            migrationBuilder.RenameTable(
                name: "AppFriendships",
                newName: "Social_Friendship");

            migrationBuilder.RenameTable(
                name: "AppChatMessages",
                newName: "Social_ChatMessage");

            migrationBuilder.RenameTable(
                name: "AppBinaryObjects",
                newName: "Common_BinaryObject");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Resource_DataDictionaryItem",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Resource_DataDictionaryItem",
                type: "varchar(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldMaxLength: 5,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Center_DailyTask",
                type: "varchar(200)",
                maxLength: 200,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Social_Friendship",
                table: "Social_Friendship",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Social_ChatMessage",
                table: "Social_ChatMessage",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Common_BinaryObject",
                table: "Common_BinaryObject",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Social_Friendship",
                table: "Social_Friendship");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Social_ChatMessage",
                table: "Social_ChatMessage");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Common_BinaryObject",
                table: "Common_BinaryObject");

            migrationBuilder.RenameTable(
                name: "Social_Friendship",
                newName: "AppFriendships");

            migrationBuilder.RenameTable(
                name: "Social_ChatMessage",
                newName: "AppChatMessages");

            migrationBuilder.RenameTable(
                name: "Common_BinaryObject",
                newName: "AppBinaryObjects");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Resource_DataDictionaryItem",
                type: "varchar(30)",
                maxLength: 30,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Code",
                table: "Resource_DataDictionaryItem",
                type: "varchar(5)",
                maxLength: 5,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(5)",
                oldMaxLength: 5)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Center_DailyTask",
                type: "varchar(200)",
                maxLength: 200,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(200)",
                oldMaxLength: 200)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppFriendships",
                table: "AppFriendships",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppChatMessages",
                table: "AppChatMessages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AppBinaryObjects",
                table: "AppBinaryObjects",
                column: "Id");
        }
    }
}
