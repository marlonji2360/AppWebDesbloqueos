using System.Data.SqlClient;

namespace AppWebDesbloqueos.Models
{
    public class DashboardData
    {
        public IConfiguration Configuration { get; }

        public DashboardData(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public List<KeyValuePair<string, int>> GetListaCounts()
        {
            var estadoCounts = new List<KeyValuePair<string, int>>();

            using (SqlConnection con = new SqlConnection(Configuration["ConnectionStrings:conexion"]))
            {
                string query = "SELECT top 10 Lista, COUNT(*) AS Total FROM Desbloqueos GROUP BY lista order by 2 desc";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        estadoCounts.Add(new KeyValuePair<string, int>(
                            reader["Lista"].ToString(),
                            Convert.ToInt32(reader["Total"])
                        ));
                    }
                }
            }

            return estadoCounts;
        }

        public List<KeyValuePair<string, int>> GetMesesCounts()
        {
            var MesesCounts = new List<KeyValuePair<string, int>>();

            using (SqlConnection con = new SqlConnection(Configuration["ConnectionStrings:conexion"]))
            {
                string query = "SELECT CASE MONTH(FECHA_CORREO) \r\n\t\tWHEN  1 THEN 'ENERO'\r\n\t\tWHEN  2 THEN 'FEBRERO'\r\n\t\tWHEN  3 THEN 'MARZO'\r\n\t\tWHEN  4 THEN 'ABRIL'\r\n\t\tWHEN  5 THEN 'MAYO'\r\n\t\tWHEN  6 THEN 'JUNIO'\r\n\t\tWHEN  7 THEN 'JULIO'\r\n\t\tWHEN  8 THEN 'AGOSTO'\r\n\t\tWHEN  9 THEN 'SEPTIEMBRE'\r\n\t\tWHEN  10 THEN 'OCTUBRE' \r\n\t\tWHEN  11 THEN 'NOVIEMBRE'\r\n\t\tWHEN  12 THEN 'DICIEMBRE'\r\n\t\tEND MES,\r\n\t\tCOUNT(*) AS Total \r\nFROM Desbloqueos \r\nWHERE YEAR(FECHA_CORREO) = YEAR(GETDATE())\r\nGROUP BY MONTH(FECHA_CORREO)\r\nORDER BY MONTH(FECHA_CORREO)";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        MesesCounts.Add(new KeyValuePair<string, int>(
                            reader["Mes"].ToString(),
                            Convert.ToInt32(reader["Total"])
                        ));
                    }
                }
            }

            return MesesCounts;
        }

        public List<KeyValuePair<string, int>> GetDiasCounts()
        {
            var DiasCounts = new List<KeyValuePair<string, int>>();

            using (SqlConnection con = new SqlConnection(Configuration["ConnectionStrings:conexion"]))
            {
                string query = "SELECT DAY(FECHA_CORREO) Dia,\t\r\n\t\tCOUNT(*) AS Total \r\nFROM Desbloqueos \r\nWHERE YEAR(FECHA_CORREO) = YEAR(GETDATE())\r\nAND MONTH(FECHA_CORREO) = MONTH(GETDATE())\r\nGROUP BY DAY(FECHA_CORREO)\r\nORDER BY DAY(FECHA_CORREO)";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        DiasCounts.Add(new KeyValuePair<string, int>(
                            reader["Dia"].ToString(),
                            Convert.ToInt32(reader["Total"])
                        ));
                    }
                }
            }

            return DiasCounts;
        }

        public List<KeyValuePair<string, int>> GetCnCounts()
        {
            var CnCounts = new List<KeyValuePair<string, int>>();

            using (SqlConnection con = new SqlConnection(Configuration["ConnectionStrings:conexion"]))
            {
                string query = "select top 10 CN, COUNT(*) AS Total\r\nfrom DESBLOQUEOS\r\ngroup by CN\r\norder by 2 desc";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CnCounts.Add(new KeyValuePair<string, int>(
                            reader["Cn"].ToString(),
                            Convert.ToInt32(reader["Total"])
                        ));
                    }
                }
            }

            return CnCounts;
        }

        public List<KeyValuePair<string, int>> GetPendientesCounts()
        {
            var PendientesCounts = new List<KeyValuePair<string, int>>();

            using (SqlConnection con = new SqlConnection(Configuration["ConnectionStrings:conexion"]))
            {
                string query = "select ESTADO, COUNT('x') AS TOTAL\r\nfrom DESBLOQUEOS\r\nWHERE ESTADO='PENDIENTE'\r\nGROUP BY ESTADO";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        PendientesCounts.Add(new KeyValuePair<string, int>(
                            reader["Estado"].ToString(),
                            Convert.ToInt32(reader["Total"])
                        ));
                    }
                }
            }

            return PendientesCounts;
        }

        public List<KeyValuePair<string, int>> GetAtendidosHoyCounts()
        {
            var AtendidosHoyCounts = new List<KeyValuePair<string, int>>();

            using (SqlConnection con = new SqlConnection(Configuration["ConnectionStrings:conexion"]))
            {
                string query = "select ID ATENDIDOS, COUNT('x') AS TOTAL\r\nfrom DESBLOQUEOS\r\nWHERE CONVERT(VARCHAR,FECHA_CORREO,103) = CONVERT(VARCHAR,GETDATE(),103)\r\nGROUP BY ID";
                SqlCommand cmd = new SqlCommand(query, con);
                con.Open();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        AtendidosHoyCounts.Add(new KeyValuePair<string, int>(
                            reader["Atendidos"].ToString(),
                            Convert.ToInt32(reader["Total"])
                        ));
                    }
                }
            }

            return AtendidosHoyCounts;
        }
    }
}
