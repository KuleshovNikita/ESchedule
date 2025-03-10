﻿using ESchedule.DataAccess.Context;
using ESchedule.Domain.Exceptions;
using ESchedule.Domain.Lessons;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ESchedule.DataAccess.Repos.Lessons;

public class LessonRepository(TenantEScheduleDbContext context) : Repository<LessonModel>(context)
{
    public override async Task<LessonModel> FirstOrDefault(Expression<Func<LessonModel, bool>> command)
        => await GetContext<LessonModel>()
                .Include(x => x.StudingGroups)
                .Include(x => x.ResponsibleTeachers)
                .Include(x => x.RelatedSchedules)
                .Include(x => x.Tenant)
                .FirstOrDefaultAsync(command) ?? throw new EntityNotFoundException();

    public override async Task<IEnumerable<LessonModel>> Where(Expression<Func<LessonModel, bool>> command)
        => await GetContext<LessonModel>()
                .Include(x => x.StudingGroups)
                .Include(x => x.ResponsibleTeachers)
                .Include(x => x.RelatedSchedules)
                .Include(x => x.Tenant)
                .Where(command)
                .ToListAsync()
                    ?? throw new EntityNotFoundException();
}
