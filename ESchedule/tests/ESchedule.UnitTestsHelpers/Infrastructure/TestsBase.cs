namespace ESchedule.UnitTestsHelpers.Infrastructure;

public abstract class TestBase<TSut> where TSut : class
{
    protected TSut Sut { get; set; }

    protected abstract TSut GetNewSut();
}