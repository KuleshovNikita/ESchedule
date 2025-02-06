namespace ESchedule.Api.Models.Updates;

public abstract record BaseUpdateModel
{
    public Guid Id { get; set; }
}
