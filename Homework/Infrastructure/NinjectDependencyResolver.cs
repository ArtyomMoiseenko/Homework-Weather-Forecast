using Homework.Database;
using Homework.Database.DAL.GenericRepository;
using Homework.Database.DAL.UnitOfWork;
using Homework.Database.Entities;
using Homework.Filters;
using Homework.Services;
using Ninject;
using Ninject.Web.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Configuration;
using System.Web.Mvc;

namespace Homework.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;

        public NinjectDependencyResolver(IKernel kernelParam)
        {
            kernel = kernelParam;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            kernel.Bind<IConfiguration>().To<Configuration>()
                .WithPropertyValue("BaseUrl", WebConfigurationManager.AppSettings["BaseUrl"])
                .WithPropertyValue("ApiKey", WebConfigurationManager.AppSettings["ApiKey"]);
            kernel.Bind<IWeatherService>().To<WeatherService>();
            kernel.Bind<DbContext>().To<ForecastWeatherContext>();
            kernel.Bind(typeof(IGenericRepository<>)).To(typeof(GenericRepository<>));
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>();
        }
    }
}