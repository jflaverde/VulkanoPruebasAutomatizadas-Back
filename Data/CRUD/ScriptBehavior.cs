using Data.DTO;
using Data.Messages;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;

namespace Data.CRUD
{
    public class ScriptBehavior:ConexionDB
    {
        /// <summary>
        /// Inserta el script a partir de una prueba
        /// </summary>
        /// <param name="tipoPrueba"></param>
        /// <returns></returns>
        public ReturnMessage AddScript(ScriptDTO script)
        {
            ReturnMessage mensaje = new ReturnMessage();
            string query = @"INSERT INTO [dbo].[SCRIPT]
                                   ([NOMBRE]
                                   ,[SCRIPT]
                                   ,[EXTENSION])
                             VALUES
                                   (@Nombre
                                   ,@Script
                                   ,@Extension)

                            SELECT @@IDENTITY AS 'Identity'";

            using (var con = ConectarDB())
            {
                con.Open();

                try
                {
                    using (SqlCommand command = new SqlCommand(query, con))
                    {
                        command.Parameters.Add(new SqlParameter("@Nombre", script.Nombre));
                        command.Parameters.Add(new SqlParameter("@Script", string.Empty));
                        command.Parameters.Add(new SqlParameter("@Extension", string.Empty));
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                script.ID = Convert.ToInt32(reader[0]);
                            }
                        }
                    }

                    mensaje.Mensaje = "El Script se creó correctamente";
                    mensaje.TipoMensaje = TipoMensaje.Correcto;
                    mensaje.obj = script;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Count not insert.");
                    mensaje.Mensaje = ex.Message;
                    mensaje.TipoMensaje = TipoMensaje.Error;
                    mensaje.obj = script;
                }
                finally
                {
                    con.Close();
                }
                return mensaje;
            }
        }

        /// <summary>
        /// Actualizar el script
        /// </summary>
        /// <param name="script"></param>
        /// <returns></returns>
        public ReturnMessage UpdateScript(ScriptDTO script)
        {
            ReturnMessage mensaje = new ReturnMessage();
            StringBuilder query = new StringBuilder();


            int flagUpdate = 0;
            query.Append(@"SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;
                            UPDATE SCRIPT SET");

            //valida que los campos tengan un valor para actualizar.
            if (!string.IsNullOrEmpty(script.Script))
            {
                query.Append(" Script=@script");
                flagUpdate = 1;
            }
            if (!string.IsNullOrEmpty(script.Extension))
            {
                if(flagUpdate==1)
                {
                    query.Append(",");
                }
                query.Append(" Extension=@extension");
                flagUpdate = 1;
            }
        

            if (flagUpdate == 1)
            {
                query.Append(" WHERE SCRIPT_ID=@script_id;");
            }

            if (flagUpdate == 1)
            {
                using (var con = ConectarDB())
                {
                    con.Open();

                    try
                    {
                        using (SqlCommand command = new SqlCommand(query.ToString(), con))
                        {
                            if (!string.IsNullOrEmpty(script.Script))
                            {
                                command.Parameters.Add(new SqlParameter("@script", script.Script));
                            }
                            if (!string.IsNullOrEmpty(script.Extension))
                            {
                                command.Parameters.Add(new SqlParameter("@extension", script.Extension));
                            }
                            if(flagUpdate==1)
                            {
                                command.Parameters.Add(new SqlParameter("@script_id", script.ID));
                            }
                           
                            command.ExecuteNonQuery();
                        }
                        mensaje.Mensaje = "El script se actualizó correctamente";
                        mensaje.TipoMensaje = TipoMensaje.Correcto;
                        mensaje.obj = script;

                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Count not insert.");
                        mensaje.Mensaje = ex.Message;
                        mensaje.TipoMensaje = TipoMensaje.Error;
                        mensaje.obj = script;
                    }
                    finally
                    {
                        con.Close();
                    }
                }


            }
            return mensaje;
        }
    }
}
