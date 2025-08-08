using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RuedaYPata.Migrations
{
    /// <inheritdoc />
    public partial class AddTipoAnimalField : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FotoUrl",
                table: "Mascotas",
                newName: "FotoRuta");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FotoRuta",
                table: "Mascotas",
                newName: "FotoUrl");
        }
    }
}
