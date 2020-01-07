using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOS_Model
{
    public class OrderInputViewModel
    {
        public T_OrderHeaderModel T_OH { get; set; }
        public IEnumerable<T_OrderDetailModel> T_OrderDetailModels { get; set; }
    }
}
