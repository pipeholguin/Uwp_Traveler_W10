using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Traveler.Models
{
   public class Viaje
    {
        public string Id { get; set; }
        public string Hora { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string Precio { get; set; }
        public string Imagen { get; set; }
        public string Carro { get; set; }
        public string Asientos { get; set; }
        public string Fecha { get; set; }
        public string Contacto { get; set; }
    }
}
