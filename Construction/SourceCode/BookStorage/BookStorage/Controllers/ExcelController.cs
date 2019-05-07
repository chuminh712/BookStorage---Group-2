using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Excel = Microsoft.Office.Interop.Excel;
using System.IO;
using BookStorage.Models;

namespace BookStorage.Controllers
{
    public class ExcelController : Controller
    {
        // GET: Excel
        public ActionResult ImportIndex()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Import(HttpPostedFileBase excelfile)
        {
            if (excelfile == null || excelfile.ContentLength == 0)
            {
                ViewBag.Error = "Please select a excel file<br>";
                return View("ImportIndex");
            }
            else
            {
                if (excelfile.FileName.EndsWith("xls") || excelfile.FileName.EndsWith("xlsx"))
                {
                    string fileName = Path.GetFileName(excelfile.FileName);
                    string path = Path.Combine(Server.MapPath("~/Content/"), fileName);
                    //string path = Server.MapPath("~/Content/" + excelfile.FileName);
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);

                    excelfile.SaveAs(path);

                    // Read data from excel file
                    Excel.Application application = new Excel.Application();
                    Excel.Workbook workbook = application.Workbooks.Open(path);
                    Excel.Worksheet worksheet = workbook.ActiveSheet;
                    Excel.Range range = worksheet.UsedRange;

                    List<Book> listBooks = new List<Book>();
                    for(int row = 3; row <= range.Rows.Count; row++)
                    {
                        Book p = new Book();
                        p.Name = ((Excel.Range)range.Cells[row, 2]).Text;
                        p.UnitID = int.Parse(((Excel.Range)range.Cells[row, 3]).Text);
                        p.Author = ((Excel.Range)range.Cells[row, 4]).Text;
                        p.BookCategoryID = int.Parse(((Excel.Range)range.Cells[row, 5]).Text);
                        p.Code = ((Excel.Range)range.Cells[row, 6]).Text;
                        p.Image = ((Excel.Range)range.Cells[row, 7]).Text;
                        p.Publisher = ((Excel.Range)range.Cells[row, 9]).Text;
                        listBooks.Add(p);
                    }
                    ViewBag.ListBooks = listBooks;
                    workbook.Close(path);
                    return View("Success");
                }
                else
                {
                    ViewBag.Error = "File type is incorrect<br>";
                    return View("ImportIndex");
                }
            }
            
        }
    }
}