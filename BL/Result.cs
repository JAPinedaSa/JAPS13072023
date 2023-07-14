using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Result
    {
        public bool Correct { get; set; } //Propiedad para saber si la espuesta fue correcta(true) o hubo un error (false)
        public string ErrorMessage { get; set; } // va a mandar el error como un mensaje
        public object Object { get; set; } // almacena un unico registro del modelo designado
        public List<object> Objects { get; set; } // almacena una lista de registros del modelo designado
        public Exception Ex { get; set; } // es la que va a identificar los errores ocurridos en el codigo
    }
}
