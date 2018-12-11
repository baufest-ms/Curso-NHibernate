using NHibernate;
using System;

namespace Baufest.NHibernate.Repository.Mappings
{
    public static class Database
    {
        private static readonly Lazy<ISessionFactory> sessionFactory = new Lazy<ISessionFactory>(CreateSessionFactory);

        public static ISessionFactory SessionFactory
        {
            get { return sessionFactory.Value; }
        }

        private static ISessionFactory CreateSessionFactory()
        {
            throw new NotImplementedException("Implementar la configuración de NHibernate");
        }


    }
}