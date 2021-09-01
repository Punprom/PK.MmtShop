using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PK.MmtShop.Service.Entities;

namespace PK.MmtShop.Service.Extensions
{
    public static class ProductModelBuilderExtender
    {
        public static void GenerateInitialProducts(this ModelBuilder builder)
        {
            builder.Entity<Product>().HasData(new Product[]
            {
                new Product() {
                    Id = new Guid("1e20bd6a-4140-4cd6-b3af-b7f3501c0a8f"), CategoryId = 1, Name = "Princess Fat fryer", Description = "Princess 10-Piece Accessory Set for Princes 3.2L Aero-fryer XL",
                    Sku = 10001, Created = DateTime.Now, Price = 26.30M
                },
                new Product() {
                    Id = new Guid("f8ec3ba2-50e8-4a1d-89a6-ddba4657ebb8"), CategoryId = 1, Name = "Russell Hobbs Power-steam Ultra 3100 W Vertical Steam Iron", Description = "Russell Hobbs Powersteam Ultra 3100 W Vertical Steam Iron 20630 - Black and Grey",
                    Sku = 10002, Created = DateTime.Now, Price = 39.95M
                },
                new Product()
                {
                    Id = new Guid("5ad7407f-6d93-46c2-8f75-65d90f88b7a5"), Name = "Bosch 06008B9B72 Cordless Lawnmower", Sku = 20001, Created = DateTime.Now, CategoryId = 2,
                    Description = "Bosch 06008B9B72 Cordless Lawnmower EasyRotak 36-550 (36 Volt, 2 x Battery 2.0 Ah , Cutting Width: 37 cm, Lawns up to 550 m2, in Carton Packaging)", Price = 237.15M
                },
                new Product()
                {
                    Id = new Guid("1724c3f2-224a-4528-8c47-4da8fc06d4ab"), Name = "Expandable Garden Host", Sku = 20002, Created = DateTime.Now, CategoryId = 2,
                    Description = @"Expandable Garden Hose, Upgraded 3-Layer Latex Hose Pipe, Solid Brass Connectors, Durable 3450D Weave, No-Kink Flexible Water Hose, 10 Function Spray 75FT/22.5M", Price = 34.69M
                },
                new Product()
                {
                    Id = new Guid("8ebe6884-cdb4-4800-8397-c0c37189be40"), Name = "Boombox CD Radio Portable", Sku = 30001, Created = DateTime.Now, CategoryId = 3,
                    Description = "CD Radio Portable CD Player Boombox with Bluetooth,FM Radio,CD-MP3/CD-R/CD-RW Compatible, USB Port, AUX Input,Headphone Output,AC/DC Operated (Gray)", Price = 41.99M
                },
                new Product()
                {
                    Id = new Guid("77f5e8cc-7bc7-4437-af7b-32f22dd952ca"), Name = "Scandisk Extreme Pro 128GB", Sku = 30002, Created = DateTime.Now, CategoryId = 3,
                    Description = "SanDisk Extreme PRO 128GB SDXC Memory Card up to 170MB/s, UHS-1, Class 10, U3, V30", Price = 24.99M
                },
                new Product()
                {
                    Id = new Guid("2b40208b-9b4e-442f-ab30-18ffaa838498"), Name = "Columbia Men's Woodburn II Walking Shoe", Sku = 40001, Created = DateTime.Now, CategoryId = 4,
                    Description = "Outdoor casual shoe with breathability, traction and comfort, Cushioned midsole for comfortable all-day wear", Price = 43.20M
                },
                new Product()
                {
                    Id = new Guid("5c3b5aa5-2320-4f41-a5b4-a798cf10e4a7"), Name = "Uneek Mens Classic Plain Pullover", Sku = 40002, Created = DateTime.Now, CategoryId = 4,
                    Description = "Uneek Mens Classic Plain Pullover Hooded Sweatshirt Hoodie Sweater (22 Colours)", Price = 8.56M
                },
                new Product()
                {
                    Id = new Guid("adcdb34f-8fbc-4142-a500-3e881e5bd2b9"), Name = "LEGO 76389 Harry Potter Hogwarts Chamber of Secrets", Sku = 50001, Created = DateTime.Now, CategoryId = 5,
                    Description = "LEGO 76389 Harry Potter Hogwarts Chamber of Secrets Modular Castle Toy with The Great Hall, 20th Anniversary Set with Collectible Golden Minifigure", Price = 108M
                },
                new Product()
                {
                    Id = new Guid("c1a028a4-ddc1-43be-a046-be93e6fd8a38"), Name = "Xbox Series S", Sku = 50002, Created = DateTime.Now, CategoryId = 5,
                    Description = "Introducing the Xbox Series S, the smallest, sleekest Xbox console ever. Experience the speed and performance of a next-gen all-digital console at an accessible price point", Price = 249M
                }




            });
        }
    }
}
