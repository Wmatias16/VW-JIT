using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
namespace JITVW
{
    class JitDatabase
    {
        public void Agregar(List<Jit> vw)
        {

            try
            {
                SqlConnection conexion = new SqlConnection();
                conexion.ConnectionString = ConfigurationManager.AppSettings[0];

                string query = "INSERT INTO JIT VALUES(@Pkn, @Fecha,@Secuencia,@Modelo,@NumeroSerie)";
                foreach (Jit vwJit in vw)
                {
                    using (SqlCommand command = new SqlCommand(query, conexion))
                    {
                       
                        command.Parameters.AddWithValue("@Pkn", vwJit.Pkn);
                        command.Parameters.AddWithValue("@Fecha", vwJit.Fecha);
                        command.Parameters.AddWithValue("@Secuencia", vwJit.Secuencia);
                        command.Parameters.AddWithValue("@Modelo", vwJit.Modelo);
                        command.Parameters.AddWithValue("@NumeroSerie", vwJit.NumeroSerie);
                        conexion.Open();
                        command.ExecuteNonQuery();
                        conexion.Close();
                    }

                }

            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
