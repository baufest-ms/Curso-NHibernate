using Baufest.NHibernate.Dominio.Entidades;
using System.Collections.Generic;
using System.Web.Http;

namespace Baufest.NHibernate.Api.Controllers
{
    public class ProductoController : ApiController
    {
        // GET: api/Producto
        public IEnumerable<Producto> Get()
        {
            return null;
        }

        // GET: api/Producto/5
        public Producto Get(int id)
        {
            return null;
        }

        // POST: api/Producto
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Producto/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Producto/5
        public void Delete(int id)
        {
        }
    }
}
