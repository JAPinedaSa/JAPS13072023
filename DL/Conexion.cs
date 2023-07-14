using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DL
{
    public class Conexion
    {
        /*Metodo que encapsula la cadena de conexion y la retorna en un string para ser usada 
        en la conexion de los metodos de la capa de negocios*/
        public static string GetConnectionString()
        {
            string cadenaConexion = "Data Source =.; Initial Catalog = ControlEscolar; Persist Security Info = True; User ID = sa; Password = pass@word1";
            
            return cadenaConexion;
        }
    }
}
