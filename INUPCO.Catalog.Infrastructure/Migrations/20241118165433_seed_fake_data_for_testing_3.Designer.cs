﻿// <auto-generated />
using System;
using INUPCO.Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace INUPCO.Catalog.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241118165433_seed_fake_data_for_testing_3")]
    partial class seed_fake_data_for_testing_3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("INUPCO.Catalog.Domain.Entities.Customers.CustomerGenericItemPharmaCodeMapping", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CustomerSpecificCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("GenericItemPharmaId")
                        .HasColumnType("int");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("GenericItemPharmaId");

                    b.HasIndex("CustomerCode", "CustomerSpecificCode")
                        .IsUnique();

                    b.ToTable("CustomerGenericItemPharmaCodeMappings");
                });

            modelBuilder.Entity("INUPCO.Catalog.Domain.Entities.GenericItemPharmas.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("GenericItemPharmaId")
                        .HasColumnType("int");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("GenericItemPharmaId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("INUPCO.Catalog.Domain.Entities.GenericItemPharmas.GenericItemPharma", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("CustomerCode")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("GenericItemPharmas");
                });

            modelBuilder.Entity("INUPCO.Catalog.Domain.Entities.Manufacturers.Manufacturer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("Manufacturers", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Country = "USA",
                            CreatedDate = new DateTime(2024, 11, 18, 16, 54, 33, 299, DateTimeKind.Utc).AddTicks(4145),
                            Name = "Pfizer"
                        },
                        new
                        {
                            Id = 2,
                            Country = "Switzerland",
                            CreatedDate = new DateTime(2024, 11, 18, 16, 54, 33, 299, DateTimeKind.Utc).AddTicks(4149),
                            Name = "Novartis"
                        });
                });

            modelBuilder.Entity("INUPCO.Catalog.Domain.Entities.Products.Product", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int?>("SubsidiaryId")
                        .HasColumnType("int");

                    b.Property<string>("TradeCode")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("SubsidiaryId");

                    b.HasIndex("TradeCode")
                        .IsUnique();

                    b.ToTable("Products", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedDate = new DateTime(2024, 11, 18, 16, 54, 33, 299, DateTimeKind.Utc).AddTicks(4208),
                            ManufacturerId = 1,
                            Name = "Lipitor",
                            SubsidiaryId = 1,
                            TradeCode = "PFE001"
                        },
                        new
                        {
                            Id = 2,
                            CreatedDate = new DateTime(2024, 11, 18, 16, 54, 33, 299, DateTimeKind.Utc).AddTicks(4210),
                            ManufacturerId = 1,
                            Name = "Xarelto",
                            TradeCode = "PFE002"
                        },
                        new
                        {
                            Id = 3,
                            CreatedDate = new DateTime(2024, 11, 18, 16, 54, 33, 299, DateTimeKind.Utc).AddTicks(4212),
                            ManufacturerId = 2,
                            Name = "Entresto",
                            SubsidiaryId = 2,
                            TradeCode = "NOV001"
                        });
                });

            modelBuilder.Entity("INUPCO.Catalog.Domain.Entities.Subsidiaries.Subsidiary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Country")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("LastModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("ManufacturerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.HasIndex("ManufacturerId");

                    b.HasIndex("Name", "Country", "ManufacturerId")
                        .IsUnique();

                    b.ToTable("Subsidiaries", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Country = "Germany",
                            CreatedDate = new DateTime(2024, 11, 18, 16, 54, 33, 299, DateTimeKind.Utc).AddTicks(4189),
                            ManufacturerId = 1,
                            Name = "Pfizer Germany GmbH"
                        },
                        new
                        {
                            Id = 2,
                            Country = "Spain",
                            CreatedDate = new DateTime(2024, 11, 18, 16, 54, 33, 299, DateTimeKind.Utc).AddTicks(4190),
                            ManufacturerId = 2,
                            Name = "Novartis Spain SA"
                        });
                });

            modelBuilder.Entity("INUPCO.Catalog.Domain.Entities.Customers.CustomerGenericItemPharmaCodeMapping", b =>
                {
                    b.HasOne("INUPCO.Catalog.Domain.Entities.GenericItemPharmas.GenericItemPharma", "GenericItemPharma")
                        .WithMany("CustomerMappings")
                        .HasForeignKey("GenericItemPharmaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GenericItemPharma");
                });

            modelBuilder.Entity("INUPCO.Catalog.Domain.Entities.GenericItemPharmas.Comment", b =>
                {
                    b.HasOne("INUPCO.Catalog.Domain.Entities.GenericItemPharmas.GenericItemPharma", null)
                        .WithMany("Comments")
                        .HasForeignKey("GenericItemPharmaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("INUPCO.Catalog.Domain.Entities.Products.Product", b =>
                {
                    b.HasOne("INUPCO.Catalog.Domain.Entities.Manufacturers.Manufacturer", "Manufacturer")
                        .WithMany("Products")
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("INUPCO.Catalog.Domain.Entities.Subsidiaries.Subsidiary", "Subsidiary")
                        .WithMany("Products")
                        .HasForeignKey("SubsidiaryId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.Navigation("Manufacturer");

                    b.Navigation("Subsidiary");
                });

            modelBuilder.Entity("INUPCO.Catalog.Domain.Entities.Subsidiaries.Subsidiary", b =>
                {
                    b.HasOne("INUPCO.Catalog.Domain.Entities.Manufacturers.Manufacturer", "Manufacturer")
                        .WithMany("Subsidiaries")
                        .HasForeignKey("ManufacturerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Manufacturer");
                });

            modelBuilder.Entity("INUPCO.Catalog.Domain.Entities.GenericItemPharmas.GenericItemPharma", b =>
                {
                    b.Navigation("Comments");

                    b.Navigation("CustomerMappings");
                });

            modelBuilder.Entity("INUPCO.Catalog.Domain.Entities.Manufacturers.Manufacturer", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("Subsidiaries");
                });

            modelBuilder.Entity("INUPCO.Catalog.Domain.Entities.Subsidiaries.Subsidiary", b =>
                {
                    b.Navigation("Products");
                });
#pragma warning restore 612, 618
        }
    }
}
