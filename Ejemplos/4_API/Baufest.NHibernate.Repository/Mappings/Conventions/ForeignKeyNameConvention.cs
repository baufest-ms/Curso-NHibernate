using FluentNHibernate;
using FluentNHibernate.Conventions;
using System;

namespace Baufest.NHibernate.HolaMundo.Conventions
{
    public class ForeignKeyNameConvention : ForeignKeyConvention
    {
        protected override string GetKeyName(Member property, Type type)
        {
            return "Id" + type.Name;
        }
    }
}
