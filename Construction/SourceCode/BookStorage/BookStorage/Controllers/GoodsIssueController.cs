using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStorage.Models;

namespace BookStorage.Controllers
{
    public class GoodsIssueController : Controller
    {
        // GET: GoodsIssue
        public ActionResult Index(string searchString, int page=1, int pageSize = 2)  
        {
                var dao = new GoodsIssue();
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
        public void SetViewBag(int? CustomerID = null)
        {
            var dao = new Supplier();
            ViewBag.SupplierID = new SelectList(dao.ListAll(), "ID", "Name", CustomerID);
            
        }

        public JsonResult GetBookPrice(int id)
        {
            var dao = new Book();
            var bookPrice = dao.GetByID(id).Price;
            return Json(bookPrice);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create (GoodsIssue goodsIssue)
        {
            if (ModelState.IsValid)
            {
                var dao = new GoodsIssue();
                int id = dao.Insert(goodsIssue);
                if(id > 0)
                {
                    return RedirectToAction("Index", "GoodsIssue");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm hóa đơn không thành công ");
                }
            }
            SetViewBag();
            return View("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new GoodsIssue();
            var goodsIssue = dao.GetByID(id);
            SetViewBag(goodsIssue.CustomerID);
            return View(goodsIssue);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(GoodsIssue goodsIssue)
        {
            if (ModelState.IsValid)
            {
                var dao = new GoodsIssue();
                var result = dao.Update(goodsIssue);
                if (result)
                {
                    return RedirectToAction("Index", "GoodsIssue");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            SetViewBag(goodsIssue.CustomerID);
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new GoodsIssue().Delete(id);
            return RedirectToAction("Index");
        }

    }
}