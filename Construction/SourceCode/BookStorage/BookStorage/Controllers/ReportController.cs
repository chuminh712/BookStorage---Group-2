using BookStorage.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStorage.Controllers
{
    public class ReportController : Controller
    {
        // GET: Inventory
        public ActionResult InventoryReport(string searchString)
        {
            var dao = new InventoryModel();
            var model = dao.GetInventoryInfo(searchString);
            ViewBag.SearchString = searchString;
            return View(model);
        }
    }
}