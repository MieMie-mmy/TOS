using System.Linq;
using System.Data.SqlClient;
using System.Data;
using TOS_Model;
using TOS_DL;

namespace Company_BL
{
    public class InformationBL
    {
        TOSEntities ent = new TOSEntities();
        public DataTable GetInformation(M_CompanyModel companymodel)
        {
            BaseDL bdl = new BaseDL();
            DataTable dtinfo = new DataTable();
            SqlParameter[] prms = new SqlParameter[1];
            prms[0] = new SqlParameter("@companyCD", SqlDbType.VarChar) { Value = companymodel.UserID};
            dtinfo = bdl.SelectData("T_Information_Select", prms);
            return dtinfo;
        }
    }
}
