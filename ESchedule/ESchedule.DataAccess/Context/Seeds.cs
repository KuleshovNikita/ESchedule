using ESchedule.Core.Interfaces;
using ESchedule.Domain.Enums;
using ESchedule.Domain.Lessons;
using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace ESchedule.DataAccess.Context
{
    public static class Seeds
    {
        private static ModelBuilder _builder = null!;
        private static IPasswordHasher _passwordHasher = null!;

        public static void ApplySeeds(ModelBuilder builder, IPasswordHasher passwordHasher)
        {
            _passwordHasher = passwordHasher;
            _builder = builder;

            ApplyUsers();
            ApplyGroups();
            ApplyLessons();
            ApplyTenantSettings();
            ApplyTenant();
            ApplyTeachersGroupsLessons();
            ApplyTeachersLessons();
            ApplyGroupsLessons();
            ApplySchedules();
        }

        private static void ApplySchedules()
        {
            var schedules = new ScheduleModel
            {
                Id = Guid.Parse("e463e458-599b-4601-a50e-c52633c44c69"),
                StartTime = new TimeSpan(0, 0, 1),
                EndTime = new TimeSpan(23, 59, 59),
                DayOfWeek = DayOfWeek.Monday,
                StudyGroupId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                TeacherId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                LessonId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                TenantId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
            };

            _builder.Entity<ScheduleModel>().HasData(schedules);
        }

        private static void ApplyTenant()
        {
            var tenants = new TenantModel
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                TenantName = "Tenant",
            };

            _builder.Entity<TenantModel>().HasData(tenants);
        }

        private static void ApplyGroupsLessons()
        {
            var entities = new GroupsLessonsModel[]
            {
                new GroupsLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    StudyGroupId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                },
                new GroupsLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    StudyGroupId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                },
                new GroupsLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    StudyGroupId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                },
                new GroupsLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    StudyGroupId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                },
                new GroupsLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    StudyGroupId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                },
                new GroupsLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    StudyGroupId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                },
                new GroupsLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    StudyGroupId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                },
                new GroupsLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    StudyGroupId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                },
                new GroupsLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    StudyGroupId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                },
                new GroupsLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    StudyGroupId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                }
            };

            _builder.Entity<GroupsLessonsModel>().HasData(entities);
        }

        private static void ApplyTeachersLessons()
        {
            var entities = new TeachersLessonsModel[]
            {
                new TeachersLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    TeacherId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                },
                new TeachersLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    TeacherId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                },
                new TeachersLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    TeacherId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                },
                new TeachersLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    TeacherId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                },
                new TeachersLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    TeacherId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                },
                new TeachersLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    TeacherId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                }
            };

            _builder.Entity<TeachersLessonsModel>().HasData(entities);
        }

        private static void ApplyTeachersGroupsLessons()
        {
            var entities = new TeachersGroupsLessonsModel[]
            {
                new TeachersGroupsLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    StudyGroupId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    TeacherId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                },
                new TeachersGroupsLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    StudyGroupId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    TeacherId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                },
                new TeachersGroupsLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    StudyGroupId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    TeacherId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                },
                new TeachersGroupsLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    StudyGroupId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    TeacherId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                },
                new TeachersGroupsLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    StudyGroupId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    TeacherId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                },
                new TeachersGroupsLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    StudyGroupId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    TeacherId = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                },
                new TeachersGroupsLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    StudyGroupId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    TeacherId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                },
                new TeachersGroupsLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000008"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    StudyGroupId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    TeacherId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                },
                new TeachersGroupsLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000009"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    StudyGroupId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    TeacherId = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                },
                new TeachersGroupsLessonsModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000010"),
                    LessonId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    StudyGroupId = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    TeacherId = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                },
            };

            _builder.Entity<TeachersGroupsLessonsModel>().HasData(entities);
        }

        private static void ApplyTenantSettings()
        {
            var settings = new TenantSettingsModel
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                TenantId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                BreaksDurationTime = new TimeSpan(0, 10, 0),
                StudyDayStartTime = new TimeSpan(8, 30, 0),
                LessonDurationTime = new TimeSpan(0, 45, 0),
            };

            _builder.Entity<TenantSettingsModel>().HasData(settings);
        }

        private static void ApplyLessons()
        {
            var lessons = new LessonModel[]
            {
                new LessonModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Title = "Math",
                    TenantId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                },
                new LessonModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Title = "Physics",
                    TenantId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                },
                new LessonModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    Title = "Chemistry",
                    TenantId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                },
                new LessonModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    Title = "Literature",
                    TenantId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                },
                new LessonModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    Title = "PE",
                    TenantId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                }
            };

            _builder.Entity<LessonModel>().HasData(lessons);
        }

        private static void ApplyGroups()
        {
            var groups = new GroupModel[]
            {
                new GroupModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    MaxLessonsCountPerDay = 5,
                    TenantId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Title = "11-A",
                },
                new GroupModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    MaxLessonsCountPerDay = 5,
                    TenantId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Title = "11-B",
                }
            };

            _builder.Entity<GroupModel>().HasData(groups);
        }

        private static void ApplyUsers()
        {
            var users = new UserModel[]
            {
                new UserModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    Age = 20,
                    Name = "Mykyta",
                    LastName = "Kuleshov",
                    FatherName = "Igorovich",
                    Login = "admin@admin.com",
                    Password = _passwordHasher.HashPassword("admin"),
                    Role = Role.Pupil,
                    IsEmailConfirmed = true,
                    TenantId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    GroupId = Guid.Parse("00000000-0000-0000-0000-000000000001")
                },
                new UserModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000002"),
                    Age = 21,
                    Name = "Mykyta2",
                    LastName = "Kuleshov2",
                    FatherName = "Igorovich2",
                    Login = "admin2@admin.com",
                    Password = _passwordHasher.HashPassword("admin"),
                    Role = Role.Pupil,
                    IsEmailConfirmed = true,
                    TenantId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    GroupId = Guid.Parse("00000000-0000-0000-0000-000000000001")
                },
                new UserModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    Age = 22,
                    Name = "Mykyta3",
                    LastName = "Kuleshov3",
                    FatherName = "Igorovich3",
                    Login = "admin3@admin.com",
                    Password = _passwordHasher.HashPassword("admin"),
                    Role = Role.Pupil,
                    IsEmailConfirmed = true,
                    TenantId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    GroupId = Guid.Parse("00000000-0000-0000-0000-000000000002")
                },
                new UserModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000004"),
                    Age = 23,
                    Name = "Mykyta4",
                    LastName = "Kuleshov4",
                    FatherName = "Igorovich4",
                    Login = "admin4@admin.com",
                    Password = _passwordHasher.HashPassword("admin"),
                    Role = Role.Pupil,
                    IsEmailConfirmed = true,
                    TenantId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    GroupId = Guid.Parse("00000000-0000-0000-0000-000000000002")
                },
                //Teachers
                new UserModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000005"),
                    Age = 40,
                    Name = "Teacher1",
                    LastName = "Teacher1",
                    FatherName = "Teacher1",
                    Login = "teacher1@admin.com",
                    Password = _passwordHasher.HashPassword("admin"),
                    Role = Role.Teacher,
                    IsEmailConfirmed = true,
                    TenantId = Guid.Parse("00000000-0000-0000-0000-000000000001")
                },
                new UserModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000006"),
                    Age = 40,
                    Name = "Teacher2",
                    LastName = "Teacher2",
                    FatherName = "Teacher2",
                    Login = "teacher2@admin.com",
                    Password = _passwordHasher.HashPassword("admin"),
                    Role = Role.Teacher,
                    IsEmailConfirmed = true,
                    TenantId = Guid.Parse("00000000-0000-0000-0000-000000000001")
                },
                new UserModel
                {
                    Id = Guid.Parse("00000000-0000-0000-0000-000000000007"),
                    Age = 40,
                    Name = "Teacher3",
                    LastName = "Teacher3",
                    FatherName = "Teacher3",
                    Login = "teacher3@admin.com",
                    Password = _passwordHasher.HashPassword("admin"),
                    Role = Role.Teacher,
                    IsEmailConfirmed = true,
                    TenantId = Guid.Parse("00000000-0000-0000-0000-000000000001")
                }
            };

            _builder.Entity<UserModel>().HasData(users);
        }
    }
}
