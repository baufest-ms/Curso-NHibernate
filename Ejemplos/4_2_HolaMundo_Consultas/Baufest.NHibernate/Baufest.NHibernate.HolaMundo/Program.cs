using Baufest.NHibernate.Dominio.Entidades;
using Baufest.NHibernate.HolaMundo.Conventions;
using Baufest.NHibernate.HolaMundo.Overrides;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Criterion;
using NHibernate.Tool.hbm2ddl;
using System.Linq;
using System.Transactions;

namespace Baufest.NHibernate.HolaMundo
{
    class Program
    {
        private static ISessionFactory sessionFactory = CrearSessionFactory();

        private static ISessionFactory CrearSessionFactory()
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

        private static void GuardarProducto()
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var transacion = session.BeginTransaction())
                {

                    var categoriaNotebooks = new Categoria { Nombre = "Notebooks" };
                    session.Save(categoriaNotebooks);

                    var productoT470 = new Producto
                    {
                        Nombre = "Lenovo T470",
                        Descripcion = "Notebook Lenovo T470 Core i5, 16GB RAM, SSD 128GB",
                        Precio = 500,
                        Categoria = categoriaNotebooks
                    };
                    session.Save(productoT470);

                    transacion.Commit();
                }
            }
        }

        private static void GuardarProductoTransactionScope()
        {
            using (var scope = new TransactionScope())
            {
                using (var session = sessionFactory.OpenSession())
                {
                    var categoriaNotebooks = new Categoria { Nombre = "Notebooks" };
                    session.Save(categoriaNotebooks);

                    var productoT470 = new Producto
                    {
                        Nombre = "Lenovo T470",
                        Descripcion = "Notebook Lenovo T470 Core i5, 16GB RAM, SSD 128GB",
                        Precio = 500,
                        Categoria = categoriaNotebooks
                    };
                    session.Save(productoT470);
                }

                scope.Complete();
            }
        }

        private static void GuardarVenta()
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var transacion = session.BeginTransaction())
                {

                    var categoriaNotebooks = new Categoria { Nombre = "Notebooks" };
                    session.Save(categoriaNotebooks);

                    var categoriaAccesorios = new Categoria { Nombre = "Accesorios" };
                    session.Save(categoriaAccesorios);

                    var productoT470 = new Producto
                    {
                        Nombre = "Lenovo T470",
                        Descripcion = "Notebook Lenovo T470 Core i5, 16GB RAM, SSD 128GB",
                        Precio = 500,
                        Categoria = categoriaNotebooks
                    };
                    session.Save(productoT470);

                    var productoT480 = new Producto
                    {
                        Nombre = "Lenovo T480",
                        Descripcion = "Notebook Lenovo T480 Core i5, 8GB RAM, SSD 128GB",
                        Precio = 500,
                        Categoria = categoriaNotebooks
                    };
                    session.Save(productoT480);

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
                    transacion.Commit();
                }
            }
        }

        private static void BuscarVentasHQL()
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var transacion = session.BeginTransaction())
                {

                    var ventasBaufest = session.CreateQuery("from Venta venta where venta.Cliente.Nombre = 'Baufest'").List<Venta>();


                    var ventasMasDeUnItem = session.CreateQuery("from Venta venta where size(venta.Items) > 1").List<Venta>();


                    var productosNotebook = session.CreateQuery("from Producto prod where prod.Categoria.Id = :idCategoria order by prod.Nombre")
                        .SetParameter("idCategoria", 1)    
                        .List<Producto>();


                    var productoId = session.CreateQuery("select prod.Id from Producto prod where prod.Nombre like '%T480%'").UniqueResult<int>();

                    transacion.Commit();
                }
            }
        }

        private static void BuscarVentasCriteria()
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var transacion = session.BeginTransaction())
                {
                    var ventasBaufest = session.CreateCriteria<Venta>("venta")
                        .CreateAlias("venta.Cliente", "cliente")
                        .Add(Restrictions.Eq("cliente.Nombre", "Baufest"))
                        .List<Venta>();

                    var productosNotebook = session.CreateCriteria<Producto>("prod")
                        .CreateAlias("prod.Categoria", "cat")
                        .Add(Restrictions.Eq("cat.Id", 1))
                        .AddOrder(Order.Asc("Nombre"))
                        .List<Producto>();

                    var productoId = session.CreateCriteria<Producto>("prod")
                        .Add(Restrictions.Like("Nombre", "T480", MatchMode.Anywhere))
                        .SetProjection(Projections.Property("Id"))
                        .UniqueResult<int>();

                    transacion.Commit();   
                }
            }
        }

        private static void BuscarVentasQueryOver()
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var transacion = session.BeginTransaction())
                {
                    var ventasBaufest = session.QueryOver<Venta>()
                        .JoinQueryOver(venta => venta.Cliente)
                        .Where(cliente => cliente.Nombre == "Baufest")
                        .List();

                    var productosNotebook = session.QueryOver<Producto>()
                        .OrderBy(prod => prod.Nombre).Asc()
                        .JoinQueryOver(prod => prod.Categoria)
                        .Where(cat => cat.Id == 1)
                        .List();

                    var productoId = session.QueryOver<Producto>()
                        .WhereRestrictionOn(prod => prod.Nombre).IsLike("T480", MatchMode.Anywhere)
                        .Select(prod => prod.Id)
                        .SingleOrDefault<int>();

                    transacion.Commit();
                }
            }
        }

        private static void BuscarVentasLINQ()
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var transacion = session.BeginTransaction())
                {

                    var ventasBaufest = session.Query<Venta>()
                        .Where(venta => venta.Cliente.Nombre == "Baufest")
                        .ToList();

                    var ventasMasDeUnItem = session.Query<Venta>()
                        .Where(venta => venta.Items.Count > 1)
                        .ToList();

                    var idCategoria = 1;
                    var productosNotebook = session.Query<Producto>()
                        .Where(prod => prod.Categoria.Id == idCategoria)
                        .OrderBy(prod => prod.Nombre);

                    var productoId = session.Query<Producto>()
                        .Where(prod => prod.Nombre.Contains("T480"))
                        .Select(prod => prod.Id)
                        .SingleOrDefault();
                        
                    transacion.Commit();
                }
            }
        }

        private static void BuscarVentasSQL()
        {
            using (var session = sessionFactory.OpenSession())
            {
                using (var transacion = session.BeginTransaction())
                {

                    var ventasBaufest = session.CreateSQLQuery(
                        @"SELECT venta.Id, venta.Cliente_id 
                            FROM Venta venta 
                            INNER JOIN Cliente cliente ON venta.Cliente_id = cliente.Id 
                            WHERE cliente.Nombre = 'Baufest'").AddEntity(typeof(Venta)).List<Venta>();

                    var ventasMasDeUnItem = session.CreateSQLQuery(
                        @"SELECT venta.Id, venta.Cliente_id 
                            FROM Venta venta 
                            WHERE (SELECT COUNT(item.Id) FROM Item item WHERE item.Venta_id = venta.Id) > 1")
                        .AddEntity(typeof(Venta)).List<Venta>();


                    var productosNotebook = session.CreateSQLQuery(
                        @"SELECT prod.Id, prod.Nombre, prod.Descripcion, prod.Precio, prod.Categoria_id
                            FROM Producto prod 
                            WHERE prod.Categoria_id = :idCategoria 
                            ORDER BY prod.Nombre").AddEntity(typeof(Producto)).SetParameter("idCategoria", 1).List<Producto>();

                    var productoId = session.CreateQuery(
                        @"SELECT prod.Id 
                            FROM Producto prod 
                            WHERE prod.Nombre LIKE '%T480%'").UniqueResult<int>();

                    session.CreateQuery("EXEC ActualizarPrecios @Porcentaje = :porcentaje")
                        .SetParameter("porcentaje", 1)
                        .ExecuteUpdate();

                    transacion.Commit();
                }
            }
        }

        static void Main(string[] args)
        {
            GuardarVenta();
            //BuscarVentasHQL();
            //BuscarVentasCriteria();
            //BuscarVentasQueryOver();
            BuscarVentasSQL();
        }
    }
}
