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
