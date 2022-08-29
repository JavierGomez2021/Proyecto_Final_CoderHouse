using System.Data.SqlClient;
using System.Text;

namespace MiPrimeraApi2.Repository
{
    public static class VentaHandler
    {
        public const String ConnectionString = "Server=DESKTOP-T00K5DR;Database=SistemaGestion;Trusted_Connection=True";

        public static bool NuevaVenta(Venta venta)
        {
            bool resultado = false;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    string insertar = "INSERT INTO VENTA" +
                                      "(COMENTARIOS)" +
                                      "VALUES(@COMENTARIOS)";

                    SqlParameter comentariosParameter = new SqlParameter("COMENTARIOS", System.Data.SqlDbType.VarChar) { Value = venta.Comentarios };

                    using (SqlCommand cmd = new SqlCommand(insertar, cn))
                    {

                        cmd.Parameters.Add(comentariosParameter);                        

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
        public static int VentaMaxId()
        {
            int maxIdVenta = 0;

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    string selectMaxId = "SELECT MAX(ID) AS MAXID FROM VENTA";
                                        
                    using (SqlCommand cmd = new SqlCommand(selectMaxId, cn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    maxIdVenta = Convert.ToInt32(reader["MAXID"]);
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
            return maxIdVenta;
        }
        public static List<Venta> GetVentas()
        {
            List<Venta> listaVentas = new List<Venta>();

            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    string ventasQuery = "SELECT V.ID, CONCAT(V.COMENTARIOS, ' - - - Producto: ', PV.IDPRODUCTO, ' Descripcion: ', P.DESCRIPCIONES) AS COMENTARIOS " +
                                         "FROM VENTA AS V " +
                                         "INNER JOIN PRODUCTOVENDIDO AS PV ON V.ID = PV.IDVENTA " +
                                         "INNER JOIN PRODUCTO AS P ON PV.IDPRODUCTO = P.ID";

                    using (SqlCommand cmd = new SqlCommand(ventasQuery, cn))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    Venta venta = new Venta();

                                    venta.Id = Convert.ToInt32(reader["ID"]);
                                    venta.Comentarios = Convert.ToString(reader["COMENTARIOS"]);

                                    listaVentas.Add(venta);
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
            return listaVentas;
        }
        public static bool DeleteVenta(int id)
        {
            bool resultado = false;
            try
            {
                using (SqlConnection cn = new SqlConnection(ConnectionString))
                {
                    cn.Open();
                    String baja = "DELETE FROM VENTA WHERE ID = @ID";

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
    }
}
