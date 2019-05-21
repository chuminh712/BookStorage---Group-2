using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;
using System.Web.Mvc;
using BookStorage.Models;
using System.Runtime.InteropServices;

namespace BookStorage.Controllers
{
    public class BookCategoryController : BaseController
    {
        // GET: BookCategory
        public ActionResult Index(string searchString, int page = 1, int pageSize = 5)
        {
            var dao = new BookCategory();
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

        public ActionResult ImportIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase excelfile)
        {
            if (ModelState.IsValid)
            {
                if (excelfile == null || excelfile.ContentLength == 0)
                {
                    SetAlert("Mời bạn chọn file excel", "danger");
                    return View("ImportIndex");
                }
                else
                {
                    if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
                    {
                        string fileName = Path.GetFileName(excelfile.FileName);
                        string path = Path.Combine(Server.MapPath("~/Data/files/"), fileName);
                        excelfile.SaveAs(path);
                        // Read data from excel file
                        Excel.Application application = new Excel.Application();
                        Excel.Workbook workbook = application.Workbooks.Open(path);
                        Excel.Worksheet worksheet = workbook.ActiveSheet;
                        Excel.Range range = worksheet.UsedRange;

                        List<BookCategory> listCategories = new List<BookCategory>();
                        for (int row = 2; row <= range.Rows.Count; row++)
                        {
                            BookCategory bc = new BookCategory();
                            bc.Name = ((Excel.Range)range.Cells[row, 1]).Text;
                            listCategories.Add(bc);
                            bc.Insert(bc);
                        }
                        ViewBag.ListCategories = listCategories;
                        workbook.Close(path);
                        SetAlert("Import file thành công", "success");
                        return View("ImportIndex");
                    }
                    else
                    {
                        SetAlert("Đây không phải file excel", "danger");
                        return View("ImportIndex");
                    }
                }
            }
            return View("ImportIndex");
        }

        public ActionResult Export()
        {
            try
            {
                string path = Server.MapPath("~/Data/files/");
                Excel.Application application = new Excel.Application();
                Excel.Workbook workbook = application.Workbooks.Add(System.Reflection.Missing.Value);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                BookCategory bookCategory = new BookCategory();
                worksheet.Cells[1, 1] = "Name";
                int row = 2;
                foreach (BookCategory b in bookCategory.ListAll())
                {
                    worksheet.Cells[row, 1] = b.Name;
                    row++;
                }
                worksheet.get_Range("A1", "A1").EntireColumn.AutoFit();
                workbook.SaveAs(path + "CategoryExport.xlsx");
                workbook.Close();
                Marshal.ReleaseComObject(workbook);
                application.Quit();
                Marshal.FinalReleaseComObject(application);
                SetAlert("Export file thành công", "success");
                return RedirectToAction("Index");
            }
            catch
            {
                SetAlert("Export file thất bại", "danger");
                return RedirectToAction("Index");
            }
        }
    }
}