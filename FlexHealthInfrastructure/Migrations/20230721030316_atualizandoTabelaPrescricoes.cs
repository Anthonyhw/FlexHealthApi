using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexHealthInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class atualizandoTabelaPrescricoes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tfh_resultados_AspNetUsers_UsuarioId",
                table: "tfh_resultados");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tfh_resultados",
                table: "tfh_resultados");

            migrationBuilder.RenameTable(
                name: "tfh_resultados",
                newName: "tfh_prescricoes");

            migrationBuilder.RenameColumn(
                name: "Exame",
                table: "tfh_prescricoes",
                newName: "Proposito");

            migrationBuilder.RenameIndex(
                name: "IX_tfh_resultados_UsuarioId",
                table: "tfh_prescricoes",
                newName: "IX_tfh_prescricoes_UsuarioId");

            migrationBuilder.AddColumn<string>(
                name: "ExameURL",
                table: "tfh_prescricoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MedicoId",
                table: "tfh_prescricoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tfh_prescricoes",
                table: "tfh_prescricoes",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_tfh_prescricoes_MedicoId",
                table: "tfh_prescricoes",
                column: "MedicoId");

            migrationBuilder.AddForeignKey(
                name: "FK_tfh_prescricoes_AspNetUsers_MedicoId",
                table: "tfh_prescricoes",
                column: "MedicoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tfh_prescricoes_AspNetUsers_UsuarioId",
                table: "tfh_prescricoes",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tfh_prescricoes_AspNetUsers_MedicoId",
                table: "tfh_prescricoes");

            migrationBuilder.DropForeignKey(
                name: "FK_tfh_prescricoes_AspNetUsers_UsuarioId",
                table: "tfh_prescricoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tfh_prescricoes",
                table: "tfh_prescricoes");

            migrationBuilder.DropIndex(
                name: "IX_tfh_prescricoes_MedicoId",
                table: "tfh_prescricoes");

            migrationBuilder.DropColumn(
                name: "ExameURL",
                table: "tfh_prescricoes");

            migrationBuilder.DropColumn(
                name: "MedicoId",
                table: "tfh_prescricoes");

            migrationBuilder.RenameTable(
                name: "tfh_prescricoes",
                newName: "tfh_resultados");

            migrationBuilder.RenameColumn(
                name: "Proposito",
                table: "tfh_resultados",
                newName: "Exame");

            migrationBuilder.RenameIndex(
                name: "IX_tfh_prescricoes_UsuarioId",
                table: "tfh_resultados",
                newName: "IX_tfh_resultados_UsuarioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tfh_resultados",
                table: "tfh_resultados",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tfh_resultados_AspNetUsers_UsuarioId",
                table: "tfh_resultados",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
