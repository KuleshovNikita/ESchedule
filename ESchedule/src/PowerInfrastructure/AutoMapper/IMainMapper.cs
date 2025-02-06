using AutoMapper;

namespace PowerInfrastructure.AutoMapper;

public interface IMainMapper : IMapper
{
    public TDest MapOnlyUpdatedProperties<TFrom, TDest>(TFrom from, TDest to)
        where TFrom : class
        where TDest : class;
}