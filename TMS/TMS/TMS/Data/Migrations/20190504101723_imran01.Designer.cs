﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TMS.Data;

namespace TMS.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20190504101723_imran01")]
    partial class imran01
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.3-rtm-10026")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("TMS.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("TMS.Models.Batch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CourseId");

                    b.Property<int>("InstructorId");

                    b.Property<int>("Number");

                    b.HasKey("Id");

                    b.HasIndex("CourseId");

                    b.HasIndex("InstructorId");

                    b.ToTable("Batch");
                });

            modelBuilder.Entity("TMS.Models.Coordinator", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("Contact")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Coordinator");
                });

            modelBuilder.Entity("TMS.Models.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Duration")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("TMS.Models.Instructor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<string>("Contact")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.ToTable("Instructor");
                });

            modelBuilder.Entity("TMS.Models.Performance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Accuracy");

                    b.Property<int>("BatchId");

                    b.Property<int>("CourseId");

                    b.Property<int>("InstructorId");

                    b.Property<int>("ProgressId");

                    b.Property<int>("TaskId");

                    b.Property<int>("TraineeId");

                    b.HasKey("Id");

                    b.HasIndex("BatchId");

                    b.HasIndex("CourseId");

                    b.HasIndex("InstructorId");

                    b.HasIndex("ProgressId")
                        .IsUnique();

                    b.HasIndex("TaskId")
                        .IsUnique();

                    b.HasIndex("TraineeId");

                    b.ToTable("Performance");
                });

            modelBuilder.Entity("TMS.Models.Progress", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("BatchId");

                    b.Property<double>("Completed");

                    b.Property<int>("CourseId");

                    b.Property<DateTime>("Date");

                    b.Property<int>("InstructorId");

                    b.Property<int>("TaskId");

                    b.Property<int>("TraineeId");

                    b.HasKey("Id");

                    b.HasIndex("BatchId");

                    b.HasIndex("CourseId");

                    b.HasIndex("InstructorId");

                    b.HasIndex("TaskId")
                        .IsUnique();

                    b.HasIndex("TraineeId");

                    b.ToTable("Progress");
                });

            modelBuilder.Entity("TMS.Models.Salary", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("BasicSalary");

                    b.Property<double>("Bonus");

                    b.Property<DateTime>("Date");

                    b.Property<int>("InstructorId");

                    b.HasKey("Id");

                    b.HasIndex("InstructorId");

                    b.ToTable("Salary");
                });

            modelBuilder.Entity("TMS.Models.Tasks", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("AssignDate");

                    b.Property<int>("BatchId");

                    b.Property<int>("CourseId");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<int>("InstructorId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int>("Number");

                    b.Property<DateTime>("SubmissionDate");

                    b.Property<int>("TraineeId");

                    b.HasKey("Id");

                    b.HasIndex("BatchId");

                    b.HasIndex("CourseId");

                    b.HasIndex("InstructorId");

                    b.HasIndex("TraineeId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TMS.Models.Trainee", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(150);

                    b.Property<int>("BatchId");

                    b.Property<string>("Contact")
                        .IsRequired();

                    b.Property<int>("CourseId");

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.HasKey("Id");

                    b.HasIndex("BatchId");

                    b.HasIndex("CourseId");

                    b.ToTable("Trainee");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("TMS.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("TMS.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TMS.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("TMS.Models.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TMS.Models.Batch", b =>
                {
                    b.HasOne("TMS.Models.Course", "Course")
                        .WithMany("Batches")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TMS.Models.Instructor", "Instructor")
                        .WithMany("Batches")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TMS.Models.Performance", b =>
                {
                    b.HasOne("TMS.Models.Batch", "Batch")
                        .WithMany("Performances")
                        .HasForeignKey("BatchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TMS.Models.Course", "Course")
                        .WithMany("Performances")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TMS.Models.Instructor", "Instructor")
                        .WithMany("Performances")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TMS.Models.Progress", "Progress")
                        .WithOne("Performance")
                        .HasForeignKey("TMS.Models.Performance", "ProgressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TMS.Models.Tasks", "Task")
                        .WithOne("Performance")
                        .HasForeignKey("TMS.Models.Performance", "TaskId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TMS.Models.Trainee", "Trainee")
                        .WithMany("Performances")
                        .HasForeignKey("TraineeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TMS.Models.Progress", b =>
                {
                    b.HasOne("TMS.Models.Batch", "Batch")
                        .WithMany("Progresses")
                        .HasForeignKey("BatchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TMS.Models.Course", "Course")
                        .WithMany("Progresses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TMS.Models.Instructor", "Instructor")
                        .WithMany("Progresses")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TMS.Models.Tasks", "Task")
                        .WithOne("Progress")
                        .HasForeignKey("TMS.Models.Progress", "TaskId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TMS.Models.Trainee", "Trainee")
                        .WithMany("Progresses")
                        .HasForeignKey("TraineeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TMS.Models.Salary", b =>
                {
                    b.HasOne("TMS.Models.Instructor", "Instructor")
                        .WithMany("Salaries")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TMS.Models.Tasks", b =>
                {
                    b.HasOne("TMS.Models.Batch", "Batch")
                        .WithMany("Task")
                        .HasForeignKey("BatchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TMS.Models.Course", "Course")
                        .WithMany("Task")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TMS.Models.Instructor", "Instructor")
                        .WithMany("Tasks")
                        .HasForeignKey("InstructorId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TMS.Models.Trainee", "Trainee")
                        .WithMany("Tasks")
                        .HasForeignKey("TraineeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("TMS.Models.Trainee", b =>
                {
                    b.HasOne("TMS.Models.Batch", "Batch")
                        .WithMany("Trainees")
                        .HasForeignKey("BatchId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("TMS.Models.Course", "Course")
                        .WithMany("Trainees")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}