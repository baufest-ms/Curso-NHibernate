using Baufest.NHibernate.Dominio.Entidades;
using Baufest.NHibernate.Repository.Mappings;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Http;

namespace Baufest.NHibernate.Api.Controllers
{
    public class CategoriaController : ApiController
    {
        // GET: api/Producto
        [Description("Lista todas las categorias disponibles")]
        public IEnumerable<Categoria> Get()
        {
            using(var session = Database.SessionFactory.OpenSession())
            {
                return session.Query<Categoria>().ToList();
            }
        }

        // GET: api/Producto/5
        [Description("Devuelve la categoría correspondiente al id")]
        public Categoria Get(int id)
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                return session.Get<Categoria>(id);
            }
        }

        // POST: api/Producto
        [Description("Crea una categoría con el nombre especificado")]
        public void Post([FromBody]string nombre)
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                var categoria = new Categoria
                {
                    Nombre = nombre
                };

                session.Save(categoria);
            }
        }

        // PUT: api/Producto/5
        [Description("Actualiza el nombre de la categoría especificada")]
        public void Put(int id, [FromBody]string nombre)
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                var categoria = session.Load<Categoria>(id);
                categoria.Nombre = nombre;
                session.Flush();
            }
        }

        // DELETE: api/Producto/5
        [Description("Elimina la categoría especificada")]
        public void Delete(int id)
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                var categoria = session.Load<Categoria>(id);
                session.Delete(categoria);
                session.Flush();
            }
        }
    }
}
