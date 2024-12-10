using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Case2BookingCoworking.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class smallChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type_Type",
                table: "Audiences");

            migrationBuilder.AddColumn<int>(
                name: "Type_Capacity",
                table: "Audiences",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type_Capacity",
                table: "Audiences");

            migrationBuilder.AddColumn<string>(
                name: "Type_Type",
                table: "Audiences",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
