using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexHealthInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoAgendamentoNaTabelaPrescricoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AgendamentoId",
                table: "tfh_prescricoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tfh_prescricoes_AgendamentoId",
                table: "tfh_prescricoes",
                column: "AgendamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_tfh_prescricoes_tfh_agendamentos_AgendamentoId",
                table: "tfh_prescricoes",
                column: "AgendamentoId",
                principalTable: "tfh_agendamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tfh_prescricoes_tfh_agendamentos_AgendamentoId",
                table: "tfh_prescricoes");

            migrationBuilder.DropIndex(
                name: "IX_tfh_prescricoes_AgendamentoId",
                table: "tfh_prescricoes");

            migrationBuilder.DropColumn(
                name: "AgendamentoId",
                table: "tfh_prescricoes");
        }
    }
}
