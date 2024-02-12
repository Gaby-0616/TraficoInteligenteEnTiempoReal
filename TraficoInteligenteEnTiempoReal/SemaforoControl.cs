using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraficoInteligenteEnTiempoReal
{
    internal class SemaforosControl
    {
        public int Id { get; set; }
        public string Estado { get; set; }
        public int TiempoLuzVerde { get; set; }
        private int TiempoLuzRoja;

        public SemaforosControl(int id, string estado, int tiempoLuzVerde, int tiempoLuzRoja)
        {
            Id = id;
            Estado = estado;
            TiempoLuzVerde = tiempoLuzVerde;
            TiempoLuzRoja = tiempoLuzRoja;
        }

        public void ActualizarEstadoSemaforo(int idSemaforo, string nuevoEstado, string tipoVehiculo)
        {
            if (tipoVehiculo == "Autobús")
            {
                TiempoLuzVerde += 10;
            }
            else if (tipoVehiculo == "Motocicleta")
            {
                TiempoLuzVerde -= 5;
            }
        }


        public void ReducirTiempoLuzRoja()
        {
            Console.WriteLine("Reduciendo tiempo de la luz roja en los semáforos...");
            // Lógica para reducir el tiempo de la luz roja
            // Por ejemplo, podrías decrementar el tiempo en una cierta cantidad
            TiempoLuzRoja -= 5; // Reducción ficticia, ajusta según tus necesidades

            // Verifica que el tiempo no sea negativo para evitar valores no válidos
            if (TiempoLuzRoja < 0)
            {
                TiempoLuzRoja = 0;
            }

            Console.WriteLine($"Nuevo tiempo de la luz roja: {TiempoLuzRoja} segundos");
        }

        public void AumentarTiempoLuzRoja()
        {
            Console.WriteLine("Aumentando tiempo de la luz roja en los semáforos...");
            // Lógica para aumentar el tiempo de la luz roja
            // Por ejemplo, podrías incrementar el tiempo en una cierta cantidad
            TiempoLuzRoja += 5; // Aumento ficticio, ajusta según tus necesidades

            Console.WriteLine($"Nuevo tiempo de la luz roja: {TiempoLuzRoja} segundos");
        }
    }
}
