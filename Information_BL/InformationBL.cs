﻿using System;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using TOS_Model;
using TOS_DL;
using System.Collections.Generic;

namespace Information_BL
{
    public class InformationBL
    {
        public DataTable GetInformation(M_CompanyModel mc)
        {
            BaseDL bdl = new BaseDL();
            M_CompanyModel mdl = new M_CompanyModel();
            T_InformationModel tmodel = new T_InformationModel();
            DataTable dtinfo = new DataTable();
            SqlParameter[] prms = new SqlParameter[1];
            prms[0] = new SqlParameter("@companyCD", SqlDbType.VarChar) { Value = mc.CompanyCD};
            dtinfo = bdl.SelectData("T_Information_Select_ForHomePage", prms);
            return dtinfo;
        }


        public DataTable Get_M_CompanyName()
        {
            BaseDL bdl = new BaseDL();
            DataTable dtcompanyname = new DataTable();
            SqlParameter[] prms = new SqlParameter[1];
            dtcompanyname = bdl.SelectData("M_Company_Select",null);
            return dtcompanyname;
        }

        public DataTable Get_M_GroupName()
        {
            BaseDL bdl = new BaseDL();
            DataTable dtgroupname = new DataTable();
            SqlParameter[] prms = new SqlParameter[1];
            dtgroupname = bdl.SelectData("M_Group_Select", null);
            return dtgroupname;
        }

        public DataTable T_Information_Select()
        {
            BaseDL bdl = new BaseDL();
            DataTable dtinformation = new DataTable();
            SqlParameter[] prms = new SqlParameter[1];
            dtinformation = bdl.SelectData("T_Information_Select", null);
            return dtinformation;
        }

        public void  News_Editor_Save(MultipleModel model)
        {
            BaseDL bdl = new BaseDL();
            DataTable dtinfo = new DataTable();
            SqlParameter[] prms = new SqlParameter[15];
            prms[0] = new SqlParameter("@destinationflag", SqlDbType.Int) { Value = model.TinfoModel .DestinationFlag};

            if(model .ComModel !=null)
                prms[1] = new SqlParameter("@companyCD", SqlDbType.VarChar) { Value = model.ComModel.CompanyCD };
            else
                prms[1] = new SqlParameter("@companyCD", SqlDbType.VarChar) { Value = DBNull.Value };

            if(model.GroupModel !=null)
                prms[2] = new SqlParameter("@groupID", SqlDbType.VarChar) { Value = model.GroupModel.GroupID };
            else
                prms[2] = new SqlParameter("@groupID", SqlDbType.VarChar) { Value = DBNull .Value };


            prms[3] = new SqlParameter("@startdate", SqlDbType.VarChar) { Value = model.TinfoModel .DisplayStartDate };
            prms[4] = new SqlParameter("@enddate", SqlDbType.VarChar) { Value = model.TinfoModel.DisplayEndDate};
            prms[5] = new SqlParameter("@infotype", SqlDbType.VarChar) { Value = model.TinfoModel.InformationType };

            if(model.TinfoModel.EffectFlag==true)
                prms[6] = new SqlParameter("@effectflag", SqlDbType.Int) { Value = 1 };
            else
                prms[6] = new SqlParameter("@effectflag", SqlDbType.Int) { Value = 0 };

            prms[7] = new SqlParameter("@date", SqlDbType.VarChar) { Value = model.TinfoModel.Date};
            prms[8] = new SqlParameter("@titlename", SqlDbType.VarChar) { Value = model.TinfoModel.TitleName };

            if(model .TinfoModel .AttachedFile1 !=null)
                prms[9] = new SqlParameter("@file1", SqlDbType.VarChar) { Value = model.TinfoModel.AttachedFile1 };
            else
                prms[9] = new SqlParameter("@file1", SqlDbType.VarChar) { Value = DBNull.Value };

            if (model.TinfoModel.AttachedFile2 != null)
                prms[10] = new SqlParameter("@file2", SqlDbType.VarChar) { Value = model.TinfoModel.AttachedFile2 };
            else
                prms[10] = new SqlParameter("@file2", SqlDbType.VarChar) { Value = DBNull.Value };

            if (model.TinfoModel.AttachedFile3 != null)
                prms[11] = new SqlParameter("@file3", SqlDbType.VarChar) { Value = model.TinfoModel.AttachedFile3 };
            else
                prms[11] = new SqlParameter("@file3", SqlDbType.VarChar) { Value = DBNull.Value };

            if (model.TinfoModel.AttachedFile4 != null)
                prms[12] = new SqlParameter("@file4", SqlDbType.VarChar) { Value = model.TinfoModel.AttachedFile4 };
            else
                prms[12] = new SqlParameter("@file4", SqlDbType.VarChar) { Value = DBNull.Value };

            prms[13] = new SqlParameter("@detail", SqlDbType.VarChar) { Value = model.TinfoModel.DetailInformation };
            prms[14] = new SqlParameter("@insertoperator", SqlDbType.VarChar) { Value = model.TinfoModel.InsertOperator  };

            bdl.InsertUpdateDeleteData("News_Editor_Insert", prms);
        }
    }
}
