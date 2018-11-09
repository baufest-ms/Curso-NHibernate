using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baufest.NHibernate.Dominio.Entidades
{
    public class Categoria
    {
        public virtual int Id {get; set;}

        public virtual string Nombre { get; set; }
    }
}
