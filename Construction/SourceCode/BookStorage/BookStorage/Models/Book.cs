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
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Book()
        {
            GoodsIssueInfoes = new HashSet<GoodsIssueInfo>();
            GoodsReceiptInfoes = new HashSet<GoodsReceiptInfo>();
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

        public virtual BookCategory BookCategory { get; set; }

        public virtual Unit Unit { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoodsIssueInfo> GoodsIssueInfoes { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoodsReceiptInfo> GoodsReceiptInfoes { get; set; }
    }
}
