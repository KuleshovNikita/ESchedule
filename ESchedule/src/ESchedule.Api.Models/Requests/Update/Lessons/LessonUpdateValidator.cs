using ESchedule.Api.Models.Extensions;
using FluentValidation;

namespace ESchedule.Api.Models.Requests.Update.Lessons;

public class LessonUpdateValidator : BaseUpdateValidator<LessonUpdateModel>
{
    public LessonUpdateValidator()
    {
        this.NotEmptyUnlessNull(x => x.Title)
            .MaximumLength(30);
        this.NotEmptyUnlessNull(x => x.TenantId);
    }
}
