using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStorage.Models;

namespace BookStorage.Controllers
{
    public class BookController : Controller
    {
        // GET: Book
        public ActionResult Index(string searchString, int page = 1, int pageSize = 2)
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
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                var dao = new Book();
                int id = dao.Insert(book);
                if (id > 0)
                {
                    return RedirectToAction("Index", "Book");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm sách không thành công");
                }
            }
            SetViewBag();
            return View("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new Book();
            var book = dao.GetByID(id);
            //SetViewBag(book.BookCategoryID);
            return View(book);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                var dao = new Book();
                var result = dao.Update(book);
                if (result)
                {
                    return RedirectToAction("Index", "Book");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            //SetViewBag(book.BookCategoryID);
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new Book().Delete(id);
            return RedirectToAction("Index");
        }

        public void SetViewBag(int? categoryID = null)
        {
            var dao = new BookCategory();
            ViewBag.BookCategoryID = new SelectList(dao.ListAll(), "ID", "Name", categoryID);
        }
    }
}