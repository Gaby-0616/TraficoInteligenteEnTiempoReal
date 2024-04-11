using System;
using TraficoInteligenteEnTiempoReal.Interfaces;
using TraficoInteligenteEnTiempoReal.Vehiculos;
using static TraficoInteligenteEnTiempoReal.SensorTráfico;

namespace TraficoInteligenteEnTiempoReal
{
    internal class SensorTráfico
    {
        public int Id { get; private set; }
        public bool Funcionando { get; private set; }
        public string Tipo { get; set; }
        public int Velocidad { get; set; }
        public string Dirección { get; set; }

        public SensorTráfico(int id, string tipo, int velocidad, string dirección)
        {
            Funcionando = true;
            Id = id;
            Tipo = tipo;
            Velocidad = velocidad;
            Dirección = dirección;
        }

        // Definir un tipo de evento para la detección de vehículos
        public class VehiculoDetectadoEventArgs : EventArgs
        {
            public Vehículo Vehículo { get; private set; }

            public VehiculoDetectadoEventArgs(Vehículo vehículo)
            {
                Vehículo = vehículo;
            }
        }


        public Vehículo DetectarVehículo()
        {
            try
            {
                var vehiculo = new Vehículo(velocidad: 100, tipo: "Coche", dirección: "Norte");
                if (Funcionando)
                {
                    Console.WriteLine($"Sensor {Id}: ¡Vehículo {vehiculo.tipo} detectado en la vía! (Velocidad: {vehiculo.velocidad}, Dirección: {vehiculo.dirección})");
                    var evento = new VehiculoDetectadoEventArgs(vehiculo);
                    OnVehiculoDetectado(evento);
                }
                else
                {
                    Console.WriteLine($"Sensor {Id}: No puede detectar vehículos. Sensor no funcional.");
                }
                return vehiculo;
            }
            catch (ArgumentNullException ex) when (ex.ParamName == "dirección")
            {
                Console.WriteLine($"Error: Se debe especificar una dirección válida para el vehículo. Parámetro faltante: {ex.ParamName}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error inesperado al detectar un vehículo: {ex.Message}");
                return null;
            }
        }

        #region Eventos
        // Declarar un evento para la detección de vehículos
        public event EventHandler<VehiculoDetectadoEventArgs> VehiculoDetectado;

        // Método para disparar el evento
        protected virtual void OnVehiculoDetectado(VehiculoDetectadoEventArgs e)
        {
            VehiculoDetectado?.Invoke(this, e);
        }



        public class InterfazGrafica : IDisposable
        {
            private SensorTráfico sensor;

            public InterfazGrafica(SensorTráfico sensor)
            {
                this.sensor = sensor;
                sensor.VehiculoDetectado += OnVehiculoDetectado;
            }

            private void OnVehiculoDetectado(object sender, VehiculoDetectadoEventArgs e)
            {
                // Actualizar la interfaz gráfica con la información del vehículo detectado
                Console.WriteLine($"Vehículo detectado: {e.Vehículo.tipo} - Velocidad: {e.Vehículo.velocidad} km/h - Dirección: {e.Vehículo.dirección}");
            }

            public void Dispose()
            {
                if (sensor != null)
                {
                    sensor.VehiculoDetectado -= OnVehiculoDetectado;
                    sensor = null;
                }
            }
        }
        #endregion


        public void DesactivarSensor()
        {
            Funcionando = false;
            Console.WriteLine($"Sensor {Tipo}: Desactivado.");
        }



        public class DatosTrafico
        {
            public int CantidadVehiculos { get; }
            public int CantidadPeatones { get; }
            public string DireccionPredominante { get; set; }

            public DatosTrafico(int cantidadVehiculos, int cantidadPeatones)
            {
                CantidadVehiculos = cantidadVehiculos;
                CantidadPeatones = cantidadPeatones;
            }


            // Método estático para simular la obtención de datos de tráfico

        }
        public static DatosTrafico ObtenerDatosTrafico()
        {

            Random random = new Random();
            int cantidadVehiculos = random.Next(0, 50);
            int cantidadPeatones = random.Next(0, 10);


            return new DatosTrafico(cantidadVehiculos, cantidadPeatones);
        }

        private List<Vehículo> GenerarVehículosAleatorios()
        {

            try
            {
                Random random = new Random();
                int cantidadVehículos = random.Next(1, 5); // Generar entre 1 y 5 vehículos
                List<Vehículo> vehículos = new List<Vehículo>();

                for (int i = 0; i < cantidadVehículos; i++)
                {
                    // Tipos de vehículos posibles (puedes agregar más según tus necesidades)
                    string[] tiposDeVehículos = { "Coche", "Camión", "Autobús", "Motocicleta" };

                    // Generar un vehículo aleatorio
                    string tipoAleatorio = tiposDeVehículos[random.Next(tiposDeVehículos.Length)];
                    int velocidadAleatoria = random.Next(20, 120); // Velocidad entre 20 y 120 km/h
                    string direcciónAleatoria = random.Next(2) == 0 ? "Norte" : "Sur"; // Dirección aleatoria

                    Vehículo vehículo = new Vehículo(velocidadAleatoria, tipoAleatorio, direcciónAleatoria);
                    vehículos.Add(vehículo);
                }

                return vehículos;
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Error: Se intentó acceder a un elemento fuera del rango del array de tipos de vehículos. Índice: {ex.Message}");
                return null;
            }

        }


        public void RecopilarDatos()
        {
            // Implementar la lógica para recopilar datos de tráfico de los sensores
            // Esta lógica podría involucrar la lectura de sensores reales o la simulación de datos
            // Puede utilizar múltiples sensores o generar datos agregados

            // Ejemplo de simulación de datos de tráfico
            Random random = new Random();
            int cantidadVehiculos = random.Next(10, 50); // Simular un rango mayor de vehículos
            int cantidadPeatones = random.Next(5, 15); // Simular un rango mayor de peatones

            DatosTrafico datosTrafico = new DatosTrafico(cantidadVehiculos, cantidadPeatones);

            // Implementar lógica para determinar la dirección predominante (opcional)

            // Guardar los datos de tráfico recopilados (por ejemplo, en una base de datos o enviarlos a un sistema central)
            Console.WriteLine($"Datos de tráfico recopilados: Vehículos: {datosTrafico.CantidadVehiculos}, Peatones: {datosTrafico.CantidadPeatones}");
        }



    }

}





