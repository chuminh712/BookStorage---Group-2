namespace BookStorage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GoodsIssue")]
    public partial class GoodsIssue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public GoodsIssue()
        {
            GoodsIssueInfoes = new HashSet<GoodsIssueInfo>();
        }

        public int ID { get; set; }

        [StringLength(500)]
        public string ReciverName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? CreatedDate { get; set; }

        public int? CustomerID { get; set; }

        [StringLength(50)]
        public string Debit { get; set; }

        [StringLength(50)]
        public string Credit { get; set; }

        [StringLength(200)]
        public string Code { get; set; }

        [StringLength(800)]
        public string OutputReason { get; set; }

        public bool Status { get; set; }

        public virtual Customer Customer { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<GoodsIssueInfo> GoodsIssueInfoes { get; set; }
    }
}
