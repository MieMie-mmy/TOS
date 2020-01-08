using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOS_Model;
using TOS_DL;
using System.Data;
using System.Data.SqlClient;

namespace Order_Portal_BL
{
    public class Order_PortalBL
    {
        public DataTable Order_Portal_List_Select(string companyCD)
        {
            BaseDL bdl = new BaseDL();
            DataTable dtorder_portal = new DataTable();
            SqlParameter[] prms = new SqlParameter[1];
            prms[0] = new SqlParameter("@companyCD", SqlDbType.VarChar) { Value = companyCD };
            dtorder_portal = bdl.SelectData("Order_Potal_Select", prms);
            return dtorder_portal;
        }
    }
}
