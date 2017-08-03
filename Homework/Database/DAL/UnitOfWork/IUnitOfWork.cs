using Homework.Database.DAL.GenericRepository;
using Homework.Database.Entities;
using System;
using System.Threading.Tasks;

namespace Homework.Database.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<City> CityRepository { get; }

        IGenericRepository<Forecast> ForecastRepository { get; }

        IGenericRepository<HistoryQuery> HistoryRepository { get; }

        Task Save();
    }
}
