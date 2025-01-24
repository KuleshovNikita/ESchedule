using AutoMapper;
using ESchedule.Api.Models.Requests;
using ESchedule.Api.Models.Responses;
using ESchedule.Api.Models.Updates;
using ESchedule.Domain.Lessons.Schedule;
using ESchedule.Domain.Lessons;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.Domain.Schedule.Rules;
using ESchedule.Domain.Tenant;
using ESchedule.Domain.Users;

namespace ESchedule.Business.Mappers.Profiles;

public class MainMapperProfile : Profile
{
    public MainMapperProfile()
    {
        CreateMap<UserCreateModel, UserModel>();
        CreateMap<UserUpdateModel, UserModel>();
        CreateMap<UserModel, UserUpdateModel>();

        CreateMap<GroupCreateModel, GroupModel>();
        CreateMap<GroupUpdateModel, GroupModel>();

        CreateMap<LessonCreateModel, LessonModel>();
        CreateMap<LessonUpdateModel, LessonModel>();

        CreateMap<TenantUpdateModel, TenantModel>();
        CreateMap<TenantCreateModel, TenantModel>();
        CreateMap<TenantSettingsUpdateModel, TenantSettingsModel>();
        CreateMap<TenantSettingsCreateModel, TenantSettingsModel>();
        CreateMap<RequestTenantAccessCreateModel, RequestTenantAccessModel>();

        CreateMap<ScheduleUpdateModel, ScheduleModel>();
        CreateMap<ScheduleCreateModel, ScheduleModel>();
        CreateMap<ScheduleModel, ScheduleItemResponse>()
            .ForPath(x => x.Teacher.Name, o => o.MapFrom(x => x.Teacher.Name))
            .ForPath(x => x.Teacher.LastName, o => o.MapFrom(x => x.Teacher.LastName))
            .ForPath(x => x.Teacher.FatherName, o => o.MapFrom(x => x.Teacher.FatherName))
            .ForMember(x => x.GroupName, o => o.MapFrom(x => x.StudyGroup.Title))
            .ForMember(x => x.LessonName, o => o.MapFrom(x => x.Lesson.Title));

        CreateMap<RuleInputModel, RuleModel>();

        CreateMap<TeachersGroupsLessonsCreateModel, TeachersGroupsLessonsModel>();
        CreateMap<TeachersLessonsCreateModel, TeachersLessonsModel>();
        CreateMap<GroupsLessonsCreateModel, GroupsLessonsModel>();
    }
}
