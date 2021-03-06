//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TOS_DL
{
    using System;
    using System.Collections.Generic;
    
    public partial class T_OrderDetail
    {
        public string OrderID { get; set; }
        public int AdminCD { get; set; }
        public int OrderItem { get; set; }
        public int StockItem { get; set; }
        public int PlanStock { get; set; }
        public decimal SalePrice { get; set; }
        public decimal TotalAmount { get; set; }
        public byte OrderStatus { get; set; }
        public Nullable<byte> DeliveryFlg { get; set; }
        public Nullable<decimal> Shippingfee { get; set; }
        public string DeliveryCompanyCD { get; set; }
        public string Memo { get; set; }
        public System.DateTime AvailableShippingDate { get; set; }
        public Nullable<System.DateTime> ShippingDate { get; set; }
        public string CustomerCD { get; set; }
        public Nullable<System.DateTime> OrderDateTime { get; set; }
        public string UpdateOperator { get; set; }
        public Nullable<System.DateTime> UpdateDateTime { get; set; }
    }
}
