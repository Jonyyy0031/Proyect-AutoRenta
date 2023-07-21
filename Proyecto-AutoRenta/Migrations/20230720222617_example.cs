using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

namespace Proyecto_AutoRenta.Migrations
{
    public partial class example : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    PkRol = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.PkRol);
                });

            migrationBuilder.CreateTable(
                name: "Vehiculo",
                columns: table => new
                {
                    PkVehiculo = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Modelo = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Tarifa = table.Column<double>(type: "double", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculo", x => x.PkVehiculo);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    PkUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    FkRol = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.PkUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_Roles_FkRol",
                        column: x => x.FkRol,
                        principalTable: "Roles",
                        principalColumn: "PkRol",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Reservas",
                columns: table => new
                {
                    PkReserva = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Correo = table.Column<string>(type: "text", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: false),
                    FkVehiculos = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservas", x => x.PkReserva);
                    table.ForeignKey(
                        name: "FK_Reservas_Vehiculo_FkVehiculos",
                        column: x => x.FkVehiculos,
                        principalTable: "Vehiculo",
                        principalColumn: "PkVehiculo",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "PkRol", "Nombre" },
                values: new object[,]
                {
                    { 1, "Administrador" },
                    { 2, "Usuario" },
                    { 3, "Editor" },
                    { 4, "Invitado" },
                    { 5, "Supervisor" }
                });

            migrationBuilder.InsertData(
                table: "Vehiculo",
                columns: new[] { "PkVehiculo", "Modelo", "Tarifa", "Tipo" },
                values: new object[,]
                {
                    { 1, "Modelo1", 1000.0, "Tipo1" },
                    { 2, "Modelo2", 1500.0, "Tipo2" },
                    { 3, "Modelo3", 1200.0, "Tipo1" },
                    { 4, "Modelo4", 2000.0, "Tipo3" },
                    { 5, "Modelo5", 1800.0, "Tipo2" }
                });

            migrationBuilder.InsertData(
                table: "Reservas",
                columns: new[] { "PkReserva", "Correo", "FkVehiculos", "Nombre", "Telefono" },
                values: new object[,]
                {
                    { 1, "correo1@example.com", 1, "Usuario1", "123456789" },
                    { 3, "correo3@example.com", 2, "Usuario3", "555555555" },
                    { 2, "correo2@example.com", 3, "Usuario2", "987654321" },
                    { 5, "correo5@example.com", 4, "Usuario5", "444444444" },
                    { 4, "correo4@example.com", 5, "Usuario4", "999999999" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reservas_FkVehiculos",
                table: "Reservas",
                column: "FkVehiculos");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_FkRol",
                table: "Usuarios",
                column: "FkRol");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservas");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Vehiculo");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
