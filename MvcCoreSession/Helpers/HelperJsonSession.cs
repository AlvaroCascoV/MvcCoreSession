using Newtonsoft.Json;

namespace MvcCoreSession.Helpers
{
    public class HelperJsonSession
    {
        //Para la serialización vamos a utilizar string mediante JSON.
        //Vamos a utilizar un nuget de Newtonsoft
        
        //VAMOS A ALMACENAR DATOS EN SESSION MEDIANTE
        //EL METODO GetString, SetString
        public static string SerializeObject<T>(T data)
        {
            //CONVERTIMOS EL OBJETO A STRING MEDIANTE NEWTON
            string json = JsonConvert.SerializeObject(data);
            return json;
        }

        public static T DeserializeObject<T>(string data)
        {
            //MEDIANTE NEWTON DESERIALIZAMOS EL OBJETO
            T objeto = JsonConvert.DeserializeObject<T>(data);
            return objeto;
        }
        //este codigo se queda sin usar porque era para explicacion
    }
}
