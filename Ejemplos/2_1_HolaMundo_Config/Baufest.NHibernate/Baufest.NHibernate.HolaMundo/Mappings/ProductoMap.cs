using Baufest.NHibernate.Dominio.Entidades;
using FluentNHibernate.Mapping;

namespace Baufest.NHibernate.HolaMundo.Mappings
{
    public class ProductoMap : ClassMap<Producto>
    {
        public ProductoMap()
        {
            Table("PRODUCTO");
            Id(x => x.Id, "ID");
            Map(x => x.Nombre, "NOMBRE");
            Map(x => x.Descripcion, "DESCRIPCION");
            Map(x => x.Precio, "PRECIO");
            Map(x => x.Insert, "INSERT");
            References(x => x.Categoria, "CATEGORIA_ID");
        }
    }
}
