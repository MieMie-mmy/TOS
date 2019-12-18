using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOS_Model;
using Company_BL;
using System.Data ;

namespace TOS.Controllers
{
    public class RegistrationsController : Controller
    {
        CompanyBL cbl = new CompanyBL();
        // GET: Registrations
        public ActionResult Company_Entry()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Insert(M_CompanyModel cm)
        {


             // DataTable dt  = cbl.Insert(cm);
            //TempData["Success"] = "登録されました。";
            //TempData["UserMessage"] = new MessageVM() { CssClassName = "alert-sucess", Title = "Success!", Message = "登録されました。" };
            //return RedirectToAction("Company_Entry");
            if (ModelState.IsValid)
            {
                //do something
                TempData["Success"] = "登録されました。";
                //return RedirectToAction("Company_Entry");
                return RedirectToAction("Company_Entry");
            }
            else
            {
                ViewData["Error"] = "Error message text.";
                return View("Company_Entry");
            }
        }



        public ActionResult Group_Entry()
        {
            return View();
        }
    }

    internal class MessageVM
    {
        public MessageVM()
        {
        }

        public string CssClassName { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
    }
}