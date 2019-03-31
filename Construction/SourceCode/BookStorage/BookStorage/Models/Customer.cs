namespace BookStorage.Models
{
    using PagedList;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Customer")]
    public partial class Customer
    {
        BookStorageDbContext db = null;

        public Customer()
        {
            db = new BookStorageDbContext();
        }

        public int Insert(Customer entity)
        {
            db.Customers.Add(entity);
            db.SaveChanges();
            return entity.ID;
        }

        public bool Update(Customer entity)
        {
            try
            {
                var customer = db.Customers.Find(entity.ID);
                customer.Name = entity.Name;
                customer.Address = entity.Address;
                customer.Phone = entity.Phone;
                customer.Email = entity.Email;
                customer.ContractedDate = entity.ContractedDate;
                customer.Status = entity.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public IEnumerable<Customer> ListAllPage(string searchString, int page, int pageSize)
        {
            IQueryable<Customer> model = db.Customers;
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
                var customer = db.Customers.Find(id);
                db.Customers.Remove(customer);
                db.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Customer GetByID(int id)
        {
            return db.Customers.Find(id);
        }

        public List<Customer> ListAll()
        {
            return db.Customers.Where(x => x.Status == true).ToList();
        }

        public int ID { get; set; }

        [StringLength(500)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        [StringLength(500)]
        public string Email { get; set; }

        [Column(TypeName = "date")]
        public DateTime? ContractedDate { get; set; }

        public bool Status { get; set; }
    }
}
