using System.Collections.Generic;

namespace Baufest.NHibernate.Dominio.Entidades
{
    public class Tarea
    {
        public virtual int Id { get; set; }

        public virtual string Descripcion { get; set; }

        public virtual IList<Asignacion> Asignaciones { get; set; }
    }
}
