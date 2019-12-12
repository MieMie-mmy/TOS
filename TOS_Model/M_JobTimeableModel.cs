using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOS_Model
{
    public class M_JobTimeableModel: BaseModel
    {
        public string CompanyCD { get; set; }
        public string OrderAblePeriod { get; set; }
        public string CancelAblePeriod { get; set; }
        public string ExceptionAboutPeriod { get; set; }
    }
}
