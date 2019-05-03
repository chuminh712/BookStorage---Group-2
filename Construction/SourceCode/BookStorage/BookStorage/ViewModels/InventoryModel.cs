using BookStorage.Models;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookStorage.ViewModels
{
    public class InventoryModel
    {
        BookStorageDbContext db = null;

        public InventoryModel()
        {
            db = new BookStorageDbContext();
        }

        public string BookName { get; set; }
        public string Image { get; set; }
        public string Unit { get; set; }
        public string Code { get; set; }
        public string Publisher { get; set; }
        public int? Quantity { get; set; }
        public DateTime? CreateDate { get; set; }
        public int? GoodsReceiptQuantity { get; set; }
        public int? GoodsIssueQuantity { get; set; }

        public IEnumerable<InventoryModel> GetInventoryInfo(string searchString)
        {
            var model = (from a in db.Books
                         join b in db.GoodsReceiptInfoes
                         on a.ID equals b.BookID
                         into inventory
                         join c in db.GoodsIssueInfoes
                         on a.ID equals c.BookID
                         into inventory2
                         select new InventoryModel()
                         {
                             BookName = a.Name,
                             Image = a.Image,
                             Code = a.Code,
                             Unit = a.Unit.Name,
                             Publisher = a.Publisher,
                             Quantity = a.Quantity,
                             CreateDate = a.CreatedDate,
                             GoodsReceiptQuantity = inventory.Sum(x => x.RealQuantity),
                             GoodsIssueQuantity = inventory2.Sum(c => c.RealQuantity)
                         });
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Code.Contains(searchString) || x.BookName.Contains(searchString) || x.Publisher.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreateDate).ToPagedList(1, 10);
        }
    }
}