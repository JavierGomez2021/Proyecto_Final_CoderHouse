using System.Data.SqlClient;
using System.Text;

namespace MiPrimeraApi2
{ 
    public static class UsuarioHandler
    {
        public const String ConnectionString = "Server=DESKTOP-T00K5DR;Database=SistemaGestion;Trusted_Connection=True";
        public static List<Usuario> GetUsuarios()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    String listaUsuariosQuery = "SELECT * FROM USUARIO";

                    using (SqlCommand cmd = new SqlCommand(listaUsuariosQuery, cn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if(reader.HasRows)
                            {
                                while(reader.Read())
                                {
                                    Usuario usuario = new Usuario();

                                    usuario.Id            = Convert.ToInt32(reader["ID"]);
                                    usuario.Nombre        = Convert.ToString(reader["NOMBRE"]);
                                    usuario.Apellido      = Convert.ToString(reader["APELLIDO"]);
                                    usuario.NombreUsuario = Convert.ToString(reader["NOMBREUSUARIO"]);
                                    usuario.Contraseña    = Convert.ToString(reader["CONTRASEÑA"]);
                                    usuario.Mail          = Convert.ToString(reader["MAIL"]);

                                    listaUsuarios.Add(usuario);
                                }
                            }
                        }
                    }
                    cn.Close();
                }
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();

                errorMessages.Append("Message:      " + ex.Message    + "\n" +
                                     "Error Number: " + ex.Number     + "\n" +
                                     "LineNumber:   " + ex.LineNumber + "\n" +
                                     "Source:       " + ex.Source     + "\n" +
                                     "Procedure:    " + ex.Procedure  + "\n");

                Console.WriteLine(errorMessages.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex);
            }
            return listaUsuarios;
        }
        public static Usuario GetUsuarioPorNombreUsuario(string nombreUsuario)
        {
            Usuario usuario = new Usuario();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    String usuarioNombreUsuarioQuery = "SELECT * FROM USUARIO WHERE NOMBREUSUARIO = @NOMBREUSUARIO";

                    SqlParameter nombreUsuarioParameter = new SqlParameter("NOMBREUSUARIO", System.Data.SqlDbType.VarChar) { Value = nombreUsuario };

                    using (SqlCommand cmd = new SqlCommand(usuarioNombreUsuarioQuery, cn))
                    {
                        cmd.Parameters.Add(nombreUsuarioParameter);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {   
                                    usuario.Id            = Convert.ToInt32(reader["ID"]);
                                    usuario.Nombre        = Convert.ToString(reader["NOMBRE"]);
                                    usuario.Apellido      = Convert.ToString(reader["APELLIDO"]);
                                    usuario.NombreUsuario = Convert.ToString(reader["NOMBREUSUARIO"]);
                                    usuario.Contraseña    = Convert.ToString(reader["CONTRASEÑA"]);
                                    usuario.Mail          = Convert.ToString(reader["MAIL"]);                                  
                                }
                            }
                        }
                    }
                    cn.Close();
                }
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();

                errorMessages.Append("Message:      " + ex.Message    + "\n" +
                                     "Error Number: " + ex.Number     + "\n" +
                                     "LineNumber:   " + ex.LineNumber + "\n" +
                                     "Source:       " + ex.Source     + "\n" +
                                     "Procedure:    " + ex.Procedure  + "\n");

                Console.WriteLine(errorMessages.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex);
            }
            return usuario;
        }
        public static Usuario InicioSesion(String nombreUsuario, String contraseña)
        {
            Usuario usuario = new Usuario();
            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    String usuarioContraseñaQuery = "SELECT * FROM USUARIO WHERE NOMBREUSUARIO = @NOMBREUSUARIO AND CONTRASEÑA = @CONTRASEÑA";

                    SqlParameter sqlParameterNombreUsuario = new SqlParameter("NOMBREUSUARIO", System.Data.SqlDbType.VarChar);
                    SqlParameter sqlParameterContraseña = new SqlParameter("CONTRASEÑA", System.Data.SqlDbType.VarChar);

                    sqlParameterNombreUsuario.Value = nombreUsuario;
                    sqlParameterContraseña.Value = contraseña;

                    using (SqlCommand cmd = new SqlCommand(usuarioContraseñaQuery, cn))
                    {
                        cmd.Parameters.Add(sqlParameterNombreUsuario);
                        cmd.Parameters.Add(sqlParameterContraseña);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    usuario.NombreUsuario = Convert.ToString(reader["NOMBREUSUARIO"]);
                                }
                            }
                        }
                    }
                    cn.Close();
                }
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();

                errorMessages.Append("Message:      " + ex.Message + "\n" +
                                     "Error Number: " + ex.Number + "\n" +
                                     "LineNumber:   " + ex.LineNumber + "\n" +
                                     "Source:       " + ex.Source + "\n" +
                                     "Procedure:    " + ex.Procedure + "\n");

                Console.WriteLine(errorMessages.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex);
            }
            return usuario;
        }
        public static bool EliminarUsuario(int id)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    String baja = "DELETE FROM USUARIO WHERE ID = @ID";

                    SqlParameter sqlParameter = new SqlParameter("ID", System.Data.SqlDbType.BigInt);
                    sqlParameter.Value = id;

                    using (SqlCommand cmd = new SqlCommand(baja, cn))
                    {                        
                        cmd.Parameters.Add(sqlParameter);                        
                        int filasAfectadas = cmd.ExecuteNonQuery();                       
                        if (filasAfectadas > 0)
                        {
                            resultado = true;
                        }
                        else
                        {
                            resultado = false;
                        }
                    }
                    cn.Close();
                }
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();

                errorMessages.Append("Message:      " + ex.Message    + "\n" +
                                     "Error Number: " + ex.Number     + "\n" +
                                     "LineNumber:   " + ex.LineNumber + "\n" +
                                     "Source:       " + ex.Source     + "\n" +
                                     "Procedure:    " + ex.Procedure  + "\n");

                Console.WriteLine(errorMessages.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex);
            }
            return resultado;
        }
        public static bool ModificarUsuario(Usuario usuario)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    String modificar = "UPDATE USUARIO SET " +
                                       "NOMBRE = @NOMBRE," +
                                       "APELLIDO = @APELLIDO," +
                                       "NOMBREUSUARIO = @NOMBREUSUARIO," +
                                       "CONTRASEÑA = @CONTRASEÑA," +
                                       "MAIL = @MAIL " +
                                       "WHERE ID = @ID";

                    SqlParameter nombreParameter        = new SqlParameter("NOMBRE",        System.Data.SqlDbType.VarChar) { Value = usuario.Nombre };
                    SqlParameter apellidoParameter      = new SqlParameter("APELLIDO",      System.Data.SqlDbType.VarChar) { Value = usuario.Apellido };
                    SqlParameter nombreUsuarioParameter = new SqlParameter("NOMBREUSUARIO", System.Data.SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                    SqlParameter contraseñaParameter    = new SqlParameter("CONTRASEÑA",    System.Data.SqlDbType.VarChar) { Value = usuario.Contraseña };
                    SqlParameter mailParameter          = new SqlParameter("MAIL",          System.Data.SqlDbType.VarChar) { Value = usuario.Mail };
                    SqlParameter iDParameter            = new SqlParameter("ID",            System.Data.SqlDbType.BigInt)  { Value = usuario.Id };
 
                    using (SqlCommand cmd = new SqlCommand(modificar, cn))
                    {
                        cmd.Parameters.Add(nombreParameter);
                        cmd.Parameters.Add(apellidoParameter);
                        cmd.Parameters.Add(nombreUsuarioParameter);
                        cmd.Parameters.Add(contraseñaParameter);
                        cmd.Parameters.Add(mailParameter);
                        cmd.Parameters.Add(iDParameter);

                        int numFilasAfectadas = cmd.ExecuteNonQuery();

                        if(numFilasAfectadas > 0)
                        {
                            resultado = true;
                        }
                        else
                        {
                            resultado = false;                        
                        }
                    }
                    cn.Close();
                }
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();

                errorMessages.Append("Message:      " + ex.Message    + "\n" +
                                     "Error Number: " + ex.Number     + "\n" +
                                     "LineNumber:   " + ex.LineNumber + "\n" +
                                     "Source:       " + ex.Source     + "\n" +
                                     "Procedure:    " + ex.Procedure  + "\n");

                Console.WriteLine(errorMessages.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex);
            }
            return resultado;
        }
        public static bool CrearUsuario(Usuario usuario)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    string insertar = "INSERT INTO USUARIO" +
                                      "(NOMBRE,APELLIDO,NOMBREUSUARIO,CONTRASEÑA,MAIL)" +
                                      "VALUES(@NOMBRE,@APELLIDO,@NOMBREUSUARIO,@CONTRASEÑA,@MAIL)";

                    SqlParameter nombreParameter        = new SqlParameter("NOMBRE",        System.Data.SqlDbType.VarChar) { Value = usuario.Nombre };
                    SqlParameter apellidoParameter      = new SqlParameter("APELLIDO",      System.Data.SqlDbType.VarChar) { Value = usuario.Apellido };
                    SqlParameter nombreUsuarioParameter = new SqlParameter("NOMBREUSUARIO", System.Data.SqlDbType.VarChar) { Value = usuario.NombreUsuario };
                    SqlParameter contraseñaParameter    = new SqlParameter("CONTRASEÑA",    System.Data.SqlDbType.VarChar) { Value = usuario.Contraseña };
                    SqlParameter mailParameter          = new SqlParameter("MAIL",          System.Data.SqlDbType.VarChar) { Value = usuario.Mail };

                    using (SqlCommand cmd = new SqlCommand(insertar, cn))
                    {
                        cmd.Parameters.Add(nombreParameter);
                        cmd.Parameters.Add(apellidoParameter);
                        cmd.Parameters.Add(nombreUsuarioParameter);
                        cmd.Parameters.Add(contraseñaParameter);
                        cmd.Parameters.Add(mailParameter);

                        int numFilasAfectadas = cmd.ExecuteNonQuery();

                        if (numFilasAfectadas > 0)
                        {
                            resultado = true;
                        }
                        else
                        {
                            resultado = false;
                        }
                    }
                    cn.Close();
                }
            }
            catch(SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();

                errorMessages.Append("Message:      " + ex.Message    + "\n" +
                                     "Error Number: " + ex.Number     + "\n" +
                                     "LineNumber:   " + ex.LineNumber + "\n" +
                                     "Source:       " + ex.Source     + "\n" +
                                     "Procedure:    " + ex.Procedure  + "\n");

                Console.WriteLine(errorMessages.ToString());            
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex);
            }
            return resultado;
        }       
        public static bool ValidarDatosUsuario(Usuario usuario)
        {
            if (usuario.Nombre == "" || usuario.Apellido == "" || usuario.NombreUsuario == "" || usuario.Contraseña == "" || usuario.Mail == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public static bool ValidarNombreUsuarioExistente(string nombreUsuario)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    String nombreUsuarioQuery = "SELECT * FROM USUARIO WHERE NOMBREUSUARIO = @NOMBREUSUARIO";

                    SqlParameter nombreUsuarioParameter = new SqlParameter("NOMBREUSUARIO", System.Data.SqlDbType.VarChar) { Value = nombreUsuario };

                    using (SqlCommand cmd = new SqlCommand(nombreUsuarioQuery, cn))
                    {
                        cmd.Parameters.Add(nombreUsuarioParameter);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    string nombreUsuarioTabla;

                                    nombreUsuarioTabla = Convert.ToString(reader["NOMBREUSUARIO"]);
                                    
                                    if(nombreUsuarioTabla == nombreUsuario)
                                    {
                                        resultado = true;
                                    }
                                    else
                                    {
                                        resultado = false;
                                    }                                   
                                }
                            }
                        }
                    }
                    cn.Close();
                }
            }
            catch (SqlException ex)
            {
                StringBuilder errorMessages = new StringBuilder();

                errorMessages.Append("Message:      " + ex.Message    + "\n" +
                                     "Error Number: " + ex.Number     + "\n" +
                                     "LineNumber:   " + ex.LineNumber + "\n" +
                                     "Source:       " + ex.Source     + "\n" +
                                     "Procedure:    " + ex.Procedure  + "\n");

                Console.WriteLine(errorMessages.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error:" + ex);
            }
            return resultado;
        }
    }      
}
