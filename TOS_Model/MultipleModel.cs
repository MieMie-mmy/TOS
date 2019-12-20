using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TOS_Model
{
    public class MultipleModel
    {

        public M_CompanyModel ComModel{ get; set; }
        public List<M_CompanyShippingModel> ShippingModel { get; set; }
        public M_CompanyTagModel TagModel { get; set; }

        public T_InformationModel TinfoModel { get; set; }

        public M_GroupModel GroupModel { get; set; }

        public M_GroupListModel GroupListModel { get; set; }

        public M_BrandModel MBrandModel { get; set; }
    }
}
