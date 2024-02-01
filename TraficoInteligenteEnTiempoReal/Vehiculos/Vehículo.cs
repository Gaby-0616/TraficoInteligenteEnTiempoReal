namespace TraficoInteligenteEnTiempoReal.Vehiculos
{
    internal class Vehículo
    {
        public int velocidad;
        public string tipo;
        public string dirección;

        public Vehículo(int velocidad, string tipo, string dirección)
        {
            this.velocidad = velocidad;
            this.tipo = tipo;
            this.dirección = dirección;
        }
    }
}