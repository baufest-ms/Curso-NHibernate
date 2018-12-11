using FluentNHibernate;
using FluentNHibernate.Conventions;
using System;

namespace Baufest.NHibernate.Repository.Mappings.Conventions
{
    public class ForeignKeyNameConvention : ForeignKeyConvention
    {
        protected override string GetKeyName(Member property, Type type)
        {
            return type.Name  + "Id";
        }
    }
}
