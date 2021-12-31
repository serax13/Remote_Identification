﻿// <auto-generated />
using System;
using Identification.Api;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Identification.Api.Migrations
{
    [DbContext(typeof(IdentityDbContext))]
    [Migration("20210913033651_CnahgeMaxLength")]
    partial class CnahgeMaxLength
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.9")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Identification.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AparmentNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("City")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<decimal>("CreditSum")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("CreditTime")
                        .HasColumnType("int");

                    b.Property<string>("DateTime")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("District")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("FIO")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("FirstName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Gender")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("HouseNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("LastName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("MartialStatus")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("MidName")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Number1")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Number2")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PassportAparmentNumber")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("PassportCity")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime?>("PassportDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PassportDistrict")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime?>("PassportEndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PassportHouseNumber")
                        .HasMaxLength(12)
                        .HasColumnType("nvarchar(12)");

                    b.Property<string>("PassportInn")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PassportIssuedBy")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PassportNumber")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PassportRegion")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PassportSeries")
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("PassportStreet")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Region")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.ToTable("SavedClients");
                });

            modelBuilder.Entity("Identification.Models.Img", b =>
                {
                    b.Property<int>("ImgId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<byte[]>("Pic1")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Pic2")
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("Pic3")
                        .HasColumnType("varbinary(max)");

                    b.HasKey("ImgId");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("SavedImg");
                });

            modelBuilder.Entity("Identification.Models.RefreshToken", b =>
                {
                    b.Property<int>("RefreshTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("ExpiryDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("RefreshTokenId");

                    b.HasIndex("UserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Identification.Models.RoleUsers", b =>
                {
                    b.Property<short>("RoleUsersId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("smallint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("RoleDesc")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleUsersId");

                    b.ToTable("RolesUsers");
                });

            modelBuilder.Entity("Identification.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("HireDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MiddleName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<short>("RoleUsersId")
                        .HasColumnType("smallint");

                    b.Property<string>("Source")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleUsersId");

                    b.ToTable("Logins");
                });

            modelBuilder.Entity("Identification.Models.Img", b =>
                {
                    b.HasOne("Identification.Models.Client", "Client")
                        .WithOne("Img")
                        .HasForeignKey("Identification.Models.Img", "ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Client");
                });

            modelBuilder.Entity("Identification.Models.RefreshToken", b =>
                {
                    b.HasOne("Identification.Models.User", "User")
                        .WithMany("RefreshTokens")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Identification.Models.User", b =>
                {
                    b.HasOne("Identification.Models.RoleUsers", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleUsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Identification.Models.Client", b =>
                {
                    b.Navigation("Img");
                });

            modelBuilder.Entity("Identification.Models.RoleUsers", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Identification.Models.User", b =>
                {
                    b.Navigation("RefreshTokens");
                });
#pragma warning restore 612, 618
        }
    }
}