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

        public  DataTable _SelectOrder(string OrderID)
        {
             T_OrderHistoryModel general_ordertable1 = new T_OrderHistoryModel();
            SqlParameter prm = new SqlParameter("@OrderID", SqlDbType.VarChar) { Value = OrderID };
            
            dt = dl.SelectData("Order_History_Select", prm);
           
            return dt;
        }
        public DataTable _SelectOrderDetail(T_OrderHistorySearch data)
        {                      
            SqlParameter[] prms = new SqlParameter[14];
           
            prms[0] = new SqlParameter("@company", SqlDbType.VarChar) { Value = DBNull.Value };

            if (data.id==null)
            {
                prms[1] = new SqlParameter("@id", SqlDbType.VarChar) { Value =DBNull.Value };
            }
            else {
                prms[1] = new SqlParameter("@id", SqlDbType.VarChar) { Value = data.id };
            }
           if(data.o_f==null)
            {
                prms[2] = new SqlParameter("@of", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            else
            {
                prms[2] = new SqlParameter("@of", SqlDbType.VarChar) { Value = Convert.ToDateTime(data.o_f) };
            }
           if(data.o_t==null)
            {
                prms[3] = new SqlParameter("@ot", SqlDbType.VarChar) { Value = DBNull.Value };
            }
           else
            {
                prms[3] = new SqlParameter("@ot", SqlDbType.VarChar) { Value = Convert.ToDateTime(data.o_t) };
            }
          if(data.s_f==null)
            {
                prms[4] = new SqlParameter("@sf", SqlDbType.VarChar) { Value = DBNull.Value };
            }
          else
            {
                prms[4] = new SqlParameter("@sf", SqlDbType.VarChar) { Value = Convert.ToDateTime(data.s_f) };
            }
           
           if(data.s_t==null)
            {
                prms[5] = new SqlParameter("@st", SqlDbType.VarChar) { Value = DBNull.Value };
            }
           else
            {
                prms[5] = new SqlParameter("@st", SqlDbType.VarChar) { Value = Convert.ToDateTime(data.s_t) };
            }
           if(data.m1==null)
            {
                prms[6] = new SqlParameter("@m1", SqlDbType.VarChar) { Value = DBNull.Value };
            }
           else
            {
                prms[6] = new SqlParameter("@m1", SqlDbType.VarChar) { Value = data.m1 };
            }
           if(data.m2==null)
            {
                prms[7] = new SqlParameter("@m2", SqlDbType.VarChar) { Value =DBNull.Value };
            }
           else
            {
                prms[7] = new SqlParameter("@m2", SqlDbType.VarChar) { Value = data.m2 };
            }
           if(data.m3==null)
            {
                prms[8] = new SqlParameter("@m3", SqlDbType.VarChar) { Value = DBNull.Value };
            }
           else
            {
                prms[8] = new SqlParameter("@m3", SqlDbType.VarChar) { Value = data.m3 };
            }
           if(data.m4==null)
            {
                prms[9] = new SqlParameter("@m4", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            else
            {
                prms[9] = new SqlParameter("@m4", SqlDbType.VarChar) { Value = data.m4 };
            }
          if(data.m5==null)
            {
                prms[10] = new SqlParameter("@m5", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            else
            {
                prms[10] = new SqlParameter("@m5", SqlDbType.VarChar) { Value = data.m5 };
            }
            if(data.m6==null)
            {
                prms[11] = new SqlParameter("@m6", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            else
            {
                prms[11] = new SqlParameter("@m6", SqlDbType.VarChar) { Value = data.m6 };
            }
            if(data.m7==null)
            {
                prms[12] = new SqlParameter("@m7", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            else
            {
                prms[12] = new SqlParameter("@m7", SqlDbType.VarChar) { Value = data.m7 };
            }
            if(data.m8==null)
            {
                prms[13] = new SqlParameter("@m8", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            else
            {
                prms[13] = new SqlParameter("@m8", SqlDbType.VarChar) { Value = data.m8 };
            }

            
                dt = dl.SelectData("Order_History_SelectSearch", prms);

            return dt;
        }
        public string _DeleteCheckedRow(string id,string company,string del_arr_a,string del_arr_o)
        {

            //SqlParameter[] prms = new SqlParameter[15];


            //prms[0] = new SqlParameter("@company", SqlDbType.VarChar) { Value = company };
            //prms[1] = new SqlParameter("@id", SqlDbType.VarChar) { Value = id };
            //prms[2] = new SqlParameter("@of", SqlDbType.VarChar) { Value = DBNull.Value };
            //prms[3] = new SqlParameter("@ot", SqlDbType.VarChar) { Value = DBNull.Value };
            //prms[4] = new SqlParameter("@sf", SqlDbType.VarChar) { Value = DBNull.Value };
            //prms[5] = new SqlParameter("@st", SqlDbType.VarChar) { Value = DBNull.Value };
            //prms[6] = new SqlParameter("@m1", SqlDbType.VarChar) { Value = DBNull.Value };
            //prms[7] = new SqlParameter("@m2", SqlDbType.VarChar) { Value = DBNull.Value };
            //prms[8] = new SqlParameter("@m3", SqlDbType.VarChar) { Value = DBNull.Value };
            //prms[9] = new SqlParameter("@m4", SqlDbType.VarChar) { Value = DBNull.Value };
            //prms[10] = new SqlParameter("@m5", SqlDbType.VarChar) { Value = DBNull.Value };
            //prms[11] = new SqlParameter("@m6", SqlDbType.VarChar) { Value = DBNull.Value };
            //prms[12] = new SqlParameter("@m7", SqlDbType.VarChar) { Value = DBNull.Value };
            //prms[13] = new SqlParameter("@m8", SqlDbType.VarChar) { Value = DBNull.Value };

            //dl.SelectData("Order_History_Delete", prms);

            var message = "OK";
            return message;
        }

        public DataSet _GetReportData(string OrderID)
        {
            DataSet ds = new DataSet();
            SqlParameter prm = new SqlParameter();
            prm = new SqlParameter("@OrderID", SqlDbType.VarChar) { Value = OrderID };
            ds = dl.SelectDataSet("Order_History_Report", prm);
            return ds;

        }

    }
}
