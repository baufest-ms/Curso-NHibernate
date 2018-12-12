namespace Baufest.NHibernate.Api.Models
{
    public class RespuestaDto
    {
        public int Id { get; set; }

        public int IdPregunta { get; set; }

        public int IdOpcion { get; set; }

        public string Usuario { get; set; }
    }
}