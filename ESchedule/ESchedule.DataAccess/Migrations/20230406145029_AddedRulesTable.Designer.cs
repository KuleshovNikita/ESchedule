﻿// <auto-generated />
using System;
using ESchedule.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ESchedule.DataAccess.Migrations
{
    [DbContext(typeof(EScheduleDbContext))]
    [Migration("20230406145029_AddedRulesTable")]
    partial class AddedRulesTable
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ESchedule.Domain.Lessons.LessonModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Lessons");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            TenantId = new Guid("00000000-0000-0000-0000-000000000001"),
                            Title = "Math"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            TenantId = new Guid("00000000-0000-0000-0000-000000000001"),
                            Title = "Physics"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000003"),
                            TenantId = new Guid("00000000-0000-0000-0000-000000000001"),
                            Title = "Chemistry"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000004"),
                            TenantId = new Guid("00000000-0000-0000-0000-000000000001"),
                            Title = "Literature"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000005"),
                            TenantId = new Guid("00000000-0000-0000-0000-000000000001"),
                            Title = "PE"
                        });
                });

            modelBuilder.Entity("ESchedule.Domain.Lessons.Schedule.ScheduleModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("DayOfWeek")
                        .HasColumnType("int");

                    b.Property<TimeSpan>("EndTime")
                        .HasColumnType("time");

                    b.Property<Guid>("LessonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("StartTime")
                        .HasColumnType("time");

                    b.Property<Guid>("StudyGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.HasIndex("StudyGroupId");

                    b.HasIndex("TeacherId");

                    b.HasIndex("TenantId");

                    b.ToTable("Schedules");
                });

            modelBuilder.Entity("ESchedule.Domain.ManyToManyModels.GroupsLessonsModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LessonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudyGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.HasIndex("StudyGroupId");

                    b.ToTable("GroupsLessons");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000001"),
                            StudyGroupId = new Guid("00000000-0000-0000-0000-000000000001")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000002"),
                            StudyGroupId = new Guid("00000000-0000-0000-0000-000000000001")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000003"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000003"),
                            StudyGroupId = new Guid("00000000-0000-0000-0000-000000000001")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000004"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000004"),
                            StudyGroupId = new Guid("00000000-0000-0000-0000-000000000001")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000005"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000005"),
                            StudyGroupId = new Guid("00000000-0000-0000-0000-000000000001")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000006"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000001"),
                            StudyGroupId = new Guid("00000000-0000-0000-0000-000000000002")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000007"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000002"),
                            StudyGroupId = new Guid("00000000-0000-0000-0000-000000000002")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000008"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000003"),
                            StudyGroupId = new Guid("00000000-0000-0000-0000-000000000002")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000009"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000004"),
                            StudyGroupId = new Guid("00000000-0000-0000-0000-000000000002")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000010"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000005"),
                            StudyGroupId = new Guid("00000000-0000-0000-0000-000000000002")
                        });
                });

            modelBuilder.Entity("ESchedule.Domain.ManyToManyModels.TeachersGroupsLessonsModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LessonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("StudyGroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.HasIndex("StudyGroupId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeachersGroupsLessons");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000001"),
                            StudyGroupId = new Guid("00000000-0000-0000-0000-000000000001"),
                            TeacherId = new Guid("00000000-0000-0000-0000-000000000005")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000008"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000002"),
                            StudyGroupId = new Guid("00000000-0000-0000-0000-000000000002"),
                            TeacherId = new Guid("00000000-0000-0000-0000-000000000005")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000009"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000002"),
                            StudyGroupId = new Guid("00000000-0000-0000-0000-000000000001"),
                            TeacherId = new Guid("00000000-0000-0000-0000-000000000005")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000005"),
                            StudyGroupId = new Guid("00000000-0000-0000-0000-000000000002"),
                            TeacherId = new Guid("00000000-0000-0000-0000-000000000007")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000010"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000003"),
                            StudyGroupId = new Guid("00000000-0000-0000-0000-000000000002"),
                            TeacherId = new Guid("00000000-0000-0000-0000-000000000006")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000003"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000003"),
                            StudyGroupId = new Guid("00000000-0000-0000-0000-000000000001"),
                            TeacherId = new Guid("00000000-0000-0000-0000-000000000006")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000004"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000004"),
                            StudyGroupId = new Guid("00000000-0000-0000-0000-000000000002"),
                            TeacherId = new Guid("00000000-0000-0000-0000-000000000007")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000005"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000004"),
                            StudyGroupId = new Guid("00000000-0000-0000-0000-000000000001"),
                            TeacherId = new Guid("00000000-0000-0000-0000-000000000007")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000006"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000005"),
                            StudyGroupId = new Guid("00000000-0000-0000-0000-000000000001"),
                            TeacherId = new Guid("00000000-0000-0000-0000-000000000007")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000007"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000001"),
                            StudyGroupId = new Guid("00000000-0000-0000-0000-000000000002"),
                            TeacherId = new Guid("00000000-0000-0000-0000-000000000006")
                        });
                });

            modelBuilder.Entity("ESchedule.Domain.ManyToManyModels.TeachersLessonsModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("LessonId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("TeacherId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("LessonId");

                    b.HasIndex("TeacherId");

                    b.ToTable("TeachersLessons");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000001"),
                            TeacherId = new Guid("00000000-0000-0000-0000-000000000005")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000002"),
                            TeacherId = new Guid("00000000-0000-0000-0000-000000000005")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000003"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000001"),
                            TeacherId = new Guid("00000000-0000-0000-0000-000000000006")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000004"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000003"),
                            TeacherId = new Guid("00000000-0000-0000-0000-000000000006")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000005"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000004"),
                            TeacherId = new Guid("00000000-0000-0000-0000-000000000007")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000006"),
                            LessonId = new Guid("00000000-0000-0000-0000-000000000005"),
                            TeacherId = new Guid("00000000-0000-0000-0000-000000000007")
                        });
                });

            modelBuilder.Entity("ESchedule.Domain.Schedule.Rules.RuleModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("RuleJson")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("ScheduleRules");
                });

            modelBuilder.Entity("ESchedule.Domain.Tenant.TenantModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("TenantName")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("TenantName")
                        .IsUnique();

                    b.ToTable("Tenant");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            TenantName = "Tenant"
                        });
                });

            modelBuilder.Entity("ESchedule.Domain.Tenant.TenantSettingsModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<TimeSpan>("BreaksDurationTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("LessonDurationTime")
                        .HasColumnType("time");

                    b.Property<TimeSpan>("StudyDayStartTime")
                        .HasColumnType("time");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("TenantId")
                        .IsUnique();

                    b.ToTable("TenantSettings");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            BreaksDurationTime = new TimeSpan(0, 0, 10, 0, 0),
                            LessonDurationTime = new TimeSpan(0, 0, 45, 0, 0),
                            StudyDayStartTime = new TimeSpan(0, 8, 30, 0, 0),
                            TenantId = new Guid("00000000-0000-0000-0000-000000000001")
                        });
                });

            modelBuilder.Entity("ESchedule.Domain.Users.GroupModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("MaxLessonsCountPerDay")
                        .HasColumnType("int");

                    b.Property<Guid>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(5)
                        .HasColumnType("nvarchar(5)");

                    b.HasKey("Id");

                    b.HasIndex("TenantId");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            MaxLessonsCountPerDay = 5,
                            TenantId = new Guid("00000000-0000-0000-0000-000000000001"),
                            Title = "11-A"
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            MaxLessonsCountPerDay = 5,
                            TenantId = new Guid("00000000-0000-0000-0000-000000000001"),
                            Title = "11-B"
                        });
                });

            modelBuilder.Entity("ESchedule.Domain.Users.UserModel", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("FatherName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid?>("GroupId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("IsEmailConfirmed")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false);

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.Property<Guid?>("TenantId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.HasIndex("TenantId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000001"),
                            Age = 20,
                            FatherName = "Igorovich",
                            GroupId = new Guid("00000000-0000-0000-0000-000000000001"),
                            IsEmailConfirmed = true,
                            LastName = "Kuleshov",
                            Login = "admin@admin.com",
                            Name = "Mykyta",
                            Password = "uXZZJu84VhkcKKMR40jQXouLlccEmO80tP0082LdQYKXDDYO",
                            Role = 0,
                            TenantId = new Guid("00000000-0000-0000-0000-000000000001")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000002"),
                            Age = 21,
                            FatherName = "Igorovich2",
                            GroupId = new Guid("00000000-0000-0000-0000-000000000001"),
                            IsEmailConfirmed = true,
                            LastName = "Kuleshov2",
                            Login = "admin2@admin.com",
                            Name = "Mykyta2",
                            Password = "kO5wVz0/TdxuRG1n/J7dDg7L+aD64iKEInU/Ea/VK5pH5tQh",
                            Role = 0,
                            TenantId = new Guid("00000000-0000-0000-0000-000000000001")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000003"),
                            Age = 22,
                            FatherName = "Igorovich3",
                            GroupId = new Guid("00000000-0000-0000-0000-000000000002"),
                            IsEmailConfirmed = true,
                            LastName = "Kuleshov3",
                            Login = "admin3@admin.com",
                            Name = "Mykyta3",
                            Password = "F4vvhl54DenHqd+YF5LjNo1Fxe+XqapTzQOeNNT6TEIRDCAS",
                            Role = 0,
                            TenantId = new Guid("00000000-0000-0000-0000-000000000001")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000004"),
                            Age = 23,
                            FatherName = "Igorovich4",
                            GroupId = new Guid("00000000-0000-0000-0000-000000000002"),
                            IsEmailConfirmed = true,
                            LastName = "Kuleshov4",
                            Login = "admin4@admin.com",
                            Name = "Mykyta4",
                            Password = "guo5H0EI9b6tN65zZzSiv1+GMOKNNjOPy2S31cn0bKg4yOwV",
                            Role = 0,
                            TenantId = new Guid("00000000-0000-0000-0000-000000000001")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000005"),
                            Age = 40,
                            FatherName = "Teacher1",
                            IsEmailConfirmed = true,
                            LastName = "Teacher1",
                            Login = "teacher1@admin.com",
                            Name = "Teacher1",
                            Password = "e9DYht48mJmZJ7CsmiXCR7oAIb93zPReB8aJ90EfeTr3iPxp",
                            Role = 1,
                            TenantId = new Guid("00000000-0000-0000-0000-000000000001")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000006"),
                            Age = 40,
                            FatherName = "Teacher2",
                            IsEmailConfirmed = true,
                            LastName = "Teacher2",
                            Login = "teacher2@admin.com",
                            Name = "Teacher2",
                            Password = "9OJpQj7jCpzcJ0YHBurff7R/A2mEfN0XmKsyIuFhEugL+ftE",
                            Role = 1,
                            TenantId = new Guid("00000000-0000-0000-0000-000000000001")
                        },
                        new
                        {
                            Id = new Guid("00000000-0000-0000-0000-000000000007"),
                            Age = 40,
                            FatherName = "Teacher3",
                            IsEmailConfirmed = true,
                            LastName = "Teacher3",
                            Login = "teacher3@admin.com",
                            Name = "Teacher3",
                            Password = "v1RNrTwJFiua2Ys99reIqD+Z15GeASwHfbkhmlI3RsTEM80g",
                            Role = 1,
                            TenantId = new Guid("00000000-0000-0000-0000-000000000001")
                        });
                });

            modelBuilder.Entity("ESchedule.Domain.Lessons.LessonModel", b =>
                {
                    b.HasOne("ESchedule.Domain.Tenant.TenantModel", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("ESchedule.Domain.Lessons.Schedule.ScheduleModel", b =>
                {
                    b.HasOne("ESchedule.Domain.Lessons.LessonModel", "Lesson")
                        .WithMany("RelatedSchedules")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ESchedule.Domain.Users.GroupModel", "StudyGroup")
                        .WithMany("StudySchedules")
                        .HasForeignKey("StudyGroupId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ESchedule.Domain.Users.UserModel", "Teacher")
                        .WithMany("StudySchedules")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ESchedule.Domain.Tenant.TenantModel", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("StudyGroup");

                    b.Navigation("Teacher");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("ESchedule.Domain.ManyToManyModels.GroupsLessonsModel", b =>
                {
                    b.HasOne("ESchedule.Domain.Lessons.LessonModel", "Lesson")
                        .WithMany("StudingGroups")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ESchedule.Domain.Users.GroupModel", "StudyGroup")
                        .WithMany("StudingLessons")
                        .HasForeignKey("StudyGroupId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("StudyGroup");
                });

            modelBuilder.Entity("ESchedule.Domain.ManyToManyModels.TeachersGroupsLessonsModel", b =>
                {
                    b.HasOne("ESchedule.Domain.Lessons.LessonModel", "Lesson")
                        .WithMany()
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ESchedule.Domain.Users.GroupModel", "StudyGroup")
                        .WithMany("GroupTeachersLessons")
                        .HasForeignKey("StudyGroupId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ESchedule.Domain.Users.UserModel", "Teacher")
                        .WithMany("StudyGroups")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("StudyGroup");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("ESchedule.Domain.ManyToManyModels.TeachersLessonsModel", b =>
                {
                    b.HasOne("ESchedule.Domain.Lessons.LessonModel", "Lesson")
                        .WithMany("ResponsibleTeachers")
                        .HasForeignKey("LessonId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("ESchedule.Domain.Users.UserModel", "Teacher")
                        .WithMany("TaughtLessons")
                        .HasForeignKey("TeacherId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Lesson");

                    b.Navigation("Teacher");
                });

            modelBuilder.Entity("ESchedule.Domain.Schedule.Rules.RuleModel", b =>
                {
                    b.HasOne("ESchedule.Domain.Tenant.TenantModel", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("ESchedule.Domain.Tenant.TenantSettingsModel", b =>
                {
                    b.HasOne("ESchedule.Domain.Tenant.TenantModel", "Tenant")
                        .WithOne("Settings")
                        .HasForeignKey("ESchedule.Domain.Tenant.TenantSettingsModel", "TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("ESchedule.Domain.Users.GroupModel", b =>
                {
                    b.HasOne("ESchedule.Domain.Tenant.TenantModel", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("ESchedule.Domain.Users.UserModel", b =>
                {
                    b.HasOne("ESchedule.Domain.Users.GroupModel", "Group")
                        .WithMany("Members")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("ESchedule.Domain.Tenant.TenantModel", "Tenant")
                        .WithMany()
                        .HasForeignKey("TenantId")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("Group");

                    b.Navigation("Tenant");
                });

            modelBuilder.Entity("ESchedule.Domain.Lessons.LessonModel", b =>
                {
                    b.Navigation("RelatedSchedules");

                    b.Navigation("ResponsibleTeachers");

                    b.Navigation("StudingGroups");
                });

            modelBuilder.Entity("ESchedule.Domain.Tenant.TenantModel", b =>
                {
                    b.Navigation("Settings")
                        .IsRequired();
                });

            modelBuilder.Entity("ESchedule.Domain.Users.GroupModel", b =>
                {
                    b.Navigation("GroupTeachersLessons");

                    b.Navigation("Members");

                    b.Navigation("StudingLessons");

                    b.Navigation("StudySchedules");
                });

            modelBuilder.Entity("ESchedule.Domain.Users.UserModel", b =>
                {
                    b.Navigation("StudyGroups");

                    b.Navigation("StudySchedules");

                    b.Navigation("TaughtLessons");
                });
#pragma warning restore 612, 618
        }
    }
}