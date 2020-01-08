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
using System.Threading.Tasks;
using System.IO;

namespace TOS.Controllers
{
    public class NewsEditorController : Controller
    {
      
        public ActionResult News_Editor(string id)
        {
            MultipleModel m = new MultipleModel();
            T_InformationModel tinfo = new T_InformationModel();
            M_CompanyModel cmodel = new M_CompanyModel();
            M_GroupModel mg = new M_GroupModel();
            InformationBL ibl = new InformationBL();
            if (id != null)
            {
                DataTable dtinfo = new DataTable();
                dtinfo = ibl.T_Information_Select_For_Edit(id);
                if (dtinfo.Rows.Count > 0)
                {
                    tinfo.DestinationFlag = Convert.ToInt32(dtinfo.Rows[0]["DestinationFlg"].ToString());
                    if(tinfo.DestinationFlag == 2)
                    {
                        cmodel.CompanyCD = dtinfo.Rows[0]["CompanyCD"].ToString();
                        cmodel.CompanyName = dtinfo.Rows[0]["CompanyName"].ToString();
                    }
                    if(tinfo.DestinationFlag == 3)
                    {
                        mg.GroupID = dtinfo.Rows[0]["GroupID"].ToString();
                        mg.GroupName = dtinfo.Rows[0]["GroupName"].ToString();
                    } 
                    tinfo.InformationType = Convert.ToInt32(dtinfo.Rows[0]["InformationType"].ToString());
                    tinfo.DisplayStartDate= Convert.ToDateTime(dtinfo.Rows[0]["DisplayStartDate"].ToString());
                    tinfo.DisplayEndDate = Convert.ToDateTime(dtinfo.Rows[0]["DisplayEndDate"].ToString());
                    tinfo.Date = Convert.ToDateTime(dtinfo.Rows[0]["Date"].ToString());
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
                m.TinfoModel = tinfo;
                m.ComModel = cmodel;
                m.GroupModel = mg;
        
                return View(m);
            }
            
        }

        [HttpGet]
        public string News_Editor_Edit(string id)
        {
            InformationBL ibl = new InformationBL();
            if (id != null)
            {
                DataTable dtinfo = new DataTable();
                dtinfo = ibl.T_Information_Select_For_Edit(id);
                if (dtinfo.Rows.Count > 0)
                {
                    string jsonresult;
                    jsonresult = JsonConvert.SerializeObject(dtinfo);
                    return jsonresult;
                }
            }
            return null;
           
        }
      
        public ActionResult T_Information_SaveEdit(MultipleModel model)
        {
            Boolean insertflag = true;
            InformationBL ibl = new InformationBL();
            model.TinfoModel.InsertOperator = Session["CompanyCD"].ToString();
            string PcName = System.Environment.MachineName;
            if (model.TinfoModel.InformationID == 0)
            {
                insertflag = ibl.News_Editor_Save(model, PcName);
                if (insertflag)
                {
                    TempData["Save"] = "Save Successfully";
                }
                else
                {
                    TempData["Nosave"] = "Save Failed";
                }
            }
            else
            {
                insertflag = ibl.News_Editor_Save(model, PcName);
                if (insertflag)
                {
                    TempData["update"] = "Updated Successfully";
                }
                else
                {
                    TempData["noupdate"] = "Update Failed";
                }
            }
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

        [HttpPost]
        public string T_Information_Delete(string id)
        {
            var message = string.Empty;
            if(id==null)
            {
                message = "NOK";
            }
            InformationBL ibl = new InformationBL();
            string InsertOperator = Session["CompanyCD"].ToString();
            string PcName = System.Environment.MachineName;
            message = ibl.News_Editor_Delete(id, PcName, InsertOperator);


            return JsonConvert.SerializeObject(message);
        }
    }
}