using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using mvcpractica01.Models;

namespace mvcpractica01.Controllers
{
    public class EquiposController : Controller
    {
        private readonly equiposDbcontext _equiposDbContext;
        public EquiposController(equiposDbcontext equiposDbContext)
        {
            _equiposDbContext = equiposDbContext;
        }
        public IActionResult Index()
        {
            var listadoMarcas = (from m in _equiposDbContext.marcas
                                 select m).ToList();
            ViewData["listadoMarcas"] = new SelectList(listadoMarcas, "id_marcas", "nombre_marca");

            var listadoEquipos = (from e in _equiposDbContext.equipos
                                  join m in _equiposDbContext.marcas on e.marca_id equals m.id_marcas
                                  select new
                                  {
                                      nombre = e.nombre,
                                      descripcion = e.descripcion,
                                      marca_id = e.marca_id,
                                      marca_nombre = m.nombre_marca
                                  }
                                ).ToList();
            ViewData["listadoEquipo"] = listadoEquipos;
            return View();
        }
        public IActionResult CrearEquipos(equipos nuevoEquipos)
        {
            _equiposDbContext.Add(nuevoEquipos);
            _equiposDbContext.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
