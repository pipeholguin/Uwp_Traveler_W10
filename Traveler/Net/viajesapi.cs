using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;
using Windows.Storage.Streams;

namespace Traveler.Net
{
   public  class viajesapi
    {

       
        HttpConnection con;
        string url = "http://localhost/traveler/public/index.php/viajes";


        public viajesapi()
        {
            con = new HttpConnection();
        }
        public async Task<bool> viaje(string origenp, string destinop,  string preciop, string asientosp, string horap, string fechap,  string carrop, string base64string, string contact)
        {
            JsonObject request = new JsonObject();
            request.Add("origen", JsonValue.CreateStringValue(origenp));
            request.Add("destino", JsonValue.CreateStringValue(destinop));
            request.Add("precio", JsonValue.CreateStringValue(preciop));
            request.Add("asientos", JsonValue.CreateStringValue(asientosp));
            request.Add("hora", JsonValue.CreateStringValue(horap));
            request.Add("fecha", JsonValue.CreateStringValue(fechap));
            request.Add("carro", JsonValue.CreateStringValue(carrop));
            request.Add("imagen", JsonValue.CreateStringValue(base64string));
            request.Add("contacto", JsonValue.CreateStringValue(contact));


            string rta = await con.requestByJsonPost(url, request.ToString());


            return false;

        }


      

   
    }
}
