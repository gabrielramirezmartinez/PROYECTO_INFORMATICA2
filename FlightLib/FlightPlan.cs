using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightLib
{
    public class FlightPlan
    {
        // Atributos

        string id; // identificador
        Position currentPosition; // posicion actual
        Position finalPosition; // posicion final
        Position InitialPosition; // posicion inicial
        double velocidad;

        // Constructures
        public FlightPlan(string id, double cpx, double cpy, double fpx, double fpy, double ipx, double ipy, double velocidad)
        {
            this.id = id;
            this.currentPosition = new Position(cpx, cpy);
            this.finalPosition = new Position(fpx, fpy);
            this.InitialPosition = new Position(ipx, ipy);
            this.velocidad = velocidad;
        }

        // Metodos

        public string GetId()
        { return id; }
        public void SetId(string id)
        { this.id = id; }
        public Position GetCurrentPosition()
        { return currentPosition; }
        public void SetCurrentPosition(Position currentPosition)
        { this.currentPosition = currentPosition; }
        public Position GetFinalPosition()
        { return finalPosition; }
        public void SetFinalPosition(Position finalPosition)
        { this.finalPosition = finalPosition; }
        public double GetVelocidad()
        { return velocidad; }
        public void SetVelocidad(double velocidad)
        { this.velocidad = velocidad; }
        public Position GetInitialPosition()
        { return InitialPosition; }
        public void SetInitialPosition(Position initialPosition)
        { this.InitialPosition = initialPosition; }

        // Hacer un metodo que diga si un vuelo ha llegado a su destino
        public bool EstaDestino()
        {
            return HasArrived();
        }

        // Hacer un metodo que detecte el conflicto cuando dos vuelos están más cerca de la distancia de seguridad
        public bool Conflicto(FlightPlan b, double distanciaSeguridad)
        {
            bool conflicto = false;
            if (this.currentPosition.Distancia(b.currentPosition) < distanciaSeguridad)
                conflicto = true;
            return conflicto;
        }
        public void EscribeConsola()
        // escribe en consola los datos del plan de vuelo
        {
            Console.WriteLine("******************************");
            Console.WriteLine("Datos del vuelo: ");
            Console.WriteLine("Identificador: {0}", id);
            // Buscar como ecribir datos reales con solo dos decimales
            Console.WriteLine("Velocidad: {0:F2}", velocidad);
            Console.WriteLine("Posición actual: ({0:F2},{1:F2})", currentPosition.GetX(), currentPosition.GetY());
            if (this.EstaDestino())
                Console.WriteLine("Ha llegado al destino");
            Console.WriteLine("******************************");
        }
        public bool HasArrived()
        {
            if (this.currentPosition.GetX() == this.finalPosition.GetX() && this.currentPosition.GetY() == this.finalPosition.GetY())
            {
                return true;

            }
            else
            {
                return false;
            }

        }
        public void Mover(double tiempo)
        // Mueve el vuelo a la posición correspondiente a viajar durante el tiempo que se recibe como parámetro
        {
            if (HasArrived() == true)
            {
                return;
            }

            // Calculamos la distancia que falta hasta el destino final
            double distanciaX = this.finalPosition.GetX() - this.currentPosition.GetX();
            double distanciaY = this.finalPosition.GetY() - this.currentPosition.GetY();

            // Distancia total en línea recta (Pitágoras)
            double distanciaTotal = Math.Sqrt((distanciaX * distanciaX) + (distanciaY * distanciaY));

            // Espacio que avanza en este ciclo (Velocidad * Tiempo)
            double espacioAvanzar = this.velocidad * tiempo;

            // Si lo que va a avanzar es mayor o igual a lo que le falta, llega al final
            if (espacioAvanzar >= distanciaTotal)
            {
                this.currentPosition.SetX(this.finalPosition.GetX());
                this.currentPosition.SetY(this.finalPosition.GetY());
            }
            else
            {
                // Avanzamos proporcionalmente
                double nuevaX = this.currentPosition.GetX() + (espacioAvanzar * (distanciaX / distanciaTotal));
                double nuevaY = this.currentPosition.GetY() + (espacioAvanzar * (distanciaY / distanciaTotal));

                this.currentPosition.SetX(nuevaX);
                this.currentPosition.SetY(nuevaY);

            }
        }
        public void Restart()
        {
            this.currentPosition.SetX(this.InitialPosition.GetX());
            this.currentPosition.SetY(this.InitialPosition.GetY());
        }
        public double Distance (FlightPlan plan)
        {
            double diffX=this.currentPosition.GetX() - plan.currentPosition.GetX();
            double diffY=this.currentPosition.GetY() - plan.currentPosition.GetY();
            double distanciafinal = Math.Sqrt((diffX * diffX) + (diffY * diffY));
            return distanciafinal;
        }
    }
}