using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LessonApi.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lessons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LessonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LessonPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lessons", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "CreateDate", "Description", "LessonName", "LessonPrice" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 2, 21, 16, 34, 32, 715, DateTimeKind.Local).AddTicks(9152), "Olur olur", "Matematik", 400m },
                    { 2, new DateTime(2024, 2, 21, 16, 34, 32, 715, DateTimeKind.Local).AddTicks(9167), "Kolay olur", "Türkçe", 200m },
                    { 3, new DateTime(2024, 2, 21, 16, 34, 32, 715, DateTimeKind.Local).AddTicks(9169), "Zor olur", "Fizik", 400m },
                    { 4, new DateTime(2024, 2, 21, 16, 34, 32, 715, DateTimeKind.Local).AddTicks(9169), " Biraz Zor", "Kimya", 300m }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lessons");
        }
    }
}
