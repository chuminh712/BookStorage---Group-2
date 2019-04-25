namespace BookStorage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using PagedList;
    using System.Data.Entity.Spatial;
    using System.Linq;
    using System.Web.Mvc;

    [Table("GoodsIssueInfo")]
    public partial class GoodsIssueInfo
    {

        BookStorageDbContext db = null;

        public GoodsIssueInfo()
        {
            db = new BookStorageDbContext();
        }

        public int Insert(GoodsIssueInfo entity)
        {
            var goodsIssue = db.GoodsIssues.Find(entity.GoodsIssueID);
            entity.Book = db.Books.Where(x => x.Code == entity.Book.Code).FirstOrDefault();
            entity.Book.Quantity -= entity.RealQuantity;
            entity.TotalPrice = entity.Book.Price * entity.RealQuantity;
            goodsIssue.TotalPrice += entity.TotalPrice;
            db.GoodsIssueInfoes.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(GoodsIssueInfo entity)
        {
            try
            {
                var GoodsIssueInfo = db.GoodsIssueInfoes.Find(entity.ID);
                var goodsIssue = db.GoodsIssues.Find(entity.GoodsIssueID);
                int? compareQuantity = 0;

                GoodsIssueInfo.ReceiptQuantity = entity.ReceiptQuantity;
                if (GoodsIssueInfo.RealQuantity > entity.RealQuantity)
                {
                    compareQuantity = GoodsIssueInfo.RealQuantity - entity.RealQuantity;
                    GoodsIssueInfo.Book.Quantity -= compareQuantity;
                    goodsIssue.TotalPrice -= compareQuantity * GoodsIssueInfo.Book.Price;
                }
                else if (GoodsIssueInfo.RealQuantity < entity.RealQuantity)
                {
                    compareQuantity = entity.RealQuantity - GoodsIssueInfo.RealQuantity;
                    GoodsIssueInfo.Book.Quantity += compareQuantity;
                    goodsIssue.TotalPrice += compareQuantity * GoodsIssueInfo.Book.Price;
                }
                GoodsIssueInfo.RealQuantity = entity.RealQuantity;
                GoodsIssueInfo.TotalPrice = entity.RealQuantity * GoodsIssueInfo.Book.Price;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var GoodsIssueInfo = db.GoodsIssueInfoes.Find(id);
                var goodsIssue = db.GoodsIssues.Find(GoodsIssueInfo.GoodsIssueID);
                GoodsIssueInfo.Book.Quantity -= GoodsIssueInfo.RealQuantity;
                goodsIssue.TotalPrice -= GoodsIssueInfo.TotalPrice;
                db.GoodsIssueInfoes.Remove(GoodsIssueInfo);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public GoodsIssueInfo GetByID(int id)
        {
            return db.GoodsIssueInfoes.Find(id);
        }



        public int ID { get; set; }

        public int? BookID { get; set; }
        public virtual Book Book { get; set; }

        public int? GoodsIssueID { get; set; }
        public virtual GoodsIssue GoodsIssue { get; set; }

        public int? ReceiptQuantity { get; set; }

        public int? RealQuantity { get; set; }


        [DisplayFormat(DataFormatString = "#.##0")]
        public decimal? TotalPrice { get; set; }
    }
}
