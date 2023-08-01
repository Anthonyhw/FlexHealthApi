using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexHealthInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class deixandoStatusPagamentoNulo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StatusPagamento",
                table: "tfh_agendamentos",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tfh_agendamentos",
                keyColumn: "StatusPagamento",
                keyValue: null,
                column: "StatusPagamento",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "StatusPagamento",
                table: "tfh_agendamentos",
                type: "longtext",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
