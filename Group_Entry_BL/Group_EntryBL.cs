using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TOS_DL;
using TOS_Model;

namespace Group_Entry_BL
{
    public class Group_EntryBL
    {
        MultipleModel model;
        public DataTable Get_M_BrandName()
        {
            BaseDL bdl = new BaseDL();
            DataTable dtcompanyname = new DataTable();
            SqlParameter[] prms = new SqlParameter[1];
            dtcompanyname = bdl.SelectData("M_Brand_Select", null);
            return dtcompanyname;
        }

        public void InsertGroupEntry(MultipleModel mModel)
        {

            DataTable dt = new DataTable();
            BaseDL dl = new BaseDL();
            SqlParameter[] prms = new SqlParameter[7];
            prms[0] = new SqlParameter("@groupID", SqlDbType.VarChar) { Value = mModel.GroupModel.GroupID };
            prms[1] = new SqlParameter("@groupName", SqlDbType.VarChar) { Value = mModel.GroupModel.GroupName };
            prms[2] = new SqlParameter("@groupInfoFlag", SqlDbType.VarChar) { Value = mModel.GroupModel.GroupInfoFlg };
            if(mModel.ComModel !=null)
            {
                prms[3] = new SqlParameter("@companyName", SqlDbType.VarChar) { Value = mModel.ComModel.CompanyName };
            }
            else
            {
                prms[3] = new SqlParameter("@companyName", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            if (mModel.MBrandModel != null)
            {
                prms[4] = new SqlParameter("@BrandName", SqlDbType.VarChar) { Value = mModel.MBrandModel.BrandName };
            }
            else
            {
                prms[4] = new SqlParameter("@BrandName", SqlDbType.VarChar) { Value = DBNull.Value };
            }
            if(mModel.TagModel!=null)
            {
                prms[5] = new SqlParameter("@tag", SqlDbType.VarChar) { Value = mModel.tagModel.Tag };
            }
            else
            {
                prms[5] = new SqlParameter("@tag", SqlDbType.VarChar) { Value = DBNull.Value };
            }
          
            prms[6] = new SqlParameter("@insertOperator", SqlDbType.VarChar) { Value = mModel.GroupModel.InsertOperator };
            dl.InsertUpdateDeleteData("Group_Entry_Insert", prms);
        }

        public DataTable Check_Duplicate_GroupEntry(MultipleModel mModel)
        {
            BaseDL bdl = new BaseDL();
            DataTable dtgroupID = new DataTable();
            SqlParameter[] prms = new SqlParameter[1];
            prms[0] = new SqlParameter("@groupID", SqlDbType.VarChar) { Value = mModel.GroupModel.GroupID };
            dtgroupID = bdl.SelectData("M_Group_SelectBy_GroupID", prms);
            return dtgroupID;
        }
    }
}
