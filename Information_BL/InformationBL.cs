using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using TOS_Model;
using TOS_DL;


namespace Information_BL
{
    public class InformationBL
    {
        public DataTable GetInformation(string CompanyCD)
        {
            BaseDL bdl = new BaseDL();
            DataTable dtinfo = new DataTable();
            SqlParameter[] prms = new SqlParameter[1];
            prms[0] = new SqlParameter("@companyCD", SqlDbType.VarChar) { Value = CompanyCD};
            dtinfo = bdl.SelectData("T_Information_Select", prms);
            return dtinfo;
        }
     
    }
}
