using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrepodPortal.DataAccess.Migrations
{
    public partial class AddedAcademicDegree : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicDegree_AspNetUsers_UserId",
                table: "AcademicDegree");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicDegree",
                table: "AcademicDegree");

            migrationBuilder.RenameTable(
                name: "AcademicDegree",
                newName: "AcademicDegrees");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicDegree_UserId",
                table: "AcademicDegrees",
                newName: "IX_AcademicDegrees_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicDegrees",
                table: "AcademicDegrees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicDegrees_AspNetUsers_UserId",
                table: "AcademicDegrees",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AcademicDegrees_AspNetUsers_UserId",
                table: "AcademicDegrees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AcademicDegrees",
                table: "AcademicDegrees");

            migrationBuilder.RenameTable(
                name: "AcademicDegrees",
                newName: "AcademicDegree");

            migrationBuilder.RenameIndex(
                name: "IX_AcademicDegrees_UserId",
                table: "AcademicDegree",
                newName: "IX_AcademicDegree_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AcademicDegree",
                table: "AcademicDegree",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AcademicDegree_AspNetUsers_UserId",
                table: "AcademicDegree",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
