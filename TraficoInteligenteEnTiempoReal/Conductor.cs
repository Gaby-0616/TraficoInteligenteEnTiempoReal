using System;

namespace TraficoInteligenteEnTiempoReal
{
    internal class Conductor
    {
        public string Nombre { get; private set; }
        public int Edad { get; private set; }
        public bool LicenciaValida { get; private set; }

        public Conductor(string nombre, int edad, bool licenciaValida)
        {
            Nombre = nombre;
            Edad = edad;
            LicenciaValida = licenciaValida;
        }


        public void IniciarViaje()
        {

            VerificarLicencia();

            Console.WriteLine($"{Nombre} ha iniciado su viaje.");

            // Lógica adicional para iniciar el viaje
        }

        public void RealizarParadaDeEmergencia()
        {
            Console.WriteLine($"{Nombre} realiza una parada de emergencia.");

            // Lógica adicional para la parada de emergencia
        }

        private void VerificarLicencia()
        {
            if (!LicenciaValida)
            {
                throw new InvalidOperationException("No se puede iniciar el viaje. Licencia no válida.");
            }
        }


        public class Carro : Conductor
        {
            public Carro(string nombre, int edad, bool licenciaValida)
                : base(nombre, edad, licenciaValida)
            {
                // Lógica específica para inicializar un Carro si es necesario
            }

            public void Acelerar()
            {
                Console.WriteLine($"El carro {Nombre} está acelerando de manera segura y suave.");
                // Lógica adicional para acelerar, como ajustar la velocidad y la transmisión
            }

            public void Frenar()
            {
                Console.WriteLine($"El carro {Nombre} está frenando de manera controlada.");
                // Lógica adicional para frenar, como aplicar los frenos de manera gradual
            }
        }

        // Clase para Autobús
        public class Autobus : Conductor
        {
            public Autobus(string nombre, int edad, bool licenciaValida)
                : base(nombre, edad, licenciaValida)
            {
                // Lógica específica para inicializar un Autobús si es necesario
            }

            public void AbrirPuertas()
            {
                Console.WriteLine($"El autobús {Nombre} está abriendo las puertas para permitir el embarque y desembarque.");
                // Lógica adicional para abrir las puertas de manera segura
            }

            public void CerrarPuertas()
            {
                Console.WriteLine($"El autobús {Nombre} está cerrando las puertas antes de continuar su ruta.");
                // Lógica adicional para cerrar las puertas antes de moverse
            }


        }
        // Clase para Motocicleta
        public class Motocicleta : Conductor
        {
            public Motocicleta(string nombre, int edad, bool licenciaValida)
                : base(nombre, edad, licenciaValida)
            {
                // Lógica específica para inicializar una Motocicleta si es necesario
            }

            public void Inclinar()
            {
                Console.WriteLine($"La motocicleta {Nombre} está inclinándose en las curvas con elegancia.");
                // Lógica adicional para inclinarse de manera suave
            }

            public void GirarManillar()
            {
                Console.WriteLine($"La motocicleta {Nombre} está girando el manillar con precisión.");
                // Lógica adicional para girar el manillar de manera controlada
            }

        }
    }
}
