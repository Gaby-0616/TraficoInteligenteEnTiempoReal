using System;
using System.Collections.Generic;
using System.Drawing;
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
        private readonly object _mutex = new object();

        public SemaforosControl(int id, string estado, int tiempoLuzVerde, int tiempoLuzRoja)
        {
            if (id <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(id), "El identificador del semáforo debe ser mayor que cero.");
            }

            if (string.IsNullOrEmpty(estado))
            {
                throw new ArgumentNullException(nameof(estado), "El estado del semáforo no puede ser nulo o vacío.");
            }

            if (tiempoLuzVerde <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(tiempoLuzVerde), "La duración de la luz verde debe ser mayor que cero.");
            }

            if (tiempoLuzRoja <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(tiempoLuzRoja), "La duración de la luz roja debe ser mayor que cero.");
            }
            Id = id;
            Estado = estado;
            TiempoLuzVerde = tiempoLuzVerde;
            TiempoLuzRoja = tiempoLuzRoja;
        }




        public void ActualizarEstadoSemaforo(int idSemaforo, string nuevoEstado, string tipoVehiculo)
        {

            if (idSemaforo != Id)
            {
                throw new ArgumentException("El identificador del semáforo no coincide con el de la instancia actual.");
            }

            if (!Enum.TryParse<TipoVehiculo>(tipoVehiculo, out TipoVehiculo tipoVehiculoEnum))
            {
                throw new ArgumentException("El tipo de vehículo especificado no es válido.");
            }
            lock (_mutex)
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
        }
        public void CambiarColorSemaforo(string color)
        {
            // Implementar lógica para cambiar el color del semáforo
            // ...

            Console.WriteLine($"Se ha cambiado el color del semáforo a {color}.");
        }

        public void ReducirTiempoLuzRoja()
        {
            Console.WriteLine("Reduciendo tiempo de la luz roja en los semáforos...");
            lock (_mutex)
            {
                if (TiempoLuzRoja <= 0)
                {
                    throw new InvalidOperationException("El tiempo de la luz roja ya es mínimo (0 segundos).");
                }
                // Lógica para reducir el tiempo de la luz roja
                // Por ejemplo, podrías decrementar el tiempo en una cierta cantidad
                TiempoLuzRoja -= 5; // Reducción ficticia, ajusta según tus necesidades

                // Verifica que el tiempo no sea negativo para evitar valores no válidos
                if (TiempoLuzRoja < 0)
                {
                    TiempoLuzRoja = 0;
                }
            }
            Console.WriteLine($"Nuevo tiempo de la luz roja: {TiempoLuzRoja} segundos");
        }

        public void AumentarTiempoLuzRoja()
        {
            lock (_mutex)
            {
                TiempoLuzRoja += 5; // Aumento ficticio, ajusta según tus necesidades
            }

            Console.WriteLine($"Nuevo tiempo de la luz roja: {TiempoLuzRoja} segundos");
        }


        public void CambiarEstadoVerde()
        {
            CambiarColorSemaforo("Verde");
        }

        public void CambiarEstadoRojo()
        {
            CambiarColorSemaforo("Rojo");
        }

        public void CambiarEstadoAmarillo()
        {
            CambiarColorSemaforo("Amarillo");
        }

        public enum TipoVehiculo
        {
            Autobús,
            Motocicleta,
            Coche,
            Camión
        }
    }
}
