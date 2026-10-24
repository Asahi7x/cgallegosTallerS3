using System.Globalization;
using System.Text.RegularExpressions;
using cgallegosTallerS3.Models;

namespace cgallegosTallerS3.Views
{
    public partial class ContactoPage : ContentPage
    {
        public ContactoPage()
        {
            InitializeComponent();
            dpFecha.Date = DateTime.Today;
        }

        private void OnTipoChanged(object? sender, EventArgs e)
        {
            txtIdentificacion.Text = string.Empty;

            switch (pkrTipo.SelectedItem as string)
            {
                case "CI":
                    txtIdentificacion.MaxLength = 10;
                    txtIdentificacion.Keyboard = Keyboard.Numeric;
                    txtIdentificacion.Placeholder = "10 dígitos";
                    break;
                case "RUC":
                    txtIdentificacion.MaxLength = 13;
                    txtIdentificacion.Keyboard = Keyboard.Numeric;
                    txtIdentificacion.Placeholder = "13 dígitos";
                    break;
                default:
                    txtIdentificacion.MaxLength = int.MaxValue;
                    txtIdentificacion.Keyboard = Keyboard.Text;
                    txtIdentificacion.Placeholder = "Número de pasaporte";
                    break;
            }
        }

        private async void OnGuardarClicked(object? sender, EventArgs e)
        {
            var error = Validar(out var salario);
            if (error is not null)
            {
                await DisplayAlertAsync("Validación", error, "OK");
                return;
            }

            var contacto = new Contacto
            {
                TipoIdentificacion = (string)pkrTipo.SelectedItem,
                Identificacion = txtIdentificacion.Text.Trim(),
                Nombres = txtNombres.Text.Trim(),
                Apellidos = txtApellidos.Text.Trim(),
                Fecha = dpFecha.Date ?? DateTime.Today,
                Correo = txtCorreo.Text.Trim(),
                Salario = salario
            };

            await Navigation.PushAsync(new ResumenPage(contacto));
        }

        private string? Validar(out decimal salario)
        {
            salario = 0;
            var tipo = pkrTipo.SelectedItem as string;
            var id = txtIdentificacion.Text?.Trim() ?? string.Empty;

            if (tipo is null)
                return "Seleccione el tipo de identificación.";
            if (tipo == "CI" && !Regex.IsMatch(id, @"^\d{10}$"))
                return "La CI debe tener exactamente 10 números.";
            if (tipo == "RUC" && !Regex.IsMatch(id, @"^\d{13}$"))
                return "El RUC debe tener exactamente 13 números.";
            if (tipo == "Pasaporte" && id.Length == 0)
                return "Ingrese el número de pasaporte.";
            if (string.IsNullOrWhiteSpace(txtNombres.Text))
                return "Ingrese los nombres.";
            if (string.IsNullOrWhiteSpace(txtApellidos.Text))
                return "Ingrese los apellidos.";
            if (!Regex.IsMatch(txtCorreo.Text?.Trim() ?? string.Empty, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
                return "Ingrese un correo válido.";

            var textoSalario = (txtSalario.Text ?? string.Empty).Replace(',', '.');
            if (!decimal.TryParse(textoSalario, NumberStyles.Number, CultureInfo.InvariantCulture, out salario) || salario <= 0)
                return "Ingrese un salario válido mayor a 0.";

            return null;
        }
    }
}
