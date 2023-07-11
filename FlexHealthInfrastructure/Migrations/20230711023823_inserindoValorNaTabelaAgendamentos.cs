using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexHealthInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class inserindoValorNaTabelaAgendamentos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Valor",
                table: "tfh_agendamentos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Valor",
                table: "tfh_agendamentos");
        }
    }
}
