using AutoMapper;
using ESchedule.Business.Mappers;
using ESchedule.DataAccess.Repos;
using ESchedule.Domain.ManyToManyModels;
using ESchedule.Domain.Tenant;

namespace ESchedule.Business.TeachersLessons
{
    public class TeachersLessonsService : BaseService<TeachersLessonsModel>
    {
        protected readonly ITenantContextProvider _tenantContextProvider;

        public TeachersLessonsService(
            IRepository<TeachersLessonsModel> repository,
            IMainMapper mapper,
            ITenantContextProvider tenantContextProvider)
            : base(repository, mapper)
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
                var item = _mapper.Map<TeachersLessonsModel>(x);
                item.TenantId = _tenantContextProvider.Current.TenantId;

                return item;
            });

            await _repository.InsertMany(mapped);
        }
    }
}
