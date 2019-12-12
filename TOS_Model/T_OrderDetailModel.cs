using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOS_Model
{
 public class T_OrderDetailModel:BaseModel
    {
        public string OrderID { get; set; }

        public int OrderItem { get; set; }

        public int StockItem { get; set; }

        public string SalePrice { get; set; }

        public string TotalAmount { get; set; }

        public string OrderDateTime { get; set; }

        public string ShippingDate { get; set; }
    }
}
