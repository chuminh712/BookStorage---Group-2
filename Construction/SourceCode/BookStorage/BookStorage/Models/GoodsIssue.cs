namespace BookStorage.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("GoodsIssue")]
    public partial class GoodsIssue
    {
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
    }
}
