using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication.Models;

namespace WebApplication.Controllers
{
    [RoutePrefix("kich-hoat")]
    public class ActiveController : Controller
    {
        DeborahEntities db = new DeborahEntities();
        [Route]
        public ActionResult Index(string serial)
        {
            var model = new Product();
            if (!string.IsNullOrEmpty(serial))
            {
                var product = db.Products.SingleOrDefault(a => a.Code == serial);
                if (product != null)
                {
                    model = product;
                }
            }

            return View(model);
        }
    }
}