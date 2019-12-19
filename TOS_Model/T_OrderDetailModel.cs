using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOS_Model
{
    public class T_OrderDetailModel : BaseModel
    {
        public string OrderID { get; set; }

        public int OrderItem { get; set; }

        public int StockItem { get; set; }

        public decimal SalePrice { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime? OrderDateTime { get; set; }

        public DateTime? ShippingDate { get; set; }
    }
}
