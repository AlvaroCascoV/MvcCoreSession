
using MvcCoreSession.Helpers;

namespace MvcCoreSession.Extensions
{
    public static class SessionExtension
    {
        //METODO PARA RECUPERAR CUALQUIER OBJETO DE SESSION
        public static T GetObject<T>(this ISession session, string key)
        {
            //AHORA MISMO YA TENEMOS DENTRO DE LA VARIABLE session
            //EL OBJETO HttpContext.Session
            //DEBEMOS RECUPERAR EL OBJETO JSON DE SESSION
            string json = session.GetString(key);
            //EN SESSION SI ALGO NO EXISTE SIEMPRE DEVUELVE null
            if(json == null)
            {
                return default(T);
            }
            else
            {
                //RECUPERAMOS EL OBJETO Y LO CONVERTIMOS CON NUESTRO HELPER
                T data = HelperJsonSession.DeserializeObject<T>(json);
                return data;
            }
        }
        public static void SetObject(this ISession session, string key, object value)
        {
            string data = HelperJsonSession.SerializeObject(value);
            session.SetString(key, data);
        }
    }
}
