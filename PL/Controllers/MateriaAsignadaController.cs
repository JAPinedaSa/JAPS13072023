using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class MateriaAsignadaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            return View();
        }
    }
}
