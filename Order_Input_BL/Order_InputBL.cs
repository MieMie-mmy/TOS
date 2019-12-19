using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TOS_DL;
using TOS_Model;
using System.Data.SqlClient;
using System.Collections;
using Newtonsoft.Json;


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

        public DataSet Order_Input_M_Item_Data(string MakerItemCD)
        {
            DataSet ds = new DataSet();
            BaseDL dl = new BaseDL();
            string[] itemcd = MakerItemCD.Split(',');
            string json_dataset;
            if (!string.IsNullOrWhiteSpace(MakerItemCD))
            {
                SqlParameter[] prms = new SqlParameter[9];
                for (int i = 0; i < itemcd.Count(); i++)
                {
                    prms[i] = new SqlParameter("@MakerItemCD"+i, SqlDbType.VarChar) { Value = itemcd[i].ToString() };
                }
                for (int i = itemcd.Count(); i < 8; i++)
                {
                    prms[i] = new SqlParameter("@MakerItemCD" + i, SqlDbType.VarChar) { Value = "" };
                }
                prms[8] = new SqlParameter("@CompanyCD", SqlDbType.VarChar) { Value = "1001" };
                ds = dl.SelectDataSet("OrderInput_M_Item_Select", prms);
                
            }
            //json_dataset = JsonConvert.SerializeObject(ds, Formatting.Indented);

            //return json_dataset;
            return ds;
            
        }

        //public string GetJSON(DataSet ds)
        //{

        //    //.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        //    //
        //    ArrayList root = new ArrayList();
        //    List<Dictionary<string, object>> table;
        //    Dictionary<string, object> data;

        //    foreach (DataTable dt in ds.Tables)
        //    {
        //        table = new List<Dictionary<string, object>>();
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            data = new Dictionary<string, object>();
        //            foreach (DataColumn col in dt.Columns)
        //            {
        //                data.Add(col.ColumnName, dr[col]);
        //            }
        //            table.Add(data);
        //        }
        //        root.Add(table);
        //    }

        //    return JsonConvert.SerializeObject(root);
        //}


        }
}
