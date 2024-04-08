﻿// <auto-generated />
using System;
using CoreWebAPIJWT.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CoreWebAPIJWT.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240408104807_InsertDatawithDatatime")]
    partial class InsertDatawithDatatime
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CoreWebAPIJWT.Models.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenity")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Details")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImageUrl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Occupancy")
                        .HasColumnType("int");

                    b.Property<double>("Rate")
                        .HasColumnType("float");

                    b.Property<int>("Sqft")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenity = "",
                            CreatedDate = new DateTime(2024, 4, 8, 16, 18, 6, 854, DateTimeKind.Local).AddTicks(8781),
                            Details = "Focus 11 tincidunt maximus leo and sed scelerious .Beautiful Desinged Created thats has very Old strcture.",
                            ImageUrl = "https://unsplash.com/photos/a-laptop-and-a-potted-plant-IqBY9blj8Ks",
                            Name = "Royal Villa",
                            Occupancy = 5,
                            Rate = 500.0,
                            Sqft = 550,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 2,
                            Amenity = "",
                            CreatedDate = new DateTime(2024, 4, 8, 16, 18, 6, 854, DateTimeKind.Local).AddTicks(8796),
                            Details = "Beautiful Desinged Created thats has very Old strcture.",
                            ImageUrl = "https://unsplash.com/photos/macbook-pro-on-top-of-brown-table-1SAnrIxw5OY",
                            Name = "Royal Pool",
                            Occupancy = 3,
                            Rate = 450.0,
                            Sqft = 500,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 3,
                            Amenity = "",
                            CreatedDate = new DateTime(2024, 4, 8, 16, 18, 6, 854, DateTimeKind.Local).AddTicks(8798),
                            Details = "Beautiful Desinged Created thats has very Old strcture.",
                            ImageUrl = "https://unsplash.com/photos/a-woman-sitting-on-a-couch-using-a-laptop-computer-IhO7j8qEaVc",
                            Name = "Royal split Villa",
                            Occupancy = 6,
                            Rate = 650.0,
                            Sqft = 1150,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 4,
                            Amenity = "",
                            CreatedDate = new DateTime(2024, 4, 8, 16, 18, 6, 854, DateTimeKind.Local).AddTicks(8799),
                            Details = "Beautiful Desinged Created thats has very Old strcture.",
                            ImageUrl = "https://unsplash.com/photos/apple-macbook-beside-computer-mouse-on-table-9l_326FISzk",
                            Name = "Diamond Villa",
                            Occupancy = 5,
                            Rate = 400.0,
                            Sqft = 350,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        },
                        new
                        {
                            Id = 5,
                            Amenity = "",
                            CreatedDate = new DateTime(2024, 4, 8, 16, 18, 6, 854, DateTimeKind.Local).AddTicks(8801),
                            Details = "Beautiful Desinged Created thats has very Old strcture.",
                            ImageUrl = "https://unsplash.com/photos/a-laptop-and-a-potted-plant-IqBY9blj8Ks",
                            Name = "Diamond Pool Villa",
                            Occupancy = 2,
                            Rate = 500.0,
                            Sqft = 550,
                            UpdatedDate = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
