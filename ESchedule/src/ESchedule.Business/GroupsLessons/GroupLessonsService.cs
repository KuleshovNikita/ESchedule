using ESchedule.Business.Mappers;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.Domain.Tenant;

namespace ESchedule.Business.GroupsLessons;

public class GroupLessonsService(
    IRepository<GroupsLessonsModel> repository,
    IMainMapper mapper,
    ITenantContextProvider tenantContextProvider
)
    : BaseService<GroupsLessonsModel>(repository, mapper)
{
    public async override Task InsertMany<TCreateModel>(IEnumerable<TCreateModel> request)
    {
        if (request == null || !request.Any())
        {
            throw new ArgumentNullException(nameof(request));
        }

        var mapped = request.Select(x =>
        {
            var item = Mapper.Map<GroupsLessonsModel>(x);
            item.TenantId = tenantContextProvider.Current.TenantId;

            return item;
        });

        await Repository.InsertMany(mapped);
    }
}