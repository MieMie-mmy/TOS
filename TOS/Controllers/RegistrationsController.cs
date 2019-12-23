using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOS_Model;
using Company_Entry_BL;
using System.Data;
using System.Transactions;
using Information_BL;
using Group_Entry_BL;
using Newtonsoft.Json;
using System.EnterpriseServices;

namespace TOS.Controllers
{
    public class RegistrationsController : Controller
    {
       
        // GET: Registrations
        public ActionResult Company_Entry()
        {
            
            if (Session["CompanyCD"] != null)
            {
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

            var option = new TransactionOptions
            {
                IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,
                Timeout = TimeSpan.MaxValue
            };
            TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option);
            using (scope)
            {
                Company_EntryBL cbl = new Company_EntryBL();

                //Insert Company
                if (model.ComModel.ZipCD1 != null)
                {
                    string zip1 = model.ComModel.ZipCD1.Substring(0, 3);
                    string zip2 = model.ComModel.ZipCD1.Substring(3);
                    model.ComModel.ZipCD1 = zip1;
                    model.ComModel.ZipCD2 = zip2;

                }

                model.ComModel.InsertOperator = Session["CompanyCD"].ToString();
                DataTable Checkdt = cbl.Check_Duplicate_CompanyCD(model.ComModel);
                if(Checkdt.Rows.Count>0)
                {
                    TempData["message"] = "Duplicate Company CD !! Please, Check !!";
                }
                else
                { 
                DataTable dt = cbl.InsertCompany(model.ComModel);
                }


                //Insert Company Shipping
                Array arrayShip = model.ShippingModel.ToArray();

                if (arrayShip.Length > 0)
                {
                    for (int i = 0; i < arrayShip.Length; i++)
                    {
                        if ((!string.IsNullOrWhiteSpace(model.ShippingModel[i].ShippingID.ToString())) && (!string.IsNullOrWhiteSpace(model.ComModel.CompanyCD.ToString())) && model.ShippingModel[i].ShippingID.ToString() != "0")
                        {

                            if (model.ShippingModel[i].ZipCD1 != null)
                            {
                                string zipShip1 = model.ShippingModel[i].ZipCD1.Substring(0, 3);
                                string zipShip2 = model.ShippingModel[i].ZipCD1.Substring(3);
                                model.ShippingModel[i].ZipCD1 = zipShip1;
                                model.ShippingModel[i].ZipCD2 = zipShip2;
                            }

                            model.ShippingModel[i].InsertOperator = Session["CompanyCD"].ToString();
                            DataTable dtShip = cbl.InsertCompanyShipping(model.ShippingModel[i], model.ComModel);
                        }
                    }
                }

                //Insert Company Tag

                Array ArrayTag = model.TagModel.ToArray();

                if (ArrayTag.Length > 0)
                {
                    for (int i = 0; i < ArrayTag.Length; i++)
                    {
                        if (model.TagModel[i].Tag != null && (!string.IsNullOrWhiteSpace(model.ComModel.CompanyCD.ToString())))
                        {

                            model.TagModel[i].InsertOperator = Session["CompanyCD"].ToString();

                            DataTable dtTag = cbl.InsertCompanyTag(model.TagModel[i], model.ComModel);

                        }
                    }
                }

                //Insert  Company  Brand
                string[] Brandstr = model.MBrandModel.BrandName.Split(',');
                model.MBrandModel.InsertOperator = Session["CompanyCD"].ToString();
                if (Brandstr.Length > 0)
                {
                    for (int i = 0; i < Brandstr.Length; i++)
                    {
                        string BrandName = Brandstr[i].ToString();
                        model.MBrandModel.BrandName = BrandName;
                        DataTable dtBrand = cbl.InsertCompanyBrand(model.MBrandModel, model.ComModel);
                    }
                }
                scope.Complete();

               if (ModelState.IsValid)
               {
                    return RedirectToAction("Company_Entry");
                   
               }
                else
                {
                    TempData["message"] = "登録されました。";
                    return View("Company_Entry");

                }
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

        [HttpPost]
        public ActionResult InsertGroupEntry(MultipleModel model)
        {

            Group_EntryBL gebl = new Group_EntryBL();
            model.GroupModel.InsertOperator = Session["CompanyCD"].ToString();
            DataTable dt = new DataTable();
            dt = gebl.Check_Duplicate_GroupEntry(model);
            if (dt.Rows.Count > 0)
            {

            }
            else
            {
                gebl.InsertGroupEntry(model);
            }
            if (ModelState.IsValid)
            {
                return RedirectToAction("Group_Entry");
            }
            else
            {
                return View("Group_Entry");

            }
        }
    }
}