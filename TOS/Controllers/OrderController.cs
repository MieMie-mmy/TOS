using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOS_Model;
using Order_Input_BL;
using Order_History_BL;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Web.UI.WebControls;


namespace TOS.Controllers
{
   
    public class OrderController : Controller
    {
        DataTable dt = new DataTable();
        // GET: Order_History
        Order_HistoryBL bl = new Order_HistoryBL();
        public ActionResult Order_History()
        {
            var OrderID = "100";
            dt = bl._SelectOrder(OrderID);
           
            return View(dt);
        }
        
        //public JsonResult GetOrderHistory()
        //{
           
        //}

        [HttpPost]
        public DataTable GetOrderHistoryDetail(T_OrderHistorySearch data)
        {
          
            dt = bl._SelectOrderDetail(data);
           
            return dt;
        }

        [HttpPost]
        public ActionResult Delete_OderHistoryDetailRow(string id)
        {
            var company = Session["CompanyCD"].ToString();
            string[] del_arr;
           
            del_arr = id.Split(',');
            var del_arr_o = "";
            var del_arr_a = "";
            for(var i=0;i<del_arr.Length;i++)
            {
                del_arr_o += (del_arr[i].Split('_'))[0] +',';
                del_arr_a += (del_arr[i].Split('_'))[1] +',';
            }
            var message = bl._DeleteCheckedRow(id,company, del_arr_a.TrimEnd(','),del_arr_o.TrimEnd(','));

            return RedirectToAction("Order_History");
        }

        public ActionResult ExportReport ()
        {
            DataSet ds = new DataSet();
           
            string savedFileName = "OrderHistory_" +(DateTime.Now).ToShortDateString()+".pdf";
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
       
       
        public ActionResult Order_Input()
        {
            Order_InputBL obl = new Order_InputBL();
            M_JobTimeableModel Mjob = new M_JobTimeableModel();
            Mjob.CompanyCD = Session["CompanyCD"].ToString(); ;
            ViewData["JobTime"] = obl.JobTimeTable_Select(Mjob);
            return View();
        }

        public ActionResult Order_Portal()
        {   
            return View();
        }
      
    }
}