namespace BookStorage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("GoodsReceiptInfo")]
    public partial class GoodsReceiptInfo
    {
        BookStorageDbContext db = null;

        public GoodsReceiptInfo()
        {
            db = new BookStorageDbContext();
        }

        public int Insert(GoodsReceiptInfo entity)
        {
            var goodsReceipt = db.GoodsReceipts.Find(entity.GoodsReceiptID);
            entity.Book = db.Books.Where(x => x.Code == entity.Book.Code).FirstOrDefault();
            entity.Book.Quantity -= entity.RealQuantity;
            entity.BookTotalPrice = entity.Book.Price * entity.RealQuantity;
            goodsReceipt.TotalPrice += entity.BookTotalPrice;
            db.GoodsReceiptInfoes.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(GoodsReceiptInfo entity)
        {
            try
            {
                var goodsReceiptInfo = db.GoodsReceiptInfoes.Find(entity.ID);
                var goodsReceipt = db.GoodsReceipts.Find(entity.GoodsReceiptID);
                int? compareQuantity = 0;

                goodsReceiptInfo.ReceiptQuantity = entity.ReceiptQuantity;
                if(goodsReceiptInfo.RealQuantity > entity.RealQuantity)
                {
                    compareQuantity = goodsReceiptInfo.RealQuantity - entity.RealQuantity;
                    goodsReceiptInfo.Book.Quantity -= compareQuantity;
                    goodsReceipt.TotalPrice -= compareQuantity * goodsReceiptInfo.Book.Price;
                }
                else if (goodsReceiptInfo.RealQuantity < entity.RealQuantity)
                {
                    compareQuantity = entity.RealQuantity - goodsReceiptInfo.RealQuantity;
                    goodsReceiptInfo.Book.Quantity += compareQuantity;
                    goodsReceipt.TotalPrice += compareQuantity * goodsReceiptInfo.Book.Price;
                }
                goodsReceiptInfo.RealQuantity = entity.RealQuantity;
                goodsReceiptInfo.BookTotalPrice = entity.RealQuantity * goodsReceiptInfo.Book.Price;
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
                var goodsReceiptInfo = db.GoodsReceiptInfoes.Find(id);
                var goodsReceipt = db.GoodsReceipts.Find(goodsReceiptInfo.GoodsReceiptID);
                goodsReceiptInfo.Book.Quantity -= goodsReceiptInfo.RealQuantity;
                goodsReceipt.TotalPrice -= goodsReceiptInfo.BookTotalPrice;
                db.GoodsReceiptInfoes.Remove(goodsReceiptInfo);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public GoodsReceiptInfo GetByID(int id)
        {
            return db.GoodsReceiptInfoes.Find(id);
        }

        public int ID { get; set; }

        public int? BookID { get; set; }
        public virtual Book Book { get; set; }

        public int? GoodsReceiptID { get; set; }
        public virtual GoodsReceipt GoodsReceipt { get; set; }

        public int? ReceiptQuantity { get; set; }

        public int? RealQuantity { get; set; }

        public decimal? BookTotalPrice { get; set; }
    }
}
