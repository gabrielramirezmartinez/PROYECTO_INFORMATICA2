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
    public partial class FormDistanciaGrid : Form
    {
        public FormDistanciaGrid(FlightPlan seleccionado, FlightPlan otro)
        {
            InitializeComponent();

            // Calculamos la distancia usando el método de la posición actual de ambos vuelos
            double distancia = seleccionado.GetCurrentPosition().Distancia(otro.GetCurrentPosition());

            // Mostramos el resultado de manera clara en el Label
            lblResultado.Text = $"Distancia entre el vuelo '{seleccionado.GetId()}' y el vuelo '{otro.GetId()}':\n\n" + $"{distancia:F2} unidades de distancia.";
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
    

