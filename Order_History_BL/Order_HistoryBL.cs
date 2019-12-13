using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOS_Model;
using TOS_DL;
using System.Data;
using System.Data.SqlClient;

namespace Order_History_BL
{
    public class Order_HistoryBL
    {
        BaseDL dl = new BaseDL();
        DataTable dt = new DataTable();

        public T_OrderHistoryModel _SelectOrder()
        {
            T_OrderHistoryModel general_ordertable1 = new T_OrderHistoryModel();
            var option = 1;
            SqlParameter prms = new SqlParameter("@option", SqlDbType.VarChar) { Value = option };
            dt = dl.SelectData("Order_History_Select", prms);
            if(dt.Rows.Count>0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    general_ordertable1.OrderID = dt.Rows[i]["OrderID"].ToString();
                    general_ordertable1.TotalAmount = Convert.ToDecimal(dt.Rows[i]["TotalAmount"].ToString());
                    general_ordertable1.ShippingName = dt.Rows[i]["ShippingName"].ToString();
                    general_ordertable1.ShippingDate = Convert.ToDateTime(dt.Rows[i]["ShippingDate"].ToString());
                    general_ordertable1.AvailableShippingDate = Convert.ToDateTime(dt.Rows[i]["AvailableShippingDate"].ToString());
                    general_ordertable1.OrderDateTime = Convert.ToDateTime(dt.Rows[i]["OrderDateTime"].ToString());


                }
            }
            
            //if(general_ordertable1.TotalAmount !=null)
            //{
            //    general_ordertable1.TotalAmount = null;
            //}
            //if (general_ordertable1.ShippingDate != null)
            //{
            //    general_ordertable1.ShippingDate = null;
            //}
            //if (general_ordertable1.AvailableShippingDate != null)
            //{
            //    general_ordertable1.AvailableShippingDate = null;
            //}
            //if (general_ordertable1.OrderDateTime != null)
            //{
            //    general_ordertable1.OrderDateTime = null;
            //}
            return general_ordertable1;
        }
    }
}
