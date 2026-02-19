using Microsoft.AspNetCore.Mvc;
using MvcCoreSession.Extensions;
using MvcCoreSession.Helpers;
using MvcCoreSession.Models;

namespace MvcCoreSession.Controllers
{
    public class EjemploSessionController : Controller
    {
        //recuperamos el helper de contextAccessor
        HelperSessionContextAccessor helper;

        public EjemploSessionController(HelperSessionContextAccessor helper)
        {
            this.helper = helper;
        }

        public IActionResult Index()
        {
            //usamos el helper de contextAccessor
            List<Mascota> mascotas = this.helper.GetMascotasSession();

            return View(mascotas);
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
        public IActionResult SessionMascotaJson(string accion)
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

                    //QUEREMOS GUARDAR EL OBJETO MASCOTA COMO STRING EN SESSION
                    string mascotaJson = HelperJsonSession.SerializeObject(mascota);
                    HttpContext.Session.SetString("MASCOTAJSON", mascotaJson);

                    ViewData["MENSAJE"] = "Mascota almacenada en Session";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    //RECUPERAMOS LOS DATOS DE MASCOTA en string
                    string jsonMascota = HttpContext.Session.GetString("MASCOTAJSON");

                    Mascota mascota = HelperJsonSession.DeserializeObject<Mascota>(jsonMascota);

                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }
        public IActionResult SessionMascotaGeneric(string accion)
        {

            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    //GUARDAMOS DATOS EN SESSION
                    Mascota mascota = new Mascota();
                    mascota.Nombre = "Tortu";
                    mascota.Raza = "Floridiana";
                    mascota.Edad = 20;

                    HttpContext.Session.SetObject("MASCOTAGENERIC", mascota);

                    ViewData["MENSAJE"] = "Mascota almacenada en Session";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    Mascota mascota = HttpContext.Session.GetObject<Mascota>("MASCOTAGENERIC");

                    ViewData["MASCOTA"] = mascota;
                }
            }
            return View();
        }
        public IActionResult SessionMascotasCollectionGeneric(string accion)
        {

            if (accion != null)
            {
                if (accion.ToLower() == "almacenar")
                {
                    //GUARDAMOS DATOS EN SESSION
                    List<Mascota> mascotasList = new List<Mascota>
                    {
                        new Mascota{Nombre="Coby",Raza="Chucho",Edad=14},
                        new Mascota{Nombre="Sebastian",Raza="Langosta",Edad=24},
                        new Mascota{Nombre="Nala",Raza="Leon",Edad=21},
                        new Mascota{Nombre="Olaf",Raza="Nieve",Edad=562},
                    };

                    HttpContext.Session.SetObject("MASCOTASLISTGENERIC", mascotasList);

                    ViewData["MENSAJE"] = "Mascotas almacenada en Session";
                }
                else if (accion.ToLower() == "mostrar")
                {
                    //RECUPERAMOS LOS DATOS
                    List<Mascota> mascotasList = HttpContext.Session.GetObject<List<Mascota>>("MASCOTASLISTGENERIC");
                    return View(mascotasList);
                }
            }
            return View();
        }
    }
}