using System;
using System.Windows.Forms;
using FlightLib;

namespace Página_principal
{
    public class DatosVuelos : Form
    {
        private FlightPlan vuelo1;
        private FlightPlan vuelo2;
        private DataGridView tabla;

        public DatosVuelos(FlightPlan vuelo1, FlightPlan vuelo2)
        {
            this.vuelo1 = vuelo1;
            this.vuelo2 = vuelo2;

            this.Text = "Datos de los vuelos";
            this.Width = 750;
            this.Height = 250;
            this.StartPosition = FormStartPosition.CenterParent;

            tabla = new DataGridView();
            tabla.Dock = DockStyle.Fill;
            tabla.ReadOnly = true;
            tabla.AllowUserToAddRows = false;
            tabla.RowHeadersVisible = false;
            tabla.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            tabla.Columns.Add("id", "Vuelo");
            tabla.Columns.Add("velocidad", "Velocidad");
            tabla.Columns.Add("posicion", "Posición actual");
            tabla.Columns.Add("destino", "Destino");

            AñadirVuelo(vuelo1);
            AñadirVuelo(vuelo2);

            tabla.CellClick += tabla_CellClick;
            this.Controls.Add(tabla);
        }

        private void AñadirVuelo(FlightPlan vuelo)
        {
            string posicion = vuelo.GetCurrentPosition().GetX() + ", " +
                              vuelo.GetCurrentPosition().GetY();
            string destino = vuelo.GetFinalPosition().GetX() + ", " +
                             vuelo.GetFinalPosition().GetY();

            tabla.Rows.Add(vuelo.GetId(), vuelo.GetVelocidad(), posicion, destino);
        }

        private void tabla_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
            {
                return;
            }

            FlightPlan seleccionado;
            FlightPlan otro;

            if (e.RowIndex == 0)
            {
                seleccionado = vuelo1;
                otro = vuelo2;
            }
            else
            {
                seleccionado = vuelo2;
                otro = vuelo1;
            }

            DistanciaVuelo ventana = new DistanciaVuelo(seleccionado, otro);
            ventana.ShowDialog(this);
        }
    }

    public class DistanciaVuelo : Form
    {
        public DistanciaVuelo(FlightPlan vuelo, FlightPlan otroVuelo)
        {
            double distancia = vuelo.Distance(otroVuelo);

            this.Text = "Distancia entre vuelos";
            this.Width = 400;
            this.Height = 140;
            this.StartPosition = FormStartPosition.CenterParent;

            Label texto = new Label();
            texto.AutoSize = true;
            texto.Left = 20;
            texto.Top = 25;
            texto.Text = "Distancia entre " + vuelo.GetId() + " y " +
                         otroVuelo.GetId() + ": " + distancia.ToString("0.##");

            this.Controls.Add(texto);
        }
    }
}
