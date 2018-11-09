using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;

namespace Baufest.NHibernate.HolaMundo.Conventions
{
    public class PrimaryKeyConvention : IIdConvention
    {
        public void Apply(IIdentityInstance instance)
        {
            instance.Column(instance.EntityType.Name.ToUpper() + "_ID");
        }
    }
}
