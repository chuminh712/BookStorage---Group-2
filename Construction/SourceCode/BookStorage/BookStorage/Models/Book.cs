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
    using System.Web;

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
            if(!entity.CreatedDate.HasValue)
            {
                entity.CreatedDate = DateTime.Today;
            }
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

        public List<Book> ListAll()
        {
            return db.Books.Where(x => x.Status == true).ToList();
        }

        public int ID { get; set; }

        [StringLength(500)]
        [Display(Name = "Tên sách")]
        public string Name { get; set; }

        [Display(Name = "Đơn vị")]
        public int? UnitID { get; set; }
        public virtual Unit Unit { get; set; }

        [StringLength(500)]
        [Display(Name = "Tác giả")]
        public string Author { get; set; }

        [Display(Name = "Danh mục sách")]
        public int? BookCategoryID { get; set; }
        public virtual BookCategory BookCategory{ get; set; }

        [StringLength(200)]
        [Display(Name = "Mã sách")]
        public string Code { get; set; }

        [Display(Name = "Giá tiền")]
        public decimal? Price { get; set; }

        [StringLength(500)]
        [Display(Name = "Hình ảnh")]
        public string Image { get; set; }

        [StringLength(500)]
        [Display(Name = "Nhà xuất bản")]
        public string Publisher { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        [Display(Name = "Số lượng")]
        public int? Quantity { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }

    }
}
