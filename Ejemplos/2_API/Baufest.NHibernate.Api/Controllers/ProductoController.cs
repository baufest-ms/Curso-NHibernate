using Baufest.NHibernate.Api.Models;
using Baufest.NHibernate.Dominio.Entidades;
using Baufest.NHibernate.Repository.Mappings;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Baufest.NHibernate.Api.Controllers
{
    public class ProductoController : ApiController
    {
        // GET: api/Producto
        public IEnumerable<ProductoDto> Get()
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                var productos = session.Query<Producto>().ToList();

                return productos.Select(ConvertirADto);
            }
        }

        // GET: api/Producto/5
        public ProductoDto Get(int id)
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                var producto = session.Get<Producto>(id);

                return ConvertirADto(producto);
            };
        }

        // POST: api/Producto
        public void Post([FromBody]ProductoDto value)
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
        }

        // PUT: api/Producto/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Producto/5
        public void Delete(int id)
        {
        }

        private ProductoDto ConvertirADto(Producto producto)
        {
            return new ProductoDto
            {
                Id = producto.Id,
                Nombre = producto.Nombre,
                Descripcion = producto.Descripcion,
                CateagoriaId = producto.Categoria.Id,
                CateagoriaNombre = producto.Categoria.Nombre
            };
        }
    }
}
