using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Bangazon.Migrations
{
    public partial class PaymentTypeExpiration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Expiration",
                table: "PaymentType",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "d3cb6c84-691a-46fd-8e22-60fc201a7f63", "AQAAAAEAACcQAAAAELaZTTtKPCn6VQgR2odGsh3nwIc5PaPfch/Ilcm/5pLDVC10mjYjtl/g2JhND2INVA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Expiration",
                table: "PaymentType");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "e429399a-1e6d-431e-a247-d7cf6573ce5c", "AQAAAAEAACcQAAAAELlPx7sihn5zS8xLTpgk0viY71Er5VnllRU1pMtHnLZXdIaFmylAvJsWb34uN3a0nA==" });
        }
    }
}
