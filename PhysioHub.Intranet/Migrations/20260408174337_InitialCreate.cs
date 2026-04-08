using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhysioHub.Intranet.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkTitle = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    PESEL = table.Column<string>(type: "nvarchar(11)", maxLength: 11, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RehabRoom",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RehabRoom", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialization",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialization", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stay",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TreatmentCatalog",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TreatmentCatalog", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Website",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LinkTitle = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Position = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Website", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Therapist",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    SpecializationId = table.Column<int>(type: "int", nullable: false),
                    PhotoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Therapist", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Therapist_Specialization_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specialization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "StayParticipation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    StayId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StayParticipation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StayParticipation_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StayParticipation_Stay_StayId",
                        column: x => x.StayId,
                        principalTable: "Stay",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentSchedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    TreatmentId = table.Column<int>(type: "int", nullable: false),
                    TherapistId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    StayParticipationId = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentSchedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentSchedule_Patient_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentSchedule_RehabRoom_RoomId",
                        column: x => x.RoomId,
                        principalTable: "RehabRoom",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentSchedule_StayParticipation_StayParticipationId",
                        column: x => x.StayParticipationId,
                        principalTable: "StayParticipation",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AppointmentSchedule_Therapist_TherapistId",
                        column: x => x.TherapistId,
                        principalTable: "Therapist",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentSchedule_TreatmentCatalog_TreatmentId",
                        column: x => x.TreatmentId,
                        principalTable: "TreatmentCatalog",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentSchedule_PatientId",
                table: "AppointmentSchedule",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentSchedule_RoomId",
                table: "AppointmentSchedule",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentSchedule_StayParticipationId",
                table: "AppointmentSchedule",
                column: "StayParticipationId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentSchedule_TherapistId",
                table: "AppointmentSchedule",
                column: "TherapistId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentSchedule_TreatmentId",
                table: "AppointmentSchedule",
                column: "TreatmentId");

            migrationBuilder.CreateIndex(
                name: "IX_StayParticipation_PatientId",
                table: "StayParticipation",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_StayParticipation_StayId",
                table: "StayParticipation",
                column: "StayId");

            migrationBuilder.CreateIndex(
                name: "IX_Therapist_SpecializationId",
                table: "Therapist",
                column: "SpecializationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentSchedule");

            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "Website");

            migrationBuilder.DropTable(
                name: "RehabRoom");

            migrationBuilder.DropTable(
                name: "StayParticipation");

            migrationBuilder.DropTable(
                name: "Therapist");

            migrationBuilder.DropTable(
                name: "TreatmentCatalog");

            migrationBuilder.DropTable(
                name: "Patient");

            migrationBuilder.DropTable(
                name: "Stay");

            migrationBuilder.DropTable(
                name: "Specialization");
        }
    }
}
