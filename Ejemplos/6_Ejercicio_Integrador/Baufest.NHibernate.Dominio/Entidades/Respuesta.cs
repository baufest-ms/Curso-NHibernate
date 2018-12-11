using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baufest.NHibernate.Dominio.Entidades
{
    public class Respuesta
    {
        public virtual int Id { get; set; }

        public virtual Pregunta Pregunta { get; set; }

        public virtual Opcion Opcion { get; set; }

        public virtual string Usuario { get; set; }
    }
}
