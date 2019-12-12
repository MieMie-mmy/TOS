using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOS_Model;
using TOS_DL;
using System.Data;

namespace Order_Portal_BL
{
    public class Order_PortalBL
    {
        public DataTable Order_Portal_List_Select()
        {
            BaseDL bdl = new BaseDL();
            DataTable dtorder_portal = new DataTable();
            dtorder_portal = bdl.SelectData("Order_Potal_Select", null);
            return dtorder_portal;
        }
    }
}
