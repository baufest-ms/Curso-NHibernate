using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Baufest.NHibernate.Api.Models
{
    public class ItemDto
    {
        public int IdProducto { get; set; }

        public string NombreProducto { get; set; }

        public int Cantidad { get; set; }
    }
}