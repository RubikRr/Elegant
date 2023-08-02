using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddListImageItemsToInicProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageItems_Products_ProductId",
                table: "ImageItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ImageItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "ImagePath", "Name" },
                values: new object[] { new Guid("8001a098-bf36-4fb7-9b46-c3c21102e288"), 3750.50m, "Крутой пиджак для крутой леди", "/images/products/image1.png", "Пиджак" });

            migrationBuilder.InsertData(
                table: "ImageItems",
                columns: new[] { "Id", "ImagePath", "ProductId" },
                values: new object[,]
                {
                    { new Guid("ade8cac6-89f9-48e7-9666-cc3109ab9a06"), "/images/products/image2.png", new Guid("8001a098-bf36-4fb7-9b46-c3c21102e288") },
                    { new Guid("b422ac72-39b5-41c9-851e-95d6a18ad191"), "/images/products/image1.png", new Guid("8001a098-bf36-4fb7-9b46-c3c21102e288") }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ImageItems_Products_ProductId",
                table: "ImageItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageItems_Products_ProductId",
                table: "ImageItems");

            migrationBuilder.DeleteData(
                table: "ImageItems",
                keyColumn: "Id",
                keyValue: new Guid("ade8cac6-89f9-48e7-9666-cc3109ab9a06"));

            migrationBuilder.DeleteData(
                table: "ImageItems",
                keyColumn: "Id",
                keyValue: new Guid("b422ac72-39b5-41c9-851e-95d6a18ad191"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("8001a098-bf36-4fb7-9b46-c3c21102e288"));

            migrationBuilder.AlterColumn<Guid>(
                name: "ProductId",
                table: "ImageItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageItems_Products_ProductId",
                table: "ImageItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id");
        }
    }
}
