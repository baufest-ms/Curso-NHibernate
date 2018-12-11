using Baufest.NHibernate.Dominio.Entidades;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace Baufest.NHibernate.Repository.Mappings.Overrides
{
    public class PreguntaMappingOverride : IAutoMappingOverride<Pregunta>
    {
        public void Override(AutoMapping<Pregunta> mapping)
        {
            mapping.HasMany(x => x.Opciones).Inverse().Cascade.AllDeleteOrphan();
        }
    }
}
