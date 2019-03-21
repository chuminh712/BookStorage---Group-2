namespace BookStorage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GoodsIssueInfo")]
    public partial class GoodsIssueInfo
    {
        public int ID { get; set; }

        public int? BookID { get; set; }

        public int? GoodsIssueID { get; set; }

        public int? ReceiptQuantity { get; set; }

        public int? RealQuantity { get; set; }

        public decimal? TotalPrice { get; set; }
    }
}
