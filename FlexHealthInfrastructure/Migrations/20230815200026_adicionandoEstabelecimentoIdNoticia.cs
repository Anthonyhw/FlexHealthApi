using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexHealthInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class adicionandoEstabelecimentoIdNoticia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EstabelecimentoId",
                table: "tfh_noticias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_tfh_noticias_EstabelecimentoId",
                table: "tfh_noticias",
                column: "EstabelecimentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_tfh_noticias_AspNetUsers_EstabelecimentoId",
                table: "tfh_noticias",
                column: "EstabelecimentoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tfh_noticias_AspNetUsers_EstabelecimentoId",
                table: "tfh_noticias");

            migrationBuilder.DropIndex(
                name: "IX_tfh_noticias_EstabelecimentoId",
                table: "tfh_noticias");

            migrationBuilder.DropColumn(
                name: "EstabelecimentoId",
                table: "tfh_noticias");
        }
    }
}
