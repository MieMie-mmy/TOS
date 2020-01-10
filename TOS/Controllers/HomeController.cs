using TOS_Model;
using Company_BL;
using System.Web.Mvc;
using System.Data;
using Information_BL;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Configuration;

namespace TOS.Controllers
{
    public class HomeController : Controller
    {
        string InformationFiles = ConfigurationManager.AppSettings["InformationFiles"].ToString();
        public ActionResult Index()
        {
           
            if (Session["CompanyCD"] != null)
            {
                Get_T_Information();
                return View();
            }
            //if (Session["UserRole"] != null)
            //{
            //    Get_T_Information();
            //    return View();
            //}
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult product_details(string id)
        {
            InformationBL bl = new InformationBL();
            DataTable dt = bl.Get_InformationTitleName(id);

            for (var i = 0; i < dt.Rows.Count; i++)
            {
                var titlename = dt.Rows[i]["TitleName"].ToString();
                var infodate = dt.Rows[i]["Column1"].ToString();
                var detailinfo = dt.Rows[i]["DetailInformation"].ToString();

                var attachfile1 = dt.Rows[i]["AttachedFile1"].ToString();
                var attachfile2 = dt.Rows[i]["AttachedFile2"].ToString();
                var attachfile3 = dt.Rows[i]["AttachedFile3"].ToString();
                var attachfile4 = dt.Rows[i]["AttachedFile4"].ToString();

                ViewData["InfoTitle"] = titlename;
                ViewData["InfoDate"] = infodate;
                ViewData["DetailInfo"] = detailinfo;
                if (!string.IsNullOrWhiteSpace(attachfile1))
                {
                    ViewData["Attach1"] = attachfile1 + "  ▼";
                }
                else
                {

                    ViewData["Attach1"] = null;
                }

                if (!string.IsNullOrWhiteSpace(attachfile2))
                {
                    ViewData["Attach2"] = attachfile2 + "  ▼";
                }
                else
                {
                    ViewData["Attach2"] = null;
                }

                if (!string.IsNullOrWhiteSpace(attachfile3))
                {
                    ViewData["Attach3"] = attachfile3 + "  ▼";
                }
                else
                {
                    ViewData["Attach3"] = null;
                }

                if (!string.IsNullOrWhiteSpace(attachfile4))
                {
                    ViewData["Attach4"] = attachfile4 + "  ▼";
                }
                else
                {
                    ViewData["Attach4"] = null;
                }

            }

            return View();
        }

        [HttpGet]
        public string Get_T_Information()
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

      
        public ActionResult MyPdfAction(string name)
        {
            var Path = ConfigurationManager.AppSettings["InformationFiles"];

            //    var FileName = "OrderHistory_1_3_2020.pdf";
            var FileName = name;
            var get_allFiles = Directory.GetFiles(Path);
            bool Check_Files=false;
            if (get_allFiles.Length > 0)
            {
                for(var i=0;i<get_allFiles.Length;i++)
                {
                    var fil = get_allFiles[i].Split('\\');
                    var k = (fil.Length) - 1;
                    var FileN = fil[k];
                    if(FileN.ToString() == FileName)
                    {
                        Check_Files = true;
                    }
                    
                }

            }

            if (Check_Files)
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(Path + FileName);
                return File(fileBytes, "application/pdf", FileName);
            }
            return RedirectToAction("Index") ;
        }

    }
}