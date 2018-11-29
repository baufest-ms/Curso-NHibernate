using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Baufest.NHibernate.Api.Models
{
    public class VentaDto
    {
        public int Id { get; set; }

        public int IdCliente { get; set; }

        public IList<ItemDto> Items { get; set; }
    }
}