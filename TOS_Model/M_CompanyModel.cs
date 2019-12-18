using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOS_Model
{
    public class M_CompanyModel : BaseModel
    {
        public string CompanyCD { get; set; }
        public string Password { get; set; }
        public string CompanyName { get; set; }

        public int UserRole { get; set; }

        public string ShortName { get; set; }
        public string ZipCD1 { get; set; }
        public string ZipCD2 { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string TelephoneNo { get; set; }
        public string FaxNo { get; set; }
        public string PresidentName { get; set; }

        public int RankingFlg { get; set; }
       
    }
}
