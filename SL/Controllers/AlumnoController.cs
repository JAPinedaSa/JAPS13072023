using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class AlumnoController : Controller
    {
        //Servicios Web Arquitectura REST

        //Servicios GET

        /* Este servicio permite acceder al metodo GetAll de la entidad Alumno que se encuentra
         dentro de la Capa de negocios(BL) */
        [HttpGet]
        [Route("api/Alumno/GetAll")]
        public IActionResult GetAll()
        {

            BL.Result result = BL.Alumno.GetAll();

            if (result.Correct)
            {
                return Ok(result.Objects);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        /* Este servicio permite acceder al metodo GetById de la entidad materia que se encuentra
         dentro de la Capa de negocios(BL) enviando un parametro proporcionado por el usuario desde
        la interfaz o por el desarrollador desde un entorno de puebas(Postman o Swagger)*/
        [HttpGet]
        [Route("api/Alumno/GetById/{IdAlumno}")]
        public IActionResult GetById(int IdAlumno)
        {

            BL.Result result = BL.Alumno.GetById(IdAlumno);

            if (result.Correct)
            {
                return Ok(result.Objects);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        //--------------------------------------------------------------------------------------

        //Servicios POST

        /* Este servicio permite acceder al metodo GetById de la entidad materia que se encuentra
         dentro de la Capa de negocios(BL) enviando un modelo proporcionado por el usuario desde
        la interfaz o por el desarrollador desde un entorno de puebas(Postman o Swagger)*/
        [HttpPost]
        [Route("api/Alumno/Add")]
        public IActionResult Add([FromBody] BL.Alumno alumno)
        {
            BL.Result result = BL.Alumno.Add(alumno);
            if (result.Correct)
            {
                return Ok(result.Objects);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }


        /* Este servicio permite acceder al metodo GetById de la entidad materia que se encuentra
         dentro de la Capa de negocios(BL) enviando un parametro y un modelo proporcionados por el usuario 
        desde la interfaz o por el desarrollador desde un entorno de puebas(Postman o Swagger)*/
        [HttpPost]
        [Route("api/Alumno/Update/{IdAlumno}")]
        public IActionResult Update(int IdAlumno, [FromBody] BL.Alumno alumno)
        {
            alumno.IdAlumno = IdAlumno;

            BL.Result result = BL.Alumno.Update(alumno);
            //ML.Result result1 = BL.Materia.Update(materia);
            //ML.Result result2 = BL.Materia.Update(materia);
            //ML.Result result3 = BL.Materia.Update(materia);

            if (result.Correct)
            {
                return Ok(result.Objects);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }

        }


        //--------------------------------------------------------------------------------------

        //Servicios Delete


        /* Este servicio permite acceder al metodo Delete de la entidad materias que se encuentra
         en la Capa de Negocios (BL) enviando un parametro , que es proporcionado por el usuario*/
        [HttpDelete]
        [Route("api/Alumno/Delete/{IdAlumno}")]
        public IActionResult Delete(int IdAlumno)
        {
            BL.Result result = BL.Materia.Delete(IdAlumno);
            if (result.Correct)
            {
                return Ok(result.Objects);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }
    }
}
