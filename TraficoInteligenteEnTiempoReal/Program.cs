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
                //List<ISensor> listaDeSensores = new List<ISensor>
                //{
                //    new SensorTráfico("SensorPrincipal"),
                //    //new SensorVelocidad("SensorVelocidad1"),
                //};
                // Crear objetos de las clases
                SensorTráfico sensorTráfico = new SensorTráfico("SensorPrincipal");
                AlgoritmoAI algoritmoDeInteligenciaArtificial = new AlgoritmoAI();
                SemaforosControl semaforosControl = new SemaforosControl(tiempoLuzRojaInicial: 30);
                ControlTráfico centroDeControlDeTráfico = new ControlTráfico(sensorTráfico, semaforosControl);

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
            for (int i = 0; i < 5; i++) // Simula 5 escenarios diferentes
            {
                Console.WriteLine($"Inicio del escenario {i + 1}");

                foreach (var conductor in conductores)
                {
                    // Iniciar el viaje del conductor
                    Console.WriteLine("Iniciando viaje del conductor...");
                    conductor.IniciarViaje();

                    // Resto del código para cada conductor...
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

                // Esperar 5 segundos entre escenarios (ajusta según tus necesidades)
                Thread.Sleep(5000);
            }
        }
    }
}






