using System;
using static TraficoInteligenteEnTiempoReal.SensorTráfico;

namespace TraficoInteligenteEnTiempoReal
{
    internal class ControlTráfico
    {
        private readonly SensorTráfico sensorDeTráfico;
        private readonly List<SensorTráfico> sensores;
        private readonly SemaforosControl semaforosControl;
        //private List<Interfaces.ISensor> listaDeSensores;

        public ControlTráfico(SensorTráfico sensorDeTráfico,/* List<SensorTráfico> sensores,*/ SemaforosControl semaforosControl)
        {
            this.sensores = sensores ?? throw new ArgumentNullException(nameof(sensores), "La lista de sensores no puede ser nula.");
            this.sensorDeTráfico = sensorDeTráfico ?? throw new ArgumentNullException(nameof(sensorDeTráfico), "El sensor de tráfico no puede ser nulo.");
            this.semaforosControl = semaforosControl;
        }

        public ControlTráfico(SensorTráfico sensorDeTráfico, /*List<Interfaces.ISensor> listaDeSensores,*/ SemaforosControl semaforosControl)
        {
            this.sensorDeTráfico = sensorDeTráfico;
            //this.listaDeSensores = listaDeSensores;
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

            int totalVehiculos = 0;
            foreach (var sensor in sensores)
            {
                totalVehiculos += SensorTráfico.ObtenerDatosTrafico().CantidadVehiculos;
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
            // Lógica para procesar los datos recopilados
            // Puedes almacenarlos en una base de datos, generar informes, etc.
        }

        private void ReducirTiempoLuzRoja()
        {
            // Lógica para reducir el tiempo de la luz roja
            // Puedes ajustar la lógica según la implementación específica del controlador de semáforos
            // Por ejemplo, puedes enviar comandos a los semáforos para ajustar dinámicamente los tiempos
            semaforosControl.ReducirTiempoLuzRoja();
        }

        private void AumentarTiempoLuzRoja()
        {
            // Lógica para aumentar el tiempo de la luz roja
            // Puedes ajustar la lógica según la implementación específica del controlador de semáforos
            // Por ejemplo, puedes enviar comandos a los semáforos para ajustar dinámicamente los tiempos
            semaforosControl.AumentarTiempoLuzRoja();
        }
    }
}
