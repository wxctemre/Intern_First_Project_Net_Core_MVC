using Microsoft.EntityFrameworkCore.Migrations;

namespace OrsaAkademi.demo.WebApi.Migrations
{
    public partial class MedyaKutupahanesiVePersonelMedyalar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Telefon",
                table: "personellers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "MedyaKutuphanesi",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MedyaAdi = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MedyaUrl = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedyaKutuphanesi", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonelMedyalar",
                columns: table => new
                {
                    TabloId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonelId = table.Column<int>(type: "int", nullable: false),
                    MedyaId = table.Column<int>(type: "int", nullable: false),
                    AktifMi = table.Column<int>(type: "int", nullable: false),
                    SilindiMi = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonelMedyalar", x => x.TabloId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MedyaKutuphanesi");

            migrationBuilder.DropTable(
                name: "PersonelMedyalar");

            migrationBuilder.AlterColumn<string>(
                name: "Telefon",
                table: "personellers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
