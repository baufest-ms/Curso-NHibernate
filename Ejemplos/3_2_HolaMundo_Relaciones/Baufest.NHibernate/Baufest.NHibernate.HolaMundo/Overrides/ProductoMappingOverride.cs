﻿using Baufest.NHibernate.Dominio.Entidades;
using FluentNHibernate.Automapping;
using FluentNHibernate.Automapping.Alterations;

namespace Baufest.NHibernate.HolaMundo.Overrides
{
    //public class ProductoMappingOverride : IAutoMappingOverride<Producto>
    //{
    //    public void Override(AutoMapping<Producto> mapping)
    //    {
    //        mapping.Table("PRODUCTOS");
    //        mapping.Map(x => x.Precio, "PRECIO_PRODUCTO");
    //    }
    //}

    //public class ProductoMappingOverride : IAutoMappingOverride<Producto>
    //{
    //    public void Override(AutoMapping<Producto> mapping)
    //    {
    //        mapping.SqlInsert("exec ProductoInsert ?, ?, ?, ?");
    //        mapping.SqlDelete("exec ProductoDeletet ?");
    //        mapping.SqlInsert("exec ProductoUpdate ?, ?, ?, ?");
    //    }
    //}
}
