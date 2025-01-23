namespace ESchedule.UnitTestsHelpers.Infrastructure;

public abstract class TestBase<TSut> where TSut : class
{
    protected abstract TSut GetNewSut();
}
