using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;
using Windows.Storage;

namespace Traveler.Net
{
    public class loginapi
    {
        HttpConnection con;
        string url = "http://localhost/traveler/public/index.php/usuario/login";
        ApplicationDataContainer data;
        JsonObject json;

        public loginapi()
        {
            con = new HttpConnection();
            
        }

        

        public async Task<bool> usuarios( string user, string pass)
        {
            data = ApplicationData.Current.LocalSettings;

            JsonObject request = new JsonObject();
            request.Add("usuario", JsonValue.CreateStringValue(user));
            request.Add("contrasena", JsonValue.CreateStringValue(pass));


             string rta = await con.requestByJsonPost(url, request.ToString());
             json = JsonObject.Parse(rta);
             string estado = json["status"].GetString();
            

             data.Values["log"]=estado;

            return false;

        
        }
        

    }

    
}
