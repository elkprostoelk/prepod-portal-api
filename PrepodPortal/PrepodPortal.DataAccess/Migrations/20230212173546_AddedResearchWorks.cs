using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrepodPortal.DataAccess.Migrations
{
    public partial class AddedResearchWorks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Publications",
                type: "date",
                nullable: true,
                defaultValue: new DateTime(2023, 2, 12, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 1, 17, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.AddColumn<long>(
                name: "ResearchWorkId",
                table: "Publications",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "ResearchWorks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    StateRegisterNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Type = table.Column<int>(type: "int", nullable: false),
                    TitleAndContentOfPerformedStage = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    ObtainedScientificResult = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    NoveltyOfScientificResult = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    PracticalResultsValue = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    HeldFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    HeldTo = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResearchWorks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserResearchWorks",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ResearchWorkId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserResearchWorks", x => new { x.UserId, x.ResearchWorkId });
                    table.ForeignKey(
                        name: "FK_UserResearchWorks_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserResearchWorks_ResearchWorks_ResearchWorkId",
                        column: x => x.ResearchWorkId,
                        principalTable: "ResearchWorks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Publications_ResearchWorkId",
                table: "Publications",
                column: "ResearchWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_UserResearchWorks_ResearchWorkId",
                table: "UserResearchWorks",
                column: "ResearchWorkId");

            migrationBuilder.AddForeignKey(
                name: "FK_Publications_ResearchWorks_ResearchWorkId",
                table: "Publications",
                column: "ResearchWorkId",
                principalTable: "ResearchWorks",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Publications_ResearchWorks_ResearchWorkId",
                table: "Publications");

            migrationBuilder.DropTable(
                name: "UserResearchWorks");

            migrationBuilder.DropTable(
                name: "ResearchWorks");

            migrationBuilder.DropIndex(
                name: "IX_Publications_ResearchWorkId",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "ResearchWorkId",
                table: "Publications");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Publications",
                type: "date",
                nullable: true,
                defaultValue: new DateTime(2023, 1, 17, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 2, 12, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}
