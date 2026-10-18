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
    public partial class PlanesDeVuelo : Form
    {
        FlightPlanList listaVuelos = new FlightPlanList();
        public PlanesDeVuelo()
        {
            InitializeComponent();
        }
        public FlightPlanList GetListaVuelos()
        {
            return listaVuelos;
        }
        private void GuardarVuelo1_Click(object sender, EventArgs e)
        {
            GuardarVuelo(ID1, Velocidad1, PosInicial1, PosFinal1);
        }

        private void GuardarVuelo2_Click(object sender, EventArgs e)
        {
            GuardarVuelo(ID2, Velocidad2, PosInicial2, PosFinal2);
        }

        private void VolverMenuPrincipal_Click(object sender, EventArgs e)
        {
            this.Close(); // Cierra el formulario actual
        }
        
        private void GuardarVuelo(TextBox idTextBox, TextBox velocidadTextBox, TextBox posInicialTextBox, TextBox posFinalTextBox)
        {
            try
            {
                string identificador = idTextBox.Text;
                double velocidad = Convert.ToDouble(velocidadTextBox.Text);
                string[] posInicialCoords = posInicialTextBox.Text.Split(' ');
                double ix = Convert.ToDouble(posInicialCoords[0]);
                double iy = Convert.ToDouble(posInicialCoords[1]);
                string[] posFinalCoords = posFinalTextBox.Text.Split(' ');
                double fx = Convert.ToDouble(posFinalCoords[0]);
                double fy = Convert.ToDouble(posFinalCoords[1]);
                // Creación del objeto FlightPlan
                FlightPlan plan = new FlightPlan(identificador, ix, iy, fx, fy,ix,iy, velocidad);

                // Guardar en la lista
                int res = listaVuelos.AddFlightPlan(plan);

                if (res == 0)
                {
                    MessageBox.Show($"Vuelo '{identificador}' guardado correctamente. Vuelos acumulados: {listaVuelos.GetNumber()}");
                }
                else
                {
                    MessageBox.Show("No se pudo guardar el vuelo: La lista está llena (máximo 10).");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar el vuelo: " + ex.Message);
            }
        }

    }
}
