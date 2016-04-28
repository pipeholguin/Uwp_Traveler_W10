using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace Traveler.Net
{
   public class usuarioapi
    {

        HttpConnection con;
        string url = "http://localhost/traveler/public/index.php/usuario";


        public usuarioapi()
        {
            con = new HttpConnection();
        }

        public async Task<bool> usuarios(string nombrep, string emailp, string celularp, string usuariop, string contrasenap)
        {
            JsonObject request = new JsonObject();
            request.Add("nombre", JsonValue.CreateStringValue(nombrep));
            request.Add("email", JsonValue.CreateStringValue(emailp));
            request.Add("celular", JsonValue.CreateStringValue(celularp));
            request.Add("usuario", JsonValue.CreateStringValue(usuariop));
            request.Add("contrasena", JsonValue.CreateStringValue(contrasenap));


            string rta = await con.requestByJsonPost(url, request.ToString());

            return false;

        }
    }
}
