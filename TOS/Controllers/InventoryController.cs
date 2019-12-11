using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TOS.Controllers
{
    public class InventoryController : Controller
    {
        // GET: Inventory
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Inventories()
        {
            return View();
        }

        public ActionResult Inventories_list()
        {
            return View();
        }
    }
}