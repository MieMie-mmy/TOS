using System.Web.Mvc;
using TOS_Model;
using Company_BL;

namespace TOS.Controllers
{
    public class UserController : Controller
    {
        CompanyBL cbl = new CompanyBL();
        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(int? errorId)
        {
            if (errorId > 0)
            {
                ViewBag.ErrorMessage = "UserName Or Password Invalid !";
            }
            else
            {
                ViewBag.ErrorMessage = "";
            }
            return View();
        }

        public ActionResult CheckLogin(M_CompanyModel cm)
        {


            M_CompanyModel mc = cbl.CheckLogin(cm);
            if (mc == null)
            {
                return RedirectToAction("Login", "User", new { @errorId = 1 });
            }
            else
            {
                Session["CompanyName"] = mc.CompanyName.ToString();
                Session["CompanyCD"] = mc.CompanyCD.ToString();
                return RedirectToAction("Order_Input", "Order");
            }
        }
    }
}