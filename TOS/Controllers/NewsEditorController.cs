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
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.IO;
using FastMember;
using System.Configuration;

namespace TOS.Controllers
{
    public class NewsEditorController : Controller
    {
        string InformationFiles = ConfigurationManager.AppSettings["InformationFiles"].ToString();
        public ActionResult News_Editor()
        {

            return View();
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

        [HttpPost]
        public ActionResult T_Information_SaveEdit(MultipleModel model, List<HttpPostedFileBase> Insertfile, string SendEditFile, string CancleFile)
        {
            string SaveFileName = string.Empty;
            //string path = Server.MapPath("~/AttachFiles/");

            if (!Directory.Exists(InformationFiles))
            {
                Directory.CreateDirectory(InformationFiles);
            }

            var getExitFiles = Directory.GetFiles(InformationFiles);
           
            string MatchFiles = "";
            //if (!string.IsNullOrWhiteSpace(CancleFile) )

            //{
               
            //}
            //else 
            if(!string.IsNullOrWhiteSpace(SendEditFile) || !string.IsNullOrWhiteSpace(CancleFile) )
            {
                MatchFiles = MatchEditFies(CancleFile, SendEditFile);
            }

            if (!String.IsNullOrWhiteSpace(MatchFiles.Replace(',',' ')))
       
            {
                var EditFiles = MatchFiles.TrimEnd(',').Split(',');

                //var EditFiles = SendEditFile.TrimEnd(',').Split(',');


                var k = EditFiles.Count();
                if ( EditFiles.Count() > 0)
                {
                    while (k > 0)
                    {
                        foreach (var exfile in getExitFiles)
                        {
                            //var exitFile = (exfile.Split('\\'))[6].Split('.')[0]; 
                            var exitFile = exfile.Split('\\');
                            //string filename = exitFile[exitFile.Length - 1].Split('.')[0];
                            //if (filename == EditFiles[k - 1].Split('.')[0])
                            //{
                            //    SaveFileName += filename + ",";
                            //}
                            string filename = exitFile[exitFile.Length - 1];
                            if (filename == EditFiles[k - 1])
                            {
                                SaveFileName += filename + ",";
                            }
                        }
                        k = k - 1;
                    }

                }

            }
           
          

            if (Insertfile != null)
            {
                foreach (HttpPostedFileBase postedFile in Insertfile)
                {

                    if (postedFile != null)
                    {

                        string fileName = Path.GetFileName(postedFile.FileName);
                        postedFile.SaveAs(InformationFiles + fileName);
                        SaveFileName += fileName + ",";


                    }
                }

            }
            if (!String.IsNullOrWhiteSpace(SaveFileName))
            {
                SaveFileName = SaveFileName.TrimEnd(',');
                var AttachFiles = SaveFileName.Split(',');
              
                for (int i = 0; i < AttachFiles.Length; i++)
                {
                    if ((AttachFiles[i] != null) || (AttachFiles[i].Trim().Length != 0))
                    {
                        if(i==0)
                            model.TinfoModel.AttachedFile1 = AttachFiles[i].ToString();
                        else if(i==1)
                            model.TinfoModel.AttachedFile2 = AttachFiles[i].ToString();
                        else if(i==2)
                            model.TinfoModel.AttachedFile3 = AttachFiles[i].ToString();
                        else
                            model.TinfoModel.AttachedFile4 = AttachFiles[i].ToString();
                    }
                }
            }



           
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

        public string MatchEditFies(string c, string e)
        {

            string MatchFile = "";
            string[] CF = new string[4];
            CF = c.TrimEnd(',').Split(',');
            string[] EF = new string[4];
            EF = (e.TrimEnd(',')).TrimStart(',').Split(',');

            var j = 0;
            if(string.IsNullOrWhiteSpace(c))
            {
                MatchFile = e;
            }
            else
            {
                if (EF.Count() > CF.Count())
                {
                    j = EF.Count();
                    for (int i = 0; i < j; i++)
                    {
                       
                        for(int jj=0;jj<CF.Count();jj++)
                        {
                            if(CF[jj].ToString() != EF[i].ToString() )
                            {
                                MatchFile += EF[i].ToString() + ",";
                            }
                        }
                        //MatchFile += CF.Where(f => !(f.Contains(EF[i]))).Select(s => s).FirstOrDefault() + ",";
                    }

                }
                else if (CF.Count() > EF.Count())
                {
                    j = CF.Count();
                    for (int i = 0; i < j; i++)
                    {
                        MatchFile += EF.Where(f =>!f.Contains( CF[i])).Select(s => s).FirstOrDefault() + ",";
                    }
                }
                else
                {
                    j = EF.Count();
                    for (int i = 0; i < j; i++)
                    {
                        MatchFile += CF.Where(f => !f.Contains(EF[i])).Select(s => s).FirstOrDefault() + ",";
                    }


                }
            }
           
            return MatchFile;
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