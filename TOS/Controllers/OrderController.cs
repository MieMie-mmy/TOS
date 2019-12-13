using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOS_Model;
using Order_History_BL;


namespace TOS.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order_History
        public ActionResult Order_History()
        {
            T_OrderHistoryModel general_ordertable1 = new T_OrderHistoryModel();
            Order_HistoryBL order_bl = new Order_HistoryBL();
            general_ordertable1 = order_bl._SelectOrder();
           
            return View(general_ordertable1);
        }

        public ActionResult Order_Input()
        {
            M_JobTimeableModel Mjob = new M_JobTimeableModel();
            Mjob.CompanyCD = Session["CompanyCD"].ToString(); ;
           // ViewData["JobTime"] = 
            return View();
        }

        public ActionResult Order_Portal()
        {   
            return View();
        }
    }
}