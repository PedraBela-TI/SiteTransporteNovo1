using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SiteTransporteNovo.Migrations
{
    /// <inheritdoc />
    public partial class RemoverObrigatoriedadeObservacao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "Agendamentos",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Observacao",
                table: "Agendamentos",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
