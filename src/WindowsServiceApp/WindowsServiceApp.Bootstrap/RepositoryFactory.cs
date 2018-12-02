using Autofac;
using WindowsServiceApp.Infrastructure.Interfaces;

namespace WindowsServiceApp.Bootstrap
{
    public class RepositoryFactory
    {
        private readonly IContainer container;

        public RepositoryFactory(IContainer container)
        {
            this.container = container;
        }

        public IRepository<T> Create<T>(IDbConnection dbConnection, string collectionName) where T : class
        {
            return container.Resolve<IRepository<T>>(new NamedParameter("dbConnection", dbConnection),
                new NamedParameter("collectionName", collectionName));
        }
    }
}
