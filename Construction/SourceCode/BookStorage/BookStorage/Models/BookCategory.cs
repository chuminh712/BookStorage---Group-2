namespace BookStorage.Models
{
    using PagedList;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("BookCategory")]
    public partial class BookCategory
    {
        BookStorageDbContext db = null;

        public BookCategory()
        {
            db = new BookStorageDbContext();
        }

        public List<BookCategory> ListAll()
        {
            return db.BookCategories.ToList();
        }

        public int ID { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public int Insert(BookCategory entity)
        {
            db.BookCategories.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public BookCategory GetByID(int id)
        {
            return db.BookCategories.Find(id);
        }

        public bool Update(BookCategory entity)
        {
            try
            {
                var book_category = db.BookCategories.Find(entity.ID);
                book_category.Name = entity.Name;
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
                var book_category = db.BookCategories.Find(id);
                db.BookCategories.Remove(book_category);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<BookCategory> ListAllPage(string searchString, int page, int pageSize)
        {
            IQueryable<BookCategory> model = db.BookCategories;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString));
            }
            return model.OrderByDescending(x => x.Name).ToPagedList(page, pageSize);
        }
    }
}
