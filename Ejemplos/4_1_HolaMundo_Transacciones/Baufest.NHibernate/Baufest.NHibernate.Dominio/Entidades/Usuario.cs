using System.Collections.Generic;

namespace Baufest.NHibernate.Dominio.Entidades
{
    public class Usuario
    {
        public virtual int Id { get; set; }

        public virtual string Nombre { get; set; }

        public virtual IList<Asignacion> Asignaciones { get; set; }
    }
}