using Order_Portal_BL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
            string CompanyCD = Session["CompanyCD"].ToString();
           // ViewData["JobTime"] = 
            return View();
        }

        public ActionResult Order_Portal()
        {
            //string CompanyCD = Session["CompanyCD"].ToString();
            return View();
        }
        public ActionResult Get_Order_Portal_List()
        {
            Order_PortalBL opbl = new Order_PortalBL();
            DataTable dtinfo = opbl.Order_Portal_List_Select();
            return View(dtinfo);
        }
    }
}