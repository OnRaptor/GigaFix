using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GigaFix.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "type_equipment",
                columns: table => new
                {
                    id_type_equipment = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_type_equipment);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "type_problem",
                columns: table => new
                {
                    id_type_problemt = table.Column<int>(type: "int", nullable: false, comment: "тип неисправностей")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_type_problemt);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "user",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "int", nullable: false),
                    login = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fullname_user = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    role = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_user);
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "application",
                columns: table => new
                {
                    id_application = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    date_addition = table.Column<DateOnly>(type: "date", nullable: true),
                    name_equipment = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_type_problem = table.Column<int>(type: "int", nullable: true),
                    comment = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    status = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name_client = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    cost = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: true),
                    date_end = table.Column<DateOnly>(type: "date", nullable: true),
                    time_work = table.Column<TimeOnly>(type: "time", nullable: true),
                    id_employee = table.Column<int>(type: "int", nullable: true),
                    work_status = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    period_execution = table.Column<DateOnly>(type: "date", nullable: true, comment: "предварительная дата завершения"),
                    id_type_equipment = table.Column<int>(type: "int", nullable: true),
                    number = table.Column<int>(type: "int", nullable: true, comment: "серийный номер"),
                    description = table.Column<string>(type: "text", nullable: true, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_application);
                    table.ForeignKey(
                        name: "Fk_id_employee",
                        column: x => x.id_employee,
                        principalTable: "user",
                        principalColumn: "id_user");
                    table.ForeignKey(
                        name: "Fk_id_type_equipment",
                        column: x => x.id_type_equipment,
                        principalTable: "type_equipment",
                        principalColumn: "id_type_equipment");
                    table.ForeignKey(
                        name: "Fk_id_type_problem",
                        column: x => x.id_type_problem,
                        principalTable: "type_problem",
                        principalColumn: "id_type_problemt");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateTable(
                name: "notifications",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    message = table.Column<string>(type: "varchar(120)", maxLength: 120, nullable: false, collation: "utf8mb4_0900_ai_ci")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    applicationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "applicationId_FK",
                        column: x => x.applicationId,
                        principalTable: "application",
                        principalColumn: "id_application");
                    table.ForeignKey(
                        name: "userId_FK",
                        column: x => x.userId,
                        principalTable: "user",
                        principalColumn: "id_user");
                })
                .Annotation("MySql:CharSet", "utf8mb4")
                .Annotation("Relational:Collation", "utf8mb4_0900_ai_ci");

            migrationBuilder.CreateIndex(
                name: "Fk_id_type_equipment_idx",
                table: "application",
                column: "id_type_equipment");

            migrationBuilder.CreateIndex(
                name: "Fk_id_type_problem_idx",
                table: "application",
                column: "id_type_problem");

            migrationBuilder.CreateIndex(
                name: "Fk_IdEmployee_idx",
                table: "application",
                column: "id_employee");

            migrationBuilder.CreateIndex(
                name: "Fk_IdStageWork_idx",
                table: "application",
                column: "work_status");

            migrationBuilder.CreateIndex(
                name: "applicationId_FK_idx",
                table: "notifications",
                column: "applicationId");

            migrationBuilder.CreateIndex(
                name: "userId_FK_idx",
                table: "notifications",
                column: "userId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notifications");

            migrationBuilder.DropTable(
                name: "application");

            migrationBuilder.DropTable(
                name: "user");

            migrationBuilder.DropTable(
                name: "type_equipment");

            migrationBuilder.DropTable(
                name: "type_problem");
        }
    }
}
