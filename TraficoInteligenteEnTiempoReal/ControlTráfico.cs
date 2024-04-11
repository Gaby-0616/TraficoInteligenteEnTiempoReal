using System;
using System.Collections.Concurrent;
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
        private readonly BlockingCollection<PeticionSemaforo> _colaPeticiones = new BlockingCollection<PeticionSemaforo>();
        private Thread _hiloProcesamientoDatos;
        public ControlTráfico(SensorTráfico sensorDeTráfico, List<SensorTráfico> sensores, SemaforosControl semaforosControl)
        {
            this.sensores = sensores ?? throw new ArgumentNullException(nameof(sensores), "La lista de sensores no puede ser nula.");
            this.sensorDeTráfico = sensorDeTráfico ?? throw new ArgumentNullException(nameof(sensorDeTráfico), "El sensor de tráfico no puede ser nulo.");
            this.semaforosControl = semaforosControl;
            _hiloProcesamientoDatos = new Thread(() => ProcesarDatosDeSensores());
            _hiloProcesamientoDatos.Start();
        }

        private void ProcesarDatosDeSensores()
        {
            while (true)
            {
                try
                {
                    var alerta = sensorDeTráfico.DetectarVehículo;
                    var datos = SensorTráfico.ObtenerDatosTrafico();

                    // Procesar datos y obtener estado del semáforo
                    // ...

                    var peticion = new PeticionSemaforo(
                 semaforosControl.Id,
                 CalcularNuevoEstado(datos.CantidadVehiculos, datos.CantidadPeatones)
                 );
                }
                catch (Exception ex)
                {
                    // ... (manejar errores)
                }
            }
        }


        // Método para recopilar datos de tráfico
        public void RecopilarDatosDeTráfico()
        {

            Console.WriteLine("Iniciando recopilación de datos de tráfico...");

            foreach (var sensor in sensores)
            {
                var alerta = sensorDeTráfico.DetectarVehículo;
                try
                {
                    var datos = SensorTráfico.ObtenerDatosTrafico();
                    ProcesarDatos(datos);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener datos del sensor: {ex.Message}");
                }
            }

            Console.WriteLine("Recopilación de datos de tráfico completada.");
        }



        // Método para optimizar los tiempos de semáforos basándose en los datos de tráfico
        public void OptimizarTiempoSemaforo()
        {
            Console.WriteLine("Optimizando los tiempos de los semáforos basándose en los datos de tráfico...");

            try
            {
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

                    Console.WriteLine("Tráfico bajo. Reduciendo tiempo de luz roja.");
                    ReducirTiempoLuzRoja();
                }
                else
                {
                    Console.WriteLine("Tráfico alto. Aumentando tiempo de luz roja.");
                    AumentarTiempoLuzRoja();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al optimizar los tiempos de los semáforos: {ex.Message}");
            }
        }

        public void EnviarTiemposOptimizadosALosSemáforos()
        {
            Console.WriteLine("Enviando tiempos optimizados a los semáforos...");
        }

        private void ProcesarDatos(DatosTrafico datos)
        {
            if (datos == null)
            {
                throw new ArgumentNullException(nameof(datos), "El objeto datos no puede ser nulo.");
            }

            Console.WriteLine("Procesando datos de tráfico...");

            try
            {
                var peticion = new PeticionSemaforo(
                semaforosControl.Id,
                CalcularNuevoEstado(datos.CantidadVehiculos, datos.CantidadPeatones)
                );
                _colaPeticiones.Add(peticion);
                _eventoPeticion.Set(); // Señalar nueva petición
                lock (_mutex)
                {
                    _peticiones.Enqueue(peticion);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al procesar datos de tráfico: {ex.Message}");
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

        private readonly AutoResetEvent _eventoPeticion = new AutoResetEvent(false);
        private void HebraConsumidora()
        {
            while (true)
            {
                try
                {
                    _eventoPeticion.WaitOne();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al esperar la señal del semáforo: {ex.Message}");
                }

                lock (_mutex)
                {
                    try
                    {
                        if (_peticiones.Count > 0)
                        {
                            var peticion = _peticiones.Dequeue();

                            try
                            {
                                semaforosControl.ActualizarEstadoSemaforo(peticion.IdSemaforo, "Verde", "Autobús");
                                Console.WriteLine($"Estado del semáforo {peticion.IdSemaforo} actualizado a {"Verde"} para {"Autobús"}.");
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine($"Error al actualizar el estado del semáforo: {ex.Message}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error al acceder a la cola de peticiones: {ex.Message}");
                    }
                }
            }
        }


    }
}
