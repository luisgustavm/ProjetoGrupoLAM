using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoLAM.Data.Migrations
{
    /// <inheritdoc />
    public partial class bancodedados : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Generos",
                columns: table => new
                {
                    GeneroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Generos", x => x.GeneroId);
                });

            migrationBuilder.CreateTable(
                name: "Livros",
                columns: table => new
                {
                    LivroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Autor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AnoPublicacao = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GeneroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Livros", x => x.LivroId);
                    table.ForeignKey(
                        name: "FK_Livros_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "GeneroId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LivrosDisponiveis",
                columns: table => new
                {
                    LivrosDisponiveisId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Estoque = table.Column<int>(type: "int", nullable: false),
                    LivroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GeneroId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LivrosDisponiveis", x => x.LivrosDisponiveisId);
                    table.ForeignKey(
                        name: "FK_LivrosDisponiveis_Generos_GeneroId",
                        column: x => x.GeneroId,
                        principalTable: "Generos",
                        principalColumn: "GeneroId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LivrosDisponiveis_Livros_LivroId",
                        column: x => x.LivroId,
                        principalTable: "Livros",
                        principalColumn: "LivroId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Livros_GeneroId",
                table: "Livros",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_LivrosDisponiveis_GeneroId",
                table: "LivrosDisponiveis",
                column: "GeneroId");

            migrationBuilder.CreateIndex(
                name: "IX_LivrosDisponiveis_LivroId",
                table: "LivrosDisponiveis",
                column: "LivroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LivrosDisponiveis");

            migrationBuilder.DropTable(
                name: "Livros");

            migrationBuilder.DropTable(
                name: "Generos");
        }
    }
}
