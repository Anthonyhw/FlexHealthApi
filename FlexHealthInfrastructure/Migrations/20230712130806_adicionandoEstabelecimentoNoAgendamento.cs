using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexHealthInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoEstabelecimentoNoAgendamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataMarcacao",
                table: "tfh_agendamentos",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<int>(
                name: "EstabelecimentoId",
                table: "tfh_agendamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tfh_agendamentos_EstabelecimentoId",
                table: "tfh_agendamentos",
                column: "EstabelecimentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_tfh_agendamentos_AspNetUsers_EstabelecimentoId",
                table: "tfh_agendamentos",
                column: "EstabelecimentoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tfh_agendamentos_AspNetUsers_EstabelecimentoId",
                table: "tfh_agendamentos");

            migrationBuilder.DropIndex(
                name: "IX_tfh_agendamentos_EstabelecimentoId",
                table: "tfh_agendamentos");

            migrationBuilder.DropColumn(
                name: "EstabelecimentoId",
                table: "tfh_agendamentos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataMarcacao",
                table: "tfh_agendamentos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }
    }
}
