using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FanDuelSolution.API.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Number = table.Column<byte>(type: "INTEGER", nullable: false),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false),
                    PositionType = table.Column<string>(type: "TEXT", maxLength: 30, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    Abbreviation = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DepthCharts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TeamId = table.Column<int>(type: "INTEGER", nullable: false),
                    PositionId = table.Column<int>(type: "INTEGER", nullable: false),
                    PlayerId = table.Column<int>(type: "INTEGER", nullable: false),
                    PositionDepth = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepthCharts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepthCharts_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepthCharts_Positions_PositionId",
                        column: x => x.PositionId,
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepthCharts_Team_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "FirstName", "LastName", "Number" },
                values: new object[,]
                {
                    { 1, "Mike", "Evans", (byte)13 },
                    { 2, "Tyler", "Johnson", (byte)18 },
                    { 3, "Donovan", "Smith", (byte)76 },
                    { 4, "Ali", "Marpet", (byte)74 },
                    { 5, "Ryan", "Jensen", (byte)66 },
                    { 6, "Alex", "Cappa", (byte)65 },
                    { 7, "Tristan", "Wirfs", (byte)78 },
                    { 8, "Jaelon", "Darden", (byte)1 },
                    { 9, "Scott", "Miller", (byte)10 },
                    { 10, "Breshad", "Perriman", (byte)16 },
                    { 11, "Cyril", "Grayson", (byte)15 }
                });

            migrationBuilder.InsertData(
                table: "Positions",
                columns: new[] { "Id", "Name", "PositionType" },
                values: new object[,]
                {
                    { 1, "LWR", "Offense" },
                    { 2, "RWR", "Offense" },
                    { 3, "LT", "Offense" },
                    { 4, "LG", "Offense" },
                    { 5, "C", "Offense" },
                    { 6, "RG", "Offense" },
                    { 7, "RT", "Offense" },
                    { 8, "TE", "Offense" },
                    { 9, "QB", "Offense" },
                    { 10, "RB", "Offense" },
                    { 11, "DE", "Defense" },
                    { 12, "NT", "Defense" },
                    { 13, "OLB", "Defense" },
                    { 14, "ILB", "Defense" },
                    { 15, "CB", "Defense" },
                    { 16, "SS", "Defense" },
                    { 17, "FS", "Defense" },
                    { 18, "RCB", "Defense" },
                    { 19, "PT", "Special Teams" },
                    { 20, "PK", "Special Teams" },
                    { 21, "LS", "Special Teams" },
                    { 22, "H", "Special Teams" },
                    { 23, "KO", "Special Teams" },
                    { 24, "PR", "Special Teams" },
                    { 25, "KR", "Special Teams" },
                    { 26, "RES", "Reserves" },
                    { 27, "FUT", "Reserves" }
                });

            migrationBuilder.InsertData(
                table: "Team",
                columns: new[] { "Id", "Abbreviation", "Name" },
                values: new object[] { 1, "TBB", "Tampa Bay Buccaneers" });

            migrationBuilder.InsertData(
                table: "DepthCharts",
                columns: new[] { "Id", "PlayerId", "PositionDepth", "PositionId", "TeamId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 1 },
                    { 2, 8, 2, 1, 1 },
                    { 3, 10, 1, 2, 1 },
                    { 4, 11, 2, 2, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DepthCharts_PlayerId",
                table: "DepthCharts",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_DepthCharts_PositionId",
                table: "DepthCharts",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_DepthCharts_TeamId",
                table: "DepthCharts",
                column: "TeamId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DepthCharts");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Team");
        }
    }
}
