using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlightLib;

namespace Página_principal
{
    public partial class ParametrosTiempoDistancia : Form
    {
        double distanciaSeguridad;
        double tiempoCiclo;

        public ParametrosTiempoDistancia()
        {
            InitializeComponent();
        }

        public double GetDistanciaSeguridad()
        {
            return distanciaSeguridad;
        }

        public double GetTiempoCiclo()
        {
            return tiempoCiclo;
        }

        private void BotonGuardarParametros_Click(object sender, EventArgs e)
        {
            GuardarDatos(DistanciaSeguridad, TiempoCiclo);
        }
        private void VolverMenuPrincipal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void GuardarDatos(TextBox distanciaTextBox, TextBox tiempoTextBox)
        {
            try
            {
                // Conversión de texto a número tal como lo haces en PlanesDeVuelo
                double distancia = Convert.ToDouble(distanciaTextBox.Text);
                double tiempo = Convert.ToDouble(tiempoTextBox.Text);
                if (distancia <=0 || tiempo <=0)
                {
                    MessageBox.Show("Los valores deben de ser mayores de 0");
                    return;
                }

                // Guardamos los datos en este mismo formulario
                this.distanciaSeguridad = distancia;
                this.tiempoCiclo = tiempo;

                MessageBox.Show("Parámetros guardados correctamente.");
                this.DialogResult = DialogResult.OK; // Esto indica que los datos se guardaron correctamente
                this.Close(); // Cierra el formulario después de guardar
            }
            catch (Exception ex)
            {
                // Si el usuario introduce letras o deja el campo vacío, salta aquí
                MessageBox.Show("Error al guardar los parámetros: " + ex.Message);
            }
        }
    }
}
