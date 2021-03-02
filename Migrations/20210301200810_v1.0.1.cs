using Microsoft.EntityFrameworkCore.Migrations;

namespace EjercicioNET.Migrations
{
    public partial class v101 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Personas_Telefonos_TelefonoID",
                table: "Personas");

            migrationBuilder.DropIndex(
                name: "IX_Personas_TelefonoID",
                table: "Personas");

            migrationBuilder.DropColumn(
                name: "TelefonoID",
                table: "Personas");

            migrationBuilder.AddColumn<int>(
                name: "PersonaID",
                table: "Telefonos",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Telefonos_PersonaID",
                table: "Telefonos",
                column: "PersonaID");

            migrationBuilder.AddForeignKey(
                name: "FK_Telefonos_Personas_PersonaID",
                table: "Telefonos",
                column: "PersonaID",
                principalTable: "Personas",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Telefonos_Personas_PersonaID",
                table: "Telefonos");

            migrationBuilder.DropIndex(
                name: "IX_Telefonos_PersonaID",
                table: "Telefonos");

            migrationBuilder.DropColumn(
                name: "PersonaID",
                table: "Telefonos");

            migrationBuilder.AddColumn<int>(
                name: "TelefonoID",
                table: "Personas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Personas_TelefonoID",
                table: "Personas",
                column: "TelefonoID");

            migrationBuilder.AddForeignKey(
                name: "FK_Personas_Telefonos_TelefonoID",
                table: "Personas",
                column: "TelefonoID",
                principalTable: "Telefonos",
                principalColumn: "TelefonoID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
