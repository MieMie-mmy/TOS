using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using TOS_DL;
using TOS_Model;

namespace Order_Input_BL
{
    public class Order_InputBL
    {
        public DataTable JobTimeTable_Select(string CompanyCD)
        {
            TOSEntities ent = new TOSEntities();
            var dtjobtimetable = ent.M_JobTimeable.Where(m => m.CompanyCD == CompanyCD).SingleOrDefault();
            DataTable dt = new DataTable();
            return  dt;
        }
    }
}
