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
    [Migration("20211125115103_update_student_avatar")]
    partial class update_student_avatar
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseCollation("utf8mb4_0900_ai_ci")
                .HasAnnotation("ProductVersion", "6.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.HasCharSet(modelBuilder, "utf8mb4");

            modelBuilder.Entity("InterSMeet.DAL.Entities.Degree", b =>
                {
                    b.Property<int>("DegreeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("degree_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("name");

                    b.Property<int>("family_id")
                        .HasColumnType("int");

                    b.Property<int>("level_id")
                        .HasColumnType("int");

                    b.HasKey("DegreeId");

                    b.HasIndex("family_id");

                    b.HasIndex("level_id");

                    b.ToTable("Degrees");
                });

            modelBuilder.Entity("InterSMeet.DAL.Entities.Family", b =>
                {
                    b.Property<int>("FamilyId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("family_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.HasKey("FamilyId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Families");
                });

            modelBuilder.Entity("InterSMeet.DAL.Entities.Image", b =>
                {
                    b.Property<int>("ImageId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("image_id");

                    b.Property<byte[]>("ImageData")
                        .IsRequired()
                        .HasColumnType("varbinary(3072)")
                        .HasColumnName("image_data");

                    b.Property<string>("ImageTitle")
                        .IsRequired()
                        .HasColumnType("longtext")
                        .HasColumnName("image_title");

                    b.HasKey("ImageId");

                    b.HasIndex("ImageData")
                        .IsUnique();

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

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("InterSMeet.DAL.Entities.Level", b =>
                {
                    b.Property<int>("LevelId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("level_id");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.HasKey("LevelId");

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("Levels");
                });

            modelBuilder.Entity("InterSMeet.DAL.Entities.Province", b =>
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

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("InterSMeet.DAL.Entities.Student", b =>
                {
                    b.Property<int>("StudentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("student_id");

                    b.Property<double>("AverageGrades")
                        .HasColumnType("double")
                        .HasColumnName("average_grades");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)")
                        .HasColumnName("birthdate");

                    b.Property<int>("avatar_id")
                        .HasColumnType("int");

                    b.Property<int>("degree_id")
                        .HasColumnType("int");

                    b.HasKey("StudentId");

                    b.HasIndex("avatar_id");

                    b.HasIndex("degree_id");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("InterSMeet.DAL.Entities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("user_id");

                    b.Property<string>("CreatedAt")
                        .HasColumnType("longtext")
                        .HasColumnName("created_at");

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
                        .HasColumnType("longtext")
                        .HasColumnName("updated_at");

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

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Username")
                        .IsUnique();

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

                    b.HasIndex("Name")
                        .IsUnique();

                    b.ToTable("user_roles");
                });

            modelBuilder.Entity("InterSMeet.DAL.Entities.Degree", b =>
                {
                    b.HasOne("InterSMeet.DAL.Entities.Family", "Family")
                        .WithMany()
                        .HasForeignKey("family_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InterSMeet.DAL.Entities.Level", "Level")
                        .WithMany()
                        .HasForeignKey("level_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Family");

                    b.Navigation("Level");
                });

            modelBuilder.Entity("InterSMeet.DAL.Entities.Student", b =>
                {
                    b.HasOne("InterSMeet.DAL.Entities.Image", "Avatar")
                        .WithMany()
                        .HasForeignKey("avatar_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("InterSMeet.DAL.Entities.Degree", "Degree")
                        .WithMany()
                        .HasForeignKey("degree_id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Avatar");

                    b.Navigation("Degree");
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
