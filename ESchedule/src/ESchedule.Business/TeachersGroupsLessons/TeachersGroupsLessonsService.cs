using ESchedule.DataAccess.Repos;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.Domain.Tenant;
using PowerInfrastructure.AutoMapper;

namespace ESchedule.Business.TeachersGroupsLessons;

public class TeachersGroupsLessonsService(
    IRepository<TeachersGroupsLessonsModel> repository,
    IMainMapper mapper,
    ITenantContextProvider tenantContextProvider
) 
    : BaseService<TeachersGroupsLessonsModel>(repository, mapper)
{
    public async override Task InsertMany<TCreateModel>(IEnumerable<TCreateModel> request)
    {
        if (request == null || !request.Any())
        {
            throw new ArgumentNullException(nameof(request));
        }

        var mapped = request.Select(x => {
            var item = Mapper.Map<TeachersGroupsLessonsModel>(x);
            item.TenantId = tenantContextProvider.Current.TenantId;

            return item;
        });

        await Repository.InsertMany(mapped);
    }
}