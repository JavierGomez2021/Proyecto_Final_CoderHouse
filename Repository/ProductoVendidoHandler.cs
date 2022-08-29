using System.Data.SqlClient;
using System.Text;

namespace MiPrimeraApi2.Repository
{
    public class ProductoVendidoHandler
    {
        public const String ConnectionString = "Server=DESKTOP-T00K5DR;Database=SistemaGestion;Trusted_Connection=True";

        public static bool CargoProductoVendido(ProductoVendido productoVendido)
        {
            bool resultado = false;
                        
            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    string insertar = "INSERT INTO PRODUCTOVENDIDO" +
                                      "(STOCK,IDPRODUCTO,IDVENTA)" +
                                      "VALUES(@STOCK,@IDPRODUCTO,@IDVENTA)";

                    SqlParameter stockParameter      = new SqlParameter("STOCK", System.Data.SqlDbType.Int)         { Value = productoVendido.Stock };
                    SqlParameter idProductoParameter = new SqlParameter("IDPRODUCTO", System.Data.SqlDbType.BigInt) { Value = productoVendido.IdProducto };
                    SqlParameter idVentaParameter    = new SqlParameter("IDVENTA", System.Data.SqlDbType.BigInt)    { Value = productoVendido.IdVenta };

                    using (SqlCommand cmd = new SqlCommand(insertar, cn))
                    {

                        cmd.Parameters.Add(stockParameter);
                        cmd.Parameters.Add(idProductoParameter);
                        cmd.Parameters.Add(idVentaParameter);

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

        public static bool GetProductoVendidoPorIdProducto(int idProducto)
        {
            {
                bool resultado = false;

                try
                {
                    using (SqlConnection conexion = new SqlConnection(ConnectionString))
                    {
                        conexion.Open();
                        string selectId = "SELECT * FROM PRODUCTOVENDIDO WHERE IDPRODUCTO = @IDPRODUCTO";

                        SqlParameter idParameter = new SqlParameter("IDPRODUCTO", System.Data.SqlDbType.BigInt) { Value = idProducto };

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

        public static List<ProductoVendido> GetProductoVendidoPorIdVenta(int idVenta)
        {
            List<ProductoVendido> listaProductosVendidos = new List<ProductoVendido>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(ConnectionString))
                {
                    conexion.Open();
                    string selectId = "SELECT * FROM PRODUCTOVENDIDO WHERE IDVENTA = @IDVENTA";

                    SqlParameter idParameter = new SqlParameter("IDVENTA", System.Data.SqlDbType.BigInt) { Value = idVenta };

                    using (SqlCommand command = new SqlCommand(selectId, conexion))
                    {
                        command.Parameters.Add(idParameter);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while(reader.Read())
                                {
                                    ProductoVendido productoVendido = new ProductoVendido();

                                    productoVendido.Id         = Convert.ToInt32(reader["Id"]);
                                    productoVendido.IdProducto = Convert.ToInt32(reader["IdProducto"]);
                                    productoVendido.Stock      = Convert.ToInt32(reader["Stock"]);
                                    productoVendido.IdVenta    = Convert.ToInt32(reader["IdVenta"]);

                                    listaProductosVendidos.Add(productoVendido);
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
            return listaProductosVendidos;
        }

        public static bool EliminoProductoVendidoPorIdProducto(int idProducto)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    String baja = "DELETE FROM PRODUCTOVENDIDO WHERE IDPRODUCTO = @IDPRODUCTO";

                    SqlParameter sqlParameter = new SqlParameter("IDPRODUCTO", System.Data.SqlDbType.BigInt);
                    sqlParameter.Value = idProducto;

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

        public static bool EliminoProductoVendidoPorIdVenta(int idVenta)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    String baja = "DELETE FROM PRODUCTOVENDIDO WHERE IDVENTA = @IDVENTA";

                    SqlParameter sqlParameter = new SqlParameter("IDVENTA", System.Data.SqlDbType.BigInt);
                    sqlParameter.Value = idVenta;

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

        public static List<ProductoVendido> GetProductosVendidos(int idUsuario)
        {
            List<ProductoVendido> listaProductosVendidos = new List<ProductoVendido>();

            try
            {
                using (SqlConnection conexion = new SqlConnection(ConnectionString))
                {
                    conexion.Open();
                    string query = "select PV.Id, PV.IdProducto, PV.IdVenta, PV.Stock " +
                                   "from producto as P                                " +
                                   "inner join productoVendido as PV                  " +
                                   "on P.Id = PV.IdProducto                           " +
                                   "inner join venta as V                             " +
                                   "on PV.IdVenta = V.Id                              " +
                                   "where P.IdUsuario = @IDUSUARIO                    ";

                    SqlParameter idUsuarioParameter = new SqlParameter("IDUSUARIO",System.Data.SqlDbType.BigInt) { Value = idUsuario};

                    using (SqlCommand cmd = new SqlCommand(query, conexion))   
                    {
                        cmd.Parameters.Add(idUsuarioParameter);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    ProductoVendido productoVendido = new ProductoVendido();

                                    productoVendido.Id         = Convert.ToInt32(reader["Id"]);
                                    productoVendido.IdProducto = Convert.ToInt32(reader["IdProducto"]);
                                    productoVendido.Stock      = Convert.ToInt32(reader["Stock"]);
                                    productoVendido.IdVenta    = Convert.ToInt32(reader["IdVenta"]);

                                    listaProductosVendidos.Add(productoVendido);
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
            return listaProductosVendidos;
        }
    }
}

