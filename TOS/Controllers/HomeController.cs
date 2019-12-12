using TOS_Model;
using Company_BL;
using System.Web.Mvc;
using System.Data;
using Information_BL;

namespace TOS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            string CompanyCD = Session["CompanyCD"].ToString();
            InformationBL ibl = new InformationBL();
            DataTable dtinfo = ibl.GetInformation(CompanyCD);
            return View(dtinfo);
        }

        public ActionResult product_details()
        {
            return View();
        }


    }
}