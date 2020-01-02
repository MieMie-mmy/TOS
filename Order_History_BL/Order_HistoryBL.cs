using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOS_Model;
using TOS_DL;
using System.Data;
using System.Data.SqlClient;
using TOS_Model;

namespace Order_History_BL
{
    public class Order_HistoryBL
    {
        BaseDL dl = new BaseDL();
        DataTable dt = new DataTable();

        public DataTable _SelectOrder(string id)
        {
            SqlParameter prm = new SqlParameter();
            if (id == null)
            {
                prm = new SqlParameter("@OrderID", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            else
            {
                prm = new SqlParameter("@OrderID", SqlDbType.VarChar) { Value = id };
            }



            dt = dl.SelectData("Order_History_Select", prm);

            return dt;
        }
        public DataTable _SelectOrderDetail(T_OrderHistorySearch data)
        {
            SqlParameter[] prms = new SqlParameter[14];

            prms[0] = new SqlParameter("@company", SqlDbType.VarChar) { Value = DBNull.Value };

            if (data.id == null)
            {
                prms[1] = new SqlParameter("@id", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            else {
                prms[1] = new SqlParameter("@id", SqlDbType.VarChar) { Value = (data.id).Trim() };
            }
            if (data.o_f == null)
            {
                prms[2] = new SqlParameter("@of", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            else
            {
                prms[2] = new SqlParameter("@of", SqlDbType.VarChar) { Value = Convert.ToDateTime(data.o_f) };
            }
            if (data.o_t == null)
            {
                prms[3] = new SqlParameter("@ot", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            else
            {
                prms[3] = new SqlParameter("@ot", SqlDbType.VarChar) { Value = Convert.ToDateTime(data.o_t) };
            }
            if (data.s_f == null)
            {
                prms[4] = new SqlParameter("@sf", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            else
            {
                prms[4] = new SqlParameter("@sf", SqlDbType.VarChar) { Value = Convert.ToDateTime(data.s_f) };
            }

            if (data.s_t == null)
            {
                prms[5] = new SqlParameter("@st", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            else
            {
                prms[5] = new SqlParameter("@st", SqlDbType.VarChar) { Value = Convert.ToDateTime(data.s_t) };
            }
            if (data.m1 == null)
            {
                prms[6] = new SqlParameter("@m1", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            else
            {
                prms[6] = new SqlParameter("@m1", SqlDbType.VarChar) { Value = (data.m1).Trim() };
            }
            if (data.m2 == null)
            {
                prms[7] = new SqlParameter("@m2", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            else
            {
                prms[7] = new SqlParameter("@m2", SqlDbType.VarChar) { Value = (data.m2).Trim() };
            }
            if (data.m3 == null)
            {
                prms[8] = new SqlParameter("@m3", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            else
            {
                prms[8] = new SqlParameter("@m3", SqlDbType.VarChar) { Value = (data.m3).Trim() };
            }
            if (data.m4 == null)
            {
                prms[9] = new SqlParameter("@m4", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            else
            {
                prms[9] = new SqlParameter("@m4", SqlDbType.VarChar) { Value = (data.m4).Trim() };
            }
            if (data.m5 == null)
            {
                prms[10] = new SqlParameter("@m5", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            else
            {
                prms[10] = new SqlParameter("@m5", SqlDbType.VarChar) { Value = (data.m5).Trim() };
            }
            if (data.m6 == null)
            {
                prms[11] = new SqlParameter("@m6", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            else
            {
                prms[11] = new SqlParameter("@m6", SqlDbType.VarChar) { Value = (data.m6).Trim() };
            }
            if (data.m7 == null)
            {
                prms[12] = new SqlParameter("@m7", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            else
            {
                prms[12] = new SqlParameter("@m7", SqlDbType.VarChar) { Value = (data.m7).Trim() };
            }
            if (data.m8 == null)
            {
                prms[13] = new SqlParameter("@m8", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            else
            {
                prms[13] = new SqlParameter("@m8", SqlDbType.VarChar) { Value = (data.m8).Trim() };
            }


            dt = dl.SelectData("Order_History_SelectSearch", prms);

            return dt;
        }
        public string _DeleteCheckedRow(string company, string del_arr_a, string del_arr_o, string AccessPC)
        {

            SqlParameter[] prms = new SqlParameter[4];

            prms[0] = new SqlParameter("@company", SqlDbType.VarChar) { Value = company };
            prms[1] = new SqlParameter("@OrderID", SqlDbType.VarChar) { Value = del_arr_o };
            prms[2] = new SqlParameter("@AdminCD", SqlDbType.VarChar) { Value = del_arr_a };

            prms[3] = new SqlParameter("@AccessPC", SqlDbType.VarChar) { Value = AccessPC };


            dl.SelectData("Order_History_Delete", prms);

            var message = "OK";
            return message;
        }

        public DataSet _GetReportData(string OrderID)
        {
            DataSet ds = new DataSet();
            SqlParameter prm = new SqlParameter();
            if (OrderID == null)
            {
                prm = new SqlParameter("@OrderID", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            else
            {
                prm = new SqlParameter("@OrderID", SqlDbType.VarChar) { Value = OrderID };
            }

            ds = dl.SelectDataSet("Order_History_Report", prm);
            return ds;

        }

        public String _MessageDialog(string id)
        {
            var ID = string.Empty;
            var key = string.Empty;
            var msgType = string.Empty;
            if(id!=null)
            {
                var msgarr = id.Split('_');
                ID = msgarr[0];
                key = msgarr[1];
                msgType = msgarr[2];
                
            }
            SqlParameter[] prm = new SqlParameter[2];
            prm[0] = new SqlParameter("@key", SqlDbType.VarChar) { Value = key };
            prm[1] = new SqlParameter("@msgType", SqlDbType.VarChar) { Value=ID};
            dt = dl.SelectData("Message_Select", prm);
            var msg = dt.Rows[0][msgType].ToString();
            return msg;
        }

        public string _CheckMakerItemCD(T_OrderHistorySearch model)
        {
            TOSEntities entity = new TOSEntities();

            var result_micd = string.Empty;
            var result = 0;
            var micd = new string[8] {model.m1,model.m2,model.m3,model.m4,model.m5,model.m6,model.m7,model.m8 };
            for (var i = 0; i < micd.Length; i++)
            {
                var micdd = micd[i];
                result = entity.T_OrderDetail
                     .Join(
                           entity.T_OrderHeader,
                           d => d.OrderID,
                           h => h.OrderID,
                           (d, h) => new { d, h })
                     .Join(entity.M_SKU,
                           ud => ud.d.AdminCD,
                           sku => sku.AdminCD,
                           (ud, sku) => new { ud, sku }
                     ).Join(entity.M_Item,
                           usku => usku.sku.MakerItemAdminCD,
                           it => it.MakerItemAdminCD,
                           (usku, it) => new { usku, it }
                     ).Where(r => r.it.MakerItemCD != micdd).Select(s => s.it.MakerItemCD).Count();

                if (result > 0 && (micdd != null))
                {
                    result_micd += micdd + ",";
                }
            }
          
          
         
            return result_micd.TrimEnd(',') ;
        }

    }
}
