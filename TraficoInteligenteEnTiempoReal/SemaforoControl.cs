using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraficoInteligenteEnTiempoReal
{
    internal class SemaforosControl
    {
        private int tiempoLuzRoja;

        public SemaforosControl(int tiempoLuzRojaInicial)
        {
            this.tiempoLuzRoja = tiempoLuzRojaInicial;
        }

        public void ReducirTiempoLuzRoja()
        {
            Console.WriteLine("Reduciendo tiempo de la luz roja en los semáforos...");
            // Lógica para reducir el tiempo de la luz roja
            // Por ejemplo, podrías decrementar el tiempo en una cierta cantidad
            tiempoLuzRoja -= 5; // Reducción ficticia, ajusta según tus necesidades

            // Verifica que el tiempo no sea negativo para evitar valores no válidos
            if (tiempoLuzRoja < 0)
            {
                tiempoLuzRoja = 0;
            }

            Console.WriteLine($"Nuevo tiempo de la luz roja: {tiempoLuzRoja} segundos");
        }

        public void AumentarTiempoLuzRoja()
        {
            Console.WriteLine("Aumentando tiempo de la luz roja en los semáforos...");
            // Lógica para aumentar el tiempo de la luz roja
            // Por ejemplo, podrías incrementar el tiempo en una cierta cantidad
            tiempoLuzRoja += 5; // Aumento ficticio, ajusta según tus necesidades

            Console.WriteLine($"Nuevo tiempo de la luz roja: {tiempoLuzRoja} segundos");
        }
    }
}
