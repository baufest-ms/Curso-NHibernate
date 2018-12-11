using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Baufest.NHibernate.Dominio.Entidades
{
    public class Opcion
    {
        public virtual int Id { get; set; }

        public virtual string Texto { get; set; }

        public virtual bool EsCorrecta { get; set; }

        public virtual Pregunta Pregunta { get; set; }
    }
}
