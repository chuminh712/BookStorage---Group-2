namespace BookStorage.Models
{
    using PagedList;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Web.Mvc;

    [Table("GoodsReceipt")]
    public partial class GoodsReceipt
    {
        BookStorageDbContext db = null;

        public GoodsReceipt()
        {
            db = new BookStorageDbContext();
        }

        public int Insert(GoodsReceipt entity)
        {
            if (entity.GoodsReceiptInfo != null)
            {
                entity.GoodsReceiptInfo.RemoveAll(x => x.BookID == null);
                foreach (var item in entity.GoodsReceiptInfo)
                {
                    var book = db.Books.Find(item.BookID);
                    if (book != null)
                    {
                        book.Quantity += item.RealQuantity;
                    }
                }
            }
            db.GoodsReceipts.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(GoodsReceipt entity)
        {
            try
            {
                var goodsReceipt = db.GoodsReceipts.Find(entity.ID);
                goodsReceipt.DelivererName = entity.DelivererName;
                goodsReceipt.CreatedDate = entity.CreatedDate;
                goodsReceipt.SupplierID = entity.SupplierID;
                goodsReceipt.Debit = entity.Debit;
                goodsReceipt.Credit = entity.Credit;
                goodsReceipt.Code = entity.Code;
                goodsReceipt.TotalPrice = entity.TotalPrice;
                goodsReceipt.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<GoodsReceipt> ListAllPage(string searchString, int page, int pageSize)
        {
            IQueryable<GoodsReceipt> model = db.GoodsReceipts;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Code.Contains(searchString) || x.Supplier.Name.Contains(searchString) || x.DelivererName.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public bool Delete(int id)
        {
            try
            {
                var goodsReceipt = db.GoodsReceipts.Find(id);
                var goodsReceiptInfoes = db.GoodsReceiptInfoes.Where(x => x.GoodsReceiptID == id);
                foreach(var item in goodsReceiptInfoes)
                {
                    var book = db.Books.Find(item.BookID);
                    book.Quantity -= item.RealQuantity;
                }
                db.GoodsReceipts.Remove(goodsReceipt);
                db.GoodsReceiptInfoes.RemoveRange(goodsReceiptInfoes);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public GoodsReceipt GetByID(int id)
        {
            return db.GoodsReceipts.Find(id);
        }

        public int ID { get; set; }
        public virtual List<GoodsReceiptInfo> GoodsReceiptInfo { get; set; }

        [StringLength(500)]
        [Display(Name = "Người giao")]
        public string DelivererName { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Nhà cung cấp")]
        public int? SupplierID { get; set; }
        public virtual Supplier Supplier { get; set; }

        [StringLength(50)]
        [Display(Name = "Nợ")]
        public string Debit { get; set; }

        [StringLength(50)]
        [Display(Name = "Có")]
        public string Credit { get; set; }

        [StringLength(200)]
        [Display(Name = "Mã phiếu nhập")]
        public string Code { get; set; }

        [Display(Name = "Tổng số tiền")]
        [DisplayFormat(DataFormatString = "#.##0")]
        public decimal? TotalPrice { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}
