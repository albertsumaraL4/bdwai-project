using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Projekt.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAfterChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Wiek",
                table: "Users",
                newName: "Town");

            migrationBuilder.RenameColumn(
                name: "Numer",
                table: "Users",
                newName: "Surname");

            migrationBuilder.RenameColumn(
                name: "Nazwisko",
                table: "Users",
                newName: "RepeatPassword");

            migrationBuilder.RenameColumn(
                name: "Miasto",
                table: "Users",
                newName: "Age");

            migrationBuilder.RenameColumn(
                name: "Imie",
                table: "Users",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "Haslo2",
                table: "Users",
                newName: "Number");

            migrationBuilder.RenameColumn(
                name: "Haslo",
                table: "Users",
                newName: "Name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Town",
                table: "Users",
                newName: "Wiek");

            migrationBuilder.RenameColumn(
                name: "Surname",
                table: "Users",
                newName: "Numer");

            migrationBuilder.RenameColumn(
                name: "RepeatPassword",
                table: "Users",
                newName: "Nazwisko");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "Imie");

            migrationBuilder.RenameColumn(
                name: "Number",
                table: "Users",
                newName: "Haslo2");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Users",
                newName: "Haslo");

            migrationBuilder.RenameColumn(
                name: "Age",
                table: "Users",
                newName: "Miasto");
        }
    }
}
