using System;
using System.Collections.Generic;
using System.Threading;
using TraficoInteligenteEnTiempoReal.Interfaces;
using static TraficoInteligenteEnTiempoReal.Conductor;

namespace TraficoInteligenteEnTiempoReal
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                List<Conductor> conductores = new List<Conductor>
                {
                    new Autobus("Autobús1", 40, true),
                    new Motocicleta("Motocicleta1", 25, true),
                    new Carro("Carro1", 35, true),
                };


                var sensorDeTráfico1 = new SensorTráfico(1, "Radar", 50, "Norte");
                var sensorDeTráfico2 = new SensorTráfico(2, "Cámara", 60, "Sur");

                List<SensorTráfico> listaDeSensores = new List<SensorTráfico> { sensorDeTráfico1, sensorDeTráfico2 };
                SensorTráfico sensorDeTráfico = new SensorTráfico(1, "Radar", 50, "Norte");
                AlgoritmoAI algoritmoDeInteligenciaArtificial = new AlgoritmoAI();
                SemaforosControl semaforosControl = new SemaforosControl(1, "Verde", 30, 10);
                //List<SensorTráfico> listaDeSensores = new List<SensorTráfico> { sensorDeTráfico };
                ControlTráfico centroDeControlDeTráfico = new ControlTráfico(sensorDeTráfico, listaDeSensores, semaforosControl);

                centroDeControlDeTráfico.IniciarHebraConsumidora();
                // Escenario de simulación
                SimularEscenario(centroDeControlDeTráfico, algoritmoDeInteligenciaArtificial, conductores);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        private static void SimularEscenario(ControlTráfico centroDeControlDeTráfico, AlgoritmoAI algoritmoDeInteligenciaArtificial, List<Conductor> conductores)
        {
            for (int i = 0; i < 30; i++) // Simula 5 escenarios diferentes
            {
                Console.WriteLine($"Inicio del escenario {i + 1}");

                foreach (var conductor in conductores)
                {
                    
                    // Iniciar el viaje del conductor
                    Console.WriteLine("Se dectecto un conductor...");
                    conductor.IniciarViaje();

                    var tipoVehiculo = conductor.GetType().Name;
                }

                // Recopilar datos de tráfico
                Console.WriteLine("\nRecopilando datos de tráfico...");
                centroDeControlDeTráfico.RecopilarDatosDeTráfico();

                // Optimizar los tiempos de los semáforos
                Console.WriteLine("\nOptimizando los tiempos de los semáforos...");
                centroDeControlDeTráfico.OptimizarTiempoSemaforo();

                // Enviar los tiempos optimizados a los semáforos
                Console.WriteLine("\nEnviando los tiempos optimizados a los semáforos...");
                centroDeControlDeTráfico.EnviarTiemposOptimizadosALosSemáforos();

                // Analizar situación de riesgo
                Console.WriteLine("\nAnalizando situación de riesgo...");
                if (algoritmoDeInteligenciaArtificial.AnalizarSituacionRiesgo())
                {
                    // Tomar medidas para evitar accidente
                    // (cambiar color semáforos, enviar alerta, activar señal de emergencia)
                    Console.WriteLine("Se detectó una situación de riesgo. Tomando medidas...");
                }

                Console.WriteLine($"Fin del escenario {i + 1}\n");

                Thread.Sleep(5000);
            }
        }
    }
}






