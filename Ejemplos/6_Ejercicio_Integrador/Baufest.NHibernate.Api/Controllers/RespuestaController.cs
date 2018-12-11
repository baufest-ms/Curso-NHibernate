using Baufest.NHibernate.Api.Models;
using Baufest.NHibernate.Dominio.Entidades;
using Baufest.NHibernate.Repository.Mappings;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Http;

namespace Baufest.NHibernate.Api.Controllers
{
    public class RespuestaController : ApiController
    {
        // GET: api/Respuesta
        [Description("Lista las respuestas cargadas. Opcionalmente permite filtrar por usuario y si la respuesta es correcta/incorrecta")]
        public IEnumerable<RespuestaDto> Get([FromUri]FiltroRespuesta filtro)
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                var respuestas = session.Query<Respuesta>();

                if (!string.IsNullOrEmpty(filtro.Usuario))
                {
                    respuestas = respuestas.Where(res => res.Usuario == filtro.Usuario);
                }

                if (filtro.Correcta != null)
                {
                    respuestas = respuestas.Where(res => res.Opcion.EsCorrecta == filtro.Correcta);
                }

                return respuestas.Select(res => new RespuestaDto
                {
                    Id = res.Id,
                    IdPregunta = res.Pregunta.Id,
                    IdOpcion = res.Opcion.Id,
                    Usuario = res.Usuario,
                }).ToList();
            }
        }

        // GET: api/Respuesta/5
        [Description("Devuelve la respuesta con el Id especificado")]
        public RespuestaDto Get(int id)
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                var respuesta = session.Get<Respuesta>(id);

                return respuesta == null
                    ? null
                    : new RespuestaDto
                    {
                        Id = respuesta.Id,
                        IdPregunta = respuesta.Pregunta.Id,
                        IdOpcion = respuesta.Opcion.Id,
                        Usuario = respuesta.Usuario
                    };
            };
        }

        // POST: api/Respuesta
        [Description("Permite cargar la respuesta para une pregunta determinada, indicando además el usuario que la respondió")]
        public void Post([FromBody]RespuestaDto value)
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var respuesta = new Respuesta
                    {
                        Pregunta = session.Load<Pregunta>(value.IdPregunta),
                        Opcion = session.Load<Opcion>(value.IdOpcion),
                        Usuario = value.Usuario
                    };

                    session.Save(respuesta);

                    transaction.Commit();
                }

            }
        }

        // DELETE: api/Respuesta/5
        [Description("Permite eliminar la respuesta con el Id especificado")]
        public void Delete(int id)
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var respuesta = session.Load<Respuesta>(id);
                    session.Delete(respuesta);

                    transaction.Commit();
                }
            }
        }
    }
}
