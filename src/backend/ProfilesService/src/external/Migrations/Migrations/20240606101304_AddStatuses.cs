using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddStatuses : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_DoctorsStatus_StatusId",
                table: "Doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorsStatus",
                table: "DoctorsStatus");

            migrationBuilder.RenameTable(
                name: "DoctorsStatus",
                newName: "DoctorStatuses");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorStatuses",
                table: "DoctorStatuses",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_DoctorStatuses_StatusId",
                table: "Doctors",
                column: "StatusId",
                principalTable: "DoctorStatuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Doctors_DoctorStatuses_StatusId",
                table: "Doctors");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DoctorStatuses",
                table: "DoctorStatuses");

            migrationBuilder.RenameTable(
                name: "DoctorStatuses",
                newName: "DoctorsStatus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DoctorsStatus",
                table: "DoctorsStatus",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Doctors_DoctorsStatus_StatusId",
                table: "Doctors",
                column: "StatusId",
                principalTable: "DoctorsStatus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
