using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexHealthInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class arrumandoNomeDaTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tf_agendamentos_AspNetUsers_MedicoId",
                table: "tf_agendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_tf_agendamentos_AspNetUsers_UsuarioId",
                table: "tf_agendamentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tf_agendamentos",
                table: "tf_agendamentos");

            migrationBuilder.RenameTable(
                name: "tf_agendamentos",
                newName: "tfh_agendamentos");

            migrationBuilder.RenameIndex(
                name: "IX_tf_agendamentos_UsuarioId",
                table: "tfh_agendamentos",
                newName: "IX_tfh_agendamentos_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_tf_agendamentos_MedicoId",
                table: "tfh_agendamentos",
                newName: "IX_tfh_agendamentos_MedicoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tfh_agendamentos",
                table: "tfh_agendamentos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tfh_agendamentos_AspNetUsers_MedicoId",
                table: "tfh_agendamentos",
                column: "MedicoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tfh_agendamentos_AspNetUsers_UsuarioId",
                table: "tfh_agendamentos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tfh_agendamentos_AspNetUsers_MedicoId",
                table: "tfh_agendamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_tfh_agendamentos_AspNetUsers_UsuarioId",
                table: "tfh_agendamentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tfh_agendamentos",
                table: "tfh_agendamentos");

            migrationBuilder.RenameTable(
                name: "tfh_agendamentos",
                newName: "tf_agendamentos");

            migrationBuilder.RenameIndex(
                name: "IX_tfh_agendamentos_UsuarioId",
                table: "tf_agendamentos",
                newName: "IX_tf_agendamentos_UsuarioId");

            migrationBuilder.RenameIndex(
                name: "IX_tfh_agendamentos_MedicoId",
                table: "tf_agendamentos",
                newName: "IX_tf_agendamentos_MedicoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tf_agendamentos",
                table: "tf_agendamentos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_tf_agendamentos_AspNetUsers_MedicoId",
                table: "tf_agendamentos",
                column: "MedicoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_tf_agendamentos_AspNetUsers_UsuarioId",
                table: "tf_agendamentos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
