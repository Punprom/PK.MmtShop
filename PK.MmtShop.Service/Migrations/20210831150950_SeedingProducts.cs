using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PK.MmtShop.Service.Migrations
{
    public partial class SeedingProducts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Product",
                columns: new[] { "Id", "CategoryId", "Description", "LastModified", "Name", "Price", "Sku" },
                values: new object[,]
                {
                    { new Guid("1e20bd6a-4140-4cd6-b3af-b7f3501c0a8f"), 1, "Princess 10-Piece Accessory Set for Princes 3.2L Aero-fryer XL", null, "Princess Fat fryer", 26.30m, 10001 },
                    { new Guid("f8ec3ba2-50e8-4a1d-89a6-ddba4657ebb8"), 1, "Russell Hobbs Powersteam Ultra 3100 W Vertical Steam Iron 20630 - Black and Grey", null, "Russell Hobbs Power-steam Ultra 3100 W Vertical Steam Iron", 39.95m, 10002 },
                    { new Guid("5ad7407f-6d93-46c2-8f75-65d90f88b7a5"), 2, "Bosch 06008B9B72 Cordless Lawnmower EasyRotak 36-550 (36 Volt, 2 x Battery 2.0 Ah , Cutting Width: 37 cm, Lawns up to 550 m2, in Carton Packaging)", null, "Bosch 06008B9B72 Cordless Lawnmower", 237.15m, 20001 },
                    { new Guid("1724c3f2-224a-4528-8c47-4da8fc06d4ab"), 2, "Expandable Garden Hose, Upgraded 3-Layer Latex Hose Pipe, Solid Brass Connectors, Durable 3450D Weave, No-Kink Flexible Water Hose, 10 Function Spray 75FT/22.5M", null, "Expandable Garden Host", 34.69m, 20002 },
                    { new Guid("8ebe6884-cdb4-4800-8397-c0c37189be40"), 3, "CD Radio Portable CD Player Boombox with Bluetooth,FM Radio,CD-MP3/CD-R/CD-RW Compatible, USB Port, AUX Input,Headphone Output,AC/DC Operated (Gray)", null, "Boombox CD Radio Portable", 41.99m, 30001 },
                    { new Guid("77f5e8cc-7bc7-4437-af7b-32f22dd952ca"), 3, "SanDisk Extreme PRO 128GB SDXC Memory Card up to 170MB/s, UHS-1, Class 10, U3, V30", null, "Scandisk Extreme Pro 128GB", 24.99m, 0 },
                    { new Guid("2b40208b-9b4e-442f-ab30-18ffaa838498"), 4, "Outdoor casual shoe with breathability, traction and comfort, Cushioned midsole for comfortable all-day wear", null, "Columbia Men's Woodburn II Walking Shoe", 43.20m, 40001 },
                    { new Guid("5c3b5aa5-2320-4f41-a5b4-a798cf10e4a7"), 4, "Uneek Mens Classic Plain Pullover Hooded Sweatshirt Hoodie Sweater (22 Colours)", null, "Uneek Mens Classic Plain Pullover", 8.56m, 40002 },
                    { new Guid("adcdb34f-8fbc-4142-a500-3e881e5bd2b9"), 5, "LEGO 76389 Harry Potter Hogwarts Chamber of Secrets Modular Castle Toy with The Great Hall, 20th Anniversary Set with Collectible Golden Minifigure", null, "LEGO 76389 Harry Potter Hogwarts Chamber of Secrets", 108m, 50001 },
                    { new Guid("c1a028a4-ddc1-43be-a046-be93e6fd8a38"), 5, "Introducing the Xbox Series S, the smallest, sleekest Xbox console ever. Experience the speed and performance of a next-gen all-digital console at an accessible price point", null, "Xbox Series S", 249m, 50002 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("1724c3f2-224a-4528-8c47-4da8fc06d4ab"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("1e20bd6a-4140-4cd6-b3af-b7f3501c0a8f"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("2b40208b-9b4e-442f-ab30-18ffaa838498"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("5ad7407f-6d93-46c2-8f75-65d90f88b7a5"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("5c3b5aa5-2320-4f41-a5b4-a798cf10e4a7"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("77f5e8cc-7bc7-4437-af7b-32f22dd952ca"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("8ebe6884-cdb4-4800-8397-c0c37189be40"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("adcdb34f-8fbc-4142-a500-3e881e5bd2b9"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("c1a028a4-ddc1-43be-a046-be93e6fd8a38"));

            migrationBuilder.DeleteData(
                table: "Product",
                keyColumn: "Id",
                keyValue: new Guid("f8ec3ba2-50e8-4a1d-89a6-ddba4657ebb8"));
        }
    }
}
