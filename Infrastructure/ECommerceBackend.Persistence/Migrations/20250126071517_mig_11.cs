using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ECommerceBackend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56dcf1be-e86c-4577-9352-71b72b62492c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f7cb4a5a-488d-468a-8bc3-6bd6e51a2642");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "c1aad5c8-8bd6-449b-9e0e-2f87b879e4e6", null, "Customer", "CUSTOMER" },
                    { "f5223c96-2096-40f6-9eda-63d7406b8888", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c1aad5c8-8bd6-449b-9e0e-2f87b879e4e6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f5223c96-2096-40f6-9eda-63d7406b8888");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "56dcf1be-e86c-4577-9352-71b72b62492c", null, "Admin", "ADMIN" },
                    { "f7cb4a5a-488d-468a-8bc3-6bd6e51a2642", null, "Customer", "CUSTOMER" }
                });
        }
    }
}
