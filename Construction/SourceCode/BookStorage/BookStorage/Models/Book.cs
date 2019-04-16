namespace BookStorage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using PagedList;
    using System.Linq;
    using System.Web.Mvc;

    [Table("Book")]
    public partial class Book
    {
        BookStorageDbContext db = null;

        public Book()
        {
            db = new BookStorageDbContext();
        }

        public int Insert(Book entity)
        {
            db.Books.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Book entity)
        {
            try
            {
                var book = db.Books.Find(entity.ID);
                book.Name = entity.Name;
                book.UnitID = entity.UnitID;
                book.Author = entity.Author;
                book.BookCategoryID = entity.BookCategoryID;
                book.Code = entity.Code;
                book.Image = entity.Image;
                book.Price = entity.Price;
                book.Publisher = entity.Publisher;
                book.Quantity = entity.Quantity;
                book.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Book> ListAllPage(string searchString, int page, int pageSize)
        {
            IQueryable<Book> model = db.Books;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Code.Contains(searchString) || x.Name.Contains(searchString) || x.Author.Contains(searchString));
            }
            return model.OrderByDescending(x => x.CreatedDate).ToPagedList(page, pageSize);
        }

        public bool Delete(int id)
        {
            try
            {
                var book = db.Books.Find(id);
                db.Books.Remove(book);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Book GetByID(int id)
        {
            return db.Books.Find(id);
        }

        public Book GetByCode(string code)
        {
            return db.Books.Where(x => x.Code == code).FirstOrDefault();
        }

        public int ID { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public int? UnitID { get; set; }
        public virtual Unit Unit { get; set; }

        [StringLength(500)]
        public string Author { get; set; }

        public int? BookCategoryID { get; set; }

        [StringLength(200)]
        public string Code { get; set; }

        [StringLength(500)]
        public string Image { get; set; }

        public decimal? Price { get; set; }

        [StringLength(500)]
        public string Publisher { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        public int? Quantity { get; set; }

        public bool Status { get; set; }
    }
}
