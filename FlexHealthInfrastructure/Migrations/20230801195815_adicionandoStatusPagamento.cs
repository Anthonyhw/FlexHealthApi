using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexHealthInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoStatusPagamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StatusPagamento",
                table: "tfh_agendamentos",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StatusPagamento",
                table: "tfh_agendamentos");
        }
    }
}
