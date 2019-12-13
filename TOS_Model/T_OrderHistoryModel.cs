using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOS_Model
{
   public class T_OrderHistoryModel
    {
        public string CompanyCD { get; set; }
        public string OrderID { get; set; }
        public decimal? TotalAmount { get; set; }
        public string ShippingName { get; set; }
        public DateTime? ShippingDate { get; set; }
        public  DateTime? AvailableShippingDate { get; set; }
        public DateTime? OrderDateTime { get; set; }
        
      
    }
    public class T_OrderHistoryModel2
    {

        public string MakerItemCD_gav { get; set; }
        public string ItemName_gav { get; set; }
        public string SizeName_gav { get; set; }
        public string ColorName_gav { get; set; }
        public int OrderItem_gav { get; set; }
        public int StockItem_gav { get; set; }
        public decimal SalePrice_gav { get; set; }
        public decimal TotalAmount_gav { get; set; }
        public DateTime OrderDateTime_gav { get; set; }
        public DateTime ShippingDate_gav { get; set; }
        public string Memo_gav { get; set; }


    }
}
