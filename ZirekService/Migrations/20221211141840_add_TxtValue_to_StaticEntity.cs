using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ZirekService.Migrations
{
    /// <inheritdoc />
    public partial class addTxtValuetoStaticEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TxtValue",
                table: "StatisticEntity",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TxtValue",
                table: "StatisticEntity");
        }
    }
}
