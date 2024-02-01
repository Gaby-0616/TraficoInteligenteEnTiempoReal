namespace TraficoInteligenteEnTiempoReal.Interfaces
{
    public interface ISensor
    {
        string Nombre { get; }
        void RecopilarDatos();
    }

    public class SensorTráfico : ISensor
    {
        public string Nombre { get; }

        public SensorTráfico(string nombre)
        {
            Nombre = nombre;
        }

        public void RecopilarDatos()
        {
            Console.WriteLine($"Recopilando datos del sensor {Nombre}...");
            // Lógica para recopilar datos del tráfico
        }
    }

    public class SensorVelocidad : ISensor
    {
        public string Nombre { get; }

        public SensorVelocidad(string nombre)
        {
            Nombre = nombre;
        }

        public void RecopilarDatos()
        {
            Console.WriteLine($"Recopilando datos de velocidad del sensor {Nombre}...");
            // Lógica para recopilar datos de velocidad
        }
    }
}