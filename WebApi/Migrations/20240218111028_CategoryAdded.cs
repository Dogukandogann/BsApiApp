using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.Migrations
{
    public partial class CategoryAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "330fdf41-9680-4c4c-8412-d57633ee4ec6");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8b1825ff-43e3-4e0a-a0f2-8941fbdbfdb8");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c61ff2ba-5b76-487d-a89e-f330f1ce740c");

            migrationBuilder.RenameColumn(
                name: "RefreshTokenExpiryTie",
                table: "AspNetUsers",
                newName: "RefreshTokenExpiryTime");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "27f64d9f-d219-496d-be1e-17506a99c556", "f37a6069-1c9a-4154-918f-4fa13ecc3501", "Admin", "ADMIN" },
                    { "86d11dfd-2fbc-41e4-a53c-b16113278aa1", "3a77e587-2c39-4d48-ace5-5a282dffecd7", "Editor", "EDITOR" },
                    { "fce45e3f-929d-48cd-b50f-f5300420d29d", "7b3616ee-98d3-4273-9c38-d4462b0970ae", "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "CategoryName" },
                values: new object[,]
                {
                    { 1, "Computer Science" },
                    { 2, "Network" },
                    { 3, "Db Management Systems" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "27f64d9f-d219-496d-be1e-17506a99c556");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "86d11dfd-2fbc-41e4-a53c-b16113278aa1");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fce45e3f-929d-48cd-b50f-f5300420d29d");

            migrationBuilder.RenameColumn(
                name: "RefreshTokenExpiryTime",
                table: "AspNetUsers",
                newName: "RefreshTokenExpiryTie");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "330fdf41-9680-4c4c-8412-d57633ee4ec6", "6937b0c1-4b80-4d91-ab69-dba7ebd0c848", "Admin", "ADMIN" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "8b1825ff-43e3-4e0a-a0f2-8941fbdbfdb8", "192b471e-9ec4-4864-ba91-ebe54032c696", "Editor", "EDITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "c61ff2ba-5b76-487d-a89e-f330f1ce740c", "6f067ccd-6cfb-4af0-8601-b7444775d645", "User", "USER" });
        }
    }
}
