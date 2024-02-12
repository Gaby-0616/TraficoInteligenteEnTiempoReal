using System;
using System.Drawing;
using static TraficoInteligenteEnTiempoReal.SensorTráfico;

namespace TraficoInteligenteEnTiempoReal
{
    internal class ControlTráfico
    {
        private readonly SensorTráfico sensorDeTráfico;
        private readonly List<SensorTráfico> sensores;
        private readonly SemaforosControl semaforosControl;
        private readonly Semaphore _consumePeticion = new Semaphore(0, 1);
        private readonly object _mutex = new object();
        private static Queue<PeticionSemaforo> _peticiones = new Queue<PeticionSemaforo>();

        public ControlTráfico(SensorTráfico sensorDeTráfico, List<SensorTráfico> sensores, SemaforosControl semaforosControl)
        {
            this.sensores = sensores ?? throw new ArgumentNullException(nameof(sensores), "La lista de sensores no puede ser nula.");
            this.sensorDeTráfico = sensorDeTráfico ?? throw new ArgumentNullException(nameof(sensorDeTráfico), "El sensor de tráfico no puede ser nulo.");
            this.semaforosControl = semaforosControl;
        }

    

        // Método para recopilar datos de tráfico
        public void RecopilarDatosDeTráfico()
        {
            
            Console.WriteLine("Iniciando recopilación de datos de tráfico...");

            foreach (var sensor in sensores)
            {
                var alerta = sensorDeTráfico.DetectarVehículo;
                var datos = SensorTráfico.ObtenerDatosTrafico();
                // Lógica para procesar los datos recopilados, por ejemplo, almacenarlos en una base de datos central
                ProcesarDatos(datos);

                
            }

            Console.WriteLine("Recopilación de datos de tráfico completada.");
        }



        // Método para optimizar los tiempos de semáforos basándose en los datos de tráfico
        public void OptimizarTiempoSemaforo()
        {
            Console.WriteLine("Optimizando los tiempos de los semáforos basándose en los datos de tráfico...");

            //var semaforo = new Semaforo(); // Se crea una instancia de la clase Semaforo

            int totalVehiculos = 0;
            int totalPeatones = 0;
            foreach (var sensor in sensores)
            {
                totalVehiculos += SensorTráfico.ObtenerDatosTrafico().CantidadVehiculos;
                totalPeatones += SensorTráfico.ObtenerDatosTrafico().CantidadPeatones;
            }

            if (totalPeatones > 10 && totalVehiculos < 10)
            {
                semaforosControl.CambiarColorSemaforo("Verde");
                semaforosControl.CambiarEstadoRojo();
            }
            else
            {
                semaforosControl.CambiarColorSemaforo("Rojo");
            }

            if (totalVehiculos < 10)
            {
                // Tráfico bajo: reducimos el tiempo de luz roja
                Console.WriteLine("Tráfico bajo. Reduciendo tiempo de luz roja.");
                ReducirTiempoLuzRoja();
            }
            else
            {
                // Tráfico alto: aumentamos el tiempo de luz roja
                Console.WriteLine("Tráfico alto. Aumentando tiempo de luz roja.");
                AumentarTiempoLuzRoja();
            }
        }

        // Método para enviar tiempos optimizados a los semáforos
        public void EnviarTiemposOptimizadosALosSemáforos()
        {
            Console.WriteLine("Enviando tiempos optimizados a los semáforos...");
        }

        private void ProcesarDatos(DatosTrafico datos)
        {
            
            Console.WriteLine("Procesando datos de tráfico...");
            var peticion = new PeticionSemaforo(
               semaforosControl.Id,
                 CalcularNuevoEstado(datos.CantidadVehiculos, datos.CantidadPeatones)
           );

            lock (_mutex)
            {
                _peticiones.Enqueue(peticion);
            }

            _consumePeticion.Release();
        }

        private void ReducirTiempoLuzRoja()
        {
           
            semaforosControl.ReducirTiempoLuzRoja();
            Console.WriteLine("Tiempo de luz roja reducido.");
        }

        private void AumentarTiempoLuzRoja()
        {
            
            semaforosControl.AumentarTiempoLuzRoja();
            Console.WriteLine("Tiempo de luz roja aumentado.");
        }

        private string CalcularNuevoEstado(int cantidadVehiculos, int cantidadPeatones)
        {
            string tipoConductor = null;
            if (cantidadVehiculos > 10 && cantidadPeatones > 10)
            {
                return "Rojo";
            }
            else if (tipoConductor == "Autobús")
            {
                return "Verde";
            }
            else
            {
                return "Amarillo";
            }
        }
        public void IniciarHebraConsumidora()
        {
            var hebraConsumidora = new Thread(HebraConsumidora);
            hebraConsumidora.Start();
        }

        private void HebraConsumidora()
        {   
            while (true)
            {
                _consumePeticion.WaitOne();

                lock (_mutex)
                {
                    if (_peticiones.Count > 0)
                    {
                        var peticion = _peticiones.Dequeue();

                        semaforosControl.ActualizarEstadoSemaforo(peticion.IdSemaforo, "Verde", "Autobús");

                        Console.WriteLine($"Estado del semáforo {peticion.IdSemaforo} actualizado a {"Verde"} para {"Autobús"}.");
                    }
                }

                
            }
        }

    }
}
