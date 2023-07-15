using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
namespace PL.Controllers
{
    public class AlumnoController : Controller
    {
        private IHostingEnvironment Environment;
        private IConfiguration Configuration;
        public AlumnoController(IHostingEnvironment _environment, IConfiguration _configuration)
        {
            Environment = _environment;
            Configuration = _configuration;
        }

        //[HttpGet]
        //public ActionResult GetAll()
        //{


        //    BL.Alumno resultAlumnos = new BL.Alumno();
        //    resultAlumnos.Alumnos = new List<Object>();

        //    using (var client = new HttpClient())
        //    {
        //        string Host = Configuration["HostServices"];
        //        client.BaseAddress = new Uri(Host);

        //        var responseTask = client.GetAsync("Alumno/GetAll");
        //        responseTask.Wait();

        //        var result = responseTask.Result;

        //        if (result.IsSuccessStatusCode)
        //        {
        //            var readTask = result.Content.ReadAsAsync<BL.Result>();
        //            readTask.Wait();

        //            foreach (var resultItem in readTask.Result.Objects)
        //            {
        //                BL.Alumno resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<BL.Alumno>(resultItem.ToString());
        //                resultAlumnos.Alumnos.Add(resultItemList);
        //            }
        //        }
        //    }
        //    return View(resultAlumnos);
        //}

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
        public ActionResult Form(int? IdAlumno)
        {
            BL.Alumno alumno = new BL.Alumno();

            if (IdAlumno == null)
            {

                return View(alumno);
            }
            else
            {
                BL.Result result = new BL.Result();

                using (var client = new HttpClient())
                {
                    string Host = Configuration["HostServices"];
                    client.BaseAddress = new Uri(Host);
                    var responseTask = client.GetAsync("Alumno/GetById/" + IdAlumno);
                    responseTask.Wait();
                    var resultAPI = responseTask.Result;
                    if (resultAPI.IsSuccessStatusCode)
                    {
                        var readTask = resultAPI.Content.ReadAsAsync<BL.Result>();
                        readTask.Wait();
                        BL.Alumno resultItemList = new BL.Alumno();
                        resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<BL.Alumno>(readTask.Result.Object.ToString());
                        result.Object = resultItemList;


                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No existen registros en la tabla Departamento";
                    }

                }


                //ML.Result result = BL.Aseguradora.GetById(idAseguradora.Value);
                if (result.Correct)
                {
                    alumno = (BL.Alumno)result.Object;

                    ViewBag.Message = "Ocurrio un error al hacer la consulta:";


                    return View(alumno);
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al hacer la consulta:" + result.ErrorMessage;
                    return View("Modal");
                }

            }


        }

        [HttpPost]
        public ActionResult Form(BL.Alumno alumno)
        {

            IFormFile file = Request.Form.Files["inpImagen"];
            if (file != null)
            {
                alumno.Imagen = Convert.ToBase64String(ConvertToBytes(file));


            }
            BL.Result result = new BL.Result();
            if (alumno.IdAlumno == 0)
            {
                using (var client = new HttpClient())
                {
                    string Host = Configuration["HostServices"];
                    client.BaseAddress = new Uri(Host);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<BL.Alumno>("Alumno/Add", alumno);
                    postTask.Wait();

                    var resultAseguradora = postTask.Result;
                    if (resultAseguradora.IsSuccessStatusCode)
                    {

                        //return RedirectToAction("GetAll");
                        ViewBag.Message = "El resigistro de Alumno a sido agrgado con exito";

                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al insertar el registro" + " " + result.ErrorMessage;
                    }

                }

                return View("Modal");


            }
            else
            {

                using (var client = new HttpClient())
                {
                    string Host = Configuration["HostServices"];
                    client.BaseAddress = new Uri(Host);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<BL.Alumno>("Alumno/Update/" + alumno.IdAlumno, alumno);
                    postTask.Wait();

                    var resultAseguradora = postTask.Result;
                    if (resultAseguradora.IsSuccessStatusCode)
                    {

                        return RedirectToAction("GetAll");

                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al insertar el registro" + " " + result.ErrorMessage;
                    }

                }


                return View("Modal");
            }
        }

        public static byte[] ConvertToBytes(IFormFile imagen)
        {
            using var Imagen = imagen.OpenReadStream();
            byte[] bytes = new byte[Imagen.Length];
            Imagen.Read(bytes, 0, (int)Imagen.Length);

            return bytes;
        }

        public ActionResult Login()
        {


            return View();

        }

        [HttpPost]
        public ActionResult Login(string Nombre, string Apellido)
        {
            //ML.Result result = BL.Usuario.GetByUserName(UserName);
            BL.Result resultLogin = new BL.Result();

            using (var client = new HttpClient())
            {
                string Host = Configuration["HostServices"];
                client.BaseAddress = new Uri(Host);
                var responseTask = client.GetAsync("Alumno/GetbyNombre/" + Nombre + "/" + Apellido);
                responseTask.Wait();
                var resultAPI = responseTask.Result;
                if (resultAPI.IsSuccessStatusCode)
                {
                    var readTask = resultAPI.Content.ReadAsAsync<BL.Result>();
                    readTask.Wait();
                    BL.Alumno resultItemList = new BL.Alumno();
                    resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<BL.Alumno>(readTask.Result.Object.ToString());
                    resultLogin.Object = resultItemList;


                    resultLogin.Correct = true;
                }
                else
                {
                    resultLogin.Correct = false;
                    resultLogin.ErrorMessage = "No existen registros en la tabla Departamento";
                }

            }

            if (resultLogin.Correct)
            {
                BL.Alumno alumno = (BL.Alumno)resultLogin.Object;
                if (alumno.ApellidoPaterno == Apellido)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.Message = "La contraseña no coincide";
                    return PartialView("ModalLogin");
                }
            }
            else
            {
                ViewBag.Message = "El usuario no existe o esta mal escrito";
                return PartialView("ModalLogin");
            }
        }
    }
}
