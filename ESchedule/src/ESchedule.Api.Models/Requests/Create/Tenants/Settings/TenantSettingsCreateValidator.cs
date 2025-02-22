using FluentValidation;

namespace ESchedule.Api.Models.Requests.Create.Tenants.Settings;

public class TenantSettingsCreateValidator : AbstractValidator<TenantSettingsCreateModel>
{
    public TenantSettingsCreateValidator()
    {
        RuleFor(x => x.StudyDayStartTime).NotEmpty();
        RuleFor(x => x.LessonDurationTime).NotEmpty();
        RuleFor(x => x.BreaksDurationTime).NotEmpty();
    }
}
