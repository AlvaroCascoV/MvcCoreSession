using Microsoft.AspNetCore.Mvc;
using MvcCoreSession.Helpers;
using MvcCoreSession.Models;

namespace MvcCoreSession.Controllers
{
    public class EjemploSessionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SessionSimple(string accion)
        {
            if (accion != null) 
            {
                if (accion.ToLower() == "almacenar")
                {
                    //GUARDAMOS DATOS EN SESSION
                    HttpContext.Session.SetString("nombre", "Programeitor");
                    HttpContext.Session.SetString("hora", DateTime.Now.ToLongTimeString());
                    ViewData["MENSAJE"] = "Datos almacenados en Session";
                }else if (accion.ToLower() == "mostrar")
                {
                    //RECUPERAMOS LOS DATOS DE SESSION
                    ViewData["NOMBRE"] = HttpContext.Session.GetString("nombre");
                    ViewData["HORA"] = HttpContext.Session.GetString("hora");
                }
            }
            return View();
        }

        public IActionResult SessionMascotaBytes(string accion)
        {

            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    //GUARDAMOS DATOS EN SESSION
                    Mascota mascota = new Mascota();
                    mascota.Nombre = "Coby";
                    mascota.Raza = "Mestizo";
                    mascota.Edad = 14;
                    //PARA ALMACENAR LA MASCOTA EN SESSION,
                    //DEBEMOS CONVERTIRLO A BYTE[]
                    byte[] data = HelperBinarySession.ObjectToByte(mascota);
                    //ALMACENAMOS EL OBJETO EN SESSION
                    HttpContext.Session.Set("MASCOTA", data);
                    ViewData["MENSAJE"] = "Mascota almacenada en Session";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    //RECUPERAMOS LOS DATOS DE MASCOTA
                    //EN BYTES QUE TENEMOS EN SESSION
                    byte[] data = HttpContext.Session.Get("MASCOTA");
                    //CONVERTIMOS BYTES A OBJECT
                    Mascota mascota = (Mascota)HelperBinarySession.ByteToObject(data);

                    //para la forma no obsoleta
                    //Mascota mascota = HelperBinarySession.ByteToObject<Mascota>(data);
                    //PARA REPRESENTARLO DE FORMA VISUAL, LO ENVIAMOS A VIEWDATA
                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }
        public IActionResult SessionMascotaCollection(string accion)
        {

            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    //GUARDAMOS DATOS EN SESSION
                    List<Mascota> mascotasList = new List<Mascota>
                    {
                        new Mascota{Nombre="Coby",Raza="Mestizo",Edad=14},
                        new Mascota{Nombre="Sebastian",Raza="Cangrejo",Edad=24},
                        new Mascota{Nombre="Nala",Raza="Leona",Edad=21},
                        new Mascota{Nombre="Olaf",Raza="Muñeco",Edad=562},
                    };
                    byte[] data = HelperBinarySession.ObjectToByte(mascotasList);
                    HttpContext.Session.Set("MASCOTAS", data);
                    ViewData["MENSAJE"] = "Mascotas almacenada en Session";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    //RECUPERAMOS LOS DATOS
                    byte[] data = HttpContext.Session.Get("MASCOTAS");
                    List<Mascota> mascotasList = (List<Mascota>)HelperBinarySession.ByteToObject(data);
                    return View(mascotasList);
                }
            }
            return View();
        }
    }
}
