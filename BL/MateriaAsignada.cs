using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class MateriaAsignada
    {
        //Propiedades para recibir el modelo de base de datos
        public int IdMateriaAsignada { get; set; }
        public Alumno Alumno { get; set; } //Obtener el IdAlumno al que se le va a modificar las materias
        public Materia Materia { get; set; }// Obtener el Id de la metaria y Aquellas que no esten asignadas
        public List<object> MatriasAsignadasAlumnos { get; set; }


        //Metodos Funcionales()

        //Metodos GET :

        ////GetAll: Metodo para traer todos los alumnos
        public static BL.Result GetAll()
        {
            BL.Result result = new BL.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "AlumnoGetAll";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tablaAlumnos = new DataTable("Tabla Alumnos");

                            da.Fill(tablaAlumnos);

                            if (tablaAlumnos.Rows.Count > 0)
                            {
                                result.Objects = new List<object>();
                                foreach (DataRow row in tablaAlumnos.Rows)
                                {
                                    BL.Alumno alumno = new BL.Alumno();

                                    alumno.IdAlumno = int.Parse(row[0].ToString());
                                    alumno.Nombre = row[1].ToString();
                                    alumno.ApellidoPaterno = row[2].ToString();
                                    alumno.ApellidoMaterno = row[3].ToString();
                                    alumno.Imagen = row[3].ToString();
                                    result.Objects.Add(alumno);
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


        /* GetAllMateriaAsignada: Metodo para traer todas las materias asignadas a un alumno en especifico
        seleccionado por su id */
        public static BL.Result GetAllMateriaAsignada(int IdAlumno)
        {
            BL.Result result = new BL.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "MateriaAsignadaGetByAlumno";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdAlumno", SqlDbType.VarChar);
                        collection[0].Value = IdAlumno;
                        cmd.Parameters.AddRange(collection);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tablaAlumnos = new DataTable("Tabla Alumnos");

                            da.Fill(tablaAlumnos);

                            if (tablaAlumnos.Rows.Count > 0)
                            {
                                result.Objects = new List<object>();
                                foreach (DataRow row in tablaAlumnos.Rows)
                                {
                                    BL.MateriaAsignada materiasAsignadas = new BL.MateriaAsignada();

                                    materiasAsignadas.IdMateriaAsignada = int.Parse(row[0].ToString());
                                    materiasAsignadas.Materia = new BL.Materia();
                                    materiasAsignadas.Materia.IdMateria = int.Parse(row[1].ToString());
                                    materiasAsignadas.Materia.Nombre = row[2].ToString();
                                    materiasAsignadas.Materia.Costo = decimal.Parse(row[3].ToString());
                                    materiasAsignadas.Alumno = new BL.Alumno();
                                    materiasAsignadas.Alumno.IdAlumno = int.Parse(row[4].ToString());
                                    materiasAsignadas.Alumno.Nombre = row[5].ToString();
                                    result.Objects.Add(materiasAsignadas);
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

        /* GetAllMateriaNoAsignada: Metodo para traer todas las materias NO asignadas a un alumno en especifico
        seleccionado por su id */
        public static BL.Result GetAllMateriaNoAsignada(int IdAlumno)
        {
            BL.Result result = new BL.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "MateriasNoAsignadasGetByAlumno";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlParameter[] collection = new SqlParameter[1];

                        collection[0] = new SqlParameter("IdAlumno", SqlDbType.VarChar);
                        collection[0].Value = IdAlumno;
                        cmd.Parameters.AddRange(collection);

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            DataTable tablaAlumnos = new DataTable("Tabla Alumnos");

                            da.Fill(tablaAlumnos);

                            if (tablaAlumnos.Rows.Count > 0)
                            {
                                result.Objects = new List<object>();
                                foreach (DataRow row in tablaAlumnos.Rows)
                                {
                                    BL.MateriaAsignada materiasNoAsignadas = new BL.MateriaAsignada();
                                    materiasNoAsignadas.Materia = new BL.Materia();
                                    materiasNoAsignadas.Materia.IdMateria = int.Parse(row[0].ToString());
                                    materiasNoAsignadas.Materia.Nombre = row[1].ToString();
                                    materiasNoAsignadas.Materia.Costo = decimal.Parse(row[2].ToString());

                                    result.Objects.Add(materiasNoAsignadas);
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


        //--------------------------------------------------------------------------------------


        //Metodos Post:

        // Add : Metodo para añadir registros a la tabla MateriaAsignada
        public static BL.Result Add(BL.MateriaAsignada asignarMaterias)
        {
            BL.Result result = new BL.Result();
            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    var query = "MateriaAsignadaAdd";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;
                        // Arreglo para almacenar los datos que fueron solicitados
                        SqlParameter[] collection = new SqlParameter[2];

                        collection[0] = new SqlParameter("IdMateria", SqlDbType.VarChar);
                        collection[0].Value = asignarMaterias.Materia.IdMateria;

                        collection[1] = new SqlParameter("IdAlumno", SqlDbType.VarChar);
                        collection[1].Value = asignarMaterias.Alumno.IdAlumno;


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
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = "ocurrio un error" + ex.Message;
            }
            return result;
        }



        //--------------------------------------------------------------------------------------

        //Metodo Delete:

        //Delete: Metodo para eliminar un registro especifico mediante Id
        public static BL.Result Delete(BL.MateriaAsignada borrarMaterias)
        {
            BL.Result result = new BL.Result();

            try
            {
                using (SqlConnection context = new SqlConnection(DL.Conexion.GetConnectionString()))
                {
                    string query = "MateriaAsignadaDelete";
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;
                        cmd.CommandType = CommandType.StoredProcedure;

                        // Arreglo para almacenar los datos que fueron solicitados
                        SqlParameter[] collection = new SqlParameter[2];

                        collection[0] = new SqlParameter("IdMateria", SqlDbType.VarChar);
                        collection[0].Value = borrarMaterias.Materia.IdMateria;
                        collection[1] = new SqlParameter("IdAlumno", SqlDbType.VarChar);
                        collection[1].Value = borrarMaterias.Alumno.IdAlumno;



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
                            result.ErrorMessage = "Ocurrió un error al Eliminar el Registro";
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
