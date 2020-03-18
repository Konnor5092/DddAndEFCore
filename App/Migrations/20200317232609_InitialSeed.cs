using Microsoft.EntityFrameworkCore.Migrations;

namespace App.Migrations
{
    public partial class InitialSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Course",
                columns: table => new
                {
                    CourseID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Course", x => x.CourseID);
                });

            migrationBuilder.CreateTable(
                name: "Student",
                columns: table => new
                {
                    StudentID = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    FavoriteCourseId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Student", x => x.StudentID);
                    table.ForeignKey(
                        name: "FK_Student_Course_FavoriteCourseId",
                        column: x => x.FavoriteCourseId,
                        principalTable: "Course",
                        principalColumn: "CourseID",
                        onDelete: ReferentialAction.Restrict);
                });

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
                values: new object[] { 1L, "alice@gmail.com", 2L, "Alice" });

            migrationBuilder.InsertData(
                table: "Student",
                columns: new[] { "StudentID", "Email", "FavoriteCourseId", "Name" },
                values: new object[] { 2L, "bob@outlook.com", 2L, "Bob" });

            migrationBuilder.CreateIndex(
                name: "IX_Student_FavoriteCourseId",
                table: "Student",
                column: "FavoriteCourseId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Student");

            migrationBuilder.DropTable(
                name: "Course");
        }
    }
}
