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
            dtinfo = bdl.SelectData("T_Information_Select_ForHomePage", prms);
            return dtinfo;
        }


        public DataTable Get_M_CompanyName()
        {
            BaseDL bdl = new BaseDL();
            DataTable dtcompanyname = new DataTable();
            SqlParameter[] prms = new SqlParameter[1];
            dtcompanyname = bdl.SelectData("M_Company_Select",null);
            return dtcompanyname;
        }

        public DataTable Get_M_GroupName()
        {
            BaseDL bdl = new BaseDL();
            DataTable dtgroupname = new DataTable();
            SqlParameter[] prms = new SqlParameter[1];
            dtgroupname = bdl.SelectData("M_Group_Select", null);
            return dtgroupname;
        }

        public DataTable T_Information_Select()
        {
            BaseDL bdl = new BaseDL();
            DataTable dtinformation = new DataTable();
            SqlParameter[] prms = new SqlParameter[1];
            dtinformation = bdl.SelectData("T_Information_Select", null);
            return dtinformation;
        }

        public DataTable Get_InformationTitleName(string id)
        {
            
          
            BaseDL dl = new BaseDL();
            SqlParameter prm = new SqlParameter("@InformationID", SqlDbType.Int) { Value=Convert.ToInt16(id)};
            DataTable dt = dl.SelectData("Select_InformationTitle",prm);
            
            
            return dt;
        }
    }
}
