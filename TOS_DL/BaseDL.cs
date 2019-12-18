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

        public DataSet SelectDataSet(string sSQL, params SqlParameter[] para)
        {
            DataSet ds1 = new DataSet();
            var newCon = new SqlConnection(conStr);
            using (var adapt = new SqlDataAdapter(sSQL, newCon))
            {
                newCon.Open();
                adapt.SelectCommand.CommandType = CommandType.StoredProcedure;
                if (para != null)
                    adapt.SelectCommand.Parameters.AddRange(para);
                adapt.Fill(ds1,"DataList");
                newCon.Close();
            }
            return ds1;
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
