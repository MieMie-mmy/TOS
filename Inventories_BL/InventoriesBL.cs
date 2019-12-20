using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOS_Model;
using TOS_DL;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Inventories_BL
{
    public class InventoriesBL
    {
        public static string conStr = ConfigurationManager.ConnectionStrings["TOSConnection"].ConnectionString;
        public DataTable dt = new DataTable();
        BaseDL dl = new BaseDL();
        public DataTable BrandName_Select()
        {
            DataTable dt = new DataTable();
            BaseDL dl = new BaseDL();
            string sql = @"select * From M_Brand";
            SqlDataAdapter adpt = new SqlDataAdapter(sql, conStr);
            adpt.Fill(dt);
            return dt;
            
        }
        public DataTable Inventory_Search(string id)
        {
            string[] arr;
            var item_name=string.Empty;
            var gclassname = string.Empty;
            var mitemcd = string.Empty;
            var brandcd = string.Empty;
            if (id!=null)
            {
                arr = id.Split('_');
                item_name = arr[0];
                gclassname = arr[1];
                mitemcd = arr[2];
                brandcd = arr[3];
                
            }
            
            
            SqlParameter[] prm = new SqlParameter[4];
           if(brandcd=="")
            {
                prm[0] = new SqlParameter("@brandcd", SqlDbType.Int) { Value = DBNull.Value };

            }
            else
            {
                prm[0] = new SqlParameter("@brandcd", SqlDbType.Int) { Value = Convert.ToInt16(brandcd) };

            }
            if (item_name=="")
            {
                prm[1] = new SqlParameter("@item_name", SqlDbType.VarChar) { Value = DBNull.Value };
            }
           else
            {
                prm[1] = new SqlParameter("@item_name", SqlDbType.VarChar) { Value = item_name };
            }
           if(gclassname=="")
            {
                prm[2] = new SqlParameter("@gclassname", SqlDbType.VarChar) { Value = DBNull.Value };
            }
           else
            {
                prm[2] = new SqlParameter("@gclassname", SqlDbType.VarChar) { Value = gclassname };
            }
           if(mitemcd =="")
            {
                prm[3] = new SqlParameter("@mitemcd", SqlDbType.VarChar) { Value = DBNull.Value };
            }
           else
            {
                prm[3] = new SqlParameter("@mitemcd", SqlDbType.VarChar) { Value = mitemcd };
            }
            

            dt = dl.SelectData("Inventories_BrandName_Select", prm);

            return dt;
        }

    }
}
