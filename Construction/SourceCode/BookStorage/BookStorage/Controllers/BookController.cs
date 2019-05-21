using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using System.Web.Mvc;
using BookStorage.Models;
using System.Runtime.InteropServices;

namespace BookStorage.Controllers
{
    public class BookController : BaseController
    {
        // GET: Book
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
        public ActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                var dao = new Book();
                int id = dao.Insert(book);
                if (id > 0)
                {
                    SetAlert("Thêm sách thành công", "success");
                    return RedirectToAction("Index", "Book");
                }
                else
                {
                    SetAlert("Thêm sách không thành công", "danger");
                    return RedirectToAction("Index", "Book");
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
            SetViewBag(book.BookCategoryID);
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
                    SetAlert("Cập nhật sách thành công", "success");
                    return RedirectToAction("Index", "Book");
                }
                else
                {
                    SetAlert("Cập nhật sách không thành công", "danger");
                    return RedirectToAction("Index", "Book");
                }
            }
            SetViewBag(book.BookCategoryID);
            return View("Index");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new Book().Delete(id);
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
                        if (System.IO.File.Exists(path))
                            System.IO.File.Delete(path);
                        excelfile.SaveAs(path);
                        // Read data from excel file
                        Excel.Application application = new Excel.Application();
                        Excel.Workbook workbook = application.Workbooks.Open(path);
                        Excel.Worksheet worksheet = workbook.ActiveSheet;
                        Excel.Range range = worksheet.UsedRange;

                        List<Book> listBooks = new List<Book>();
                        for (int row = 2; row <= range.Rows.Count; row++)
                        {
                            Book b = new Book();
                            b.Name = ((Excel.Range)range.Cells[row, 1]).Text;
                            b.UnitID = int.Parse(((Excel.Range)range.Cells[row, 2]).Text);
                            b.Unit = new Unit().GetByID(int.Parse(((Excel.Range)range.Cells[row, 2]).Text));
                            b.Author = ((Excel.Range)range.Cells[row, 3]).Text;
                            b.BookCategoryID = int.Parse(((Excel.Range)range.Cells[row, 4]).Text);
                            b.BookCategory = new BookCategory().GetByID(int.Parse(((Excel.Range)range.Cells[row, 4]).Text));
                            b.Code = ((Excel.Range)range.Cells[row, 5]).Text;
                            b.Price = decimal.Parse(((Excel.Range)range.Cells[row, 6]).Text);
                            b.Image = ((Excel.Range)range.Cells[row, 7]).Text;
                            b.Publisher = ((Excel.Range)range.Cells[row, 8]).Text;
                            b.Quantity = int.Parse(((Excel.Range)range.Cells[row, 10]).Text);
                            b.Status = (bool)((Excel.Range)range.Cells[row, 11]).Value2;
                            listBooks.Add(b);
                            b.Insert(b);
                        }
                        ViewBag.ListBooks = listBooks;
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
                Book book = new Book();
                worksheet.Cells[1, 1] = "Name";
                worksheet.Cells[1, 2] = "UnitID";
                worksheet.Cells[1, 3] = "Author";
                worksheet.Cells[1, 4] = "BookCategoryID";
                worksheet.Cells[1, 5] = "Code";
                worksheet.Cells[1, 6] = "Price";
                worksheet.Cells[1, 7] = "Image";
                worksheet.Cells[1, 8] = "Publisher";
                worksheet.Cells[1, 9] = "CreatedDate";
                worksheet.Cells[1, 10] = "Quantity";
                worksheet.Cells[1, 11] = "Status";
                int row = 2;
                foreach (Book b in book.ListAll())
                {
                    worksheet.Cells[row, 1] = b.Name;
                    worksheet.Cells[row, 2] = b.UnitID;
                    worksheet.Cells[row, 3] = b.Author;
                    worksheet.Cells[row, 4] = b.BookCategoryID;
                    worksheet.Cells[row, 5] = b.Code;
                    worksheet.Cells[row, 6] = b.Price;
                    worksheet.Cells[row, 7] = b.Image;
                    worksheet.Cells[row, 8] = b.Publisher;
                    worksheet.Cells[row, 9] = b.CreatedDate;
                    worksheet.Cells[row, 10] = b.Quantity;
                    worksheet.Cells[row, 11] = b.Status;
                    row++;
                }
                worksheet.get_Range("A1", "L1").EntireColumn.AutoFit();
                workbook.SaveAs(path + "Export.xlsx");
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
