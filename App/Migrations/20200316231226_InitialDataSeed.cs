using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class InitialDataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Course",
                columns: new[] { "CourseID", "Name" },
                values: new object[,]
                {
                    { 1L, "Calculus" },
                    { 2L, "Chemistry" },
                    { 3L, "Literature" },
                    { 4L, "Trigonometry" },
                    { 5L, "Microeconomics" }
                });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "StudentID", "Email", "FavoriteCourseId", "Name" },
                values: new object[,]
                {
                    { 1L, "alice@gmail.com", 2L, "Alice" },
                    { 2L, "bob@outlook.com", 2L, "Bob" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "CourseID",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "CourseID",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "CourseID",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "CourseID",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "Course",
                keyColumn: "CourseID",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "Student",
                keyColumn: "StudentID",
                keyValue: 2L);
        }
    }
}
