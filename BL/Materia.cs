using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{

    public class Materia
    {
        //Propiedades para recibir el modelo de base de datos
        public int IdMateria { get; set; }
        public string Nombre { get; set; }
        public decimal Costo { get; set; }
        public List<object> Materias { get; set; }


        //Metodos Funcionales()

        //Metodos GET :

        //GetAll: Metodo para traer todas las materias existentes en la tabla "Materias"
        public static BL.Result GetAll()
        {
            BL.Result result = new BL.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    var query = "MateriaGetAll";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tablaMaterias = new DataTable("Tabla Materias");

                            da.Fill(tablaMaterias);

                            if (tablaMaterias.Rows.Count > 0)
                            {
                                result.Objects = new List<object>();
                                foreach (DataRow row in tablaMaterias.Rows)
                                {
                                    BL.Materia materia = new BL.Materia();

                                    materia.IdMateria = int.Parse(row[0].ToString());
                                    materia.Nombre = row[1].ToString();
                                    materia.Costo = decimal.Parse( row[2].ToString());
                                    result.Objects.Add(materia);
                                }
                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;

                            }

                        }
                        cmd.Connection.Open();
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }

        //GetById: Metodo para poder traer una materia especifica, seleccionada por su Id
        public static BL.Result GetById(int IdMateria)
        {
            BL.Result result = new BL.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "MateriaGetById";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;


                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdMateria", SqlDbType.VarChar);
                        collection[0].Value = IdMateria;

                        cmd.Parameters.AddRange(collection);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tablaMateria = new DataTable("Tabla Materia");

                            da.Fill(tablaMateria);

                            if (tablaMateria.Rows.Count > 0)
                            {

                                DataRow row = tablaMateria.Rows[0];
                                BL.Materia materia = new BL.Materia();
                                materia.IdMateria = int.Parse(row[0].ToString());
                                materia.Nombre = row[1].ToString();
                                materia.Costo = decimal.Parse(row[2].ToString());
                                

                                result.Object = materia;


                                result.Correct = true;
                            }
                            else
                            {
                                result.Correct = false;

                            }


                        }

                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;

        }

        //--------------------------------------------------------------------------------------


        //Metodos Post:


        // Add : Metodo para añadir registros a la tabla materia
        public static BL.Result Add(BL.Materia materia)

        {
            BL.Result result = new BL.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "MateriaAdd";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Arreglo para almacenar los datos que fueron solicitados
                        SqlParameter[] collection = new SqlParameter[2];

                        collection[0] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[0].Value = materia.Nombre;

                        collection[1] = new SqlParameter("Costo", SqlDbType.VarChar);
                        collection[1].Value = materia.Costo;



                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery(); //0 -no se insertó //>=1 se insertó

                        if (RowsAffected >= 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al ingresar el Alumno";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;


        }

        //Update: Metodo para actualizar un registro especifico mediante Id en la tabla materia
        public static BL.Result Update(BL.Materia materia)
        {
            BL.Result result = new BL.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "MateriaUpdate";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Arreglo para almacenar los datos que fueron solicitados
                        SqlParameter[] collection = new SqlParameter[3];


                        collection[0] = new SqlParameter("IdMateria", SqlDbType.VarChar);
                        collection[0].Value = materia.IdMateria;

                        collection[1] = new SqlParameter("Nombre", SqlDbType.VarChar);
                        collection[1].Value = materia.Nombre;

                        collection[2] = new SqlParameter("Costo", SqlDbType.VarChar);
                        collection[2].Value = materia.Costo;

                   


                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery(); //0 -no se Actualizo //>=1 se Actualizo

                        if (RowsAffected >= 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al modificar la materia";
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;

        }



        //--------------------------------------------------------------------------------------

        //Metodo Delete:


        //Delete: Metodo para eliminar un registro especifico mediante Id
        public static BL.Result Delete(int IdMateria)
        {
            BL.Result result = new BL.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "MateriaDelete";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Arreglo para almacenar los datos que fueron solicitados
                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdMateria", SqlDbType.VarChar);
                        collection[0].Value = IdMateria;



                        cmd.Parameters.AddRange(collection);

                        cmd.Connection.Open();

                        int RowsAffected = cmd.ExecuteNonQuery(); //0 -no se insertó //>=1 se insertó

                        if (RowsAffected >= 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.ErrorMessage = "Ocurrió un error al eliminar la materia";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }


            return result;
        }

      
    }
}
