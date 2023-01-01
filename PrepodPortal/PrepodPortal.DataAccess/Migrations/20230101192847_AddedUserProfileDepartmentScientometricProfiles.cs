using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PrepodPortal.DataAccess.Migrations
{
    public partial class AddedUserProfileDepartmentScientometricProfiles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Town = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomeTown = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: true),
                    AcademicTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ScienceDegree = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartmentId = table.Column<long>(type: "bigint", nullable: false),
                    AvatarImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ScientometricDbProfiles",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ProfileLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserProfileId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScientometricDbProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScientometricDbProfiles_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1L, "Кафедра комп’ютерних наук та програмної інженерії" },
                    { 2L, "Кафедра фізики" },
                    { 3L, "Кафедра алгебри, геометрії та математичного аналізу" },
                    { 4L, "Кафедра географії та екології" },
                    { 5L, "Кафедра біології людини та імунології" },
                    { 6L, "Кафедра ботаніки" },
                    { 7L, "Кафедра педагогіки, психології й освітнього менеджменту імені проф. Є. Петухова" },
                    { 8L, "Кафедра спеціальної освіти" },
                    { 9L, "Кафедра теорії та методики дошкільної та початкової освіти" },
                    { 10L, "Кафедра педагогіки та психології дошкільної та початкової освіти" },
                    { 11L, "Кафедра національного, міжнародного права та правоохоронної діяльності" },
                    { 12L, "Кафедра готельно-ресторанного та туристичного бізнесу" },
                    { 13L, "Кафедра економіки, менеджменту та адміністрування" },
                    { 14L, "Кафедра фінансів, обліку та підприємництва" },
                    { 15L, "Кафедра англійської філології та світової літератури імені професора Олега Мішукова" },
                    { 16L, "Кафедра німецької та романської філології" },
                    { 17L, "Кафедра української і слов'янської філології та журналістики" },
                    { 18L, "Кафедра музичного мистецтва" },
                    { 19L, "Кафедра культурології" },
                    { 20L, "Кафедра образотворчого мистецтва і дизайну" },
                    { 21L, "Кафедра хореографічного мистецтва" },
                    { 22L, "Кафедра фізичної терапії та ерготерапії" },
                    { 23L, "Кафедра хімії та фармації" },
                    { 24L, "Кафедра медицини" },
                    { 25L, "Кафедра філософії, соціології та соціальної роботи" },
                    { 26L, "Кафедра психології" },
                    { 27L, "Кафедра історії, археології та методики викладання" },
                    { 28L, "Кафедра медико-біологічних основ фізичного виховання та спорту" },
                    { 29L, "Кафедра теорії та методики фізичного виховання" },
                    { 30L, "Кафедра олімпійського та професійного спорту" },
                    { 31L, "Не вказано" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Departments_Title",
                table: "Departments",
                column: "Title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ScientometricDbProfiles_UserProfileId",
                table: "ScientometricDbProfiles",
                column: "UserProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_DepartmentId",
                table: "UserProfiles",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ScientometricDbProfiles");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_Email",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);
        }
    }
}
