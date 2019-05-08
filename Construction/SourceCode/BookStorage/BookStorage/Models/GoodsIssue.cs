namespace BookStorage.Models
{
    using System;
    using PagedList;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Web.Mvc;

    [Table("GoodsIssue")]
    public partial class GoodsIssue
    {
        BookStorageDbContext db = null;

        public GoodsIssue()
        {
            db = new BookStorageDbContext();
        }

        public int Insert(GoodsIssue entity)
        {
            if (entity.GoodsIssueInfo != null)
            {
                entity.GoodsIssueInfo.RemoveAll(x => x.BookID == null);
                foreach (var item in entity.GoodsIssueInfo)
                {
                    var book = db.Books.Find(item.BookID);
                    if (book != null)
                    {
                        book.Quantity -= item.RealQuantity;
                    }
                }
            }
            db.GoodsIssues.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(GoodsIssue entity)
        {
            try
            {
                var goodsIssue = db.GoodsIssues.Find(entity.ID);
                goodsIssue.Debit = entity.Debit;
                goodsIssue.CreatedDate = entity.CreatedDate;
                goodsIssue.Credit = entity.Credit;
                goodsIssue.Code = entity.Code;
                goodsIssue.Status = entity.Status;
                goodsIssue.OutputReason = entity.OutputReason;
                goodsIssue.Status = entity.Status;
                goodsIssue.ReciverName = entity.ReciverName;
                goodsIssue.CustomerID = entity.CustomerID;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<GoodsIssue> ListAllPage(string searchString, int page, int pageSize)
        {
            IQueryable<GoodsIssue> model = db.GoodsIssues;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Code.Contains(searchString) || x.ReciverName.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public bool Delete(int id)
        {
            try
            {
                var goodsIssue = db.GoodsIssues.Find(id);
                var goodsIssueInfoes = db.GoodsIssueInfoes.Where(x => x.GoodsIssueID == id);
                foreach (var item in goodsIssueInfoes)
                {
                    var book = db.Books.Find(item.BookID);
                    book.Quantity -= item.RealQuantity;
                }
                db.GoodsIssues.Remove(goodsIssue);
                db.GoodsIssueInfoes.RemoveRange(goodsIssueInfoes);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public GoodsIssue GetByID(int id)
        {
            return db.GoodsIssues.Find(id);
        }

        public List<GoodsIssue> GetGoodsIssueList(DateTime fromDate, DateTime toDate)
        {
            var goodsIssueList = db.GoodsIssues.AsQueryable();
            if (fromDate != null)
            {
                goodsIssueList = goodsIssueList.Where(x => x.CreatedDate >= fromDate).AsQueryable();
            }
            if (toDate != null)
            {
                goodsIssueList = goodsIssueList.Where(x => x.CreatedDate <= toDate).AsQueryable();
            }
            return goodsIssueList.ToList();
        }

        public int ID { get; set; }
        public virtual List<GoodsIssueInfo> GoodsIssueInfo { get; set; }

        [StringLength(500)]
        [Display(Name = "Người nhận")]
        public string ReciverName { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày tạo")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Khách hàng")]
        public int? CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        [StringLength(50)]
        [Display(Name = "Nợ")]
        public string Debit { get; set; }

        [StringLength(50)]
        [Display(Name = "Có")]
        public string Credit { get; set; }

        [StringLength(200)]
        [Display(Name = "Mã phiếu nhập")]
        public string Code { get; set; }

        [StringLength(800)]
        [Display(Name = "Lý do xuất")]
        public string OutputReason { get; set; }

        [Display(Name = "Tổng số tiền")]
        [DisplayFormat(DataFormatString = "#.##0")]
        public decimal? TotalPrice { get; set; }


        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

        //public int? BookID { get; internal set; }
        //public int? GoodsIssueID { get; internal set; }
        //public int? ReceiptQuantity { get; internal set; }
        //public int? RealQuantity { get; internal set; }
    }
}
