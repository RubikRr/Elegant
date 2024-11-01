using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Elegant.DAL.Migrations
{
    /// <inheritdoc />
    public partial class DeleteInitProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageItems_Products_ProductId",
                table: "ImageItems");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                values: new object[,]
                {
                    { new Guid("816373ff-e3b1-486a-855b-9649462f0799"), 5700.75m, "Даже патрик обзавидуется такому платью", "/images/products/image2.png", "Платье" },
                    { new Guid("84f2c652-5b7d-4cbd-bb9c-dbfdfbe91be3"), 3500.75m, "Туфельки для красотульки", "/images/products/image3.png", "Туфли" },
                    { new Guid("e9869a80-68c8-42a5-8f5e-aa1f7d6714b0"), 3750.50m, "Крутой пиджак для крутой леди", "/images/products/image1.png", "Пиджак" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_ImageItems_Products_ProductId",
                table: "ImageItems",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
