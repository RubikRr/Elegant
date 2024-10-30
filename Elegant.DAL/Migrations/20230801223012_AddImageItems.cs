using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace OnlineShop.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddImageItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("06f628a3-2522-4fc1-a3c3-879a6a276d5c"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("89c5ba17-6407-4a8d-ba2a-8f5e55c42d3b"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("ebf5d0d3-5b7d-44a1-a41b-396a02fe6f49"));

            migrationBuilder.CreateTable(
                name: "ImageItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ImagePath = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ImageItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "ImagePath", "Name" },
                values: new object[,]
                {
                    { new Guid("816373ff-e3b1-486a-855b-9649462f0799"), 5700.75m, "Даже патрик обзавидуется такому платью", "/images/products/image2.png", "Платье" },
                    { new Guid("84f2c652-5b7d-4cbd-bb9c-dbfdfbe91be3"), 3500.75m, "Туфельки для красотульки", "/images/products/image3.png", "Туфли" },
                    { new Guid("e9869a80-68c8-42a5-8f5e-aa1f7d6714b0"), 3750.50m, "Крутой пиджак для крутой леди", "/images/products/image1.png", "Пиджак" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ImageItems_ProductId",
                table: "ImageItems",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ImageItems");

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("816373ff-e3b1-486a-855b-9649462f0799"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("84f2c652-5b7d-4cbd-bb9c-dbfdfbe91be3"));

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e9869a80-68c8-42a5-8f5e-aa1f7d6714b0"));

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Cost", "Description", "ImagePath", "Name" },
                values: new object[,]
                {
                    { new Guid("06f628a3-2522-4fc1-a3c3-879a6a276d5c"), 3500.75m, "Туфельки для красотульки", "/images/products/image3.png", "Туфли" },
                    { new Guid("89c5ba17-6407-4a8d-ba2a-8f5e55c42d3b"), 3750.50m, "Крутой пиджак для крутой леди", "/images/products/image1.png", "Пиджак" },
                    { new Guid("ebf5d0d3-5b7d-44a1-a41b-396a02fe6f49"), 5700.75m, "Даже патрик обзавидуется такому платью", "/images/products/image2.png", "Платье" }
                });
        }
    }
}
