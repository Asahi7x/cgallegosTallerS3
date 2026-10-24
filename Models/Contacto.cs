namespace cgallegosTallerS3.Models
{
    public class Contacto
    {
        // Aporte personal del afiliado al IESS (relación de dependencia)
        public const decimal PorcentajeAporteIess = 0.0945m;

        public string TipoIdentificacion { get; set; } = string.Empty;
        public string Identificacion { get; set; } = string.Empty;
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public DateTime Fecha { get; set; }
        public string Correo { get; set; } = string.Empty;
        public decimal Salario { get; set; }

        public decimal AporteIess => Math.Round(Salario * PorcentajeAporteIess, 2);
    }
}
