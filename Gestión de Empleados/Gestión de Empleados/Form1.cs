using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace GestionEmpleados
{
    public partial class FrmRegistroEmpleados : Form
    {
        public FrmRegistroEmpleados()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (ValidarFormulario())
            {
                MessageBox.Show(
                    "Datos guardados correctamente:\n\n" +
                    $"Nombre: {txtNombre.Text}\n" +
                    $"Edad: {txtEdad.Text}\n" +
                    $"Correo: {txtCorreo.Text}\n" +
                    $"Cargo: {cmbCargo.SelectedItem}",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                lblEstado.Text = "Registro validado y guardado correctamente.";
                lblEstado.ForeColor = System.Drawing.Color.DarkGreen;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtNombre.Clear();
            txtEdad.Clear();
            txtCorreo.Clear();
            cmbCargo.SelectedIndex = -1;
            errorProvider.Clear();
            lblEstado.Text = "";
            txtNombre.Focus();
        }

        private bool ValidarFormulario()
        {
            bool ok = true;
            errorProvider.Clear();
            lblEstado.Text = "";
            lblEstado.ForeColor = System.Drawing.Color.Firebrick;

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                ok = false;
                errorProvider.SetError(txtNombre, "El nombre es obligatorio.");
            }

            if (!int.TryParse(txtEdad.Text, out int edad) || edad < 18 || edad > 65)
            {
                ok = false;
                errorProvider.SetError(txtEdad, "La edad debe estar entre 18 y 65.");
            }

            if (!EsCorreoValido(txtCorreo.Text))
            {
                ok = false;
                errorProvider.SetError(txtCorreo, "Correo electrónico inválido.");
            }

            if (cmbCargo.SelectedIndex < 0)
            {
                ok = false;
                errorProvider.SetError(cmbCargo, "Debe seleccionar un cargo.");
            }

            if (!ok) lblEstado.Text = "Corrige los errores antes de guardar.";
            return ok;
        }

        private static bool EsCorreoValido(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo)) return false;
            return Regex.IsMatch(correo, @"^[^@\s]+@[^@\s]+\.[^@\s]+$", RegexOptions.IgnoreCase);
        }

        private void txtEdad_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
                e.Handled = true;
        }
    }
}
