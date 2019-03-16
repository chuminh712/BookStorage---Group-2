namespace BookStorage.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("GoodsReceiptInfo")]
    public partial class GoodsReceiptInfo
    {
        public int ID { get; set; }

        public int? BookID { get; set; }

        public int? GoodsReceiptID { get; set; }

        public int? ReceiptQuantity { get; set; }

        public int? RealQuantity { get; set; }

        public decimal? TotalPrice { get; set; }

        public virtual Book Book { get; set; }

        public virtual GoodsReceipt GoodsReceipt { get; set; }
    }
}
