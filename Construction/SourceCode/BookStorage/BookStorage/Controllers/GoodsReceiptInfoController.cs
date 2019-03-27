using BookStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStorage.Controllers
{
    public class GoodsReceiptInfoController : Controller
    {
        [HttpGet]
        public ActionResult Create(int id)
        {
            var goodsReceiptInfo = new GoodsReceiptInfo() { GoodsReceiptID = id };
            return View(goodsReceiptInfo);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(GoodsReceiptInfo goodsReceiptInfo)
        {
            if (ModelState.IsValid)
            {
                var dao = new GoodsReceiptInfo();
                int id = dao.Insert(goodsReceiptInfo);
                if (id > 0)
                {
                    return RedirectToAction("Detail", "GoodsReceipt", new { ID = goodsReceiptInfo.GoodsReceiptID });
                }
                else
                {
                    ModelState.AddModelError("", "Thêm chi tiết phiếu nhập không thành công");
                }
            }
            return RedirectToAction("Index", "GoodsReceipt");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new GoodsReceiptInfo();
            var goodsReceiptInfo = dao.GetByID(id);
            return View(goodsReceiptInfo);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(GoodsReceiptInfo goodsReceiptInfo)
        {
            if (ModelState.IsValid)
            {
                var dao = new GoodsReceiptInfo();
                var result = dao.Update(goodsReceiptInfo);
                if (result)
                {
                    return RedirectToAction("Detail", "GoodsReceipt", new { ID = goodsReceiptInfo.GoodsReceiptID });
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            return RedirectToAction("Index", "GoodsReceipt");
        }

        public ActionResult Delete(int id)
        {
            new GoodsReceiptInfo().Delete(id);
            return RedirectToAction("Detail", "GoodsReceipt");
        }
    }
}