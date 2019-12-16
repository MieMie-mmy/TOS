using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOS_Model;

namespace TOS.Controllers
{
    public class InventoriesController : Controller
    {
        // GET: Inventories
        public ActionResult Index()
        {
           
         
            return View();
        }
        public ActionResult inventories()
        {
            T_InventoriesModel brand = new T_InventoriesModel();
            brand.BrandName = "";

            return View(brand);

        }

        
        

        
    }
}