﻿using System;
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
                            TempData["Imsg"] = "Duplicate CompanyCD is "+ model.ComModel.CompanyCD;
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
                            string zip1 = model.ComModel.ZipCD1.Substring(0, 3);
                            string zip2 = model.ComModel.ZipCD1.Substring(3);
                            model.ComModel.ZipCD1 = zip1;
                            model.ComModel.ZipCD2 = zip2;

                        }

                        model.ComModel.InsertOperator = Session["CompanyCD"].ToString();
                        DataTable dt = cbl.InsertCompany(model.ComModel,PcName);



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
                                    if(!String.IsNullOrWhiteSpace(BrandName))
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

                    DataTable dtIMsg = cbl.Message_Select("1002", "I");
                    string message = string.Empty;
                    if (dtIMsg.Rows.Count > 0)
                    {
                        TempData["Imsg"] = dtIMsg.Rows[0]["Message1"].ToString();
                    }


                }
                return RedirectToAction("Company_Entry");

            }
            catch(Exception ex)
            {
                string st = ex.ToString();
                
                if (st.Contains("M_CompanyShipping_Insert"))
                {

                    TempData["Emsg"] = "SQL Query Error !! please,check in  M_CompanyShipping_Insert  Store Procedure";
                }
                else if (st.Contains("M_Company_Insert"))
                    {
                        TempData["Emsg"] = "SQL Query Error !! please,check in  M_Company_Insert  Store Procedure";
                }
                else if (st.Contains("M_CompanyTag_Insert"))
                {
                    TempData["Emsg"] = "SQL Query Error !! please,check in   M_CompanyTag_Insert  Store Procedure";
                }
                else if (st.Contains("M_CompanyBrand_Insert"))
                {
                    TempData["Emsg"] = "SQL Query Error !! please,check in  M_CompanyBrand_Insert  Store Procedure";
                }
                else
                {
                    TempData["Emsg"] = st;
                }
                return RedirectToAction("Company_Entry");

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

            //System.Web.UI.ScriptManager script_manager = new System.Web.UI.ScriptManager();
            Group_EntryBL gebl = new Group_EntryBL();
            model.GroupModel.InsertOperator = Session["CompanyCD"].ToString();
            Boolean insertFlag = false;
            DataTable dt = new DataTable();
            dt = gebl.Check_Duplicate_GroupEntry(model);
            if (dt.Rows.Count > 0)
            {
                DataTable dtIMsg = gebl.M_Message_Select("1002", "I");
                string message = string.Empty;
                if (dtIMsg.Rows.Count > 0)
                {
                    TempData["Imsg"] = dtIMsg.Rows[0]["Message1"].ToString();
                }
            }
            else
            {
                string PcName = System.Environment.MachineName;
                insertFlag = gebl.InsertGroupEntry(model,PcName);
                if(insertFlag)
                {
                    DataTable dtIMsg = gebl.M_Message_Select("1001", "I");
                    string message = string.Empty;
                    if (dtIMsg.Rows.Count > 0)
                    {
                        TempData["Imsg"] = dtIMsg.Rows[0]["Message1"].ToString();
                    }
                }
                else
                {
                    DataTable dtEMsg = gebl.M_Message_Select("1001", "E");
                    string message = string.Empty;
                    if (dtEMsg.Rows.Count > 0)
                    {
                        TempData["Emsg"] = dtEMsg.Rows[0]["Message1"].ToString();
                    }
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

        //public string ShowMessage(string MessageID,string msgType)
        //{
        //    Group_EntryBL gbl = new Group_EntryBL();
        //    DataTable dtMsg = gbl.M_Message_Select(MessageID,msgType);
        //    string message = string.Empty;
        //    if (dtMsg.Rows.Count > 0)
        //    {
        //        message = dtMsg.Rows[0]["Message1"].ToString();
        //        message += !string.IsNullOrWhiteSpace(dtMsg.Rows[0]["Message2"].ToString()) ? "\n\n" + dtMsg.Rows[0]["Message2"].ToString() : string.Empty;
        //        message += !string.IsNullOrWhiteSpace(dtMsg.Rows[0]["Message3"].ToString()) ? "\n\n" + dtMsg.Rows[0]["Message3"].ToString() : string.Empty;
        //        message += !string.IsNullOrWhiteSpace(dtMsg.Rows[0]["Message4"].ToString()) ? "\n\n" + dtMsg.Rows[0]["Message4"].ToString() : string.Empty;
        //        message += !string.IsNullOrWhiteSpace(dtMsg.Rows[0]["Message5"].ToString()) ? "\n\n" + dtMsg.Rows[0]["Message5"].ToString() : string.Empty;


        //        return message;
        //    }
        //    else
        //    {
        //        return  null;
        //    }

        //}
    }
}