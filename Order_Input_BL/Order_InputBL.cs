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
        //public DataTable ShippingName_Select(string CompanyName)
        //{
        //    DataTable dt = new DataTable();
        //    BaseDL bl = new BaseDL();
        //    if ( CompanyName!= null)
        //    {

        //        SqlParameter[] prms = new SqlParameter[1];
        //        prms[0] = new SqlParameter("@companyname", SqlDbType.VarChar) { Value = CompanyName };
        //        dt = bl.SelectData("OrderInput_ShippingName_Select", prms);
        //    }
        //    return dt;
        //}

        //public List<string> ShippingName_Select(M_CompanyShippingModel mcs)
        //{
        //    TOSEntities ent = new TOSEntities();

        //    M_CompanyShipping ms = ent.M_CompanyShipping.Where(m => m.CompanyCD == mcs.CompanyCD).AsEnumerable();
        //    var ms = ent.M_CompanyShipping.Where(m => m.CompanyCD == mcs.CompanyCD).ToList();
        //    var shipname = (from mshipping in ent.M_CompanyShipping
        //                    where mshipping.CompanyCD == mcs.CompanyCD
        //                    select new { mshipping.ShippingName });


        //    List<string> d = new List<string>();
        //    //shipname.ToList();
        //    //if (shipname != null)
        //    //{
        //    //    mcs.ShippingID = shipname.ToList();
        //    //    mcs.ShippingName = ms.ShippingName;
        //    //}
        //    return d;
        //}


    }
}
