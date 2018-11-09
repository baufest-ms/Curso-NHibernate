namespace Baufest.NHibernate.Dominio.Entidades
{
    public class Item
    {
        public virtual int Id { get; set; }

        public virtual Venta Venta { get; set; }

        public virtual Producto Producto { get; set; }

        public virtual int Cantidad { get; set; }
    }
}
