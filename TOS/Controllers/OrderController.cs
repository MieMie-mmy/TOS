using System;
using System.Collections.Generic;
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
            return View();
        }

        public ActionResult Order_Portal()
        {
            return View();
        }
    }
}