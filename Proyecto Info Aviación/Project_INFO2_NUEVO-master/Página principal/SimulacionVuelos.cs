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
    public partial class SimulacionVuelos : Form
    {
        FlightPlan vuelo1;
        FlightPlan vuelo2;
        double tiempoCiclo;
        double distanciaSeguridad;
        System.Windows.Forms.Timer temporizador;
        bool simulacionActiva = false;
        bool comprobacionInicialRealizada = false;
        int numeroPasos = 5000;
        // Imagen que se dibuja en lugar del punto azul.
        private Image avionImage;

        public SimulacionVuelos(FlightPlan vuelo1, FlightPlan vuelo2, double tiempoCiclo, double distanciaSeguridad)
        {
            InitializeComponent();
            // Busca la imagen junto al ejecutable.
            string rutaImagen = System.IO.Path.Combine(Application.StartupPath, "AvionP.png");
            // using libera la imagen original al terminar el bloque.
            using (Image imagen = Image.FromFile(rutaImagen))
            {
                // Copia la imagen para poder liberar el archivo original.
                avionImage = new Bitmap(imagen);
            }
            // Libera la copia en memoria al cerrar la ventana.
            // La flecha => es una función corta: al cerrar el formulario, ejecuta Dispose().
            FormClosed += (s, args) => avionImage.Dispose();
            // Activa el doble búfer del panel para reducir el parpadeo al redibujar.
            // Reflexión permite buscar y cambiar una propiedad mientras el programa se ejecuta.
            // Aquí se usa porque DoubleBuffered está protegida y no se puede cambiar directamente.
            typeof(Panel).GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(panelAviones, true, null);
            this.vuelo1 = vuelo1;
            this.vuelo2 = vuelo2;
            this.tiempoCiclo = tiempoCiclo;
            this.distanciaSeguridad = distanciaSeguridad;
            // El Label del diseñador muestra las ecuaciones calculadas para ambos vuelos.
            labelEcuaciones.Text = ObtenerEcuacionTrayectoria(vuelo1)
                + Environment.NewLine
                + ObtenerEcuacionTrayectoria(vuelo2);
            temporizador = new System.Windows.Forms.Timer();
            // Actualiza aproximadamente 60 veces por segundo.
            temporizador.Interval = 16;
            temporizador.Tick += Temporizador_Tick;
            panelAviones.MouseClick += panelAviones_MouseClick;
        }

        private void panelAviones_Paint(object sender, PaintEventArgs e)
        {
            DibujarTrayectoria(e.Graphics, vuelo1);
            DibujarTrayectoria(e.Graphics, vuelo2);
            DibujarDistanciaSeguridad(e.Graphics, vuelo1);
            DibujarDistanciaSeguridad(e.Graphics, vuelo2);
            DibujarAvion(e.Graphics, vuelo1);
            DibujarAvion(e.Graphics, vuelo2);
        }

        // Inicia o pausa el temporizador y actualiza el texto del botón.
        private void BotonAvanzar_Click(object sender, EventArgs e)
        {
            if (simulacionActiva == false)
            {
                if (comprobacionInicialRealizada == false)
                {
                    if (ComprobarYResolverConflicto() == false)
                    {
                        return;
                    }

                    comprobacionInicialRealizada = true;
                }

                simulacionActiva = true;
                BotonAvanzar.Text = "Detener";
                temporizador.Start();
            }
            else
            {
                simulacionActiva = false;
                temporizador.Stop();
                BotonAvanzar.Text = "Avanzar";
            }
        }

        private bool ComprobarYResolverConflicto()
        {
            double puntoX;
            double puntoY;

            if (PredecirConflicto(out puntoX, out puntoY) == false)
            {
                return true;
            }

            DialogResult respuesta = MessageBox.Show(
                this,
                CrearMensajeConflicto(puntoX, puntoY)
                    + Environment.NewLine
                    + "¿Quieres que cambie automáticamente la velocidad de un vuelo para evitarlo?",
                "Conflicto previsto",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (respuesta == DialogResult.No)
            {
                return true;
            }

            FlightPlan vueloModificado = IntentarResolverConflicto();

            if (vueloModificado == null)
            {
                MessageBox.Show(
                    this,
                    "No se encontró un cambio de velocidad que evite el conflicto. La simulación no se iniciará.",
                    "No se pudo resolver",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return false;
            }

            MessageBox.Show(
                this,
                "Se cambió la velocidad del vuelo " + vueloModificado.GetId()
                    + " a " + vueloModificado.GetVelocidad().ToString("0.####")
                    + ". La simulación comenzará con esa velocidad.",
                "Conflicto resuelto",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            return true;
        }

        private FlightPlan IntentarResolverConflicto()
        {
            double velocidadOriginal1 = vuelo1.GetVelocidad();
            double velocidadOriginal2 = vuelo2.GetVelocidad();
            double puntoX;
            double puntoY;

            // Prueba velocidades entre el 10% y el 300% para el primer vuelo.
            // Por ejemplo, porcentaje = 15 significa probar el 150% de la velocidad original.
            // Cada velocidad candidata se comprueba con la misma predicción de conflicto.
            for (int porcentaje = 1; porcentaje <= 30; porcentaje++)
            {
                if (porcentaje == 10)
                {
                    // 10 equivale al 100%: sería volver a probar la velocidad original.
                    continue;
                }

                double velocidadNueva = velocidadOriginal1 * porcentaje / 10.0;
                vuelo1.SetVelocidad(velocidadNueva);

                if (PredecirConflicto(out puntoX, out puntoY) == false)
                {
                    return vuelo1;
                }
            }

            vuelo1.SetVelocidad(velocidadOriginal1);

            // Si no funciona el primer vuelo, prueba cambiar la velocidad del segundo.
            for (int porcentaje = 1; porcentaje <= 30; porcentaje++)
            {
                if (porcentaje == 10)
                {
                    // También se omite el 100% del segundo avión, pues ya se probó sin cambios.
                    continue;
                }

                double velocidadNueva = velocidadOriginal2 * porcentaje / 10.0;
                vuelo2.SetVelocidad(velocidadNueva);

                if (PredecirConflicto(out puntoX, out puntoY) == false)
                {
                    return vuelo2;
                }
            }

            vuelo2.SetVelocidad(velocidadOriginal2);
            return null;
        }
        // Calcula y devuelve la ecuación de la recta entre el inicio y el destino.
        private string ObtenerEcuacionTrayectoria(FlightPlan vuelo)
        {
            double xInicial = vuelo.GetInitialPosition().GetX();
            double yInicial = vuelo.GetInitialPosition().GetY();
            double xFinal = vuelo.GetFinalPosition().GetX();
            double yFinal = vuelo.GetFinalPosition().GetY();
            double diferenciaX = xFinal - xInicial;
            // Si X casi no cambia, la trayectoria es vertical y se expresa como x = constante.
            if (Math.Abs(diferenciaX) < 0.000001)
            {
                return "Vuelo " + vuelo.GetId() + ": x = " + xInicial.ToString("0.##");
            }
            // La pendiente (cambio en Y dividido entre cambio en X) describe la recta como y = mx + b.
            // Si X no cambia, la división daría error; por eso arriba se trata aparte la recta vertical.
            double pendiente = (yFinal - yInicial) / diferenciaX;
            // Calcula dónde cruza la recta el eje Y (el término b de y = mx + b).
            double ordenada = yInicial - pendiente * xInicial;
            // El operador ?: selecciona el texto del signo según el valor de la ordenada.
            string signo = ordenada < 0 ? " - " : " + ";
            return "Vuelo " + vuelo.GetId() + ": y = "+ pendiente.ToString("0.##") + "x" + signo+ Math.Abs(ordenada).ToString("0.##");
        }
        private void DibujarTrayectoria(Graphics dibujo, FlightPlan vuelo)
        {
            int xInicial = (int)vuelo.GetInitialPosition().GetX();
            int yInicial = (int)vuelo.GetInitialPosition().GetY();
            int xFinal = (int)vuelo.GetFinalPosition().GetX();
            int yFinal = (int)vuelo.GetFinalPosition().GetY();
            Pen lapiz = new Pen(Color.LightGray, 2);
            dibujo.DrawLine(lapiz, xInicial, yInicial, xFinal, yFinal);

            lapiz.Dispose();
        }
        private void DibujarDistanciaSeguridad(
            Graphics dibujo,
            FlightPlan vuelo)
        {
            int x = (int)vuelo.GetCurrentPosition().GetX();
            int y = (int)vuelo.GetCurrentPosition().GetY();
            int radio = (int)distanciaSeguridad;
            Pen lapiz = new Pen(Color.Orange, 2);
            dibujo.DrawEllipse( lapiz,x - radio, y - radio,radio * 2,radio * 2);
            lapiz.Dispose();
        }
        // Dibuja el avión centrado en su posición y con el morro orientado al destino.
        private void DibujarAvion(Graphics dibujo, FlightPlan vuelo)
        {
            int x = (int)vuelo.GetCurrentPosition().GetX();
            int y = (int)vuelo.GetCurrentPosition().GetY();
            // Tamaño en píxeles del icono dibujado en el panel.
            const int tamañoAvion = 24;
            double dx = vuelo.GetFinalPosition().GetX() - vuelo.GetCurrentPosition().GetX();
            double dy = vuelo.GetFinalPosition().GetY() - vuelo.GetCurrentPosition().GetY();
            // Atan2 convierte los cambios X/Y en un ángulo de dirección, incluso si el avión va hacia atrás o arriba.
            // Se pasa de radianes a grados porque el dibujo de Windows Forms usa grados.
            // Los 45 grados corrigen la orientación original del icono.
            float anguloRumbo = (float)(Math.Atan2(dy, dx) * 180.0 / Math.PI + 45.0);
            // Guarda y restaura el estado para que el giro solo afecte al dibujo de este avión.
            System.Drawing.Drawing2D.GraphicsState estado = dibujo.Save();
            // Mueve temporalmente el origen de coordenadas al avión, para que gire sobre su centro.
            dibujo.TranslateTransform(x, y);
            // Gira la imagen según el rumbo calculado.
            dibujo.RotateTransform(anguloRumbo);
            dibujo.DrawImage(avionImage,-tamañoAvion / 2, -tamañoAvion / 2, tamañoAvion,tamañoAvion);
            dibujo.Restore(estado);
            dibujo.DrawString(vuelo.GetId(),this.Font,Brushes.Black, x + 14, y);
        }
        private void panelAviones_MouseClick(object sender, MouseEventArgs e)
        {
            int x1 = (int)vuelo1.GetCurrentPosition().GetX();
            int y1 = (int)vuelo1.GetCurrentPosition().GetY();
            // El margen de 14 píxeles hace más fácil pulsar sobre el icono del avión.
            if (Math.Abs(e.X - x1) <= 14 && Math.Abs(e.Y - y1) <= 14)
            {
                MostrarInformacion(vuelo1);
                return;
            }
            int x2 = (int)vuelo2.GetCurrentPosition().GetX();
            int y2 = (int)vuelo2.GetCurrentPosition().GetY();

            if (Math.Abs(e.X - x2) <= 14&& Math.Abs(e.Y - y2) <= 14)
            {
                MostrarInformacion(vuelo2);
            }
        }

        private void MostrarInformacion(FlightPlan vuelo)
        {
            string coordenadaX = vuelo.GetCurrentPosition().GetX().ToString("0.####");
            string coordenadaY = vuelo.GetCurrentPosition().GetY().ToString("0.####");

            MessageBox.Show("Vuelo: " + vuelo.GetId()
                + "\nVelocidad: " + vuelo.GetVelocidad()
                + "\nPosición actual: " + coordenadaX + ", " + coordenadaY
                + "\n¿Ha llegado?: " + vuelo.HasArrived());
        }

        private void panelAviones_MouseClick_1(object sender, MouseEventArgs e)
        {
            int x1 = (int)vuelo1.GetCurrentPosition().GetX();
            int y1 = (int)vuelo1.GetCurrentPosition().GetY();

            // El margen de 14 píxeles hace más fácil pulsar sobre el icono del avión.
            if (Math.Abs(e.X - x1) <= 14 && Math.Abs(e.Y - y1) <= 14)
            {
                MostrarInformacion(vuelo1);
                return;
            }

            int x2 = (int)vuelo2.GetCurrentPosition().GetX();
            int y2 = (int)vuelo2.GetCurrentPosition().GetY();

            if (Math.Abs(e.X - x2) <= 14 && Math.Abs(e.Y - y2) <= 14)
            {
                MostrarInformacion(vuelo2);
            }
        }
        // Calcula la menor separación prevista considerando las velocidades y las llegadas.
        // puntoX y puntoY son parámetros de salida: además del true/false, el método devuelve
        // las coordenadas del punto medio donde los aviones estarán más cerca.
        private bool PredecirConflicto(out double puntoX, out double puntoY)
        {
            double x1Inicio = vuelo1.GetCurrentPosition().GetX();
            double y1Inicio = vuelo1.GetCurrentPosition().GetY();
            double x2Inicio = vuelo2.GetCurrentPosition().GetX();
            double y2Inicio = vuelo2.GetCurrentPosition().GetY();
            double x1Final = vuelo1.GetFinalPosition().GetX();
            double y1Final = vuelo1.GetFinalPosition().GetY();
            double x2Final = vuelo2.GetFinalPosition().GetX();
            double y2Final = vuelo2.GetFinalPosition().GetY();
            double distancia1 = Distancia(x1Inicio, y1Inicio, x1Final, y1Final);
            double distancia2 = Distancia(x2Inicio, y2Inicio, x2Final, y2Final);
            double velocidad1 = vuelo1.GetVelocidad();
            double velocidad2 = vuelo2.GetVelocidad();
            // Tiempo = distancia / velocidad. Con ese tiempo se calcula cuánto avanza cada avión
            // en X y en Y por unidad de tiempo (vx y vy).
            // ?: calcula distancia/velocidad si la velocidad es positiva; si no, asigna cero.
            double tiempoLlegada1 = velocidad1 > 0 ? distancia1 / velocidad1 : 0;
            double tiempoLlegada2 = velocidad2 > 0 ? distancia2 / velocidad2 : 0;
            // ?: calcula la velocidad de cada eje; evita dividir por cero si no hay tiempo de vuelo.
            double vx1 = tiempoLlegada1 > 0 ? (x1Final - x1Inicio) / tiempoLlegada1 : 0;
            double vy1 = tiempoLlegada1 > 0 ? (y1Final - y1Inicio) / tiempoLlegada1 : 0;
            double vx2 = tiempoLlegada2 > 0 ? (x2Final - x2Inicio) / tiempoLlegada2 : 0;
            double vy2 = tiempoLlegada2 > 0 ? (y2Final - y2Inicio) / tiempoLlegada2 : 0;
            // Separa el tiempo en tramos porque la situación cambia cuando un avión llega:
            // antes de llegar se mueve; después permanece quieto en su destino.
            List<double> limitesTiempo = new List<double> { 0, tiempoLlegada1, tiempoLlegada2 };
            limitesTiempo.Sort(); // Deja primero el tiempo inicial y después las llegadas.
            double minimaDistanciaCuadrada = DistanciaCuadrada(x1Inicio - x2Inicio, y1Inicio - y2Inicio);
            // Si el mínimo está al inicio, el punto de aviso será el punto medio actual.
            puntoX = (x1Inicio + x2Inicio) / 2;
            puntoY = (y1Inicio + y2Inicio) / 2;
            for (int i = 0; i < limitesTiempo.Count - 1; i++)
            {
                double inicioIntervalo = limitesTiempo[i];
                double finIntervalo = limitesTiempo[i + 1];
                double duracion = finIntervalo - inicioIntervalo;
                if (duracion <= 0)
                    continue;
                // El punto medio del intervalo indica si cada avión todavía está en movimiento.
                double puntoMedio = inicioIntervalo + duracion / 2;
                bool vuelo1EnMovimiento = puntoMedio < tiempoLlegada1;
                bool vuelo2EnMovimiento = puntoMedio < tiempoLlegada2;
                // ?: usa posición interpolada mientras vuela o el destino si ya llegó.
                double x1 = vuelo1EnMovimiento ? x1Inicio + vx1 * inicioIntervalo : x1Final;
                double y1 = vuelo1EnMovimiento? y1Inicio + vy1 * inicioIntervalo : y1Final;
                double x2 = vuelo2EnMovimiento? x2Inicio + vx2 * inicioIntervalo : x2Final;
                double y2 = vuelo2EnMovimiento? y2Inicio + vy2 * inicioIntervalo : y2Final;
                // La velocidad relativa describe cómo cambia la separación entre los aviones.
                // Restar sus velocidades permite calcular si se acercan o se alejan en este tramo.
                double velocidadRelativaX = (vuelo1EnMovimiento ? vx1 : 0)- (vuelo2EnMovimiento ? vx2 : 0);
                double velocidadRelativaY = (vuelo1EnMovimiento ? vy1 : 0)- (vuelo2EnMovimiento ? vy2 : 0);
                double diferenciaX = x1 - x2;
                double diferenciaY = y1 - y2;
                double velocidadRelativaCuadrada = DistanciaCuadrada(velocidadRelativaX,velocidadRelativaY);
                double tiempoMasCercano = 0;
                if (velocidadRelativaCuadrada > 0)
                {
                    // Esta fórmula encuentra el instante del tramo en que la distancia entre ambos es mínima.
                    // La separación cambia según la velocidad relativa; la fórmula calcula cuándo
                    // esa separación queda más corta. Después se limita al tramo que estamos revisando.
                    tiempoMasCercano = -(diferenciaX * velocidadRelativaX+ diferenciaY * velocidadRelativaY) / velocidadRelativaCuadrada;
                    // Recorta ese instante para que quede dentro del intervalo actual.
                    tiempoMasCercano = Math.Max(0, Math.Min(duracion, tiempoMasCercano));
                }
                // Calcula las posiciones de ambos aviones en el instante más cercano.
                double posicion1X = x1 + (vuelo1EnMovimiento ? vx1 : 0) * tiempoMasCercano;
                double posicion1Y = y1 + (vuelo1EnMovimiento ? vy1 : 0) * tiempoMasCercano;
                double posicion2X = x2 + (vuelo2EnMovimiento ? vx2 : 0) * tiempoMasCercano;
                double posicion2Y = y2 + (vuelo2EnMovimiento ? vy2 : 0) * tiempoMasCercano;
                double distanciaCuadrada = DistanciaCuadrada(posicion1X - posicion2X,posicion1Y - posicion2Y);
                if (distanciaCuadrada < minimaDistanciaCuadrada)
                {
                    minimaDistanciaCuadrada = distanciaCuadrada;
                    // El aviso señala el punto medio entre las posiciones de máxima proximidad.
                    puntoX = (posicion1X + posicion2X) / 2;
                    puntoY = (posicion1Y + posicion2Y) / 2;
                }
            }
            // Hay conflicto previsto si la menor separación queda dentro de la distancia de seguridad.
            return Math.Sqrt(minimaDistanciaCuadrada) < distanciaSeguridad;
        }
        // Construye el texto con los dos identificadores y las coordenadas del punto estimado.
        private string CrearMensajeConflicto(double puntoX, double puntoY)
        {
            return "Entre el vuelo " + vuelo1.GetId()+ " y el vuelo " + vuelo2.GetId()+ " va a haber un conflicto en el punto de sus trayectorias: (" + puntoX.ToString("0.##") + ", "+ puntoY.ToString("0.##") + ").";
        }
        // Calcula la distancia recta entre dos coordenadas.
        private double Distancia(double x1, double y1, double x2, double y2)
        {
            return Math.Sqrt(DistanciaCuadrada(x2 - x1, y2 - y1));
        }
        // Calcula la suma de cuadrados; se usa para comparar distancias sin raíz.
        private double DistanciaCuadrada(double x, double y)
        {
            return x * x + y * y;
        }
        // Avanza ambos aviones, pide el redibujado y comprueba conflicto o llegada.
        private void Temporizador_Tick(object sender, EventArgs e)
        {
            // Escala el avance al intervalo de 16 ms para mantener la velocidad original.
            // Se divide el ciclo de simulación en muchos pasos pequeños para que el movimiento
            // y la detección de conflictos se actualicen con más frecuencia y se vean suaves.
            double tiempoPequeño = tiempoCiclo / numeroPasos * (temporizador.Interval / 100.0);
            vuelo1.Mover(tiempoPequeño);
            vuelo2.Mover(tiempoPequeño);
            // Solicita que se vuelva a dibujar el panel con las nuevas posiciones.
            panelAviones.Invalidate();
            if (vuelo1.Conflicto(vuelo2, distanciaSeguridad))
            {
                temporizador.Stop();
                simulacionActiva = false;
                // El botón vuelve a indicar que se puede iniciar/reanudar.
                BotonAvanzar.Text = "Avanzar";

                // El punto del aviso durante la simulación es el punto medio de las posiciones actuales.
                double puntoX = (vuelo1.GetCurrentPosition().GetX() + vuelo2.GetCurrentPosition().GetX()) / 2;
                double puntoY = (vuelo1.GetCurrentPosition().GetY() + vuelo2.GetCurrentPosition().GetY()) / 2;
                MessageBox.Show(CrearMensajeConflicto(puntoX, puntoY),"Conflicto detectado",MessageBoxButtons.OK,MessageBoxIcon.Warning);
            }
            if (vuelo1.HasArrived() && vuelo2.HasArrived())
            {
                temporizador.Stop();
                simulacionActiva = false;
                // Restablece el texto cuando los dos vuelos han llegado.
                BotonAvanzar.Text = "Avanzar";
                MessageBox.Show("Los dos aviones han llegado a su destino.");
            }
        }
        private void botonConflicto_Click(object sender, EventArgs e)
        {
            double puntoX;
            double puntoY;

            if (PredecirConflicto(out puntoX, out puntoY))
            {
                MessageBox.Show(
                    CrearMensajeConflicto(puntoX, puntoY),
                    "Conflicto previsto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
            else
            {
                MessageBox.Show(
                    "No se prevé que los vuelos entren en conflicto.",
                    "Comprobación de conflicto",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            temporizador.Stop();
            this.Close();
        }
    }

}





