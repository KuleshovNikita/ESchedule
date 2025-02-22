using ESchedule.Api.Models.Extensions;

namespace ESchedule.Api.Models.Requests.Update.Tenants.Settings;

public class TenantSettingsUpdateValidator : BaseUpdateValidator<TenantSettingsUpdateModel>
{
    public TenantSettingsUpdateValidator()
    {
        this.NotEmptyUnlessNull(x => x.StudyDayStartTime);
        this.NotEmptyUnlessNull(x => x.LessonDurationTime);
        this.NotEmptyUnlessNull(x => x.BreaksDurationTime);
        this.NotEmptyUnlessNull(x => x.CreatorId);
        this.NotEmptyUnlessNull(x => x.TenantId);
    }
}
