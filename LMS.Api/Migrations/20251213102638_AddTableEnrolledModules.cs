using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LMS.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddTableEnrolledModules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Module_Courses_CourseId",
                table: "Module");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Module",
                table: "Module");

            migrationBuilder.RenameTable(
                name: "Module",
                newName: "Modules");

            migrationBuilder.RenameIndex(
                name: "IX_Module_CourseId",
                table: "Modules",
                newName: "IX_Modules_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Modules",
                table: "Modules",
                column: "ModuleId");

            migrationBuilder.CreateTable(
                name: "EnrolledModules",
                columns: table => new
                {
                    EnrolledModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EnrolledCourseId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModuleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnrolledModules", x => x.EnrolledModuleId);
                    table.ForeignKey(
                        name: "FK_EnrolledModules_EnrolledCourses_EnrolledCourseId",
                        column: x => x.EnrolledCourseId,
                        principalTable: "EnrolledCourses",
                        principalColumn: "EnrolledCourseId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EnrolledModules_Modules_ModuleId",
                        column: x => x.ModuleId,
                        principalTable: "Modules",
                        principalColumn: "ModuleId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EnrolledModules_EnrolledCourseId",
                table: "EnrolledModules",
                column: "EnrolledCourseId");

            migrationBuilder.CreateIndex(
                name: "IX_EnrolledModules_ModuleId",
                table: "EnrolledModules",
                column: "ModuleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Modules_Courses_CourseId",
                table: "Modules",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Modules_Courses_CourseId",
                table: "Modules");

            migrationBuilder.DropTable(
                name: "EnrolledModules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Modules",
                table: "Modules");

            migrationBuilder.RenameTable(
                name: "Modules",
                newName: "Module");

            migrationBuilder.RenameIndex(
                name: "IX_Modules_CourseId",
                table: "Module",
                newName: "IX_Module_CourseId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Module",
                table: "Module",
                column: "ModuleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Module_Courses_CourseId",
                table: "Module",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "CourseId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
