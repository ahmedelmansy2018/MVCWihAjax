using SearchWihAjax.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SearchWihAjax.Controllers
{
    public class CategoryController : Controller
    {
        //
        // GET: /Category/
        CompanyEntities db = new CompanyEntities();

        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Categories()
        {
            return Json(db.Categories, JsonRequestBehavior.AllowGet);

        }
    }
}
