using Baufest.NHibernate.Dominio.Entidades;
using Baufest.NHibernate.HolaMundo.Mappings;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace Baufest.NHibernate.HolaMundo
{
    class Program
    {
        public static ISessionFactory CrearSessionFactory()
        {         
            return Fluently
                 .Configure()
                 .Database(
                     MsSqlConfiguration.MsSql2012.ConnectionString(
                            x => x.FromConnectionStringWithKey("IntroNH")))

                 .Mappings(m => m.FluentMappings.AddFromAssemblyOf<ProductoMap>())
                 .ExposeConfiguration(cfg => {
                     cfg.SetProperty(Environment.DefaultSchema, "dbo");
                     cfg.SetProperty(Environment.Hbm2ddlKeyWords, "auto-quote");
                     })
                 .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(true, true))
                 .BuildSessionFactory();
        }

        static void Main(string[] args)
        {
            var sessionfactory = CrearSessionFactory();
            using(var session = sessionfactory.OpenSession())
            {
                var categoria = new Categoria { Nombre = "Notebooks" };
                session.Save(categoria);

                var producto = new Producto
                {
                    Nombre = "Lenovo T470",
                    Descripcion = "Laptop Lenovo T470 Core i5, 16GB RAM, SSD 128GB",
                    Precio = 500,
                    Categoria = categoria
                };

                session.Save(producto);
            }
        }
    }
}
