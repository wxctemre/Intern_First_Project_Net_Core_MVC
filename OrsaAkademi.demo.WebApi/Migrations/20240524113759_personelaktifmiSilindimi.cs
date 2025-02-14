using Microsoft.EntityFrameworkCore.Migrations;

namespace OrsaAkademi.demo.WebApi.Migrations
{
    public partial class personelaktifmiSilindimi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "aktifMi",
                table: "personellers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "silindiMi",
                table: "personellers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "aktifMi",
                table: "personellers");

            migrationBuilder.DropColumn(
                name: "silindiMi",
                table: "personellers");
        }
    }
}
