using AutoMapper;
using System.Linq.Expressions;

namespace ESchedule.UnitTestsHelpers.MockImplementations;

public class TestConfigurationProvider : IConfigurationProvider
{
    public void AssertConfigurationIsValid()
    {
        throw new NotImplementedException();
    }

    public LambdaExpression BuildExecutionPlan(Type sourceType, Type destinationType)
    {
        throw new NotImplementedException();
    }

    public void CompileMappings()
    {
        throw new NotImplementedException();
    }

    public IMapper CreateMapper()
    {
        throw new NotImplementedException();
    }

    public IMapper CreateMapper(Func<Type, object> serviceCtor)
    {
        throw new NotImplementedException();
    }
}
