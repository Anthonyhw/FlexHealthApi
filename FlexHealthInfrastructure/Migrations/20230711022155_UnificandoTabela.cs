using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlexHealthInfrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UnificandoTabela : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tfh_consultas");

            migrationBuilder.DropTable(
                name: "tfh_exames");

            migrationBuilder.CreateTable(
                name: "tf_agendamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataConsulta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataMarcacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    MedicoId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tf_agendamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tf_agendamentos_AspNetUsers_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tf_agendamentos_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tf_agendamentos_MedicoId",
                table: "tf_agendamentos",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_tf_agendamentos_UsuarioId",
                table: "tf_agendamentos",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tf_agendamentos");

            migrationBuilder.CreateTable(
                name: "tfh_consultas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    DataConsulta = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataMarcacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tfh_consultas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tfh_consultas_AspNetUsers_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tfh_consultas_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tfh_exames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedicoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    DataExame = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataMarcacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tfh_exames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tfh_exames_AspNetUsers_MedicoId",
                        column: x => x.MedicoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tfh_exames_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tfh_consultas_MedicoId",
                table: "tfh_consultas",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_tfh_consultas_UsuarioId",
                table: "tfh_consultas",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_tfh_exames_MedicoId",
                table: "tfh_exames",
                column: "MedicoId");

            migrationBuilder.CreateIndex(
                name: "IX_tfh_exames_UsuarioId",
                table: "tfh_exames",
                column: "UsuarioId");
        }
    }
}
