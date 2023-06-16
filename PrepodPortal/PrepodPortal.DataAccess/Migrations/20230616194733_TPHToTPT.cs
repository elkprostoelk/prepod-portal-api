using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrepodPortal.DataAccess.Migrations
{
    public partial class TPHToTPT : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ArticleType",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "Article_Issue",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "Article_Number",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "Article_PageNumbers",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "Article_Tome",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "Article_Url",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "EditionName",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "EditionTitle",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "GryphGiven",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "GryphType",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "Isbn",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "Issn",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "Issue",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "LectureTheses_Isbn",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "MonographType",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "Number",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "OrderNum",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "OrderNumber",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "PageNumbers",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "PublisherTitle",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "SchoolBookType",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "SchoolBook_Isbn",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "SchoolBook_PublisherTitle",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "ScientometricDb",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "SnipIndex",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "Tome",
                table: "Publications");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Publications");

            migrationBuilder.CreateTable(
                name: "Articles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    ArticleType = table.Column<int>(type: "int", nullable: false),
                    EditionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Issue = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Tome = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PageNumbers = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Issn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ScientometricDb = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    SnipIndex = table.Column<float>(type: "real", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Articles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Articles_Publications_Id",
                        column: x => x.Id,
                        principalTable: "Publications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LectureTheses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    EditionTitle = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Number = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Issue = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    Tome = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    PageNumbers = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Isbn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrderNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Url = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LectureTheses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LectureTheses_Publications_Id",
                        column: x => x.Id,
                        principalTable: "Publications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Monographs",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    PublisherTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    GryphGiven = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    MonographType = table.Column<int>(type: "int", nullable: false),
                    Isbn = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Monographs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Monographs_Publications_Id",
                        column: x => x.Id,
                        principalTable: "Publications",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "SchoolBooks",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    SchoolBookType = table.Column<int>(type: "int", nullable: false),
                    GryphType = table.Column<int>(type: "int", nullable: false),
                    Isbn = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    OrderNum = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    OrderDate = table.Column<DateTime>(type: "date", nullable: false, defaultValueSql: "GETDATE()"),
                    PublisherTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SchoolBooks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SchoolBooks_Publications_Id",
                        column: x => x.Id,
                        principalTable: "Publications",
                        principalColumn: "Id");
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Articles");

            migrationBuilder.DropTable(
                name: "LectureTheses");

            migrationBuilder.DropTable(
                name: "Monographs");

            migrationBuilder.DropTable(
                name: "SchoolBooks");

            migrationBuilder.AddColumn<int>(
                name: "ArticleType",
                table: "Publications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Article_Issue",
                table: "Publications",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Article_Number",
                table: "Publications",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Article_PageNumbers",
                table: "Publications",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Article_Tome",
                table: "Publications",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Article_Url",
                table: "Publications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditionName",
                table: "Publications",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditionTitle",
                table: "Publications",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GryphGiven",
                table: "Publications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "GryphType",
                table: "Publications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Isbn",
                table: "Publications",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Issn",
                table: "Publications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Issue",
                table: "Publications",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LectureTheses_Isbn",
                table: "Publications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonographType",
                table: "Publications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Number",
                table: "Publications",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Publications",
                type: "date",
                nullable: true,
                defaultValueSql: "GETDATE()");

            migrationBuilder.AddColumn<string>(
                name: "OrderNum",
                table: "Publications",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrderNumber",
                table: "Publications",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PageNumbers",
                table: "Publications",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PublisherTitle",
                table: "Publications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SchoolBookType",
                table: "Publications",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SchoolBook_Isbn",
                table: "Publications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SchoolBook_PublisherTitle",
                table: "Publications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ScientometricDb",
                table: "Publications",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "SnipIndex",
                table: "Publications",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tome",
                table: "Publications",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Publications",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);
        }
    }
}
