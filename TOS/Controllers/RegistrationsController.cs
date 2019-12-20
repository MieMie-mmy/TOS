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
        public ActionResult InsertCompany(MultipleModel model)
        {

            Company_EntryBL cbl = new Company_EntryBL();

            if (model.ComModel.ZipCD1 != null)
            {
                string zip1 = model.ComModel.ZipCD1.Substring(0, 3);
                string zip2 = model.ComModel.ZipCD1.Substring(3);
                model.ComModel.ZipCD1 = zip1;
                model.ComModel.ZipCD2 = zip2;

            }
            model.ComModel.InsertOperator = Session["CompanyCD"].ToString();
            DataTable dt = cbl.InsertCompany(model.ComModel);
            Array array = model.ShippingModel.ToArray();

            if (array.Length > 0)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    if ((!string.IsNullOrWhiteSpace(model.ShippingModel[i].ShippingID.ToString())) && (!string.IsNullOrWhiteSpace(model.ComModel.CompanyCD.ToString())) && model.ShippingModel[i].ShippingID.ToString()!="0")
                    {

                        if (model.ShippingModel[i].ZipCD1 != null)
                        {
                            string zipShip1 = model.ShippingModel[i].ZipCD1.Substring(0, 3);
                            string zipShip2 = model.ShippingModel[i].ZipCD1.Substring(3);
                            model.ShippingModel[i].ZipCD1 = zipShip1;
                            model.ShippingModel[i].ZipCD2 = zipShip2;
                        }

                        model.ShippingModel[i].InsertOperator = Session["CompanyCD"].ToString();
                        //DataTable dtShip = cbl.InsertCompanyShipping(model.ShippingModel[i], model.ComModel);
                    }
            }
        }
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