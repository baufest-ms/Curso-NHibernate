using System.Collections.Generic;

namespace Baufest.NHibernate.Dominio.Entidades
{
    public class Pregunta
    {
        public virtual int Id { get; set; }

        public virtual string Texto { get; set; }

        public virtual IList<Opcion> Opciones { get; set; }
    }
}
