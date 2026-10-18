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
    public partial class PáginaPrincipal : Form
    {
        public static FlightLib.FlightPlan avion1 = null;
        public static FlightLib.FlightPlan avion2 = null;
        FlightPlanList vuelosGuardados = new FlightPlanList();
        double distanciaSeguridad = 0;
        double tiempoCiclo = 0;
        System.Windows.Forms.Timer temporizador;
        Button botonAvanzar;
        bool simulacionActiva = false;
        int numeroPasos = 20;
        public PáginaPrincipal()
        {
            InitializeComponent();
        }
        
        private void PlanesDeVuelo_Click(object sender, EventArgs e)
        {
            //Creas la instancia del formulario al que quieres ir
            PlanesDeVuelo formPlanesVuelo = new PlanesDeVuelo();
            //Muestras el nuevo formulario
            formPlanesVuelo.ShowDialog();
            FlightPlanList listaRecuperada = formPlanesVuelo.GetListaVuelos();

            //(GetNumber() nos dice cuántos vuelos hay en el vector)
            if (listaRecuperada.GetNumber() > 0)
            {
                // Guardamos la lista recuperada en nuestra variable del menú principal
                this.vuelosGuardados = listaRecuperada;

                MessageBox.Show("Se han recuperado " + vuelosGuardados.GetNumber() + " planes de vuelo desde el formulario.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ParametrosTiempoDistancia formulario = new ParametrosTiempoDistancia();
            DialogResult resultado = formulario.ShowDialog();
            if (resultado == DialogResult.OK)
            {
                distanciaSeguridad = formulario.GetDistanciaSeguridad();
                tiempoCiclo = formulario.GetTiempoCiclo();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (vuelosGuardados.GetNumber() < 2)
            {
                MessageBox.Show("No hay suficientes vuelos guardados para iniciar la simulación. Debes guardar al menos dos vuelos.");
                return;
            }
            if (distanciaSeguridad <= 0 || tiempoCiclo <= 0)
            {
                MessageBox.Show("Primero guarda los parámetros de distancia de seguridad y tiempo de ciclo antes de iniciar la simulación.");
                return;
            }
            FlightPlan vuelo1 = vuelosGuardados.GetFlightPlan(0);
            FlightPlan vuelo2 = vuelosGuardados.GetFlightPlan(1);
            SimulacionVuelos formulario = new SimulacionVuelos(vuelo1, vuelo2, tiempoCiclo, distanciaSeguridad);
            formulario.ShowDialog();
        }
    }
}
