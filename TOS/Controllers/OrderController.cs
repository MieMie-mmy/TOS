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

namespace TOS.Controllers
{

    
    public class OrderController : Controller
    {
        Order_HistoryBL bl = new Order_HistoryBL();
        DataTable dt = new DataTable();

        // GET: Order_History
        public ActionResult Order_History()
        {
            return View();
        }
        [HttpPost]
        public string OH_GetFirstTable()
        {
           
            var OrderID = "101";//Get ID from Order Input
            dt = bl._SelectOrder(OrderID);
            var Jsondata = JsonConvert.SerializeObject(dt);
            return Jsondata;
        }
        [HttpPost]
        public string OH_GetSecondTable(T_OrderHistorySearch data)
        {

            dt = bl._SelectOrderDetail(data);
            var Jsondata = JsonConvert.SerializeObject(dt);
            return Jsondata;
           
        }

        [HttpPost]
        public ActionResult Delete_OderHistoryDetailRow(string id)
        {
            var company = Session["CompanyCD"].ToString();
            string[] del_arr;
            var del_arr_o = "";
            var del_arr_a = "";
            if(id==null)
            {

            }
          else
            {
                del_arr = id.Split(',');

                for (var i = 0; i < del_arr.Length; i++)
                {
                    del_arr_o += (del_arr[i].Split('_'))[0] + ',';
                    del_arr_a += (del_arr[i].Split('_'))[1] + ',';
                }
            }
           
            
            var message = bl._DeleteCheckedRow(company, del_arr_a.TrimEnd(','), del_arr_o.TrimEnd(','));

            return RedirectToAction("Order_History");
        }


        public ActionResult ExportReport()
        {
            DataSet ds = new DataSet();

            string savedFileName = "OrderHistory_" + (DateTime.Now).ToShortDateString() + ".pdf";
            var OrderID = "100";
            ds = bl._GetReportData(OrderID);
            Report.Order_History_Report ohrpt = new Report.Order_History_Report();
            ohrpt.Database.Tables["OH_Body"].SetDataSource(ds.Tables[1]);
            ohrpt.Database.Tables["OH_Header"].SetDataSource(ds.Tables[0]);
            Stream str = ohrpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            str.Seek(0, SeekOrigin.Begin);
            return File(str, "application/pdf", savedFileName);

        }

        public ActionResult Order_Input(string MakerItemCD)
        {
            Order_InputBL obl = new Order_InputBL();
            M_JobTimeableModel Mjob = new M_JobTimeableModel();
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
            
            Order_InputBL oib = new Order_InputBL();
           
            string ItemCD = "cps-test,BAQ005";
            DataSet dst = new DataSet();
            dst = oib.Order_Input_M_Item_Data(ItemCD);
            if (dst.Tables.Count > Convert.ToInt32(id))
            {
                string jsonresult;
                jsonresult = JsonConvert.SerializeObject(dst.Tables["Datalist"]);
                return jsonresult;
            }
            else
            {
                return null;
            }
           
        }


        public ActionResult Order_Portal()
        {
            if (Session["CompanyCD"] != null)
            {
                Order_InputBL obl = new Order_InputBL();
                M_JobTimeableModel Mjob = new M_JobTimeableModel();
                Mjob.CompanyCD = Session["CompanyCD"].ToString();
                ViewData["JobTime"] = obl.JobTimeTable_Select(Mjob);

                //Order_PortalBL opbl = new Order_PortalBL();
                //DataTable dtorderpotal = opbl.Order_Portal_List_Select();

                return View();
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public string Get_Order_Portal_List()
        {
            Order_PortalBL opbl = new Order_PortalBL();
            return DataTableToJSONWithJSONNet(opbl.Order_Portal_List_Select());
        }

        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }
    }
}