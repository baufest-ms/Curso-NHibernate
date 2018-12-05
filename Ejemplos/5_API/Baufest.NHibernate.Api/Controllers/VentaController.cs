using Baufest.NHibernate.Api.Models;
using Baufest.NHibernate.Dominio.Entidades;
using Baufest.NHibernate.Repository.Mappings;
using NHibernate.Linq;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Baufest.NHibernate.Api.Controllers
{
    public class VentaController : ApiController
    {
        // GET: api/Venta
        public IEnumerable<VentaDto> Get([FromUri]FiltroVenta filtro)
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                var ventas = session.Query<Venta>();

                if (filtro.IdCliente.HasValue)
                {
                    ventas = ventas.Where(venta => venta.Cliente.Id == filtro.IdCliente);
                }

                if (filtro.IdProducto.HasValue)
                {
                    ventas = ventas.Where(venta => venta.Items.Any(item => item.Producto.Id == filtro.IdProducto));
                }

                ventas = ventas.FetchMany(vta => vta.Items).ThenFetch(it => it.Producto);

                var listaVentas = ventas.ToList();

                return listaVentas.Select(venta => new VentaDto
                {
                    Id = venta.Id,
                    IdCliente = venta.Cliente.Id,
                    Items = venta.Items.Select(i => new ItemDto
                                                    {
                                                        IdProducto = i.Producto.Id,
                                                        NombreProducto = i.Producto.Nombre,
                                                        Cantidad = i.Cantidad
                                                    }).ToList()
                }).ToList();
            }
        }

        // GET: api/Venta/1
        public VentaDto Get(int id)
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                var venta = session.Get<Venta>(id);

                return venta == null
                    ? null
                    : new VentaDto
                    {
                        Id = venta.Id,
                        IdCliente = venta.Cliente.Id,
                        Items = venta.Items.Select(i => new ItemDto { IdProducto = i.Producto.Id, NombreProducto = i.Producto.Nombre, Cantidad = i.Cantidad }).ToList()
                    };
            };
        }

        // POST: api/Venta
        public void Post([FromBody]VentaDto value)
        {
            using (var session = Database.SessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var venta = new Venta
                    {
                        Cliente = session.Load<Cliente>(value.IdCliente),
                        Items = new List<Item>()
                    };

                    foreach (var itemDto in value.Items)
                    {
                        var producto = session.Load<Producto>(itemDto.IdProducto);
                        venta.Items.Add(
                            new Item
                            {
                                Producto = producto,
                                Cantidad = itemDto.Cantidad,
                                Venta = venta
                            });
                    }

                    session.Save(venta);
                    transaction.Commit();
                }
            }
        }
    }
}
