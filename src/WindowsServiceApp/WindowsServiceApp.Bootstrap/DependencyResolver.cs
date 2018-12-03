using Autofac;
using System.Collections.Specialized;
using System.Configuration;
using WindowsServiceApp.Infrastructure;
using WindowsServiceApp.Infrastructure.Interfaces;
using WindowsServiceApp.Mongo;

namespace WindowsServiceApp.Bootstrap
{
    public class DependencyResolver
    {
        private ContainerBuilder _builder;
        private IContainer _container;

        private IContainer container
        {
            get => _container ?? (_container = _builder.Build());
        }

        public DependencyResolver()
        {
            _builder = new ContainerBuilder();
        }

        public TService GetService<TService>()
        {
            return container.Resolve<TService>();
        }

        public IRepository<T> GetRepository<T>(string collectionName) where T : class
        {
            var repo = container.Resolve(typeof(IRepository<T>), new NamedParameter("dbConnection", GetService<IDbConnection>()),
                new NamedParameter("collectionName", collectionName)) as IRepository<T>;
            return repo;
        }

        public IEventLogReader GetLogReader(string eventLogName)
        {
            return container.Resolve<IEventLogReader>(new NamedParameter("logName", eventLogName));
        }

        public IEmailSender GetEmailSender()
        {
            var config = container.Resolve<IConfigurationService>();
            return container.Resolve<IEmailSender>(new NamedParameter("smtpConfiguration", config.GetSmtpConfiguration()));
        }

        public void ConfigureServices()
        {
            _builder.RegisterType<MarkupBuilder>().As<IMarkupBuilder>().SingleInstance();
            _builder.RegisterType<EventLogReader>().As<IEventLogReader>().SingleInstance();
            _builder.RegisterType<EmailSender>().As<IEmailSender>().SingleInstance();
        }

        public void ConfigureDatabase(string dbServer, string dbName)
        {
            _builder.Register(d => new DbConnection(dbServer, dbName))
                .As<IDbConnection>()
                .SingleInstance();

            _builder.RegisterGeneric(typeof(Repository<>)).As(typeof(IRepository<>))
                .SingleInstance();
        }

        public void ConfigureAppConfig(NameValueCollection appSettings, ConnectionStringSettingsCollection connectionStrings)
        {
            _builder.Register(c => new ConfigurationService(appSettings, connectionStrings))
                .As<IConfigurationService>()
                .SingleInstance();
        }
    }
}
