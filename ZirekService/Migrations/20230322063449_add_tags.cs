using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ZirekService.Migrations
{
    /// <inheritdoc />
    public partial class addtags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnWordsTypes");

            migrationBuilder.DropTable(
                name: "keyWords");

            migrationBuilder.RenameColumn(
                name: "TypeId",
                table: "EnWords",
                newName: "TagId");

            migrationBuilder.CreateTable(
                name: "TagEntity",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    WordsNodeEntityId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TagEntity_WordsNodes_WordsNodeEntityId",
                        column: x => x.WordsNodeEntityId,
                        principalTable: "WordsNodes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TagEntity_WordsNodeEntityId",
                table: "TagEntity",
                column: "WordsNodeEntityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TagEntity");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "EnWords",
                newName: "TypeId");

            migrationBuilder.CreateTable(
                name: "EnWordsTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnWordsTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "keyWords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    WordsNodeEntityId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_keyWords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_keyWords_WordsNodes_WordsNodeEntityId",
                        column: x => x.WordsNodeEntityId,
                        principalTable: "WordsNodes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_keyWords_WordsNodeEntityId",
                table: "keyWords",
                column: "WordsNodeEntityId");
        }
    }
}
