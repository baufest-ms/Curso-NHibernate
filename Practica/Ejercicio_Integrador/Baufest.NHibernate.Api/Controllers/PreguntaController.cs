using Baufest.NHibernate.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Http;

namespace Baufest.NHibernate.Api.Controllers
{
    public class PreguntaController : ApiController
    {
        [Description("Lista las preguntas cargadas con sus opciones. Opcionalmente permite filtrar por el texto de la pregunta")]
        public IEnumerable<PreguntaDto> Get([FromUri]string texto = null)
        {
            throw new NotImplementedException("Lógica no implementada");
        }

        // GET: api/Respuesta/5
        [Description("Devuelve la pregunta con el Id especificado")]
        public PreguntaDto Get(int id)
        {
            throw new NotImplementedException("Lógica no implementada");
        }

        // POST: api/Respuesta
        [Description("Crea una pregunta con sus opciones posibles")]
        public void Post([FromBody]PreguntaDto value)
        {
            throw new NotImplementedException("Lógica no implementada");
        }

        // DELETE: api/Respuesta/5
        [Description("Elimina una pregunta por Id")]
        public void Delete(int id)
        {
            throw new NotImplementedException("Lógica no implementada");
        }
    }
}
