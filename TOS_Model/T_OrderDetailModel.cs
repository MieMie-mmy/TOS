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

        public int AdminCD { get; set; }

        public int OrderItem { get; set; }

        public int StockItem { get; set; }

        public int PlanStock { get; set; }

        public decimal SalePrice { get; set; }

        public decimal TotalAmount { get; set; }

        public int OrderStatus { get; set; }

        public int DeliveryFlg { get; set; }

        public decimal Shippingfee { get; set; }

        public String DeliveryCompanyCD { get; set; }

        public String Memo { get; set; }

        public String CustomerCD { get; set; }

        public DateTime? AvailableShippingDate { get; set; }

        public DateTime? ShippingDate { get; set; }

        public DateTime? OrderDateTime { get; set; }

     
    }
}
