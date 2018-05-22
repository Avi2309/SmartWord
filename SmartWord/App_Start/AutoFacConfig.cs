using Autofac;
using Autofac.Integration.WebApi;
using Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using DataAccess;
using DataAccess.Contracts;
using Domain.Services;
using Services.Helpers;
using Services.Services;

namespace SmartWord.App_Start
{
    public class AutofacWebapiConfig
    {

        public static IContainer Container;

        public static void Initialize(HttpConfiguration config)
        {
            Initialize(config, RegisterServices(new ContainerBuilder()));
        }


        public static void Initialize(HttpConfiguration config, IContainer container)
        {
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }

        private static IContainer RegisterServices(ContainerBuilder builder)
        {
            //Register your Web API controllers.  
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<ApiService>()
                .As<IApiService>()
                .SingleInstance();

            builder.RegisterType<StatisticsService>()
                .As<IStatisticsService>()
                .SingleInstance();

            builder.RegisterType<StorageService>()
                .As<IStorageService>()
                .SingleInstance();

            builder.RegisterType<HttpService>()
                .As<IHttpService>()
                .SingleInstance();

            builder.RegisterType<WordStatRepository>()
                .As<IWordsStatRepository>()
                .SingleInstance();

            //Set the dependency resolver to be Autofac.  
            Container = builder.Build();

            return Container;
        }

    }
}