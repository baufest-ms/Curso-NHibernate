using System;

namespace Baufest.NHibernate.Dominio.Entidades
{
    public class Asignacion
    {
        public virtual int Id { get; set; }

        public virtual Usuario Usuario {get; set; }

        public virtual Tarea Tarea { get; set; }

        public virtual DateTime Desde { get; set; }

        public virtual DateTime Hasta { get; set; }
    }
}
