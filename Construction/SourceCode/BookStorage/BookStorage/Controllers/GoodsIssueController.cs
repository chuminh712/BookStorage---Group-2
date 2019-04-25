using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BookStorage.Models;
using BookStorage.Common;
using Rotativa;


namespace BookStorage.Controllers
{
    public class GoodsIssueController : BaseController
    {
        // GET: GoodsIssue
        public ActionResult Index(string searchString, int page = 1, int pageSize = 2)
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
            var dao = new Customer();
            ViewBag.CustomerID = new SelectList(dao.ListAll(), "ID", "Name", CustomerID);

        }

        public JsonResult GetBookPrice(string code)
        {
            var dao = new Book();
            var bookPrice = dao.GetByCode(code).Price;
            return Json(bookPrice);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(GoodsIssue goodsIssue)
        {
            if (ModelState.IsValid)
            {
                var dao = new GoodsIssue();
                int id = dao.Insert(goodsIssue);
                if (id > 0)
                {
                    SetAlert("Thêm hóa đơn thành công", "success");
                    return RedirectToAction("Index", "GoodsIssue");
                }
                else
                {
                    SetAlert("Thêm hóa đơn không thành công", "danger");
                    return RedirectToAction("Index", "GoodsIssue");
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
                    SetAlert("Cập nhật hóa đơn thành công", "success");
                    return RedirectToAction("Index", "GoodsIssue");
                }
                else
                {
                    SetAlert("Cập nhật hóa đơn không thành công", "success");
                    return RedirectToAction("Index", "GoodsIssue");
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

        public ActionResult Detail(int id)
        {
            var dao = new GoodsIssue();
            var goodsIssue = dao.GetByID(id);
            return View(goodsIssue);
        }

        public ActionResult Print(int id)
        {
            var dao = new GoodsIssue();
            var goodsIssue = dao.GetByID(id);
            return View(goodsIssue);
        }

        public ActionResult PrintPdf(int id)
        {
            var dao = new GoodsReceipt();
            var goodsReceipt = dao.GetByID(id);
            ViewBag.TotalPriceText = NumberToText.NumberToTextVN((decimal)goodsReceipt.TotalPrice);
            return new ViewAsPdf()
            {
                FormsAuthenticationCookieName = System.Web.Security.FormsAuthentication.FormsCookieName,
                ViewName = "Print",
                Model = goodsReceipt
            };
        }

    }
}