using Baufest.NHibernate.Dominio.Entidades;
using System.Collections.Generic;
using System.Web.Http;

namespace Baufest.NHibernate.Api.Controllers
{
    public class CategoriaController : ApiController
    {
        // GET: api/Producto
        public IEnumerable<Categoria> Get()
        {
            return null;
        }

        // GET: api/Producto/5
        public Categoria Get(int id)
        {
            return new Categoria
            {
                Id = 1,
                Nombre = "Una categoria"
            };
        }

        // POST: api/Producto
        public void Post([FromBody]string nombre)
        {
        }

        // PUT: api/Producto/5
        public void Put(int id, [FromBody]string nombre)
        {
        }

        // DELETE: api/Producto/5
        public void Delete(int id)
        {
        }
    }
}
