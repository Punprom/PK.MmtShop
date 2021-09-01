﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PK.MmtShop.Service.Context;

namespace PK.MmtShop.Service.Migrations
{
    [DbContext(typeof(MmtDbContext))]
    [Migration("20210830152141_InitialSetup")]
    partial class InitialSetup
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PK.MmtShop.Service.Entities.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("varchar(20)");

                    b.HasKey("Id");

                    b.ToTable("Category");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Home"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Garden"
                        },
                        new
                        {
                            Id = 3,
                            Name = "Electronics"
                        },
                        new
                        {
                            Id = 4,
                            Name = "Fitness"
                        },
                        new
                        {
                            Id = 5,
                            Name = "Toys"
                        });
                });

            modelBuilder.Entity("PK.MmtShop.Service.Entities.CategoryRange", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("SkuRange")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("CategoryRange");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CategoryId = 1,
                            SkuRange = 10000
                        },
                        new
                        {
                            Id = 2,
                            CategoryId = 2,
                            SkuRange = 20000
                        },
                        new
                        {
                            Id = 3,
                            CategoryId = 3,
                            SkuRange = 30000
                        },
                        new
                        {
                            Id = 4,
                            CategoryId = 4,
                            SkuRange = 40000
                        },
                        new
                        {
                            Id = 5,
                            CategoryId = 5,
                            SkuRange = 50000
                        });
                });

            modelBuilder.Entity("PK.MmtShop.Service.Entities.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Created")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("GETDATE()");

                    b.Property<string>("Description")
                        .HasColumnType("varchar(1000)");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("datetime");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(100)");

                    b.Property<decimal>("Price")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("Sku")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.HasIndex("Sku")
                        .IsUnique();

                    b.ToTable("Product");
                });

            modelBuilder.Entity("PK.MmtShop.Service.Entities.CategoryRange", b =>
                {
                    b.HasOne("PK.MmtShop.Service.Entities.Category", "Category")
                        .WithMany("CategoryRanges")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("PK.MmtShop.Service.Entities.Product", b =>
                {
                    b.HasOne("PK.MmtShop.Service.Entities.Category", "ProductCategory")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductCategory");
                });

            modelBuilder.Entity("PK.MmtShop.Service.Entities.Category", b =>
                {
                    b.Navigation("CategoryRanges");

                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
