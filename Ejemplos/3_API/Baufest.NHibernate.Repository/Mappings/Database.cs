using Baufest.NHibernate.Dominio.Entidades;
using Baufest.NHibernate.HolaMundo.Conventions;
using Baufest.NHibernate.HolaMundo.Overrides;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
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
            return Fluently
                 .Configure()
                 .Database(                
                        MsSqlConfiguration.MsSql2012.ConnectionString(x => x.FromConnectionStringWithKey("IntroNH")))
                 .Mappings(m => m.AutoMappings.Add(
                                    AutoMap.AssemblyOf<Producto>()
                                        .Where(t => t.Namespace == typeof(Producto).Namespace)
                                        .Conventions.AddFromAssemblyOf<PrimaryKeyConvention>()
                                        .UseOverridesFromAssemblyOf<VentaMappingOverride>()))
                 .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                 .BuildSessionFactory();
        }


    }
}