using Microsoft.EntityFrameworkCore.Migrations;

namespace Bangazon.Migrations
{
    public partial class LocalDelivery : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LocalDelivery",
                table: "Product",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "fc849e1a-d192-4314-a36e-65e8eedcbd04", "AQAAAAEAACcQAAAAEDNIN7CnortAh0mgb6A4iDBWz7e2CnelT12kNueNAPJuKbLyU6EjFHmBXovLbF1QNw==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalDelivery",
                table: "Product");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d3cb6c84-691a-46fd-8e22-60fc201a7f63", "AQAAAAEAACcQAAAAELaZTTtKPCn6VQgR2odGsh3nwIc5PaPfch/Ilcm/5pLDVC10mjYjtl/g2JhND2INVA==" });
        }
    }
}
