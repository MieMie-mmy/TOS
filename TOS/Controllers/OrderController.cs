using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOS_Model;
using Order_Input_BL;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Order_Portal_BL;
using Order_History_BL;
using System.IO;
using Group_Entry_BL;
using Base_BL;
using FastMember;

namespace TOS.Controllers
{

    
    public class OrderController : Controller
    {
        Order_HistoryBL bl = new Order_HistoryBL();
        DataTable dt = new DataTable();
        BaseBL bbl = new BaseBL();

        // GET: Order_History
        public ActionResult Order_History(string id)
        {

            ViewBag.InputOrderID = id;
            //ViewBag.InputOrderID = "J200110103754";
            return View();
        }
        [HttpPost]
        public string OH_GetFirstTable(string id)
        {
           
            //var OrderID = "101";//Get ID from Order Input
            dt = bl._SelectOrder(id);
            var Jsondata = JsonConvert.SerializeObject(dt);
            return Jsondata;
        }
       
        [HttpPost]
        public string OH_GetSecondTable(T_OrderHistorySearch data)
        {
            var companyCD = Session["CompanyCD"].ToString();
            dt = bl._SelectOrderDetail(data, companyCD);
            var Jsondata = JsonConvert.SerializeObject(dt);
            return Jsondata;
           
        }

        [HttpPost]
        public string Delete_OderHistoryDetailRow(string id)
        {
            var message = string.Empty;
            if(id==null)
            {
                message = "NOK";
            }
            else
            {
                var company = Session["CompanyCD"].ToString();
                string[] del_arr;
                var del_arr_o = "";
                var del_arr_a = "";
                
                del_arr = id.Split(',');

                    for (var i = 0; i < del_arr.Length; i++)
                    {
                        del_arr_o += (del_arr[i].Split('_'))[0] + ',';
                        del_arr_a += (del_arr[i].Split('_'))[1] + ',';
                    }
                
               
               
                var AccessPC = System.Environment.MachineName;

                message = bl._DeleteCheckedRow(company, del_arr_a.TrimEnd(','), del_arr_o.TrimEnd(','), AccessPC);

            }

            return JsonConvert.SerializeObject( message);
        }

       
        public FileStreamResult ExportReport(string id)
        {
           
            DataSet ds = new DataSet();

            string savedFileName = "OrderHistory_" + (DateTime.Now).ToShortDateString() + ".pdf";
            var OrderID = id;
           
            ds = bl._GetReportData(OrderID);
            Report.Order_History_Report ohrpt = new Report.Order_History_Report();
            ohrpt.Database.Tables["OH_Body"].SetDataSource(ds.Tables[1]);
            ohrpt.Database.Tables["OH_Header"].SetDataSource(ds.Tables[0]);


            Stream str = ohrpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            str.Seek(0, SeekOrigin.Begin);

            return File(str, "application/pdf");
          

        }

        [HttpPost]
       public string Check_MakerItemCD(T_OrderHistorySearch model)
        {
            string result = bl._CheckMakerItemCD(model);
            return JsonConvert.SerializeObject(result);
        }

        [HttpPost]
        public string OrderHistoryMessage(string id)
        {
            string msg =bbl._MessageDialog(id);
                    
            return JsonConvert.SerializeObject(msg);
        }

        public ActionResult Order_Input(string id)
        {
            Order_InputBL obl = new Order_InputBL();
            M_JobTimeableModel Mjob = new M_JobTimeableModel();
            DataSet dst = new DataSet();
            //  string ItemCD = "cps-test,cd";
            dst = obl.Order_Input_M_Item_Data(id);
            DataSet dsnew = new DataSet();
            String Img_Name = "";
            if (dst.Tables.Count > 0)
            {
                int tabcount = 0;
                int count = dst.Tables.Count;
                for (int i = 0; i < count; i++)
                {
                    if (dst.Tables[i].Rows.Count == 0)
                    {
                        dst.Tables.RemoveAt(i);
                        count--;
                        i--;
                    }
                    else
                    {
                        DataTable dtnew = dst.Tables[i];
                        Img_Name += dtnew.Rows[0]["ImageName"].ToString() +",";
                        dtnew.Columns.Remove("ImageName");
                        dtnew.TableName = "Table" + tabcount;
                        tabcount++;
                        //dsnew.Tables.Add(dst.Tables[i].Copy());
                        dsnew.Tables.Add(dtnew.Copy());
                    }
                }
            }
            if (!string.IsNullOrWhiteSpace(Img_Name) && Img_Name.Contains(","))
            {
                Img_Name = Img_Name.Remove(Img_Name.Length - 1);
            }
            ViewBag.ImageName = Img_Name;
            Session["MakerItem"] = id;
            ViewBag.count = dsnew.Tables.Count;
            Session["dtsmitem"] = dsnew;
            if (Session["CompanyCD"] != null)
            {
                Mjob.CompanyCD = Session["CompanyCD"].ToString();
                ViewData["JobTime"] = obl.JobTimeTable_Select(Mjob);
                return View();
            }
            else
            {
                ViewData["JobTime"] = Mjob;
                return View();
            }

        }

        [HttpGet]
        public string ShippingName_Select()
        {
            DataTable dt = new DataTable();
            Order_InputBL oib = new Order_InputBL();
            string jsonresult;
            if (Session["CompanyCD"] != null)
            {
                string CompanyCD = Session["CompanyCD"].ToString();
                dt = oib.ShippingName_Select(CompanyCD);
            }

            jsonresult = JsonConvert.SerializeObject(dt);
            return jsonresult;
        }

        public string Order_Input_M_Item_Select(string id)
        {
            DataSet ds = new DataSet();
            ds = Session["dtsmitem"] as DataSet;
            if (ds.Tables.Count > Convert.ToInt32(id))
            {
                string jsonresult;
                jsonresult = JsonConvert.SerializeObject(ds.Tables["Table" + id]);
                return jsonresult;
            }
            else
            {
                string jsonresult;
                DataTable dt = new DataTable();
                dt.Columns.Add("MakerItemCD");
                dt.Columns.Add("ItemName");
                dt.Columns.Add("BrandName");
                dt.Columns.Add("ListPrice(InTax)");
                dt.Columns.Add("SalePrice(InTax)");
                dt.Columns.Add("Lot");
                jsonresult = JsonConvert.SerializeObject(dt);
                return jsonresult;
            }
            //  }

        }

        public string Order_Input_M_SKU_Select(string id)
        {
            //String MakerItem = Session["MakerItem"] as string;
            DataSet dsk = new DataSet();
            dsk = Session["dtsmitem"] as DataSet;
            Order_InputBL oib = new Order_InputBL();
            DataTable dt = new DataTable();
            //string[] itemcd = MakerItem.Split(',');
            int i = Convert.ToInt32(id);
            if (dsk.Tables.Count > i && dsk.Tables["Table" + id].Rows.Count > 0)
            {
                DataTable dttemp = dsk.Tables["Table" + id].Copy();
                string mcd = dttemp.Rows[0]["MakerItemCD"].ToString(); //itemcd[i].ToString();
                dt = oib.Order_Input_M_SKU(mcd);
                if (dt.Rows.Count > 0)
                {
                    string jsonresult;
                    jsonresult = JsonConvert.SerializeObject(dt);
                    return jsonresult;
                }

            }
            return null;

        }


        [HttpPost]
        public ActionResult InserOrder(T_OrderHeaderModel T_Orderheader, List<T_OrderDetailModel> T_OrderDetail)
        {

            Order_InputBL oib = new Order_InputBL();
            DataTable dtorderdetail = new DataTable();
            using (var reader = ObjectReader.Create(T_OrderDetail, "OrderID", "AdminCD", "OrderItem", "StockItem", "SalePrice", "TotalAmount", "Memo", "AvailableShippingDate"))
            {
                dtorderdetail.Load(reader);
            }
            if (dtorderdetail.Rows.Count > 0)
            {
                if (Session["CompanyCD"] != null)
                {
                    string CompanyCD = Session["CompanyCD"].ToString();
                    T_Orderheader.UpdateOperator = CompanyCD;
                }
                T_Orderheader.AccessPC = System.Environment.MachineName;

                if (oib.Order_Input_Insert(T_Orderheader, dtorderdetail))
                {
                    Session["Error"] = null;
                    return Json(new {orderid = T_Orderheader.OrderID},JsonRequestBehavior.AllowGet);                   //return View();//RedirectToAction("../Order/Order_History/" + T_Orderheader.OrderID);
                }
                else
                {
                    Session["Error"] = "Error";
                    return Json(new { msg = "Error" });
                }
            }
            else
            {
                Session["Error"] = "Error";
                return Json(new { msg = "Error" });
            }

        }


        public ActionResult Order_Portal()
        {
            M_JobTimeableModel Mjob = new M_JobTimeableModel();
            if (Session["CompanyCD"] != null)
            {
                Order_InputBL obl = new Order_InputBL();
                Mjob.CompanyCD = Session["CompanyCD"].ToString();
                ViewData["JobTime"] = obl.JobTimeTable_Select(Mjob);

                return View();
            }
            else
            {
                ViewData["JobTime"] = Mjob;
                return View();
            }
        }
        [HttpGet]
        public string Get_Order_Portal_List()
        {
            DataTable dt = new DataTable();
            string jsonresult;
            if (Session["CompanyCD"] != null)
            {
                Order_PortalBL opbl = new Order_PortalBL();
                string companyCD = Session["CompanyCD"].ToString();
                dt = opbl.Order_Portal_List_Select(companyCD);
            }
            jsonresult = DataTableToJSONWithJSONNet(dt);
            return jsonresult;
        }

        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }

        [HttpGet]
        public JsonResult GetMessage()
        {
            string msg = "NoData";
            TempData["Nmsg"] = "NoData";
            return Json(msg, JsonRequestBehavior.AllowGet);
        }

    }
}