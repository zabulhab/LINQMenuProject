﻿// <auto-generated />
using System;
using EFTest;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFTest.Migrations
{
    [DbContext(typeof(MenuContext))]
    partial class MenuContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9");

            modelBuilder.Entity("EFTest.Dish", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("ID");

                    b.ToTable("Dishes");
                });

            modelBuilder.Entity("EFTest.Ingredient", b =>
                {
                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int?>("DishID")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasDairy")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("HasGluten")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsMeat")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsNut")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsSpicy")
                        .HasColumnType("INTEGER");

                    b.HasKey("Name");

                    b.HasIndex("DishID");

                    b.ToTable("Ingredients");
                });

            modelBuilder.Entity("EFTest.Ingredient", b =>
                {
                    b.HasOne("EFTest.Dish", null)
                        .WithMany("Ingredients")
                        .HasForeignKey("DishID");
                });
#pragma warning restore 612, 618
        }
    }
}
