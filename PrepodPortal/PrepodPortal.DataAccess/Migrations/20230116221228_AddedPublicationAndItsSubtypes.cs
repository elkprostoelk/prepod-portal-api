using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrepodPortal.DataAccess.Migrations
{
    public partial class AddedPublicationAndItsSubtypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_UserId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AspNetUsers");

            migrationBuilder.CreateTable(
                name: "Publications",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PublicationType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    PublishedLocation = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PublishedYear = table.Column<int>(type: "int", nullable: true),
                    TotalPagesCount = table.Column<int>(type: "int", nullable: false),
                    AuthorPagesCount = table.Column<int>(type: "int", nullable: false),
                    TotalPrintedPageCount = table.Column<int>(type: "int", nullable: true),
                    PrintedAuthorPagesCount = table.Column<int>(type: "int", nullable: true),
                    ArticleType = table.Column<int>(type: "int", nullable: true),
                    EditionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Issn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ScientometricDb = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SnipIndex = table.Column<float>(type: "real", nullable: true),
                    EditionTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Issue = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Tome = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PageNumbers = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    OrderNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PublisherTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GryphGiven = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MonographType = table.Column<int>(type: "int", nullable: true),
                    Isbn = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    SchoolBookType = table.Column<int>(type: "int", nullable: true),
                    GryphType = table.Column<int>(type: "int", nullable: true),
                    OrderNum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    OrderDate = table.Column<DateTime>(type: "date", nullable: true, defaultValue: new DateTime(2023, 1, 17, 0, 0, 0, 0, DateTimeKind.Local)),
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Publications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserPublications",
                columns: table => new
                {
                    PublicationId = table.Column<long>(type: "bigint", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPublications", x => new { x.UserId, x.PublicationId });
                    table.ForeignKey(
                        name: "FK_UserPublications_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserPublications_Publications_PublicationId",
                        column: x => x.PublicationId,
                        principalTable: "Publications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserPublications_PublicationId",
                table: "UserPublications",
                column: "PublicationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserPublications");

            migrationBuilder.DropTable(
                name: "Publications");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "AspNetUsers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserId",
                table: "AspNetUsers",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetUsers_UserId",
                table: "AspNetUsers",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
