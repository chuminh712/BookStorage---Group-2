namespace BookStorage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Unit")]
    public partial class Unit
    {
        BookStorageDbContext db = null;

        public Unit()
        {
            db = new BookStorageDbContext();
        }

        public int ID { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        public List<Unit> ListAll()
        {
            return db.Units.ToList();
        }

        public Unit GetByID(int id)
        {
            return db.Units.Find(id);
        }
    }
}
