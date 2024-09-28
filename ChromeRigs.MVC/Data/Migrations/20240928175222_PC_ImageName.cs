using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChromeRigs.MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class PC_ImageName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "PCs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "PCs");
        }
    }
}
