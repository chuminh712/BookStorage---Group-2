namespace BookStorage.Models
{
    using PagedList;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Supplier")]
    public partial class Supplier
    {
        BookStorageDbContext db = null;

        public Supplier()
        {
            db = new BookStorageDbContext();
        }

        public int Insert(Supplier entity)
        {
            db.Suppliers.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Supplier entity)
        {
            try
            {
                var supplier = db.Suppliers.Find(entity.ID);
                supplier.Name = entity.Name;
                supplier.Address = entity.Address;
                supplier.Phone = entity.Phone;
                supplier.Email = entity.Email;
                supplier.ContractedDate = entity.ContractedDate;
                supplier.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Supplier> ListAllPage(string searchString, int page, int pageSize)
        {
            IQueryable<Supplier> model = db.Suppliers;
            if (!string.IsNullOrEmpty(searchString))
            {
                model = model.Where(x => x.Name.Contains(searchString) || x.Phone.Contains(searchString) || x.Address.Contains(searchString));
            }
            return model.OrderByDescending(x => x.ContractedDate).ToPagedList(page, pageSize);
        }

        public bool Delete(int id)
        {
            try
            {
                var supplier = db.Suppliers.Find(id);
                db.Suppliers.Remove(supplier);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Supplier GetByID(int id)
        {
            return db.Suppliers.Find(id);
        }

        public List<Supplier> ListAll()
        {
            return db.Suppliers.Where(x => x.Status == true).ToList();
        }

        public int ID { get; set; }

        [StringLength(500)]
        [Display(Name = "Nhà cung cấp")]
        public string Name { get; set; }

        [StringLength(500)]
        [Display(Name = "Địa chỉ")]
        public string Address { get; set; }

        [StringLength(50)]
        [Display(Name = "Số điện thoại")]
        public string Phone { get; set; }

        [StringLength(500)]
        public string Email { get; set; }

        [Column(TypeName = "date")]
        [Display(Name = "Ngày hợp tác")]
        public DateTime? ContractedDate { get; set; }

        [Display(Name = "Trạng thái")]
        public bool Status { get; set; }
    }
}
