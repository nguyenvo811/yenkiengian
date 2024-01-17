using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FSite.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin,Operator")]
    public class AdminDefaultController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}