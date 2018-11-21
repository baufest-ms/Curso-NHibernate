namespace Baufest.NHibernate.Api.Models
{
    public class ProductoDto
    {
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }

        public int CateagoriaId { get; set; }

        public string CateagoriaNombre { get; set; }
    }
}