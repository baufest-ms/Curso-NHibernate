using Baufest.NHibernate.Dominio.Entidades;
using Baufest.NHibernate.HolaMundo.Overrides;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
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
                    MsSqlConfiguration.MsSql2012
                        .ConnectionString(x => x.FromConnectionStringWithKey("IntroNH")))
                 .Mappings(m => m.AutoMappings.Add(
                            AutoMap.AssemblyOf<Producto>()
                                .Where(t => t.Namespace == typeof(Producto).Namespace)
                                //.Conventions.AddFromAssemblyOf<ForeignKeyNameConvention>()
                                .UseOverridesFromAssemblyOf<VentaMappingOverride>()
                                ))
                 .ExposeConfiguration(cfg => new SchemaUpdate(cfg).Execute(false, true))
                 .BuildSessionFactory();
        }

        private static void GuardarVenta()
        {
            var sessionfactory = CrearSessionFactory();
            using (var session = sessionfactory.OpenSession())
            {
                var categoriaNotebooks = new Categoria { Nombre = "Notebooks" };
                session.Save(categoriaNotebooks);

                var categoriaAccesorios = new Categoria { Nombre = "Accesorios" };
                session.Save(categoriaAccesorios);

                var productoT470 = new Producto
                {
                    Nombre = "Lenovo T470",
                    Descripcion = "NOtebook Lenovo T470 Core i5, 16GB RAM, SSD 128GB",
                    Precio = 500,
                    Categoria = categoriaNotebooks
                };
                session.Save(productoT470);

                var productoMouse = new Producto
                {
                    Nombre = "Mouse Inalámbrico Logitech",
                    Descripcion = "Mouse Inalámbrico Logitech MT55",
                    Precio = 10,
                    Categoria = categoriaAccesorios
                };
                session.Save(productoMouse);

                var clienteBaufest = new Cliente { Nombre = "Baufest" };
                session.Save(clienteBaufest);

                var venta = new Venta { Cliente = clienteBaufest };
                venta.AgregarItem(productoT470, 5);
                venta.AgregarItem(productoMouse, 10);

                session.Save(venta);
            }
        }

        static void Main(string[] args)
        {
            GuardarVenta();
        }
    }
}
