using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Baufest.NHibernate.HolaMundo.Conventions
{
    public class PropertyNameConvention : IPropertyConvention
    {
        public void Apply(IPropertyInstance instance)
        {
            instance.Column(instance.Property.Name.ToUpper());
        }
    }
}
