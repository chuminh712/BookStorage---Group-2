namespace BookStorage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GoodsReceipt")]
    public partial class GoodsReceipt
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GoodsReceipt()
        {
            GoodsReceiptInfoes = new HashSet<GoodsReceiptInfo>();
        }

        public int ID { get; set; }

        [StringLength(500)]
        public string DelivererName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        public int? SupplierID { get; set; }

        [StringLength(50)]
        public string Debit { get; set; }

        [StringLength(50)]
        public string Credit { get; set; }

        [StringLength(200)]
        public string Code { get; set; }

        public bool Status { get; set; }

        public virtual Supplier Supplier { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoodsReceiptInfo> GoodsReceiptInfoes { get; set; }
    }
}
