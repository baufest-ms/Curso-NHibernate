using System.Collections.Generic;

namespace Baufest.NHibernate.Dominio.Entidades
{
    public class Venta
    {
        public virtual int Id { get; set; }

        public virtual Cliente Cliente { get; set; }

        public virtual IList<Item> Items { get; set; } 
    }
}
