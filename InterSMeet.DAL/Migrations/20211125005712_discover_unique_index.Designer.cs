﻿// <auto-generated />
using System;
using InterSMeet.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace InterSMeet.DAL.Migrations
{
    [DbContext(typeof(InterSMeetDbContext))]
    [Migration("20211125005712_discover_unique_index")]
    partial class discover_unique_index
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");

            modelBuilder.Entity("InterSMeet.DAL.Entities.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("image_id");

                    b.Property<byte[]>("ImageData")
                        .IsRequired()
                        .HasColumnType("longblob")
                        .HasColumnName("image_data");

                    b.Property<string>("ImageTitle")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("image_title");

                    b.HasKey("ImageId");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("InterSMeet.DAL.Entities.Language", b =>
                {
                    b.Property<int>("LanguageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("language_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("varchar(5)")
                        .HasColumnName("name");

                    b.HasKey("LanguageId");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("InterSMeet.DAL.Entities.nce", b =>
                {
                    b.Property<int>("ProvinceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("province_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.HasKey("ProvinceId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Province");
                });

            modelBuilder.Entity("InterSMeet.DAL.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<string>("CreatedAt")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("email");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("first_name");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(70)
                        .HasColumnType("varchar(70)")
                        .HasColumnName("last_name");

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("location");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("password");

                    b.Property<string>("UpdatedAt")
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("username");

                    b.Property<int>("language_id")
                        .HasColumnType("int");

                    b.Property<int>("province_id")
                        .HasColumnType("int");

                    b.Property<int?>("role_id")
                        .HasColumnType("int");

                    b.HasKey("UserId");

                    b.HasIndex("language_id");

                    b.HasIndex("province_id");

                    b.HasIndex("role_id");

                    b.HasIndex(new[] { "Email" }, "IDX_97672ac88f789774dd47f7c8be")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("InterSMeet.DAL.Entities.UserRole", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("role_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(40)
                        .HasColumnType("varchar(40)")
                        .HasColumnName("name");

                    b.HasKey("RoleId");

                    b.ToTable("user_roles");
                });

            modelBuilder.Entity("InterSMeet.DAL.Entities.User", b =>
                {
                    b.HasOne("InterSMeet.DAL.Entities.Language", "Language")
                        .WithMany()
                        .HasForeignKey("language_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InterSMeet.DAL.Entities.Province", "Province")
                        .WithMany()
                        .HasForeignKey("province_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InterSMeet.DAL.Entities.UserRole", "Role")
                        .WithMany()
                        .HasForeignKey("role_id");

                    b.Navigation("Language");

                    b.Navigation("Province");

                    b.Navigation("Role");
                });
#pragma warning restore 612, 618
        }
    }
}
