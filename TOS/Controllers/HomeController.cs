using TOS_Model;
using Company_BL;
using System.Web.Mvc;
using System.Data;

namespace TOS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(M_CompanyModel companymodel)
        {
            T_InformationModel tinfo = new T_InformationModel();
            InformationBL ibl = new InformationBL();
            DataTable dtinfo = ibl.GetInformation(companymodel);
            return View(tinfo);
        }

        public ActionResult product_details()
        {
            return View();
        }


    }
}