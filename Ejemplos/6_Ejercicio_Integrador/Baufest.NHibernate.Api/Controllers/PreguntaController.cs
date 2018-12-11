using Baufest.NHibernate.Api.Models;
using Baufest.NHibernate.Dominio.Entidades;
using Baufest.NHibernate.Repository.Mappings;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web.Http;

namespace Baufest.NHibernate.Api.Controllers
{
    public class PreguntaController : ApiController
    {
        [Description("Lista las preguntas cargadas con sus opciones. Opcionalmente permite filtrar por el texto de la pregunta")]
        public IEnumerable<PreguntaDto> Get([FromUri]string texto = null)
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                var preguntas = session.Query<Pregunta>();

                if (!string.IsNullOrEmpty(texto))
                {
                    preguntas = preguntas.Where(p => p.Texto.Contains(texto));
                }

                return preguntas.Select(p => new PreguntaDto
                {
                    Id = p.Id,
                    Texto = p.Texto,
                    Opciones = p.Opciones.Select(o => new OpcionDto
                                            {
                                                Id = o.Id,
                                                Texto = o.Texto,
                                                EsCorrecta = o.EsCorrecta
                                            }).ToList()
                }).ToList();
            }
        }

        // GET: api/Respuesta/5
        [Description("Devuelve la pregunta con el Id especificado")]
        public PreguntaDto Get(int id)
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                var pregunta = session.Get<Pregunta>(id);

                return pregunta == null
                    ? null
                    : new PreguntaDto
                    {
                        Id = pregunta.Id,
                        Texto = pregunta.Texto,
                        Opciones = pregunta.Opciones.Select(o => new OpcionDto
                        {
                            Id = o.Id,
                            Texto = o.Texto,
                            EsCorrecta = o.EsCorrecta
                        }).ToList()
                    };
            };
        }

        // POST: api/Respuesta
        [Description("Crea una pregunta con sus opciones posibles")]
        public void Post([FromBody]PreguntaDto value)
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var pregunta = new Pregunta { Texto = value.Texto };

                    pregunta.Opciones = value.Opciones.Select(o => new Opcion
                                                            {
                                                                Pregunta = pregunta,
                                                                Texto = o.Texto,
                                                                EsCorrecta = o.EsCorrecta
                                                            }).ToList();

                    session.Save(pregunta);

                    transaction.Commit();
                }

            }
        }

        // DELETE: api/Respuesta/5
        [Description("Elimina una pregunta por Id")]
        public void Delete(int id)
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var respuesta = session.Load<Pregunta>(id);
                    session.Delete(respuesta);

                    transaction.Commit();
                }
            }
        }
    }
}
