using Homework.Database.DAL.GenericRepository;
using Homework.Database.Entities;
using System;

namespace Homework.Database.DAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private ForecastWeatherContext _context = new ForecastWeatherContext();
        private IGenericRepository<City> _cityRepository;
        private IGenericRepository<Forecast> _forecastRepository;
        private IGenericRepository<HistoryQuery> _historyRepository;
        private bool _disposed = false;

        public IGenericRepository<City> CityRepository
        {
            get
            {
                if (this._cityRepository == null)
                {
                    this._cityRepository = new GenericRepository<City>(_context);
                }
                return _cityRepository;
            }
        }

        public IGenericRepository<Forecast> ForecastRepository
        {
            get
            {
                if (this._forecastRepository == null)
                {
                    this._forecastRepository = new GenericRepository<Forecast>(_context);
                }
                return _forecastRepository;
            }
        }

        public IGenericRepository<HistoryQuery> HistoryRepository
        {
            get
            {
                if (this._historyRepository == null)
                {
                    this._historyRepository = new GenericRepository<HistoryQuery>(_context);
                }
                return _historyRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}