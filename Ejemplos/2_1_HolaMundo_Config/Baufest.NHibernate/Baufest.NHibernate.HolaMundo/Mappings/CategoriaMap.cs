using Baufest.NHibernate.Dominio.Entidades;
using FluentNHibernate.Mapping;

namespace Baufest.NHibernate.HolaMundo.Mappings
{
    public class CategoriaMap : ClassMap<Categoria>
    {
        public CategoriaMap()
        {
            Table("CATEGORIA");
            Id(x => x.Id, "ID");
            Map(x => x.Nombre, "NOMBRE");
        }
    }
}
