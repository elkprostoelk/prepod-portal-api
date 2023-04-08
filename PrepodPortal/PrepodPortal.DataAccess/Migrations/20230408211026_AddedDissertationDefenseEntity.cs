using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrepodPortal.DataAccess.Migrations
{
    public partial class AddedDissertationDefenseEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Publications",
                type: "date",
                nullable: true,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true,
                oldDefaultValue: new DateTime(2023, 2, 12, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.CreateTable(
                name: "DissertationDefenses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Theme = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CipherAndSpecialty = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DefenseDate = table.Column<DateTime>(type: "date", nullable: true),
                    ReceiveDiplomaDate = table.Column<DateTime>(type: "date", nullable: true),
                    DiplomaNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DefenseLocationAndWhoAssignedBy = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ScientificDirector = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DissertationDefenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DissertationDefenses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DissertationDefenses_UserId",
                table: "DissertationDefenses",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DissertationDefenses");

            migrationBuilder.AlterColumn<DateTime>(
                name: "OrderDate",
                table: "Publications",
                type: "date",
                nullable: true,
                defaultValue: new DateTime(2023, 2, 12, 0, 0, 0, 0, DateTimeKind.Local),
                oldClrType: typeof(DateTime),
                oldType: "date",
                oldNullable: true,
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
