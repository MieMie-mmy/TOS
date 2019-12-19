using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOS_DL;

namespace Group_Entry_BL
{
    public class Group_EntryBL
    {
        public DataTable Get_M_BrandName()
        {
            BaseDL bdl = new BaseDL();
            DataTable dtcompanyname = new DataTable();
            SqlParameter[] prms = new SqlParameter[1];
            dtcompanyname = bdl.SelectData("M_Brand_Select", null);
            return dtcompanyname;
        }
    }
}
