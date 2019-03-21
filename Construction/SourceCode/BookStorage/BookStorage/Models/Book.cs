namespace BookStorage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Book")]
    public partial class Book
    {
        BookStorageDbContext db = null;

        public Book()
        {
            db = new BookStorageDbContext();
        }

        public Book GetByID(int id)
        {
            return db.Books.Find(id);
        }

        public int ID { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public int? UnitID { get; set; }

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
