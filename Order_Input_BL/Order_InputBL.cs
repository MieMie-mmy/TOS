using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TOS_DL;
using TOS_Model;
using System.Data.SqlClient;

namespace Order_Input_BL
{
    public class Order_InputBL
    {
        public M_JobTimeableModel JobTimeTable_Select(M_JobTimeableModel Mjt)
        {
            TOSEntities ent = new TOSEntities();
            M_JobTimeable mj = ent.M_JobTimeable.Where(m => m.CompanyCD == Mjt.CompanyCD).SingleOrDefault();
            if (mj != null)
            {
                Mjt.CompanyCD = mj.CompanyCD;
                Mjt.OrderAblePeriod = mj.OrderAblePeriod;
                Mjt.CancelAblePeriod = mj.CancelAblePeriod;
                Mjt.ExceptionAboutPeriod = mj.ExceptionAboutPeriod;
            }
            return Mjt  ;
        }
        public DataTable ShippingName_Select(string CompanyCD)
        {
            DataTable dt = new DataTable();
            BaseDL dl = new BaseDL();
            if (!string.IsNullOrWhiteSpace(CompanyCD))
            {
                SqlParameter[] prms = new SqlParameter[1];
                prms[0] = new SqlParameter("@companyCD", SqlDbType.VarChar) { Value = CompanyCD };
                dt = dl.SelectData("OrderInput_ShippingName_Select", prms);
            }
            return dt;
        }
    }
}
