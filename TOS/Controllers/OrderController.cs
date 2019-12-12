using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOS_Model;

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
            M_JobTimeableModel Mjob = new M_JobTimeableModel();
            Mjob.CompanyCD = Session["CompanyCD"].ToString(); ;
            ViewData["JobTime"] = 
            return View();
        }

        public ActionResult Order_Portal()
        {   
            return View();
        }
    }
}