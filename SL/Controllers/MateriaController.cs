using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class MateriaController : ControllerBase
    {

        //Servicios Web Arquitectura REST

        //Servicios GET

        /* Este servicio permite acceder al metodo GetAll de la entidad materia que se encuentra
         dentro de la Capa de negocios(BL) */
        [EnableCors("AccesoCore")]
        [HttpGet]
        [Route("api/Materia/GetAll")]
        public IActionResult GetAll()
        {

            BL.Result result = BL.Materia.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        /* Este servicio permite acceder al metodo GetById de la entidad materia que se encuentra
         dentro de la Capa de negocios(BL) enviando un parametro proporcionado por el usuario desde
        la interfaz o por el desarrollador desde un entorno de puebas(Postman o Swagger)*/
        [EnableCors("AccesoCore")]
        [HttpGet]
        [Route("api/Materia/GetById/{IdMateria}")]
        public IActionResult GetById(int IdMateria)
        {

            BL.Result result = BL.Materia.GetById(IdMateria);

            if (result.Correct)
            {
                return Ok(result);
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
        [EnableCors("AccesoCore")]
        //[HttpPost("api/Materia/Add")]
        [HttpPost]
        [Route("api/Materia/Add")]
        public IActionResult Add([FromBody] BL.Materia materia)
        {
            BL.Result result = BL.Materia.Add(materia);
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
        [EnableCors("AccesoCore")]
        [HttpPost]
        [Route("api/Materia/Update/{IdMateria}")]
        public IActionResult Update(int IdMateria, [FromBody] BL.Materia materia)
        {
            materia.IdMateria = IdMateria;

            BL.Result result = BL.Materia.Update(materia);
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
        [EnableCors("AccesoCore")]
        [HttpDelete]
        [Route("api/Materia/Delete/{IdMateria}")]
        public IActionResult Delete(int IdMateria)
        {
            BL.Result result = BL.Materia.Delete(IdMateria);
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
