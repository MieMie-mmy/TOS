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
using System.Transactions;


namespace Order_Input_BL
{
    public class Order_InputBL
    {
        public M_JobTimeableModel JobTimeTable_Select(M_JobTimeableModel Mjt)
        {
            TOSEntities ent = new TOSEntities();
            //M_JobTimeable mj = ent.M_JobTimeable.Where(m => m.CompanyCD == Mjt.CompanyCD).SingleOrDefault();
            M_JobTimeable mj = ent.M_JobTimeable.SingleOrDefault();
            if (mj != null)
            {
                Mjt.CompanyCD = mj.CompanyCD;
                Mjt.OrderAblePeriod = mj.OrderAblePeriod;
                Mjt.CancelAblePeriod = mj.CancelAblePeriod;
                Mjt.ExceptionAboutPeriod = mj.ExceptionAboutPeriod;
            }
            return Mjt  ;
        }
        public DataTable ShippingName_Select(string CompanyCD,int ShippingID)
        {
            DataTable dt = new DataTable();
            BaseDL dl = new BaseDL();
            if (!string.IsNullOrWhiteSpace(CompanyCD))
            {
                SqlParameter[] prms = new SqlParameter[2];
                if (CompanyCD == null)
                {
                    prms[0] = new SqlParameter("@companyCD", SqlDbType.VarChar) { Value = DBNull.Value };
                }
                else
                {
                    prms[0] = new SqlParameter("@companyCD", SqlDbType.VarChar) { Value = CompanyCD };
                }
                if (CompanyCD == null)
                {
                    prms[1] = new SqlParameter("@ShippingID", SqlDbType.VarChar) { Value = DBNull.Value };
                }
                else
                {
                    prms[1] = new SqlParameter("@ShippingID", SqlDbType.VarChar) { Value = ShippingID };
                }
                //prms[0] = new SqlParameter("@companyCD", SqlDbType.VarChar) { Value = CompanyCD };
                //prms[1] = new SqlParameter("@ShippingID", SqlDbType.Int) { Value = ShippingID };
                dt = dl.SelectData("OrderInput_ShippingName_Select", prms);
            }
            return dt;
        }

        public DataSet Order_Input_M_Item_Data(string MakerItemCD)
        {
            DataSet ds = new DataSet();
            BaseDL dl = new BaseDL();
            
            if (!string.IsNullOrWhiteSpace(MakerItemCD))
            {
                string[] itemcd = MakerItemCD.Split(',');
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
          
            return ds;
            
        }

        public DataTable Order_Input_M_SKU(string makeritemCD)
        {
            DataTable dt = new DataTable();
            BaseDL dl = new BaseDL();
            if (!string.IsNullOrWhiteSpace(makeritemCD))
            {
                SqlParameter[] prms = new SqlParameter[1];
                prms[0] = new SqlParameter("@MakerItemCD", SqlDbType.VarChar) { Value = makeritemCD };
                dt = dl.SelectData("OrderInput_M_SKU_Select", prms);
            }
            return dt;
        }

        public Boolean Order_Input_Insert(T_OrderHeaderModel toh, DataTable dtorder)
        {

            try
            {
                BaseDL dl = new BaseDL();
                SqlParameter[] prms = new SqlParameter[13];

                prms[0] = new SqlParameter("@OrderID", SqlDbType.VarChar) { Value = toh.OrderID };
                prms[1] = new SqlParameter("@ShippingID", SqlDbType.VarChar) { Value = toh.ShippingID };

                if (toh.ShippingName == null)
                {
                    prms[2] = new SqlParameter("@ShippingName", SqlDbType.VarChar) { Value = DBNull.Value };
                }
                else
                {
                    prms[2] = new SqlParameter("@ShippingName", SqlDbType.VarChar) { Value = toh.ShippingName };
                }
                if (toh.ZipCD1 == null)
                {
                    prms[3] = new SqlParameter("@ZipCD1", SqlDbType.VarChar) { Value = DBNull.Value };
                }
                else
                {
                    prms[3] = new SqlParameter("@ZipCD1", SqlDbType.VarChar) { Value = toh.ZipCD1 };
                }
                if (toh.ZipCD2 == null)
                {
                    prms[4] = new SqlParameter("@ZipCD2", SqlDbType.VarChar) { Value = DBNull.Value};
                }
                else
                {
                    prms[4] = new SqlParameter("@ZipCD2", SqlDbType.VarChar) { Value = toh.ZipCD2 };
                }
                if (toh.Address1 == null)
                {
                    prms[5] = new SqlParameter("@Address1", SqlDbType.VarChar) { Value = DBNull.Value };
                }
                else
                {
                    prms[5] = new SqlParameter("@Address1", SqlDbType.VarChar) { Value = toh.Address1 };
                }
                if (toh.Address2 == null)
                {
                    prms[6] = new SqlParameter("@Address2", SqlDbType.VarChar) { Value = DBNull.Value };
                }
                else
                {
                    prms[6] = new SqlParameter("@Address2", SqlDbType.VarChar) { Value = toh.Address2 };
                }
                if(toh.TelephoneNO == null)
                {
                    prms[7] = new SqlParameter("@PhoneNo", SqlDbType.VarChar) { Value = DBNull.Value };
                }
                else
                {
                    prms[7] = new SqlParameter("@PhoneNo", SqlDbType.VarChar) { Value = toh.TelephoneNO };
                }

                    prms[8] = new SqlParameter("@TotalAmount", SqlDbType.VarChar) { Value = toh.TotalAmount };
                if (toh.Memo == null)
                {
                    prms[9] = new SqlParameter("@Memo", SqlDbType.VarChar) { Value = DBNull.Value };
                }
                else
                {
                    prms[9] = new SqlParameter("@Memo", SqlDbType.VarChar) { Value = toh.Memo };
                }
                prms[10] = new SqlParameter("@UpdateOperator", SqlDbType.VarChar) { Value = toh.UpdateOperator };
                prms[11] = new SqlParameter("@AccessPC", SqlDbType.VarChar) { Value = toh.AccessPC };
                dtorder.TableName = "order";
                System.IO.StringWriter writer = new System.IO.StringWriter();
                dtorder.WriteXml(writer, XmlWriteMode.WriteSchema, false);
                string result = writer.ToString();
                prms[12] = new SqlParameter("@xml", SqlDbType.Xml) { Value = result };
                var option = new TransactionOptions
                {
                    IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,
                    Timeout = TimeSpan.MaxValue
                };
                using (TransactionScope scopt = new TransactionScope(TransactionScopeOption.Required, option))
                {
                    dl.InsertUpdateDeleteData("Order_Input_Insert", prms);               
                    scopt.Complete();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
