using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PhysioHub.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovePositionFromArticle : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Article");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Article",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
