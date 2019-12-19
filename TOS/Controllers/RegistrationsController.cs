using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOS_Model;
using Company_Entry_BL;
using System.Data ;
using Information_BL;
using Group_Entry_BL;
using Newtonsoft.Json;

namespace TOS.Controllers
{
    public class RegistrationsController : Controller
    {
       
        // GET: Registrations
        public ActionResult Company_Entry()
        {
            
            if (Session["CompanyCD"] != null)
            {
                ViewBag.Message = "Welcome to my demo!";
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
        [HttpPost]
        public ActionResult InsertCompany(M_CompanyModel cm,M_CompanyShippingModel cmShip )
        {
            
             Company_EntryBL cbl = new Company_EntryBL();

            if (cm.ZipCD1 != null)
            {
                string zip1 = cm.ZipCD1.Substring(0, 3);
                string zip2 = cm.ZipCD1.Substring(3);
                cm.ZipCD1 = zip1;
                cm.ZipCD2 = zip2;

            }
            
            if (cmShip.ZipCD1 != null)
            {
                string zipShip1 = cmShip.ZipCD1.Substring(0, 3);
                string zipShip2 = cmShip.ZipCD1.Substring(3);
                cmShip.ZipCD1 = zipShip1;
                cmShip.ZipCD2 = zipShip2;
            }
           
            cm.InsertOperator=Session["CompanyCD"].ToString();
            cmShip.InsertOperator = Session["CompanyCD"].ToString();

            DataTable dt  = cbl.InsertCompany(cm);
            DataTable dtShip = cbl.InsertCompanyShipping(cmShip,cm);

            if (ModelState.IsValid)
            {
               return RedirectToAction("Company_Entry");
            }
            else
            {
                ViewBag.Success = "登録されました。";
               return View("Company_Entry");
               
            }
        }
        public ActionResult Group_Entry()
        {
            return View();
        }
        [HttpGet]
        public string M_Companay_Select()
        {
            InformationBL ibl = new InformationBL();
            return DataTableToJSONWithJSONNet(ibl.Get_M_CompanyName());
        }
        [HttpGet]
        public string M_Brand_Select()
        {
            Group_EntryBL gbl = new Group_EntryBL();
            return DataTableToJSONWithJSONNet(gbl.Get_M_BrandName());
        }
        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }
    }
}