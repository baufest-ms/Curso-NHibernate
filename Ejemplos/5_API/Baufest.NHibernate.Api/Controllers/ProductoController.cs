using Baufest.NHibernate.Api.Models;
using Baufest.NHibernate.Dominio.Entidades;
using Baufest.NHibernate.Repository.Mappings;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Http;

namespace Baufest.NHibernate.Api.Controllers
{
    public class ProductoController : ApiController
    {
        // GET: api/Producto
        public IEnumerable<ProductoDto> Get([FromUri]FiltroProducto filtro)
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                var nombre = filtro.Nombre ?? string.Empty;
                var descripcion = filtro.Descripcion ?? string.Empty;

                var productos = session
                    .CreateSQLQuery(
                       @"SELECT prod.IdProducto, prod.Nombre, prod.Descripcion, prod.Precio, prod.IdCategoria
                            FROM Producto prod 
                            WHERE prod.Nombre LIKE :nombre AND
                                  prod.Descripcion LIKE :descripcion AND
                                  (:idCategoria IS NULL OR prod.IdCategoria = :idCategoria)")
                    .AddEntity(typeof(Producto))
                    .SetParameter("nombre", "%" + nombre + "%")
                    .SetParameter("descripcion", "%" + descripcion + "%")
                    .SetParameter("idCategoria", filtro.IdCategoria)
                    .List<Producto>();

                return productos.Select(producto => new ProductoDto
                {
                    Id = producto.Id,
                    Nombre = producto.Nombre,
                    Descripcion = producto.Descripcion,
                    CateagoriaId = producto.Categoria.Id,
                    CateagoriaNombre = producto.Categoria.Nombre
                }).ToList();
            }
        }

        // GET: api/Producto/5
        public ProductoDto Get(int id)
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                var producto = session.Get<Producto>(id);

                return producto == null
                    ? null
                    : new ProductoDto
                    {
                        Id = producto.Id,
                        Nombre = producto.Nombre,
                        Descripcion = producto.Descripcion,
                        CateagoriaId = producto.Categoria.Id,
                        CateagoriaNombre = producto.Categoria.Nombre
                    };
            };
        }

        // POST: api/Producto
        public void Post([FromBody]ProductoDto value)
        {
            using (var scope = new TransactionScope())
            {
                using (var session = Database.SessionFactory.OpenSession())
                {
                    var producto = new Producto
                    {
                        Nombre = value.Nombre,
                        Descripcion = value.Descripcion,
                        Precio = value.Precio,
                        Categoria = session.Load<Categoria>(value.CateagoriaId)
                    };

                    session.Save(producto);
                }

                scope.Complete();
            }
        }

        // PUT: api/Producto/5
        public void Put(int id, [FromBody]ProductoDto value)
        {
            using (var scope = new TransactionScope())
            {
                using (var session = Database.SessionFactory.OpenSession())
                {
                    var producto = session.Get<Producto>(id);
                    producto.Nombre = value.Nombre;
                    producto.Descripcion = value.Descripcion;
                    producto.Precio = value.Precio;
                    producto.Categoria = session.Load<Categoria>(value.CateagoriaId);
                }

                scope.Complete();
            }
        }

        // DELETE: api/Producto/5
        public void Delete(int id)
        {
            using (var scope = new TransactionScope())
            {
                using (var session = Database.SessionFactory.OpenSession())
                {
                    session.Delete(session.Get<Producto>(id));
                }

                scope.Complete();
            }
        }
    }
}
