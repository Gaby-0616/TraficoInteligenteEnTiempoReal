using System;
using TraficoInteligenteEnTiempoReal.Interfaces;
using TraficoInteligenteEnTiempoReal.Vehiculos;

namespace TraficoInteligenteEnTiempoReal
{
    internal class SensorTráfico /*: ISensor*/
    {
        public string Id { get; private set; }
        public string Nombre { get; }
        public bool Funcionando { get; private set; }
        //private readonly ISensor sensorPrincipal;
        //private readonly List<ISensor> listaDeSensores;
        //private readonly SemaforosControl semaforosControl;

        public SensorTráfico(string nombre)
        {
            Funcionando = true;
            Nombre = nombre;
        }
        //public  Vehículo DetectarVehículo()
        //{
        //    // En esta lógica simulada, se genera una lista de vehículos detectados
        //    List<Vehículo> vehículosDetectados = GenerarVehículosAleatorios();

        //    // Mostrar información sobre los vehículos detectados
        //    Console.WriteLine($"Sensor de tráfico {Nombre}: Detección de vehículos en progreso...");
        //    foreach (var vehículo in vehículosDetectados)
        //    {
        //        Console.WriteLine($"Vehículo detectado - Tipo: {vehículo.tipo}, Velocidad: {vehículo.velocidad}, Dirección: {vehículo.dirección}");
        //    }
        //    Console.WriteLine($"Sensor de tráfico {Nombre}: ¡Vehículos detectados correctamente!");


        //}

        public Vehículo DetectarVehículo()
        {
            var vehiculo = new Vehículo(velocidad: 100, tipo: "Coche", dirección: "Norte");
            if (Funcionando)
            {



                Console.WriteLine($"Sensor {Id}: ¡Vehículo {vehiculo.tipo} detectado en la vía! (Velocidad: {vehiculo.velocidad}, Dirección: {vehiculo.dirección})");
            }
            else
            {
                Console.WriteLine($"Sensor {Id}: No puede detectar vehículos. Sensor no funcional.");
            }
            return vehiculo;
        }

        public void DesactivarSensor()
        {
            Funcionando = false;
            Console.WriteLine($"Sensor {Nombre}: Desactivado.");
        }

        public class DatosTrafico
        {
            public int CantidadVehiculos { get; }
            public int CantidadPeatones { get; }

            public DatosTrafico(int cantidadVehiculos, int cantidadPeatones)
            {
                CantidadVehiculos = cantidadVehiculos;
                CantidadPeatones = cantidadPeatones;
            }


            // Método estático para simular la obtención de datos de tráfico

        }
        public static DatosTrafico ObtenerDatosTrafico()
        {
            // Lógica para simular la obtención de datos de tráfico
            // En un entorno real, esta lógica dependerá de cómo se integran tus sensores con el sistema
            // y cómo obtienen y estructuran los datos de tráfico.

            // En este ejemplo, se genera aleatoriamente una cantidad de vehículos y peatones.
            Random random = new Random();
            int cantidadVehiculos = random.Next(0, 50);
            int cantidadPeatones = random.Next(0, 10);

            // Se crea y devuelve una instancia de la clase DatosTrafico con los datos generados.
            return new DatosTrafico(cantidadVehiculos, cantidadPeatones);
        }

        private List<Vehículo> GenerarVehículosAleatorios()
        {
            // Lógica simulada para generar una lista de vehículos detectados aleatoriamente
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

        public void RecopilarDatos()
        {
            throw new NotImplementedException();
        }
    }
}
