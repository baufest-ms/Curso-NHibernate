using System.Collections.Generic;

namespace Baufest.NHibernate.Api.Models
{
    public class PreguntaDto
    {
        public int Id { get; set; }

        public string Texto { get; set; }

        public IList<OpcionDto> Opciones { get; set; }
    }
}