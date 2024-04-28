using AutoMapper;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.Domain.Tenant;

namespace ESchedule.Business.GroupLessons
{
    internal class GroupLessonsService : BaseService<GroupsLessonsModel>
    {
        public GroupLessonsService(
            IRepository<GroupsLessonsModel> repository, 
            IMapper mapper, 
            ITenantContextProvider tenantContextProvider) 
            : base(repository, mapper, tenantContextProvider)
        {
        }

        public async override Task InsertMany<TCreateModel>(IEnumerable<TCreateModel> request)
        {
            if(request == null || !request.Any())
            {
                throw new ArgumentNullException(nameof(request));
            }

            var mapped = request.Select(x => {
                var item = _mapper.Map<GroupsLessonsModel>(x);
                item.TenantId = _tenantContextProvider.Current.TenantId;

                return item;
            });

            await _repository.InsertMany(mapped);
        }
    }
}
