using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelProjekat.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Korisnik",
                columns: table => new
                {
                    IDKorisnika = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojPasosa = table.Column<int>(type: "int", nullable: false),
                    Ime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BrTelefona = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Korisnik", x => x.IDKorisnika);
                });

            migrationBuilder.CreateTable(
                name: "Zaposleni",
                columns: table => new
                {
                    IDRadnika = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeZaposleni = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PrezimeZaposleni = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BrojLicence = table.Column<int>(type: "int", nullable: false),
                    BrTelefona = table.Column<int>(type: "int", nullable: false),
                    Plata = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zaposleni", x => x.IDRadnika);
                });

            migrationBuilder.CreateTable(
                name: "Zgrada",
                columns: table => new
                {
                    IDZgrade = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeZgrade = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Grad = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zgrada", x => x.IDZgrade);
                });

            migrationBuilder.CreateTable(
                name: "Prijava",
                columns: table => new
                {
                    IDPrijave = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BrojSobe = table.Column<int>(type: "int", nullable: false),
                    KorisnikIDKorisnika = table.Column<int>(type: "int", nullable: true),
                    ZaposleniIDRadnika = table.Column<int>(type: "int", nullable: true),
                    ZgradaIDZgrade = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prijava", x => x.IDPrijave);
                    table.ForeignKey(
                        name: "FK_Prijava_Korisnik_KorisnikIDKorisnika",
                        column: x => x.KorisnikIDKorisnika,
                        principalTable: "Korisnik",
                        principalColumn: "IDKorisnika",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prijava_Zaposleni_ZaposleniIDRadnika",
                        column: x => x.ZaposleniIDRadnika,
                        principalTable: "Zaposleni",
                        principalColumn: "IDRadnika",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Prijava_Zgrada_ZgradaIDZgrade",
                        column: x => x.ZgradaIDZgrade,
                        principalTable: "Zgrada",
                        principalColumn: "IDZgrade",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Prijava_KorisnikIDKorisnika",
                table: "Prijava",
                column: "KorisnikIDKorisnika");

            migrationBuilder.CreateIndex(
                name: "IX_Prijava_ZaposleniIDRadnika",
                table: "Prijava",
                column: "ZaposleniIDRadnika");

            migrationBuilder.CreateIndex(
                name: "IX_Prijava_ZgradaIDZgrade",
                table: "Prijava",
                column: "ZgradaIDZgrade");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Prijava");

            migrationBuilder.DropTable(
                name: "Korisnik");

            migrationBuilder.DropTable(
                name: "Zaposleni");

            migrationBuilder.DropTable(
                name: "Zgrada");
        }
    }
}
