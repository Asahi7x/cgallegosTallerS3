using System.Globalization;
using System.Text;
using cgallegosTallerS3.Models;

namespace cgallegosTallerS3.Views
{
    public partial class ResumenPage : ContentPage
    {
        private static readonly CultureInfo Cultura = CultureInfo.InvariantCulture;
        private readonly Contacto _contacto;

        public ResumenPage(Contacto contacto)
        {
            InitializeComponent();
            _contacto = contacto;

            lblTipo.Text = contacto.TipoIdentificacion;
            lblIdentificacion.Text = contacto.Identificacion;
            lblNombres.Text = contacto.Nombres;
            lblApellidos.Text = contacto.Apellidos;
            lblFecha.Text = contacto.Fecha.ToString("dd/MM/yyyy");
            lblCorreo.Text = contacto.Correo;
            lblSalario.Text = $"$ {contacto.Salario.ToString("N2", Cultura)}";
            lblAporte.Text = $"$ {contacto.AporteIess.ToString("N2", Cultura)}";
        }

        private async void OnExportarClicked(object? sender, EventArgs e)
        {
            var contenido = new StringBuilder()
                .AppendLine("===== DATOS DEL CONTACTO =====")
                .AppendLine($"Tipo de identificación: {_contacto.TipoIdentificacion}")
                .AppendLine($"Identificación: {_contacto.Identificacion}")
                .AppendLine($"Nombres: {_contacto.Nombres}")
                .AppendLine($"Apellidos: {_contacto.Apellidos}")
                .AppendLine($"Fecha: {_contacto.Fecha:dd/MM/yyyy}")
                .AppendLine($"Correo: {_contacto.Correo}")
                .AppendLine($"Salario: $ {_contacto.Salario.ToString("N2", Cultura)}")
                .AppendLine($"Aporte IESS (9.45 %): $ {_contacto.AporteIess.ToString("N2", Cultura)}")
                .ToString();

            var nombreArchivo = $"contacto_{_contacto.Identificacion}_{DateTime.Now:yyyyMMddHHmmss}.txt";
            var ruta = Path.Combine(FileSystem.AppDataDirectory, nombreArchivo);

            try
            {
                await File.WriteAllTextAsync(ruta, contenido, Encoding.UTF8);
                await DisplayAlertAsync("Exportado", $"Archivo guardado en:\n{ruta}", "OK");

                await Share.Default.RequestAsync(new ShareFileRequest
                {
                    Title = "Compartir contacto",
                    File = new ShareFile(ruta)
                });
            }
            catch (Exception ex)
            {
                await DisplayAlertAsync("Error", $"No se pudo exportar el archivo: {ex.Message}", "OK");
            }
        }
    }
}
