using Microsoft.EntityFrameworkCore.Migrations;

namespace MovieDemo.DB.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieRatings",
                columns: table => new
                {
                    MovieId = table.Column<long>(type: "bigint", nullable: false),
                    AvgRating = table.Column<decimal>(type: "decimal(3,1)", nullable: true),
                    NumVotes = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieRatings", x => x.MovieId);
                });

            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    MovieId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    YearOfRelease = table.Column<int>(type: "int", nullable: true),
                    RunningTime = table.Column<int>(type: "int", nullable: true),
                    Genres = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.MovieId);
                });

            migrationBuilder.CreateTable(
                name: "MovieUserRatings",
                columns: table => new
                {
                    MovieId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    AvgRating = table.Column<decimal>(type: "decimal(3,1)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieUserRatings", x => new { x.MovieId, x.UserId });
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.InsertData(
                table: "MovieRatings",
                columns: new[] { "MovieId", "AvgRating", "NumVotes" },
                values: new object[,]
                {
                    { 5L, 1.5m, 1L },
                    { 3L, 1m, 1L },
                    { 2L, 1m, 1L },
                    { 1L, 0.5m, 1L },
                    { 6L, 2m, 1L },
                    { 7L, 2m, 1L },
                    { 8L, 2.5m, 1L },
                    { 9L, 2.5m, 1L },
                    { 10L, 3m, 1L },
                    { 4L, 1.5m, 1L }
                });

            migrationBuilder.InsertData(
                table: "MovieUserRatings",
                columns: new[] { "MovieId", "UserId", "AvgRating" },
                values: new object[,]
                {
                    { 1L, 1L, 0.5m },
                    { 4L, 4L, 1.5m },
                    { 5L, 5L, 1.5m },
                    { 6L, 6L, 2m },
                    { 7L, 7L, 2m },
                    { 8L, 8L, 2.5m },
                    { 9L, 9L, 2.5m },
                    { 10L, 10L, 3m },
                    { 2L, 2L, 1m },
                    { 3L, 3L, 1m }
                });

            migrationBuilder.InsertData(
                table: "Movies",
                columns: new[] { "MovieId", "Genres", "RunningTime", "Title", "YearOfRelease" },
                values: new object[,]
                {
                    { 1L, "Short,Talk -Show", 120, "Creative Lite", 2019 },
                    { 14L, "Animation,Comedy,Fantasy", 120, "Washington", 2019 },
                    { 15L, "Drama", 120, "Lazor Wulf", 2019 },
                    { 2L, "Comedy,Talk-Show", 120, "A Little Late with Lilly Singh", 2019 },
                    { 3L, "Documentary,Sport", 120, "The Queen's Gambit", 2019 },
                    { 5L, "Comedy,Drama,Romance", 120, "The Substitute", 2019 },
                    { 6L, "Biography,Documentary,Drama", 120, "Harry's Heroes: The Full English", 2019 },
                    { 7L, "Comedy,Drama,Romance", 120, "Stolen Away", 2019 },
                    { 4L, "Action,Adventure,Crime", 120, "Followers", 2019 },
                    { 9L, "Animation,Comedy,Fantasy", 120, "The Gift", 2019 },
                    { 10L, "Drama", 120, "Feel Good", 2019 },
                    { 11L, "Drama", 120, "Lost Treasures of Egypt", 2019 },
                    { 12L, "Animation,Comedy,Fantasy", 120, "Stu, My Name is Stu", 2019 },
                    { 13L, "Drama", 120, "Thirumanam", 2019 },
                    { 8L, "Crime,Drama,Music", 120, "Home for Christmas", 2019 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Name" },
                values: new object[,]
                {
                    { 18L, "UserName18" },
                    { 11L, "UserName11" },
                    { 17L, "UserName17" },
                    { 16L, "UserName16" },
                    { 15L, "UserName15" },
                    { 14L, "UserName14" },
                    { 13L, "UserName13" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "Name" },
                values: new object[,]
                {
                    { 12L, "UserName12" },
                    { 10L, "UserName10" },
                    { 3L, "UserName3" },
                    { 8L, "UserName8" },
                    { 7L, "UserName7" },
                    { 6L, "UserName6" },
                    { 5L, "UserName5" },
                    { 4L, "UserName4" },
                    { 2L, "UserName2" },
                    { 1L, "UserName1" },
                    { 19L, "UserName19" },
                    { 9L, "UserName9" },
                    { 20L, "UserName20" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieRatings");

            migrationBuilder.DropTable(
                name: "Movies");

            migrationBuilder.DropTable(
                name: "MovieUserRatings");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
