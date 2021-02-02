using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Testing2.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Telemetry",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TelemetryNumber = table.Column<string>(type: "text", nullable: true),
                    timeOfRequest = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    webRequestSent = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    webRequestRecieved = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StoreRequestStarted = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    StoreRequestFinished = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    successful = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telemetry", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Telemetry");
        }
    }
}
