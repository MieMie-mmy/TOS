using Information_BL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOS_Model;


namespace TOS.Controllers
{
    public class DetailInformationController : Controller
    {
        // GET: DetailInformation
        
        public ActionResult Index()
        {
            if (Session["CompanyCD"] != null)
            {
                Get_Detail_Information();
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
           
        }

        [HttpGet]
        public string Get_Detail_Information()
        {
            M_CompanyModel mc = new M_CompanyModel();
            mc.CompanyCD = Session["CompanyCD"].ToString();
            InformationBL ibl = new InformationBL();
            return DataTableToJSONWithJSONNet(ibl.GetInformation(mc));
        }

        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }
        

    }
}
