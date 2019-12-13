using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOS_Model
{
    public class M_CompanyShipping:BaseModel
    {
        public string CompanyCD { get; set; }
        public int ShippingID { get; set; }
        public string ShippingName { get; set; }
        public string ZipCD1 { get; set; }
        public string ZipCD2 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string TelephoneNO { get; set; }
        public string FaxNO { get; set; }

    }
}
