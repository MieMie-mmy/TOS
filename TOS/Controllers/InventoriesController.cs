using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOS_Model;
using System.Data;
using Newtonsoft.Json;
using System.Data.SqlClient;


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
            //T_InventoriesModel brand = new T_InventoriesModel();
            //brand.BrandName = "";

            return View();

        }

        [HttpGet]
        public string BrandName_Select()
        {
            DataTable dt = new DataTable();
            string jsonresult;
            jsonresult = JsonConvert.SerializeObject(dt);
            return jsonresult;
        }
        



    }
}