using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class MateriaAsignadaController : Controller
    {
        //Servicios Web Arquitectura REST

        //Servicios GET

        /* Este servicio permite acceder al metodo GetAll de la entidad materiaAsignada que se encuentra
         dentro de la Capa de negocios(BL) */
        [HttpGet]
        [Route("api/MateriaAsignada/GetAll")]
        public IActionResult GetAll()
        {

            BL.Result result = BL.MateriaAsignada.GetAll();

            if (result.Correct)
            {
                return Ok(result.Objects);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        /* Este servicio permite acceder al metodo GetAllMateriaAsignada de la entidad materiaAsignada
        que se encuentra dentro de la Capa de negocios(BL) enviando un parametro dado por el usuario 
        desde la interfaz o por el desarrollador desde un entorno de puebas(Postman o Swagger)*/
        [HttpGet]
        [Route("api/MateriaAsignada/GetAllMateriaAsignada/{IdAlumno}")]
        public IActionResult GetAllMateriaAsignada(int IdAlumno)
        {

            BL.Result result = BL.MateriaAsignada.GetAllMateriaAsignada(IdAlumno);

            if (result.Correct)
            {
                return Ok(result.Objects);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }


        /* Este servicio permite acceder al metodo GetAllMateriaNoAsignada de la entidad materiaAsignada
       que se encuentra dentro de la Capa de negocios(BL) enviando un parametro dado por el usuario 
       desde la interfaz o por el desarrollador desde un entorno de puebas(Postman o Swagger)*/
        [HttpGet]
        [Route("api/MateriaAsignada/GetAllMateriaNoAsignada/{IdAlumno}")]
        public IActionResult GetAllMateriaNoAsignada(int IdAlumno)
        {

            BL.Result result = BL.MateriaAsignada.GetAllMateriaNoAsignada(IdAlumno);

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
        [Route("api/MateriaAsignada/Add")]
        public IActionResult Add([FromBody] BL.MateriaAsignada materiaAsignada)
        {
            BL.Result result = BL.MateriaAsignada.Add(materiaAsignada);
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
        [Route("api/MateriaAsignada/Delete/{IdAlumno}")]
        public IActionResult Delete(BL.MateriaAsignada borrarMaterias)
        {
            BL.Result result = BL.MateriaAsignada.Delete(borrarMaterias);
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
