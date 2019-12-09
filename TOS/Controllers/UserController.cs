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

        public ActionResult Login()
        {           
            return View();
        }

        public ActionResult CheckLogin(M_CompanyModel cm)
        {
            M_CompanyModel mc = cbl.CheckLogin(cm);
            @ViewBag.ErrorMessage = "Login Failed!";
            return RedirectToAction("Login", "User");
        }
    }
}