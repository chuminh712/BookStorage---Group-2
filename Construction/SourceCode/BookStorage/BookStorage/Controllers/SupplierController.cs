using BookStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStorage.Controllers
{
    public class SupplierController : BaseController
    {
        // GET: Supplier
        public ActionResult Index(string searchString, int page = 1, int pageSize = 2)
        {
            var dao = new Supplier();
            var model = dao.ListAllPage(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                var dao = new Supplier();
                int id = dao.Insert(supplier);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Supplier");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm nhà cung cấp không thành công");
                }
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new Supplier();
            var supplier = dao.GetByID(id);
            return View(supplier);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Supplier supplier)
        {
            if (ModelState.IsValid)
            {
                var dao = new Supplier();
                var result = dao.Update(supplier);
                if (result)
                {
                    return RedirectToAction("Index", "Supplier");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return View("Index");
        }

        public ActionResult Detail(int id)
        {
            var dao = new Supplier();
            var supplier = dao.GetByID(id);
            return View(supplier);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new Supplier().Delete(id);
            return RedirectToAction("Index");
        }
    }
}