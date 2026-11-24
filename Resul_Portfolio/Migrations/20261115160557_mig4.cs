using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Resul_Portfolio.Migrations
{
    /// <inheritdoc />
    public partial class mig4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GitHupUrl",
                table: "Projects",
                newName: "GithupUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "GithupUrl",
                table: "Projects",
                newName: "GitHupUrl");
        }
    }
}
