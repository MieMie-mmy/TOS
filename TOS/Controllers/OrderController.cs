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

namespace TOS.Controllers
{

    public class OrderController : Controller
    {

        // GET: Order_History
        public ActionResult Order_History()
        {
            return View();
        }

        public ActionResult Order_Input()
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
            return View();
        }
    }
}