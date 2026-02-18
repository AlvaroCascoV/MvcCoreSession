using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace MvcCoreSession.Helpers
{
    public class HelperBinarySession
    {
        //VAMOS A CREAR LOS METODOS DE TIPO static
        //PORQUE PARA CONVERTIR NO VOY A UTILIZAR NADA DE ESTA CLASE
        //SOLO LA FUNCIONALIDAD
        //CONVERTIMOS OBJETO A BYTE[]

        public static byte[] ObjectToByte(Object objeto)
        {
            BinaryFormatter formatter = new BinaryFormatter(); 
            using (MemoryStream stream = new MemoryStream()) 
            {
                formatter.Serialize(stream, objeto); return stream.ToArray(); 
            }
        }
        //CONVERTIMOS DE BYTE[] A OBJETO
        public static Object ByteToObject(byte[] data) 
        {
            BinaryFormatter formatter = new BinaryFormatter(); 
            using (MemoryStream stream = new MemoryStream()) 
            {
                stream.Write(data, 0, data.Length); 
                stream.Seek(0, SeekOrigin.Begin); 
                Object objeto = formatter.Deserialize(stream); 
                return objeto; 
            }
        }
        //Forma de hacerlo evitando el metodo obsoleto
        //CONVERTIMOS OBJETO A BYTE[]
        //public static byte[] ObjectToByte<T>(T objeto)
        //{
        //    return JsonSerializer.SerializeToUtf8Bytes(objeto);
        //}

        ////CONVERTIMOS DE BYTE[] A OBJETO
        //public static T ByteToObject<T>(byte[] data)
        //{
        //    return JsonSerializer.Deserialize<T>(data);
        //}
    }
}
