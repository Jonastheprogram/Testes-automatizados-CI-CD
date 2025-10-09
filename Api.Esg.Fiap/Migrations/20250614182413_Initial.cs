using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Esg.Fiap.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coletas",
                columns: table => new
                {
                    ColetaId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    PontoColeta = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    CapacidadeMax = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    QtdAtual = table.Column<double>(type: "BINARY_DOUBLE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coletas", x => x.ColetaId);
                });

            migrationBuilder.CreateTable(
                name: "Residuos",
                columns: table => new
                {
                    ResiduoId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NomeResiduo = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    TipoResiduo = table.Column<string>(type: "NVARCHAR2(50)", maxLength: 50, nullable: false),
                    Peso = table.Column<double>(type: "BINARY_DOUBLE", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residuos", x => x.ResiduoId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Coletas");

            migrationBuilder.DropTable(
                name: "Residuos");
        }
    }
}
