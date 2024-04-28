using AutoMapper;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.Domain.Tenant;

namespace ESchedule.Business.TeachersGroupsLessons
{
    public class TeachersGroupsLessonsService : BaseService<TeachersGroupsLessonsModel>
    {
        protected readonly ITenantContextProvider _tenantContextProvider;

        public TeachersGroupsLessonsService(
            IRepository<TeachersGroupsLessonsModel> repository, 
            IMapper mapper, 
            ITenantContextProvider tenantContextProvider) : base(repository, mapper)
        {
            _tenantContextProvider = tenantContextProvider;
        }

        public async override Task InsertMany<TCreateModel>(IEnumerable<TCreateModel> request)
        {
            if (request == null || !request.Any())
            {
                throw new ArgumentNullException(nameof(request));
            }

            var mapped = request.Select(x => {
                var item = _mapper.Map<TeachersGroupsLessonsModel>(x);
                item.TenantId = _tenantContextProvider.Current.TenantId;

                return item;
            });

            await _repository.InsertMany(mapped);
        }
    }
}
