using EjercicioNET.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace EjercicioNET.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> logger;
        private readonly PersonasContext db;

        public HomeController(ILogger<HomeController> logger, PersonasContext context)
        {
            this.logger = logger;
            this.db = context;
        }

        public IActionResult Index()
        {
            return View(db.Personas.ToList());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public void AgregarPersona(string nombre, string dia, string mes, string anio, double credito)
        {
            String fecha = mes+"/"+dia+"/"+anio;
            var date = DateTime.Parse(fecha,new CultureInfo("en-US", true));
            Persona persona = new Persona{
                Nombre = nombre,
                FechaNacimiento = date,
                CreditoMaximo = credito
            };
            db.Personas.Add(persona);
            db.SaveChanges();
        }

        public JsonResult EditarPersona(int ID)
        {
            Persona personaEditar = db.Personas.FirstOrDefault(p => p.ID == ID);
            return Json(personaEditar);
        }

        public void DatosEditados(int ID, string nombre, string fecha, double credito)
        {
            var date = DateTime.Parse(fecha,new CultureInfo("en-US", true));
            
            Persona persona = db.Personas.FirstOrDefault(p => p.ID == ID);
            persona.CreditoMaximo = credito;
            persona.Nombre = nombre;
            persona.FechaNacimiento = date;
            db.Personas.Update(persona);
            db.SaveChanges();
        }

        public IActionResult BorrarPersona(int ID)
        {
            Persona persona = db.Personas.FirstOrDefault(p => p.ID == ID);
            List<Telefono> telefonos = db.Telefonos.Where(t => t.Persona == persona).ToList();
            if(persona != null)
            {
                foreach (Telefono telefono in telefonos)
                {
                    db.Remove(telefono);
                    db.SaveChanges();
                }
                db.Remove(persona);
                db.SaveChanges();
            }
            return View("Index", db.Personas.ToList());
        }

        public JsonResult Search(string nombre)
        {
            List<Persona> personas = db.Personas.Where(p => p.Nombre.ToLower().Equals(nombre.ToLower())).ToList();
            return Json(personas);
        }

        public JsonResult Telefonos_Persona(int ID)
        {
            Persona persona = db.Personas.FirstOrDefault(p => p.ID == ID);
            List<Telefono> telefonos = db.Telefonos.Where(t => t.Persona.Equals(persona)).ToList();
            return Json(telefonos);
        }

        public void Nuevo_Telefono(string numero, int ID)
        {
            Persona persona = db.Personas.FirstOrDefault(p => p.ID == ID);
            Telefono telefono = new Telefono{
                Numero_Telefono = numero,
                Persona = persona
            };

            db.Telefonos.Add(telefono);
            db.SaveChanges();
        }

        public void Borrar_Telefono(int ID)
        {
            Telefono telefono = db.Telefonos.FirstOrDefault(t => t.TelefonoID == ID);
            db.Telefonos.Remove(telefono);
            db.SaveChanges();
        }

        public void Actualizar_Numero(string numero, int ID)
        {
            Telefono telefono = db.Telefonos.FirstOrDefault(p => p.TelefonoID == ID);
            telefono.Numero_Telefono = numero;
            db.Update(telefono);
            db.SaveChanges();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
