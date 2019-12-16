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
        public DataTable GetInformation(M_CompanyModel mc)
        {
            BaseDL bdl = new BaseDL();
            M_CompanyModel mdl = new M_CompanyModel();
            T_InformationModel tmodel = new T_InformationModel();
            DataTable dtinfo = new DataTable();
            SqlParameter[] prms = new SqlParameter[1];
            prms[0] = new SqlParameter("@companyCD", SqlDbType.VarChar) { Value = mc.CompanyCD};
            dtinfo = bdl.SelectData("T_Information_Select", prms);
            return dtinfo;
        }
     
    }
}
