using BookStorage.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookStorage.ViewModels
{
    public class ProfitModel
    {
        public int ID { get; set; }

        [NotMapped]
        public List<GoodsReceipt> GoodsReceipt { get; set; }

        [NotMapped]
        public List<GoodsIssue> GoodsIssue { get; set; }

        [DisplayFormat(DataFormatString = "#.##0")]
        public decimal? GoodsReceiptTotal { get; set; }

        [DisplayFormat(DataFormatString = "#.##0")]
        public decimal? GoodsIssueTotal { get; set; }

        [DisplayFormat(DataFormatString = "#.##0")]
        public decimal? TotalProfit { get; set; }

        public ProfitModel GetProfitInfo(DateTime? fromDate, DateTime? toDate)
        {
            var goodsReceiptDao = new GoodsReceipt();
            var goodsIssueDao = new GoodsIssue();
            decimal? totalProfit = 0;
            decimal? goodsReceiptTotal = 0;
            decimal? goodsIssueTotal = 0;
            List<GoodsReceipt> goodsReceiptList = goodsReceiptDao.GetGoodsReceiptList(fromDate, toDate);
            List<GoodsIssue> goodsIssueList = goodsIssueDao.GetGoodsIssueList(fromDate, toDate);
            if (goodsReceiptList != null)
            {
                foreach (var item in goodsReceiptList)
                {
                    goodsReceiptTotal = goodsReceiptTotal + item.TotalPrice;
                }
            }
            if (goodsIssueList != null)
            {
                foreach (var item in goodsIssueList)
                {
                    goodsIssueTotal = goodsIssueTotal + item.TotalPrice;
                }
            }

            totalProfit = goodsReceiptTotal - goodsIssueTotal;

            ProfitModel profitModel = new ProfitModel();
            profitModel.GoodsReceipt = goodsReceiptList;
            profitModel.GoodsIssue = goodsIssueList;
            profitModel.GoodsReceiptTotal = goodsReceiptTotal;
            profitModel.GoodsIssueTotal = goodsIssueTotal;
            profitModel.TotalProfit = totalProfit;
            return profitModel;
        }
    }
}
