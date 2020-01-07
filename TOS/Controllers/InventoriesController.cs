using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOS_Model;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;
using Inventories_BL;

namespace TOS.Controllers
{
    public class InventoriesController : Controller
    {
        // GET: Inventories
        DataTable dt = new DataTable();
        InventoriesBL bl = new InventoriesBL();
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult inventories()
        {
            return View();
        }

        [HttpGet]
        public string BrandName_Select()
        {
            T_InventoriesModel brand = new T_InventoriesModel();
            brand.BrandName = "";
            DataTable dt = new DataTable();
            string jsonresult;
            InventoriesBL bl = new InventoriesBL();
            dt = bl.BrandName_Select();
            jsonresult = JsonConvert.SerializeObject(dt);
            return jsonresult;
        }
        [HttpPost]
        public string _InventorySearch(string id)

        {
            dt = bl.Inventory_Search(id);
            string jsonresult = JsonConvert.SerializeObject(dt);
            return jsonresult;
        }

    }
}