using TOS_Model;
using Company_BL;
using System.Web.Mvc;
using System.Data;
using Information_BL;
using Newtonsoft.Json;
using System.IO;
using System.Xml.Linq;
using System.Runtime.InteropServices.ComTypes;


namespace TOS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["CompanyCD"] != null)
            {
                Get_T_Information();
                return View();
            }
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
                ViewData["Attach1"] = attachfile1;
                ViewData["Attach2"] = attachfile2;
                ViewData["Attach3"] = attachfile3;
                ViewData["Attach4"] = attachfile4;



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

        [HttpGet]
        public ActionResult MyPdfAction()

        {
                            string path = Server.MapPath("~/FileFolder");
                string fileName = "OrderHistory_2019-12-20 (2).pdf";

                byte[] fileBytes = System.IO.File.ReadAllBytes(path + @"\" + fileName);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

           

        }



    }
}