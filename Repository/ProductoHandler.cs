using System.Data.SqlClient;
using System.Text;

namespace MiPrimeraApi2
{
    public static class ProductoHandler 
    {
        public const String ConnectionString = "Server=DESKTOP-T00K5DR;Database=SistemaGestion;Trusted_Connection=True";
        public static Producto GetProductoPorId(int id)
        {
            {
                Producto producto = new Producto();

                try
                {
                    using (SqlConnection conexion = new SqlConnection(ConnectionString))
                    {
                        conexion.Open();
                        string selectId = "SELECT * FROM PRODUCTO WHERE ID = @ID";

                        SqlParameter idParameter = new SqlParameter("ID",System.Data.SqlDbType.BigInt) { Value = id};

                        using (SqlCommand command = new SqlCommand(selectId, conexion))
                        {
                            command.Parameters.Add(idParameter);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    while (reader.Read())
                                    {
                                        producto.Id          = Convert.ToInt32(reader["Id"]);
                                        producto.Descripcion = Convert.ToString(reader["Descripciones"]);
                                        producto.Costo       = Convert.ToInt32(reader["Costo"]);
                                        producto.PrecioVenta = Convert.ToInt32(reader["PrecioVenta"]);
                                        producto.Stock       = Convert.ToInt32(reader["Stock"]);
                                        producto.IdUsuario   = Convert.ToInt32(reader["IdUsuario"]);
                                    }
                                 }
                            }
                        }
                        conexion.Close();
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
                return producto;
            }
        }
        public static List<Producto> GetProductos()
        {
            List<Producto> listaProductos = new List<Producto>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(ConnectionString))
                {
                    conexion.Open();

                    using (SqlCommand command = new SqlCommand("SELECT * FROM PRODUCTO", conexion))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Producto producto = new Producto();

                                    producto.Id          = Convert.ToInt32(reader["Id"]);
                                    producto.Descripcion = Convert.ToString(reader["Descripciones"]);
                                    producto.Costo       = Convert.ToInt32(reader["Costo"]);
                                    producto.PrecioVenta = Convert.ToInt32(reader["PrecioVenta"]);
                                    producto.Stock       = Convert.ToInt32(reader["Stock"]);
                                    producto.IdUsuario   = Convert.ToInt32(reader["IdUsuario"]);

                                    listaProductos.Add(producto);
                                }
                            }
                        }
                    }
                    conexion.Close();
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
            return listaProductos;
        }
        public static bool CrearProducto(Producto producto)
        {
            bool resultado = false;
 
            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    string insertarProducto = "INSERT INTO PRODUCTO" +
                                              "(DESCRIPCIONES,COSTO,PRECIOVENTA,STOCK,IDUSUARIO)" +
                                              "VALUES(@DESCRIPCIONES,@COSTO,@PRECIOVENTA,@STOCK,@IDUSUARIO)";

                    SqlParameter descripcionParameter = new SqlParameter("DESCRIPCIONES", System.Data.SqlDbType.VarChar) { Value = producto.Descripcion };
                    SqlParameter costoParameter       = new SqlParameter("COSTO",         System.Data.SqlDbType.BigInt ) { Value = producto.Costo       };
                    SqlParameter precioVentaParameter = new SqlParameter("PRECIOVENTA",   System.Data.SqlDbType.BigInt ) { Value = producto.PrecioVenta };
                    SqlParameter stockParameter       = new SqlParameter("STOCK",         System.Data.SqlDbType.BigInt ) { Value = producto.Stock       };
                    SqlParameter idUsuarioParameter   = new SqlParameter("IDUSUARIO",     System.Data.SqlDbType.Int    ) { Value = producto.IdUsuario   };

                    using (SqlCommand cmd = new SqlCommand(insertarProducto, cn))
                    {

                        cmd.Parameters.Add(descripcionParameter);
                        cmd.Parameters.Add(costoParameter);
                        cmd.Parameters.Add(precioVentaParameter);
                        cmd.Parameters.Add(stockParameter);
                        cmd.Parameters.Add(idUsuarioParameter);

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
        public static bool EliminarProducto(int id)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    String baja = "DELETE FROM PRODUCTO WHERE ID = @ID";

                    SqlParameter sqlParameter = new SqlParameter("ID", System.Data.SqlDbType.BigInt);
                    sqlParameter.Value = id;

                    using (SqlCommand cmd = new SqlCommand(baja, cn))
                    {
                        cmd.Parameters.Add(sqlParameter);
                        
                        int filasAfectadas = cmd.ExecuteNonQuery();

                        if(filasAfectadas > 0)
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
        public static bool ModificarProducto(Producto producto)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    String modificar = "UPDATE PRODUCTO SET " +
                                       "DESCRIPCIONES = @DESCRIPCIONES," +
                                       "COSTO = @COSTO," +
                                       "PRECIOVENTA = @PRECIOVENTA," +
                                       "STOCK = @STOCK," +
                                       "IDUSUARIO = @IDUSUARIO " +
                                       "WHERE ID = @ID";
                    using (SqlCommand cmd = new SqlCommand(modificar, cn))
                    {
                        cmd.Parameters.AddWithValue("@ID", producto.Id);
                        cmd.Parameters.AddWithValue("@DESCRIPCIONES", producto.Descripcion);
                        cmd.Parameters.AddWithValue("@COSTO", producto.Costo);
                        cmd.Parameters.AddWithValue("@PRECIOVENTA", producto.PrecioVenta);
                        cmd.Parameters.AddWithValue("STOCK", producto.Stock);
                        cmd.Parameters.AddWithValue("IDUSUARIO", producto.IdUsuario);

                        int filasAfectadas = cmd.ExecuteNonQuery();
                        if(filasAfectadas > 0)
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
        public static bool ModificarProductoStock(Producto producto)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    String modificar = "UPDATE PRODUCTO SET " +
                                       "STOCK = @STOCK " +                                       
                                       "WHERE ID = @ID";

                    using (SqlCommand cmd = new SqlCommand(modificar, cn))
                    {                        
                        cmd.Parameters.AddWithValue("STOCK", producto.Stock);
                        cmd.Parameters.AddWithValue("ID", producto.Id);
                     
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
        public static bool ExisteProducto(int id)
        {
            {
                bool resultado = false;

                try
                {
                    using (SqlConnection conexion = new SqlConnection(ConnectionString))
                    {
                        conexion.Open();
                        string selectId = "SELECT * FROM PRODUCTO WHERE ID = @ID";

                        SqlParameter idParameter = new SqlParameter("ID", System.Data.SqlDbType.BigInt) { Value = id };

                        using (SqlCommand command = new SqlCommand(selectId, conexion))
                        {
                            command.Parameters.Add(idParameter);

                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.HasRows)
                                {
                                    resultado = true;
                                }
                            }
                        }
                        conexion.Close();
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
}