using BookStorage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookStorage.Controllers
{
    public class GoodsInfoController : BaseController
    {
        [HttpGet]
        public ActionResult Create(int id)
        {
            var goodsIssueInfo = new GoodsIssueInfo() { GoodsIssueID = id };
            return View(goodsIssueInfo);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(GoodsIssueInfo goodsIssueInfo)
        {
            if (ModelState.IsValid)
            {
                var dao = new GoodsIssueInfo();
                int id = dao.Insert(goodsIssueInfo);
                if (id > 0)
                {
                    return RedirectToAction("Detail", "GoodsIssue", new { ID = goodsIssueInfo.GoodsIssueID });
                }
                else
                {
                    ModelState.AddModelError("", "Thêm chi tiết phiếu xuất không thành công");
                }
            }
            return RedirectToAction("Index", "GoodsIssue");
        }
    }
}