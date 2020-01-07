using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class Seed01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedAt", "DeletedAt", "Description", "IsDeleted", "ModifiedAt", "Name", "Quantity" },
                values: new object[] { 1, new DateTime(2019, 11, 18, 14, 53, 20, 372, DateTimeKind.Local).AddTicks(7113), null, "Iphone 7 128GB", false, null, "Iphone", 300 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
