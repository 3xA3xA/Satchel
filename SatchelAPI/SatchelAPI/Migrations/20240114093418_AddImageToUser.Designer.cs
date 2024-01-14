﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Satchel.Infrastructure;

#nullable disable

namespace SatchelAPI.Migrations
{
    [DbContext(typeof(SatchelDbContext))]
    [Migration("20240114093418_AddImageToUser")]
    partial class AddImageToUser
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Satchel.Application.Models.BrandType", b =>
                {
                    b.Property<int>("BrandTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BrandTypeId"));

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("BrandTypeId");

                    b.ToTable("BrandTypes");
                });

            modelBuilder.Entity("Satchel.Application.Models.Favourites", b =>
                {
                    b.Property<int>("FavouritesId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FavouritesId"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FavouritesId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Favourites");
                });

            modelBuilder.Entity("Satchel.Application.Models.Feedback", b =>
                {
                    b.Property<int>("FeedbackId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("FeedbackId"));

                    b.Property<float>("Estimation")
                        .HasColumnType("real");

                    b.Property<string>("FeedbackText")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("FeedbackId");

                    b.HasIndex("ProductId");

                    b.HasIndex("UserId");

                    b.ToTable("Feedbacks");
                });

            modelBuilder.Entity("Satchel.Application.Models.GenderType", b =>
                {
                    b.Property<int>("GenderTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("GenderTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("GenderTypeId");

                    b.ToTable("GenderTypes");
                });

            modelBuilder.Entity("Satchel.Application.Models.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderId"));

                    b.Property<int>("OrderStatusTypeId")
                        .HasColumnType("int");

                    b.Property<int>("PaymentTypeId")
                        .HasColumnType("int");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("ShippingTypeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("OrderId");

                    b.HasIndex("OrderStatusTypeId");

                    b.HasIndex("PaymentTypeId");

                    b.HasIndex("ProductId");

                    b.HasIndex("ShippingTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("Satchel.Application.Models.OrderStatusType", b =>
                {
                    b.Property<int>("OrderStatusTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("OrderStatusTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("OrderStatusTypeId");

                    b.ToTable("OrderStatusTypes");
                });

            modelBuilder.Entity("Satchel.Application.Models.PaymentType", b =>
                {
                    b.Property<int>("PaymentTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PaymentTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("PaymentTypeId");

                    b.ToTable("PaymentTypes");
                });

            modelBuilder.Entity("Satchel.Application.Models.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductId"));

                    b.Property<int>("BrandTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("nvarchar(1000)");

                    b.Property<int>("GenderTypeId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("Price")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ProductTypeId")
                        .HasColumnType("int");

                    b.HasKey("ProductId");

                    b.HasIndex("BrandTypeId");

                    b.HasIndex("GenderTypeId");

                    b.HasIndex("ProductTypeId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Satchel.Application.Models.ProductType", b =>
                {
                    b.Property<int>("ProductTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProductTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int?>("ParentProductTypeId")
                        .HasColumnType("int");

                    b.HasKey("ProductTypeId");

                    b.HasIndex("ParentProductTypeId");

                    b.ToTable("ProductTypes");
                });

            modelBuilder.Entity("Satchel.Application.Models.ShippingType", b =>
                {
                    b.Property<int>("ShippingTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShippingTypeId"));

                    b.Property<int>("Days")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ShippingTypeId");

                    b.ToTable("ShippingTypes");
                });

            modelBuilder.Entity("Satchel.Application.Models.ShoppingCart", b =>
                {
                    b.Property<int>("ShoppingCartId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ShoppingCartId"));

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.Property<int>("SizeTypeId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ShoppingCartId");

                    b.HasIndex("ProductId");

                    b.HasIndex("SizeTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("ShoppingCarts");
                });

            modelBuilder.Entity("Satchel.Application.Models.SizeType", b =>
                {
                    b.Property<int>("SizeTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SizeTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.HasKey("SizeTypeId");

                    b.ToTable("SizeTypes");
                });

            modelBuilder.Entity("Satchel.Application.Models.SizeTypeToProductType", b =>
                {
                    b.Property<int>("SizeTypeToProductTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SizeTypeToProductTypeId"));

                    b.Property<int>("ProductTypeId")
                        .HasColumnType("int");

                    b.Property<int>("SizeTypeId")
                        .HasColumnType("int");

                    b.HasKey("SizeTypeToProductTypeId");

                    b.HasIndex("ProductTypeId");

                    b.HasIndex("SizeTypeId");

                    b.ToTable("SizeTypeToProductTypes");
                });

            modelBuilder.Entity("Satchel.Application.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Image")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<int>("UserTypeId")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("UserTypeId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("Satchel.Application.Models.UserType", b =>
                {
                    b.Property<int>("UserTypeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserTypeId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("UserTypeId");

                    b.ToTable("UserTypes");
                });

            modelBuilder.Entity("SatchelAPI.Application.Models.ProductImages", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ImageId"));

                    b.Property<string>("ImagePath")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)");

                    b.Property<int>("ProductId")
                        .HasColumnType("int");

                    b.HasKey("ImageId");

                    b.HasIndex("ProductId");

                    b.ToTable("ProductImages");
                });

            modelBuilder.Entity("Satchel.Application.Models.Favourites", b =>
                {
                    b.HasOne("Satchel.Application.Models.Product", "Product")
                        .WithMany("Favourites")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Satchel.Application.Models.User", "User")
                        .WithMany("Favourites")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Satchel.Application.Models.Feedback", b =>
                {
                    b.HasOne("Satchel.Application.Models.Product", "Product")
                        .WithMany("Feedbacks")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Satchel.Application.Models.User", "User")
                        .WithMany("Feedbacks")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Satchel.Application.Models.Order", b =>
                {
                    b.HasOne("Satchel.Application.Models.OrderStatusType", "OrderStatusType")
                        .WithMany("Orders")
                        .HasForeignKey("OrderStatusTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Satchel.Application.Models.PaymentType", "PaymentType")
                        .WithMany("Orders")
                        .HasForeignKey("PaymentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Satchel.Application.Models.Product", "Product")
                        .WithMany("Orders")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Satchel.Application.Models.ShippingType", "ShippingType")
                        .WithMany("Orders")
                        .HasForeignKey("ShippingTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Satchel.Application.Models.User", "User")
                        .WithMany("Orders")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrderStatusType");

                    b.Navigation("PaymentType");

                    b.Navigation("Product");

                    b.Navigation("ShippingType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Satchel.Application.Models.Product", b =>
                {
                    b.HasOne("Satchel.Application.Models.BrandType", "BrandType")
                        .WithMany("Products")
                        .HasForeignKey("BrandTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Satchel.Application.Models.GenderType", "GenderType")
                        .WithMany("Products")
                        .HasForeignKey("GenderTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Satchel.Application.Models.ProductType", "ProductType")
                        .WithMany("Products")
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("BrandType");

                    b.Navigation("GenderType");

                    b.Navigation("ProductType");
                });

            modelBuilder.Entity("Satchel.Application.Models.ProductType", b =>
                {
                    b.HasOne("Satchel.Application.Models.ProductType", "ParentProductType")
                        .WithMany()
                        .HasForeignKey("ParentProductTypeId");

                    b.Navigation("ParentProductType");
                });

            modelBuilder.Entity("Satchel.Application.Models.ShoppingCart", b =>
                {
                    b.HasOne("Satchel.Application.Models.Product", "Product")
                        .WithMany("ShoppingCarts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Satchel.Application.Models.SizeType", "SizeType")
                        .WithMany("ShoppingCarts")
                        .HasForeignKey("SizeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Satchel.Application.Models.User", "User")
                        .WithMany("ShoppingCarts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");

                    b.Navigation("SizeType");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Satchel.Application.Models.SizeTypeToProductType", b =>
                {
                    b.HasOne("Satchel.Application.Models.ProductType", "ProductType")
                        .WithMany("SizeTypeToProductTypes")
                        .HasForeignKey("ProductTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Satchel.Application.Models.SizeType", "SizeType")
                        .WithMany("SizeTypeToProductTypes")
                        .HasForeignKey("SizeTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProductType");

                    b.Navigation("SizeType");
                });

            modelBuilder.Entity("Satchel.Application.Models.User", b =>
                {
                    b.HasOne("Satchel.Application.Models.UserType", "UserType")
                        .WithMany("Users")
                        .HasForeignKey("UserTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("UserType");
                });

            modelBuilder.Entity("SatchelAPI.Application.Models.ProductImages", b =>
                {
                    b.HasOne("Satchel.Application.Models.Product", "Product")
                        .WithMany("ProductImages")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Satchel.Application.Models.BrandType", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Satchel.Application.Models.GenderType", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Satchel.Application.Models.OrderStatusType", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Satchel.Application.Models.PaymentType", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Satchel.Application.Models.Product", b =>
                {
                    b.Navigation("Favourites");

                    b.Navigation("Feedbacks");

                    b.Navigation("Orders");

                    b.Navigation("ProductImages");

                    b.Navigation("ShoppingCarts");
                });

            modelBuilder.Entity("Satchel.Application.Models.ProductType", b =>
                {
                    b.Navigation("Products");

                    b.Navigation("SizeTypeToProductTypes");
                });

            modelBuilder.Entity("Satchel.Application.Models.ShippingType", b =>
                {
                    b.Navigation("Orders");
                });

            modelBuilder.Entity("Satchel.Application.Models.SizeType", b =>
                {
                    b.Navigation("ShoppingCarts");

                    b.Navigation("SizeTypeToProductTypes");
                });

            modelBuilder.Entity("Satchel.Application.Models.User", b =>
                {
                    b.Navigation("Favourites");

                    b.Navigation("Feedbacks");

                    b.Navigation("Orders");

                    b.Navigation("ShoppingCarts");
                });

            modelBuilder.Entity("Satchel.Application.Models.UserType", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
