using Microsoft.EntityFrameworkCore.Migrations;

namespace OrsaAkademi.demo.WebApi.Migrations
{
    public partial class tekrarlialan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "okullar",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Okulid = table.Column<int>(type: "int", nullable: false),
                    Mezunolduguyil = table.Column<short>(type: "smallint", nullable: false),
                    personelid = table.Column<short>(type: "smallint", nullable: false),
                    aktifMi = table.Column<int>(type: "int", nullable: false),
                    silindiMi = table.Column<int>(type: "int", nullable: false),
                    sirano = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_okullar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "paramokullar",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Okuladi = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_paramokullar", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonelEgitimId",
                columns: table => new
                {
                    TabloId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelTabloId = table.Column<int>(type: "int", nullable: false),
                    MedyaID = table.Column<int>(type: "int", nullable: false),
                    aktifMi = table.Column<int>(type: "int", nullable: false),
                    silindiMi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelEgitimId", x => x.TabloId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "okullar");

            migrationBuilder.DropTable(
                name: "paramokullar");

            migrationBuilder.DropTable(
                name: "PersonelEgitimId");
        }
    }
}
