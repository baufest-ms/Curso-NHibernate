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
        // GET: api/Categoria
        [Description("Lista todas las categorias disponibles")]
        public IEnumerable<Categoria> Get([FromUri]string nombre = null)
        {
            using(var session = Database.SessionFactory.OpenSession())
            {
                var categorias = session.Query<Categoria>();
                if (!string.IsNullOrEmpty(nombre))
                {
                    categorias = categorias.Where(cat => cat.Nombre.Contains(nombre));
                }

                return categorias.ToList();
            }
        }

        // GET: api/Categoria/5
        [Description("Devuelve la categoría correspondiente al id")]
        public Categoria Get(int id)
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                return session.Get<Categoria>(id);
            }
        }

        // POST: api/Categoria
        [Description("Crea una categoría con el nombre especificado")]
        public void Post([FromBody]string nombre)
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                using(var transaction = session.BeginTransaction())
                {
                    var categoria = new Categoria
                    {
                        Nombre = nombre
                    };

                    session.Save(categoria);

                    transaction.Commit();
                }

            }
        }

        // PUT: api/Categoria/5
        [Description("Actualiza el nombre de la categoría especificada")]
        public void Put(int id, [FromBody]string nombre)
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var categoria = session.Load<Categoria>(id);
                    categoria.Nombre = nombre;

                    transaction.Commit();
                }
            }
        }

        // DELETE: api/Categoria/5
        [Description("Elimina la categoría especificada")]
        public void Delete(int id)
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var categoria = session.Load<Categoria>(id);
                    session.Delete(categoria);

                    transaction.Commit();
                }
            }
        }
    }
}
