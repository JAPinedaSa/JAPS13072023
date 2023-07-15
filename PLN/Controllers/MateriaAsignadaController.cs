using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace PL.Controllers
{
    public class MateriaAsignadaController : Controller
    {

        private IHostingEnvironment Environment;
        private IConfiguration Configuration;
        public MateriaAsignadaController(IHostingEnvironment _environment, IConfiguration _configuration)
        {
            Environment = _environment;
            Configuration = _configuration;
        }


        [HttpGet]
        public ActionResult GetAll()
        {
            BL.Result resultWebApi = new BL.Result();
            resultWebApi.Objects = new List<object>();
            using (var client = new HttpClient())
            {
                string Host = Configuration["HostServices"];
                client.BaseAddress = new Uri(Host);

                var responseTask = client.GetAsync("Alumno/GetAll");
                responseTask.Wait();
                var result = responseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<BL.Result>();
                    readTask.Wait();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        BL.Alumno resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<BL.Alumno>(resultItem.ToString());
                        resultWebApi.Objects.Add(resultItemList);
                    }
                    BL.Alumno alumno = new BL.Alumno();
                    alumno.Alumnos = resultWebApi.Objects;
                    return View(alumno);
                }
            }
            return View(resultWebApi);
        }

        [HttpGet]
        public ActionResult GetAllMateriaAsignada(int IdAlumno, string Nombre, string ApellidoPaterno, string ApellidoMaterno)
        {
            BL.MateriaAsignada materiaAsignada = new BL.MateriaAsignada();
            BL.Result resultMatriasAsignada = BL.MateriaAsignada.GetAllMateriaAsignada(IdAlumno);
            materiaAsignada.MatriasAsignadasAlumnos = resultMatriasAsignada.Objects.ToList();
            BL.Result resultAlumno = BL.Alumno.GetById(IdAlumno);
            materiaAsignada.Alumno = (BL.Alumno)resultAlumno.Object;

            return View(materiaAsignada);
        }

        [HttpGet]
        public ActionResult GetAllMateriaNoAsignada(int IdAlumno)
        {
            BL.MateriaAsignada materiaNoAsignada = new BL.MateriaAsignada();
            BL.Result resultMateriasNoAsignadas = BL.MateriaAsignada.GetAllMateriaNoAsignada(IdAlumno);
            materiaNoAsignada.MatriasAsignadasAlumnos = resultMateriasNoAsignadas.Objects.ToList();
            BL.Result resultAlumno = BL.Alumno.GetById(IdAlumno);
            materiaNoAsignada.Alumno = (BL.Alumno)resultAlumno.Object;

            return View(materiaNoAsignada);
        }

        [HttpPost]
        public ActionResult Form(BL.MateriaAsignada materiasAsignadas)
        {
            BL.Result result = new BL.Result();
            if (materiasAsignadas != null)
            {
                foreach (string IdMateria in materiasAsignadas.MatriasAsignadasAlumnos)
                {
                    BL.MateriaAsignada rowMateriasAsignadas = new BL.MateriaAsignada();

                    rowMateriasAsignadas.Alumno = new BL.Alumno();
                    rowMateriasAsignadas.Alumno.IdAlumno = materiasAsignadas.Alumno.IdAlumno;

                    rowMateriasAsignadas.Materia = new BL.Materia();
                    rowMateriasAsignadas.Materia.IdMateria = int.Parse(IdMateria);

                    BL.Result resultAddMateriasAsignadas = BL.MateriaAsignada.Add(rowMateriasAsignadas);

                }
                result.Correct = true;
                ViewBag.Message = "Se ha actualizado al alumno";
                ViewBag.MateriasAsignadas = true;
                ViewBag.IdAlumno = materiasAsignadas.Alumno.IdAlumno;
            }


            else
            {
                result.Correct = false;
            }

            return View("ModalMaterias");

        }
        [HttpPost]
        public ActionResult FormDelete(BL.MateriaAsignada borrarMaterias)
        {
            BL.Result result = new BL.Result();
            if (borrarMaterias != null)
            {
                foreach (string IdMateria in borrarMaterias.MatriasAsignadasAlumnos)
                {
                    BL.MateriaAsignada rowMateriasAsignadas = new BL.MateriaAsignada();

                    rowMateriasAsignadas.Alumno = new BL.Alumno();
                    rowMateriasAsignadas.Alumno.IdAlumno = borrarMaterias.Alumno.IdAlumno;

                    rowMateriasAsignadas.Materia = new BL.Materia();
                    rowMateriasAsignadas.Materia.IdMateria = int.Parse(IdMateria);

                    BL.Result resultAddMateriasAsignadas = BL.MateriaAsignada.Delete(rowMateriasAsignadas);

                }
                result.Correct = true;
                ViewBag.Message = "Se ha Eliminado la Materia";
                ViewBag.MateriasAsignadas = true;
                ViewBag.IdAlumno = borrarMaterias.Alumno.IdAlumno;

            }


            else
            {
                result.Correct = false;

                ViewBag.Message = "ha ocurrido un error";
            }

            return PartialView("ModalMaterias");

        }
    }
}
