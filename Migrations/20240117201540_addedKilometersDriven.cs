using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VehicleManager.Migrations
{
    public partial class addedKilometersDriven : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "KilometersDriven",
                table: "Vehicle",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "KilometersDriven",
                table: "Vehicle");
        }
    }
}
