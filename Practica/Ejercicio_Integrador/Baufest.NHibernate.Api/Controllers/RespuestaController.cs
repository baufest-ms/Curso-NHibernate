using Baufest.NHibernate.Api.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.Http;

namespace Baufest.NHibernate.Api.Controllers
{
    public class RespuestaController : ApiController
    {
        // GET: api/Respuesta
        [Description("Lista las respuestas cargadas. Opcionalmente permite filtrar por usuario y si la respuesta es correcta/incorrecta")]
        public IEnumerable<RespuestaDto> Get([FromUri]FiltroRespuesta filtro)
        {
            throw new NotImplementedException("Lógica no implementada");
        }

        // GET: api/Respuesta/5
        [Description("Devuelve la respuesta con el Id especificado")]
        public RespuestaDto Get(int id)
        {
            throw new NotImplementedException("Lógica no implementada");
        }

        // POST: api/Respuesta
        [Description("Permite cargar la respuesta para une pregunta determinada, indicando además el usuario que la respondió")]
        public void Post([FromBody]RespuestaDto value)
        {
            throw new NotImplementedException("Lógica no implementada");
        }

        // DELETE: api/Respuesta/5
        [Description("Permite eliminar la respuesta con el Id especificado")]
        public void Delete(int id)
        {
            throw new NotImplementedException("Lógica no implementada");
        }
    }
}
