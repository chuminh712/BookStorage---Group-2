namespace BookStorage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using PagedList;
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
                db.GoodsIssues.Remove(goodsIssue);
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

        

        public int ID { get; set; }
        public virtual List<GoodsIssue> GoodsIssues { get; set; }

        [StringLength(500)]
        public string ReciverName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        public int? CustomerID { get; set; }

        [StringLength(50)]
        public string Debit { get; set; }

        [StringLength(50)]
        public string Credit { get; set; }

        [StringLength(200)]
        public string Code { get; set; }

        [StringLength(800)]
        public string OutputReason { get; set; }

        public bool Status { get; set; }
    }
}
