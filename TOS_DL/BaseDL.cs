using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace TOS_DL
{
    public class BaseDL
    {
        public static string conStr = ConfigurationManager.ConnectionStrings["TOSConnection"].ConnectionString;
        public DataTable SelectData(string sSQL, params SqlParameter[] para)
        {
            DataTable dt = new DataTable();
            var newCon = new SqlConnection(conStr);
            using (var adapt = new SqlDataAdapter(sSQL, newCon))
            {
                newCon.Open();
                adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (para != null)
                    adapt.SelectCommand.Parameters.AddRange(para);
                adapt.Fill(dt);
                newCon.Close();
            }
            return dt;
        }

        public static DataTable GetBrandNameList()
        {
            DataTable dt = new DataTable();
            string sql = @"select BrandName From M_Brand";
            SqlDataAdapter adpt = new SqlDataAdapter(sql, conStr);
            adpt.Fill(dt);
            return dt;
        }

        public void InsertUpdateDeleteData(string sSQL, params SqlParameter[] para)
        {
            var newCon = new SqlConnection(conStr);
            SqlCommand cmd = new SqlCommand(sSQL, newCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(para);
            cmd.Connection.Open();
            cmd.ExecuteNonQuery();
            cmd.Connection.Close();
        }
    }
}
