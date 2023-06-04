using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Eventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Local = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataEvento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Tema = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QtdPessoas = table.Column<int>(type: "int", nullable: false),
                    ImagemURL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Eventos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Palestrantes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MiniCurriculo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImagemURl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Palestrantes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PalestrantesEventos",
                columns: table => new
                {
                    PalestranteId = table.Column<int>(type: "int", nullable: false),
                    EventoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PalestrantesEventos", x => new { x.EventoId, x.PalestranteId });
                });

            migrationBuilder.CreateTable(
                name: "Lotes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Preco = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataInicio = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DataFim = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    EventoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lotes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lotes_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RedesSociais",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    URL = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EventoId = table.Column<int>(type: "int", nullable: true),
                    PalestranteId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RedesSociais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RedesSociais_Eventos_EventoId",
                        column: x => x.EventoId,
                        principalTable: "Eventos",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RedesSociais_Palestrantes_PalestranteId",
                        column: x => x.PalestranteId,
                        principalTable: "Palestrantes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EventoPalestrantesEvento",
                columns: table => new
                {
                    EventosId = table.Column<int>(type: "int", nullable: false),
                    PalestrantesEventosEventoId = table.Column<int>(type: "int", nullable: false),
                    PalestrantesEventosPalestranteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventoPalestrantesEvento", x => new { x.EventosId, x.PalestrantesEventosEventoId, x.PalestrantesEventosPalestranteId });
                    table.ForeignKey(
                        name: "FK_EventoPalestrantesEvento_Eventos_EventosId",
                        column: x => x.EventosId,
                        principalTable: "Eventos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventoPalestrantesEvento_PalestrantesEventos_PalestrantesEventosEventoId_PalestrantesEventosPalestranteId",
                        columns: x => new { x.PalestrantesEventosEventoId, x.PalestrantesEventosPalestranteId },
                        principalTable: "PalestrantesEventos",
                        principalColumns: new[] { "EventoId", "PalestranteId" },
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PalestrantePalestrantesEvento",
                columns: table => new
                {
                    PalestrantesId = table.Column<int>(type: "int", nullable: false),
                    PalestrantesEventosEventoId = table.Column<int>(type: "int", nullable: false),
                    PalestrantesEventosPalestranteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PalestrantePalestrantesEvento", x => new { x.PalestrantesId, x.PalestrantesEventosEventoId, x.PalestrantesEventosPalestranteId });
                    table.ForeignKey(
                        name: "FK_PalestrantePalestrantesEvento_PalestrantesEventos_PalestrantesEventosEventoId_PalestrantesEventosPalestranteId",
                        columns: x => new { x.PalestrantesEventosEventoId, x.PalestrantesEventosPalestranteId },
                        principalTable: "PalestrantesEventos",
                        principalColumns: new[] { "EventoId", "PalestranteId" },
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PalestrantePalestrantesEvento_Palestrantes_PalestrantesId",
                        column: x => x.PalestrantesId,
                        principalTable: "Palestrantes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventoPalestrantesEvento_PalestrantesEventosEventoId_PalestrantesEventosPalestranteId",
                table: "EventoPalestrantesEvento",
                columns: new[] { "PalestrantesEventosEventoId", "PalestrantesEventosPalestranteId" });

            migrationBuilder.CreateIndex(
                name: "IX_Lotes_EventoId",
                table: "Lotes",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_PalestrantePalestrantesEvento_PalestrantesEventosEventoId_PalestrantesEventosPalestranteId",
                table: "PalestrantePalestrantesEvento",
                columns: new[] { "PalestrantesEventosEventoId", "PalestrantesEventosPalestranteId" });

            migrationBuilder.CreateIndex(
                name: "IX_RedesSociais_EventoId",
                table: "RedesSociais",
                column: "EventoId");

            migrationBuilder.CreateIndex(
                name: "IX_RedesSociais_PalestranteId",
                table: "RedesSociais",
                column: "PalestranteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventoPalestrantesEvento");

            migrationBuilder.DropTable(
                name: "Lotes");

            migrationBuilder.DropTable(
                name: "PalestrantePalestrantesEvento");

            migrationBuilder.DropTable(
                name: "RedesSociais");

            migrationBuilder.DropTable(
                name: "PalestrantesEventos");

            migrationBuilder.DropTable(
                name: "Eventos");

            migrationBuilder.DropTable(
                name: "Palestrantes");
        }
    }
}
