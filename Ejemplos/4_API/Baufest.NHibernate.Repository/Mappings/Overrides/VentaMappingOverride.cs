using Baufest.NHibernate.Dominio.Entidades;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace Baufest.NHibernate.HolaMundo.Overrides
{
    public class VentaMappingOverride : IAutoMappingOverride<Venta>
    {
        public void Override(AutoMapping<Venta> mapping)
        {
            mapping.HasMany(x => x.Items).Inverse().Cascade.AllDeleteOrphan();
        }
    }
}
