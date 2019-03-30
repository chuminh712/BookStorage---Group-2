using BookStorage.Models;
using System.Web.Mvc;

namespace BookStorage.Controllers
{
    public class GoodsReceiptController : Controller
    {
        // GET: GoodsReceipt
        public ActionResult Index(string searchString, int page = 1, int pageSize = 2)
        {
            var dao = new GoodsReceipt();
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
        public ActionResult Create(GoodsReceipt goodsReceipt)
        {
            if (ModelState.IsValid)
            {
                var dao = new GoodsReceipt();
                int id = dao.Insert(goodsReceipt);
                if (id > 0)
                {
                    return RedirectToAction("Index", "GoodsReceipt");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm phiếu xuất không thành công");
                }
            }
            SetViewBag();
            return View("Index");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var dao = new GoodsReceipt();
            var goodsReceipt = dao.GetByID(id);
            SetViewBag(goodsReceipt.SupplierID);
            return View(goodsReceipt);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(GoodsReceipt goodsReceipt)
        {
            if (ModelState.IsValid)
            {
                var dao = new GoodsReceipt();
                var result = dao.Update(goodsReceipt);
                if (result)
                {
                    return RedirectToAction("Index", "GoodsReceipt");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật không thành công");
                }
            }
            SetViewBag(goodsReceipt.SupplierID);
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new GoodsReceipt().Delete(id);
            return RedirectToAction("Index");
        }

        public void SetViewBag(int? supplierID = null)
        {
            var dao = new Supplier();
            ViewBag.SupplierID = new SelectList(dao.ListAll(), "ID", "Name", supplierID);
        }

        public JsonResult GetBookPrice(int id)
        {
            var dao = new Book();
            var bookPrice = dao.GetByID(id).Price;
            return Json(bookPrice);
        }
    }
}