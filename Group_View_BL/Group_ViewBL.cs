using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOS_DL;
using TOS_Model;

namespace Group_View_BL
{
    public class Group_ViewBL
    {
        public DataTable Get_Group_View()
        {
            BaseDL bdl = new BaseDL();
            DataTable dtorder_portal = new DataTable();
            SqlParameter[] prms = new SqlParameter[2];
            prms[0] = new SqlParameter("@id", SqlDbType.VarChar) { Value = DBNull.Value };
            prms[1] = new SqlParameter("@option", SqlDbType.VarChar) { Value = 1 };
            dtorder_portal = bdl.SelectData("Group_View_Select", prms);
            return dtorder_portal;
        }
        public DataTable Get_Group_View_ForEdit(string id)
        {
            BaseDL bdl = new BaseDL();
            DataTable dtinformation = new DataTable();
            SqlParameter[] prms = new SqlParameter[2];
            prms[0] = new SqlParameter("@id", SqlDbType.VarChar) { Value = id };
            prms[1] = new SqlParameter("@option", SqlDbType.VarChar) { Value = 2 };
            dtinformation = bdl.SelectData("Group_View_Select", prms);
            return dtinformation;
        }
        public string Group_Info_Delete(string id, string PcName, string InsertOperator)
        {
            var message = string.Empty;
            BaseDL bdl = new BaseDL();
            DataTable dtinfo = new DataTable();
            SqlParameter[] prms = new SqlParameter[3];
            prms[0] = new SqlParameter("@id", SqlDbType.VarChar) { Value = id };
            prms[1] = new SqlParameter("@InsertOperator", SqlDbType.VarChar) { Value = InsertOperator };
            prms[2] = new SqlParameter("@AccessPC", SqlDbType.VarChar) { Value = PcName };
            bdl.InsertUpdateDeleteData("Group_View_Delete", prms);
            message = "OK";
            return message;
        }
    }
}
