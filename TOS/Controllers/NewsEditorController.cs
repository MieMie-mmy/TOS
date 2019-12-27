using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOS_Model;
using Information_BL;
using Group_Entry_BL;
using Newtonsoft.Json;
using System.Data;

namespace TOS.Controllers
{
    public class NewsEditorController : Controller
    {
      
        public ActionResult News_Editor(string id)
        {
            if (id != null)
            {
                MultipleModel m = new MultipleModel();
                T_InformationModel tinfo = new T_InformationModel();
                M_CompanyModel cmodel = new M_CompanyModel();
                M_GroupModel mg = new M_GroupModel();
                InformationBL ibl = new InformationBL();
                DataTable dtinfo = new DataTable();
                dtinfo = ibl.T_Information_Select_For_Edit(id);
                if (dtinfo.Rows.Count > 0)
                {
                    tinfo.DestinationFlag = Convert.ToInt32(dtinfo.Rows[0]["DestinationFlg"].ToString());
                    if(tinfo.DestinationFlag == 2)
                       cmodel.CompanyCD = dtinfo.Rows[0]["CompanyCD"].ToString();
                    if(tinfo.DestinationFlag == 3)
                        mg.GroupID= dtinfo.Rows[0]["GroupID"].ToString();
                    tinfo.DisplayStartDate= Convert.ToDateTime(dtinfo.Rows[0]["DisplayStartDate"].ToString());
                    tinfo.DisplayEndDate = Convert.ToDateTime(dtinfo.Rows[0]["DisplayEndDate"].ToString());
                    tinfo.Date = dtinfo.Rows[0]["Date"].ToString();
                    tinfo.TitleName = dtinfo.Rows[0]["TitleName"].ToString();
                    tinfo.DetailInformation = dtinfo.Rows[0]["DetailInformation"].ToString();
                    m.TinfoModel = tinfo;
                    m.ComModel = cmodel;
                    m.GroupModel = mg;
                }
                return View(m);
            }
            else
            {
                return View();
            }
            
        }
      
        public ActionResult T_Information_SaveEdit(MultipleModel model)
        {
            InformationBL ibl = new InformationBL();
            model.TinfoModel.InsertOperator = Session["CompanyCD"].ToString();
            ibl.News_Editor_Save(model);
            return RedirectToAction("News_Editor");
        }


        [HttpGet]
        public string M_Companay_Select()
        {
            T_InformationModel tinfo = new T_InformationModel();
            InformationBL ibl = new InformationBL();
            return DataTableToJSONWithJSONNet(ibl.Get_M_CompanyName());
        }

        [HttpGet]
        public string M_Group_Select()
        {
            T_InformationModel tinfo = new T_InformationModel();
            InformationBL ibl = new InformationBL();
            return DataTableToJSONWithJSONNet(ibl.Get_M_GroupName());
        }

        [HttpGet]
        public string T_Information_Select()
        {
            T_InformationModel tinfo = new T_InformationModel();
            InformationBL ibl = new InformationBL();
            return DataTableToJSONWithJSONNet(ibl.T_Information_Select());
        }

        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }

        public ActionResult T_Information_Delete(string id)
        {
            Boolean deleteflag = true;
            InformationBL ibl = new InformationBL();
            ibl.News_Editor_Delete(id);
            return RedirectToAction("News_Editor");
            //if(deleteflag)
            //{
            //    Group_EntryBL gebl = new Group_EntryBL();
            //    DataTable dtEMsg = gebl.M_Message_Select("1001", "Q");
            //    string message = string.Empty;
            //    if (dtEMsg.Rows.Count > 0)
            //    {
            //        TempData["Emsg"] = dtEMsg.Rows[0]["Message2"].ToString();
            //    }
            //}
            //else
            //{
            //    Group_EntryBL gebl = new Group_EntryBL();
            //    DataTable dtEMsg = gebl.M_Message_Select("1001", "E");
            //    string message = string.Empty;
            //    if (dtEMsg.Rows.Count > 0)
            //    {
            //        TempData["Emsg"] = dtEMsg.Rows[0]["Message1"].ToString();
            //    }
            //}


        }

    }
}