using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SecurityWEB.Migrations
{
    /// <inheritdoc />
    public partial class AddAluno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Alunos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    SophiaId = table.Column<int>(type: "int", nullable: false),
                    SophiaRm = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PathFoto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UrlFoto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Alunos", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Alunos",
                columns: new[] { "Id", "DataCadastro", "Nome", "PathFoto", "SophiaId", "SophiaRm", "UrlFoto" },
                values: new object[] { 1, new DateTime(2024, 9, 18, 16, 31, 4, 715, DateTimeKind.Local).AddTicks(7186), "Aluno 1", "\\\\10.100.15.232\\teste.jpg", 1111, 1112, "http://10.100.15.232:3000" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Alunos");
        }
    }
}
