using BookStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStorage.Controllers
{
    public class CustomerController : BaseController
    {
        // GET: Customer
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new Customer();
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
        public ActionResult Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var dao = new Customer();
                int id = dao.Insert(customer);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Customer");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm nhà phân phối không thành công");
                }
            }
            return View("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new Customer();
            var customer = dao.GetByID(id);
            return View(customer);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                var dao = new Customer();
                var result = dao.Update(customer);
                if (result)
                {
                    return RedirectToAction("Index", "Customer");
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
            var dao = new Customer();
            var customer = dao.GetByID(id);
            return View(customer);
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new Customer().Delete(id);
            return RedirectToAction("Index");
        }
    }
}