namespace ESchedule.Api.Models.Requests.Update;

public abstract record BaseUpdateModel
{
    public Guid Id { get; set; }
}
