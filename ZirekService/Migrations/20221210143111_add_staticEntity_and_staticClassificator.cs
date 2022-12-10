using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ZirekService.Migrations
{
    /// <inheritdoc />
    public partial class addstaticEntityandstaticClassificator : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatisticClassificator",
                columns: table => new
                {
                    StatisticClassificatorId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticClassificator", x => x.StatisticClassificatorId);
                });

            migrationBuilder.CreateTable(
                name: "StatisticEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<float>(type: "real", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    StatisticClassificatorId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatisticEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatisticEntity_StatisticClassificator_StatisticClassificat~",
                        column: x => x.StatisticClassificatorId,
                        principalTable: "StatisticClassificator",
                        principalColumn: "StatisticClassificatorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatisticEntity_StatisticClassificatorId",
                table: "StatisticEntity",
                column: "StatisticClassificatorId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StatisticEntity");

            migrationBuilder.DropTable(
                name: "StatisticClassificator");
        }
    }
}
