namespace BookStorage.Models
{
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

    }
}
