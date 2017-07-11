using Homework.Database.DAL.GenericRepository;
using Homework.Database.Entities;
using System;

namespace Homework.Database.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<City> CityRepository { get; }

        IGenericRepository<Forecast> ForecastRepository { get; }

        IGenericRepository<HistoryQuery> HistoryRepository { get; }

        void Save();
    }
}
