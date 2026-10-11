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
    public partial class FormTablaVuelos : Form
    {
        private FlightPlan vuelo1;
        private FlightPlan vuelo2;

        public FormTablaVuelos(FlightPlan v1, FlightPlan v2)
        {
            InitializeComponent();
            this.vuelo1 = v1;
            this.vuelo2 = v2;

            CargarDatosEnGrid();

            // Suscribimos el evento cuando el usuario hace clic en una fila de la tabla
            dataGridView1.CellClick += DataGridView1_CellClick;
        }

        private void CargarDatosEnGrid()
        {
            // Limpiamos por si acaso y definimos las columnas
            dataGridView1.Columns.Clear();
            dataGridView1.Columns.Add("Id", "Identificador");
            dataGridView1.Columns.Add("Velocidad", "Velocidad");
            dataGridView1.Columns.Add("PosX", "Posición Actual X");
            dataGridView1.Columns.Add("PosY", "Posición Actual Y");

            // Añadimos los datos del Vuelo 1
            dataGridView1.Rows.Add(vuelo1.GetId(), vuelo1.GetVelocidad(),
                                   vuelo1.GetCurrentPosition().GetX().ToString("0.##"),
                                   vuelo1.GetCurrentPosition().GetY().ToString("0.##"));

            // Añadimos los datos del Vuelo 2
            dataGridView1.Rows.Add(vuelo2.GetId(), vuelo2.GetVelocidad(),
                                   vuelo2.GetCurrentPosition().GetX().ToString("0.##"),
                                   vuelo2.GetCurrentPosition().GetY().ToString("0.##"));
        }

        private void DataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Comprobamos que se hizo clic en una fila válida (evitando el encabezado)
            if (e.RowIndex >= 0)
            {
                FlightPlan vueloSeleccionado;
                FlightPlan vueloOtro;

                // Si selecciona la fila 0 es el vuelo 1; si es la fila 1 es el vuelo 2
                if (e.RowIndex == 0)
                {
                    vueloSeleccionado = vuelo1;
                    vueloOtro = vuelo2;
                }
                else
                {
                    vueloSeleccionado = vuelo2;
                    vueloOtro = vuelo1;
                }

                // Abrimos el formulario de distancia pasándole el vuelo seleccionado y el otro
                FormDistanciaGrid formDistancia = new FormDistanciaGrid(vueloSeleccionado, vueloOtro);
                formDistancia.ShowDialog();
            }
        }
        private void btnCerrar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
    

