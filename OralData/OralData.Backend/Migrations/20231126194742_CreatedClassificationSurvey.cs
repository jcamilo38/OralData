using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OralData.Backend.Migrations
{
    /// <inheritdoc />
    public partial class CreatedClassificationSurvey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClassificationSurveys",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Synthoms = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    severityOfSymptoms = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    relevantMedicalHistory = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    otherDetails = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClassificationSurveys", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClassificationSurveys_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClassificationSurveys_UserId",
                table: "ClassificationSurveys",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClassificationSurveys");
        }
    }
}
