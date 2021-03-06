﻿using System;
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

        public Boolean InsertGroupEntry(MultipleModel mModel,string PcName)
        {
            try
            {
                DataTable dt = new DataTable();
                BaseDL dl = new BaseDL();
                SqlParameter[] prms = new SqlParameter[9];
                prms[0] = new SqlParameter("@groupID", SqlDbType.VarChar) { Value = mModel.GroupModel.GroupID };
                prms[1] = new SqlParameter("@groupName", SqlDbType.VarChar) { Value = mModel.GroupModel.GroupName };
                prms[2] = new SqlParameter("@groupInfoFlag", SqlDbType.VarChar) { Value = mModel.GroupModel.GroupInfoFlg };
                if (mModel.ComModel != null)
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
                if (mModel.tModel != null)
                {
                    prms[5] = new SqlParameter("@tag", SqlDbType.VarChar) { Value = mModel.tModel.Tag };
                }
                else
                {
                    prms[5] = new SqlParameter("@tag", SqlDbType.VarChar) { Value = DBNull.Value };
                }
                prms[6] = new SqlParameter("@AccessPC", SqlDbType.VarChar) { Value=PcName};
                prms[7] = new SqlParameter("@insertOperator", SqlDbType.VarChar) { Value = mModel.GroupModel.InsertOperator };
                prms[8] = new SqlParameter("@saveUpdateFlag", SqlDbType.VarChar) { Value=mModel.GroupModel.SaveUpdateFlag };
                dl.InsertUpdateDeleteData("Group_Entry_Insert", prms);
                return true;
            }
            catch(Exception ex)
            {
                string aa = ex.Message;
                return false;
            }
           
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

        
        public DataTable M_Message_Select(string messageKey,string msgType)
        {
            BaseDL bdl = new BaseDL();
            DataTable dtMessage = new DataTable();
            SqlParameter[] prms = new SqlParameter[2];
            prms[0] = new SqlParameter("@key", SqlDbType.VarChar) { Value = messageKey };
            prms[1] = new SqlParameter("@msgType", SqlDbType.VarChar) { Value = msgType };
            dtMessage = bdl.SelectData("Message_Select", prms);
            return dtMessage;
        }
    }
}
