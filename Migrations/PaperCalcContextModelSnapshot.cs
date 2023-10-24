﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PaperCalc.Data;

#nullable disable

namespace PaperCalc.Migrations
{
    [DbContext(typeof(PaperCalcContext))]
    partial class PaperCalcContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PaperCalc.Models.BindingCoilsStock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BindingCoilType")
                        .HasColumnType("int");

                    b.Property<double>("CoilSize")
                        .HasColumnType("float");

                    b.Property<double>("CoilsPerPack")
                        .HasColumnType("float");

                    b.Property<double>("PricePerPack")
                        .HasColumnType("float");

                    b.Property<string>("RingsRatio")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SheetsHeld")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("BindingCoilsStock");
                });

            modelBuilder.Entity("PaperCalc.Models.BindingCoverStock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("PricePerPack")
                        .HasColumnType("float");

                    b.Property<double>("SheetsPerPack")
                        .HasColumnType("float");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StockBrand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StockType")
                        .HasColumnType("int");

                    b.Property<string>("Supplier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("BindingCoverStock");
                });

            modelBuilder.Entity("PaperCalc.Models.BookletFormInputs", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Colour")
                        .HasColumnType("bit");

                    b.Property<Guid>("CoverStockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("CustomFlatSize")
                        .HasColumnType("bit");

                    b.Property<double>("DesignCost")
                        .HasColumnType("float");

                    b.Property<double>("FileHandlingCost")
                        .HasColumnType("float");

                    b.Property<Guid?>("FlatSizeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<int>("HolePunches")
                        .HasColumnType("int");

                    b.Property<Guid>("InnerStockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Kinds")
                        .HasColumnType("float");

                    b.Property<double>("Pages")
                        .HasColumnType("float");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<double>("SetupCost")
                        .HasColumnType("float");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("BookletFormInputs");
                });

            modelBuilder.Entity("PaperCalc.Models.DocumentFormInputs", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Binding")
                        .HasColumnType("int");

                    b.Property<bool>("Colour")
                        .HasColumnType("bit");

                    b.Property<double>("DesignCost")
                        .HasColumnType("float");

                    b.Property<bool>("DoubleSided")
                        .HasColumnType("bit");

                    b.Property<double>("FileHandlingCost")
                        .HasColumnType("float");

                    b.Property<Guid>("FlatSizeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Folds")
                        .HasColumnType("int");

                    b.Property<double?>("Height")
                        .HasColumnType("float");

                    b.Property<int>("HolePunches")
                        .HasColumnType("int");

                    b.Property<double>("Kinds")
                        .HasColumnType("float");

                    b.Property<bool>("Lamination")
                        .HasColumnType("bit");

                    b.Property<double>("Pages")
                        .HasColumnType("float");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<double>("SetupCost")
                        .HasColumnType("float");

                    b.Property<int>("Staples")
                        .HasColumnType("int");

                    b.Property<Guid>("StockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double?>("Width")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("DocumentFormInputs");
                });

            modelBuilder.Entity("PaperCalc.Models.DocumentsStock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CoatType")
                        .HasColumnType("int");

                    b.Property<double>("HeightOf100Sheets")
                        .HasColumnType("float");

                    b.Property<double>("PricePer1000")
                        .HasColumnType("float");

                    b.Property<double>("SheetsPerPack")
                        .HasColumnType("float");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StockBrand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StockType")
                        .HasColumnType("int");

                    b.Property<string>("Supplier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("DocumentsStock");
                });

            modelBuilder.Entity("PaperCalc.Models.FlatSize", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("ForCalculation")
                        .HasColumnType("int");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<double>("MultiplierMax")
                        .HasColumnType("float");

                    b.Property<double>("MultiplierMin")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("QuantityMax")
                        .HasColumnType("int");

                    b.Property<int>("QuantityMin")
                        .HasColumnType("int");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("FlatSizes");
                });

            modelBuilder.Entity("PaperCalc.Models.InputsForJobs", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CalculationType")
                        .HasColumnType("int");

                    b.Property<Guid>("InputId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("JobId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.ToTable("InputsForJobs");
                });

            modelBuilder.Entity("PaperCalc.Models.Job", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Buissnessname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClientName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Job");
                });

            modelBuilder.Entity("PaperCalc.Models.Sra3AndBookletsStock", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("CoatType")
                        .HasColumnType("int");

                    b.Property<double>("HeightOf100Sheets")
                        .HasColumnType("float");

                    b.Property<double>("PricePer1000")
                        .HasColumnType("float");

                    b.Property<double>("SheetsPerPack")
                        .HasColumnType("float");

                    b.Property<string>("Size")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StockBrand")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StockType")
                        .HasColumnType("int");

                    b.Property<string>("Supplier")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Sra3AndBookletsStock");
                });

            modelBuilder.Entity("PaperCalc.Models.Sra3FormInput", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Colour")
                        .HasColumnType("bit");

                    b.Property<int>("Creases")
                        .HasColumnType("int");

                    b.Property<bool>("CustomFlatSize")
                        .HasColumnType("bit");

                    b.Property<double>("DesignCost")
                        .HasColumnType("float");

                    b.Property<bool>("DoubleSided")
                        .HasColumnType("bit");

                    b.Property<double>("FileHandlingCost")
                        .HasColumnType("float");

                    b.Property<Guid?>("FlatSizeId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Folds")
                        .HasColumnType("int");

                    b.Property<double>("Height")
                        .HasColumnType("float");

                    b.Property<int>("HolePunches")
                        .HasColumnType("int");

                    b.Property<double>("Kinds")
                        .HasColumnType("float");

                    b.Property<bool>("Lamination")
                        .HasColumnType("bit");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.Property<double>("SetupCost")
                        .HasColumnType("float");

                    b.Property<int>("Staples")
                        .HasColumnType("int");

                    b.Property<Guid>("StockId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Width")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Sra3FormInput");
                });

            modelBuilder.Entity("PaperCalc.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Admin")
                        .HasColumnType("bit");

                    b.Property<byte[]>("PasswordHash")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("User");
                });
#pragma warning restore 612, 618
        }
    }
}
