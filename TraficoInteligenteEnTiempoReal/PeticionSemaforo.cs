using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TraficoInteligenteEnTiempoReal
{
    public class PeticionSemaforo
    {
        public int IdSemaforo { get; set; }
        public string NuevoEstado { get; set; }
        public string TipoVehiculo { get; set; }
        public PeticionSemaforo(int idSemaforo, string nuevoEstado/*, string tipoVehiculo*/)
        {
            IdSemaforo = idSemaforo;
            NuevoEstado = nuevoEstado;
            //TipoVehiculo = tipoVehiculo;
        }
    }
    
       
}
