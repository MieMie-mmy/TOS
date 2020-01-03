using System.Web.Mvc;
using TOS_Model;
using Company_BL;
using System.Data;
using TOS_DL;
using System.Linq;

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
                TOSEntities en = new TOSEntities();
                string IDs = "E";
                string Key = "1002";
                ViewBag.ErrorMessage = en.Messages.Where(s => s.ID.Equals(IDs) && s.KEY.Equals(Key)).Select(s => s.Message1).FirstOrDefault();

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
                return RedirectToAction("Index", "Home");
            }
        }
    }
}