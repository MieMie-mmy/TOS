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
using Group_View_BL;
using System.EnterpriseServices;
using TOS_DL;
using Base_BL;

namespace TOS.Controllers
{
    public class RegistrationsController : Controller
    {
        DataTable dt = new DataTable();
        Company_EntryBL bl = new Company_EntryBL();


        // GET: Registrations

        public ActionResult Company_Entry(string id)
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
            try
            {


                Company_EntryBL cbl = new Company_EntryBL();
                DataTable Checkdt = cbl.Check_Duplicate_CompanyCD(model.ComModel);
                if (Checkdt.Rows.Count > 0)
                {
                    DataTable dtIMsg = cbl.Message_Select("1006", "I");
                    string message = string.Empty;
                    if (dtIMsg.Rows.Count > 0)
                    {
                        // TempData["Imsg"] = dtIMsg.Rows[0]["Message1"].ToString();
                        TempData["Dmsg"] = "Duplicate CompanyCD is " + model.ComModel.CompanyCD;
                    }
                }
                else
                {
                    var option = new TransactionOptions
                    {
                        IsolationLevel = System.Transactions.IsolationLevel.ReadUncommitted,
                        Timeout = TimeSpan.MaxValue
                    };
                    TransactionScope scope = new TransactionScope(TransactionScopeOption.Required, option);
                    using (scope)
                    {
                        string PcName = System.Environment.MachineName;

                        //Insert Company
                        if (model.ComModel.ZipCD1 != null)
                        {
                            //string zip1 = model.ComModel.ZipCD1.Substring(0, 3);
                            //string zip2 = model.ComModel.ZipCD1.Substring(3);
                            string[] zips = model.ComModel.ZipCD1.Split(new Char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                            string zip1 = zips[0].ToString();
                            string zip2 = zips[1].ToString();

                            model.ComModel.ZipCD1 = zip1;
                            model.ComModel.ZipCD2 = zip2;

                        }

                        model.ComModel.InsertOperator = Session["CompanyCD"].ToString();
                        DataTable dt = cbl.InsertCompany(model.ComModel, PcName);



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
                                        //string zipShip1 = model.ShippingModel[i].ZipCD1.Substring(0, 3);
                                        //string zipShip2 = model.ShippingModel[i].ZipCD1.Substring(3);
                                        string[] zipships = model.ShippingModel[i].ZipCD1.Split(new Char[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                                        string zipShip1 = zipships[0].ToString();
                                        string zipShip2 = zipships[1].ToString();
                                        model.ShippingModel[i].ZipCD1 = zipShip1;
                                        model.ShippingModel[i].ZipCD2 = zipShip2;
                                    }

                                    model.ShippingModel[i].InsertOperator = Session["CompanyCD"].ToString();
                                    DataTable dtShip = cbl.InsertCompanyShipping(model.ShippingModel[i], model.ComModel, PcName);
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

                                    DataTable dtTag = cbl.InsertCompanyTag(model.TagModel[i], model.ComModel, PcName);

                                }
                            }
                        }

                        //Insert  Company  Brand
                        if (model.MBrandModel.BrandName != null)
                        {
                            string[] Brandstr = model.MBrandModel.BrandName.Split(',');
                            model.MBrandModel.InsertOperator = Session["CompanyCD"].ToString();
                            if (Brandstr.Length > 0)
                            {
                                for (int i = 0; i < Brandstr.Length; i++)
                                {
                                    string BrandName = Brandstr[i].ToString();
                                    if (!String.IsNullOrWhiteSpace(BrandName))
                                    {
                                        model.MBrandModel.BrandName = BrandName;
                                        DataTable dtBrand = cbl.InsertCompanyBrand(model.MBrandModel, model.ComModel, PcName);
                                    }
                                }
                            }
                        }
                        scope.Complete();
                    }


                    //if (ModelState.IsValid)
                    //{
                    //    return RedirectToAction("Company_Entry");

                    //}
                    //else
                    //{
                    //    TempData["message"] = "登録されました。";
                    //    return View("Company_Entry");

                    //}

                    //DataTable dtMsg = cbl.Insert_Message_Select("1002", "I");
                    //string message = string.Empty;
                    //if (dtMsg.Rows.Count > 0)
                    //{
                    //    TempData["Emsg"] = dtMsg.Rows[0]["Message1"].ToString();
                    //}

                    //DataTable dtIMsg = cbl.Message_Select("1002", "I");
                    //string message = string.Empty;
                    //if (dtIMsg.Rows.Count > 0)
                    //{
                    TempData["Imsg"] = "success";
                    // }


                }
                return RedirectToAction("Company_Entry");

            }
            catch (Exception ex)
            {
                string st = ex.ToString();

                //if (st.Contains("M_CompanyShipping_Insert"))
                //{

                //    TempData["Emsg"] = "SQL Query Error !! please,check in  M_CompanyShipping_Insert  Store Procedure";
                //}
                //else if (st.Contains("M_Company_Insert"))
                //    {
                //        TempData["Emsg"] = "SQL Query Error !! please,check in  M_Company_Insert  Store Procedure";
                //}
                //else if (st.Contains("M_CompanyTag_Insert"))
                //{
                //    TempData["Emsg"] = "SQL Query Error !! please,check in   M_CompanyTag_Insert  Store Procedure";
                //}
                //else if (st.Contains("M_CompanyBrand_Insert"))
                //{
                //    TempData["Emsg"] = "SQL Query Error !! please,check in  M_CompanyBrand_Insert  Store Procedure";
                //}
                //else
                //{
                TempData["Emsg"] = "Unsuccess";
                // }
                return RedirectToAction("Company_Entry");

            }
        }
        public ActionResult Group_Entry(string id)
        {
            if (id == null)
            {
                ViewBag.Groupid = "";
                return View();
            }
            else
            {
                ViewBag.Groupid = id;
                return View();
            }
        }

        [HttpGet]
        public string Get_Group_View_ForEdit(string id)
        {
            Group_ViewBL gbl = new Group_ViewBL();
            if (id != null)
            {
                DataTable dtinfo = new DataTable();
                dtinfo = gbl.Get_Group_View_ForEdit(id);
                if (dtinfo.Rows.Count > 0)
                {
                    string jsonresult;
                    jsonresult = JsonConvert.SerializeObject(dtinfo);
                    return jsonresult;
                }
            }
            return null;

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
            try
            {
                Group_EntryBL gebl = new Group_EntryBL();
                model.GroupModel.InsertOperator = Session["CompanyCD"].ToString();
                Boolean insertFlag = false;
                if (model.GroupModel.SaveUpdateFlag == null)
                {
                    model.GroupModel.SaveUpdateFlag = "Save";
                    DataTable dt = new DataTable();
                    dt = gebl.Check_Duplicate_GroupEntry(model);
                    if (dt.Rows.Count > 0)
                    {
                        TempData["Imsg"] = "Duplicate";
                    }
                    else
                    {
                        string PcName = System.Environment.MachineName;
                        insertFlag = gebl.InsertGroupEntry(model, PcName);
                        if (insertFlag)
                        {
                            TempData["Smsg"] = "success";
                        }
                        else
                        {
                            TempData["Emsg"] = "Unsuccess";
                        }
                    }
                }
                else
                {
                    string PcName = System.Environment.MachineName;
                    string a = model.GroupModel.SaveUpdateFlag;
                    insertFlag = gebl.InsertGroupEntry(model, PcName);
                    if (insertFlag)
                    {
                        TempData["USmsg"] = "success";
                    }
                    else
                    {
                        TempData["UEmsg"] = "Unsuccess";
                    }
                }

                //if (ModelState.IsValid)
                //{
                return RedirectToAction("Group_Entry");
                //}
                //else
                //{
                //    return View("Group_Entry");
                //}
            }
            catch (Exception ex)
            {
                string st = ex.ToString();
                return RedirectToAction("Group_Entry");
            }
        }

        public ActionResult Group_View()
        {
            return View();
        }

        public string Bind_Group_View()
        {
            DataTable dt = new DataTable();
            string jsonresult;
            if (Session["CompanyCD"] != null)
            {
                Group_ViewBL gvbl = new Group_ViewBL();
                dt = gvbl.Get_Group_View();
            }
            jsonresult = DataTableToJSONWithJSONNet(dt);
            return jsonresult;
        }
        [HttpPost]
        public string Group_View_Delete(string id)
        {
            var message = string.Empty;
            if (id == null)
            {
                message = "NOK";
            }
            else
            {

                Group_ViewBL gbl = new Group_ViewBL();
                string InsertOperator = Session["CompanyCD"].ToString();
                string PcName = System.Environment.MachineName;
                message = gbl.Group_Info_Delete(id, PcName, InsertOperator);
            }
            return JsonConvert.SerializeObject(message);
        }
        public ActionResult CompanyUpdate_View(M_CompanyModel mModel)
        {
            return View();
        }


        public string _GetTable()
        {

            dt = bl.CompanyUpdateView_select();
            var Jsondata = JsonConvert.SerializeObject(dt);
            return Jsondata;
            // return View();
        }
        
        
    }
}
