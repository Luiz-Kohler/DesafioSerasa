using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "COMPANY",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "VARCHAR(50)", nullable: false),
                    RELIABILITY = table.Column<int>(type: "INT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_COMPANY", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DEBIT",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEBIT", x => x.ID);
                    table.ForeignKey(
                        name: "FK_DEBIT_COMPANY_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "COMPANY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "INVOICE",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_INVOICE", x => x.ID);
                    table.ForeignKey(
                        name: "FK_INVOICE_COMPANY_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "COMPANY",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DEBIT_CompanyId",
                table: "DEBIT",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_INVOICE_CompanyId",
                table: "INVOICE",
                column: "CompanyId");

            migrationBuilder.Sql(@"INSERT INTO COMPANY (NAME, RELIABILITY) VALUES ('SERASA', 50)");
            migrationBuilder.Sql(@"INSERT INTO COMPANY (NAME, RELIABILITY) VALUES ('GOL', 50)");
            migrationBuilder.Sql(@"INSERT INTO COMPANY (NAME, RELIABILITY) VALUES ('TAM', 50)");
            migrationBuilder.Sql(@"INSERT INTO COMPANY (NAME, RELIABILITY) VALUES ('AIR-FRANCE', 50)");
            migrationBuilder.Sql(@"INSERT INTO COMPANY (NAME, RELIABILITY) VALUES ('AVIANCA', 50)");
            migrationBuilder.Sql(@"INSERT INTO COMPANY (NAME, RELIABILITY) VALUES ('AMERICAN AIRLINES', 50)");
            migrationBuilder.Sql(@"INSERT INTO COMPANY (NAME, RELIABILITY) VALUES ('DELTA AIRLINES', 50)");
            migrationBuilder.Sql(@"INSERT INTO COMPANY (NAME, RELIABILITY) VALUES ('COPA AIRLINES', 50)");
            migrationBuilder.Sql(@"INSERT INTO COMPANY (NAME, RELIABILITY) VALUES ('LATAM AIRLINES', 50)");
            migrationBuilder.Sql(@"INSERT INTO COMPANY (NAME, RELIABILITY) VALUES ('AZUL', 50)");

            migrationBuilder.Sql(@"INSERT INTO COMPANY (NAME, RELIABILITY) VALUES ('AIR EUROPA', 50)");
            migrationBuilder.Sql(@"INSERT INTO COMPANY (NAME, RELIABILITY) VALUES ('OCEAN AIR', 50)");
            migrationBuilder.Sql(@"INSERT INTO COMPANY (NAME, RELIABILITY) VALUES ('EMIRATES FLY', 50)");
            migrationBuilder.Sql(@"INSERT INTO COMPANY (NAME, RELIABILITY) VALUES ('AERO-MEXIO', 50)");
            migrationBuilder.Sql(@"INSERT INTO COMPANY (NAME, RELIABILITY) VALUES ('AEROSUR', 50)");
            migrationBuilder.Sql(@"INSERT INTO COMPANY (NAME, RELIABILITY) VALUES ('AIR-CANADA', 50)");
            migrationBuilder.Sql(@"INSERT INTO COMPANY (NAME, RELIABILITY) VALUES ('AIR-MINAS', 50)");
            migrationBuilder.Sql(@"INSERT INTO COMPANY (NAME, RELIABILITY) VALUES ('TACA AIRLINES', 50)");
            migrationBuilder.Sql(@"INSERT INTO COMPANY (NAME, RELIABILITY) VALUES ('AIR CHINA', 50)");
            migrationBuilder.Sql(@"INSERT INTO COMPANY (NAME, RELIABILITY) VALUES ('U N I T E D', 50)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DEBIT");

            migrationBuilder.DropTable(
                name: "INVOICE");

            migrationBuilder.DropTable(
                name: "COMPANY");
        }
    }
}
