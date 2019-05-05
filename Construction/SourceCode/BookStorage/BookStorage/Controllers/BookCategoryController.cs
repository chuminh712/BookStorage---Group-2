using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStorage.Models;

namespace BookStorage.Controllers
{
    public class BookCategoryController : Controller
    {
        // GET: BookCategory
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new Book();
            var model = dao.ListAllPage(searchString, page, pageSize);
            ViewBag.SearchString = searchString;
            return View(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(BookCategory book_category)
        {
            if (ModelState.IsValid)
            {
                var dao = new BookCategory();
                int id = dao.Insert(book_category);
                if (id > 0)
                {
                    return RedirectToAction("Index", "BookCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm danh muc sách không thành công");
                }
            }
            SetViewBag();
            return View("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new BookCategory();
            var book_category = dao.GetByID(id);
            SetViewBag();
            return View(book_category);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(BookCategory book_category)
        {
            if (ModelState.IsValid)
            {
                var dao = new BookCategory();
                var result = dao.Update(book_category);
                if (result)
                {
                    return RedirectToAction("Index", "BookCategory");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            SetViewBag();
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new BookCategory().Delete(id);
            return RedirectToAction("Index");
        }

        public void SetViewBag(int? categoryID = null, int? unitID = null)
        {
            ViewBag.BookCategoryID = new SelectList(new BookCategory().ListAll(), "ID", "Name", categoryID);
            ViewBag.UnitID = new SelectList(new Unit().ListAll(), "ID", "Name", unitID);
        }
    }
}