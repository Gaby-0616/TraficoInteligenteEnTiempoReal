using System;
using TraficoInteligenteEnTiempoReal.Interfaces;
using TraficoInteligenteEnTiempoReal.Vehiculos;

namespace TraficoInteligenteEnTiempoReal
{
    internal class SensorTráfico 
    {
       

        public int Id { get; private set; }
        //public string Nombre { get; }
        public bool Funcionando { get; private set; }
        public string Tipo { get; set; }
        public int Velocidad { get; set; }
        public string Dirección { get; set; }

        public SensorTráfico(int id, string tipo, int velocidad, string dirección /*string nombre*/)
        {
            Funcionando = true;
            //Nombre = nombre;
            Id = id;
            Tipo = tipo;
            Velocidad = velocidad;
            Dirección = dirección;
        }

      

       
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
            Console.WriteLine($"Sensor {Tipo}: Desactivado.");
        }

       

        public class DatosTrafico
        {
            public int CantidadVehiculos { get; }
            public int CantidadPeatones { get; }
            public string DireccionPredominante { get; set; }

            public DatosTrafico(int cantidadVehiculos, int cantidadPeatones /*string direccionPredominante*/)
            {
                CantidadVehiculos = cantidadVehiculos;
                CantidadPeatones = cantidadPeatones;
                //DireccionPredominante = direccionPredominante;
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
