using System.Data.SqlClient;
using System.Web;
using System.Data;
using TOS_Model;
using TOS_DL;
using System.Collections.Generic;
using System;

namespace Company_Entry_BL
{
    public class Company_EntryBL
    {
        public DataTable InsertCompany(M_CompanyModel mModel,string PcName)
        {

            DataTable dt = new DataTable();
            BaseDL dl = new BaseDL();


            if (mModel !=null)
            {

                SqlParameter[] prms = new SqlParameter[15];

         
            prms[0] = new SqlParameter("@companyCD", SqlDbType.VarChar) { Value = mModel.CompanyCD };
            prms[1] = new SqlParameter("@companyName", SqlDbType.VarChar) { Value = mModel.CompanyName };
            prms[2] = new SqlParameter("@Passwd", SqlDbType.VarChar) { Value = mModel.Password };
            prms[3] = new SqlParameter("@User", SqlDbType.VarChar) { Value = mModel.UserRole };
                if (!string.IsNullOrWhiteSpace(mModel.ShortName))
                {
                    prms[4] = new SqlParameter("@ShortName", SqlDbType.VarChar) { Value = mModel.ShortName };
                }
                else
                {
                    prms[4] = new SqlParameter("@ShortName", SqlDbType.VarChar) { Value = System.DBNull.Value };
                }
                prms[5] = new SqlParameter("@Zip1", SqlDbType.VarChar) { Value = mModel.ZipCD1 };
                prms[6] = new SqlParameter("@Zip2", SqlDbType.VarChar) { Value = mModel.ZipCD2 };
                if (!string.IsNullOrWhiteSpace(mModel.Address1))
                {
                    prms[7] = new SqlParameter("@Add1", SqlDbType.VarChar) { Value = mModel.Address1 };
                }
                else
                {
                    prms[7] = new SqlParameter("@Add1", SqlDbType.VarChar) { Value = System.DBNull.Value };
                }

                if (!string.IsNullOrWhiteSpace(mModel.Address2))
                {
                    prms[8] = new SqlParameter("@Add2", SqlDbType.VarChar) { Value = mModel.Address2 };
                }
                else
                {
                    prms[8] = new SqlParameter("@Add2", SqlDbType.VarChar) { Value = System.DBNull.Value };
                }
                if (!string.IsNullOrWhiteSpace(mModel.TelephoneNo))
                {
                    prms[9] = new SqlParameter("@phno", SqlDbType.VarChar) { Value = mModel.TelephoneNo };
                }
                else
                {
                    prms[9] = new SqlParameter("@phno", SqlDbType.VarChar) { Value = System.DBNull.Value };
                }
                if (!string.IsNullOrWhiteSpace(mModel.FaxNo))
                {
                    prms[10] = new SqlParameter("@faxNo", SqlDbType.VarChar) { Value = mModel.FaxNo };
                }
                else
                {
                    prms[10] = new SqlParameter("@faxNo", SqlDbType.VarChar) { Value = System.DBNull.Value };
                }
                if (!string.IsNullOrWhiteSpace(mModel.PresidentName))
                {
                    prms[11] = new SqlParameter("@PresiName", SqlDbType.VarChar) { Value = mModel.PresidentName };
                }
                else
                {
                    prms[11] = new SqlParameter("@PresiName", SqlDbType.VarChar) { Value = System.DBNull.Value };
                }

               if(mModel.RankingFlg != 0)
                { 
                 prms[12] = new SqlParameter("@RnkFlag", SqlDbType.VarChar) { Value = mModel.RankingFlg };
                }
                else
                {
                    prms[12] = new SqlParameter("@RnkFlag", SqlDbType.VarChar) { Value = 0 };
                }
                prms[13] = new SqlParameter("@insertOperator", SqlDbType.VarChar) { Value = mModel.InsertOperator };
                prms[14] = new SqlParameter("@AccessPC", SqlDbType.VarChar) { Value = PcName };
                dl.InsertUpdateDeleteData("M_Company_Insert", prms);
            }
            return dt;
        }
        public DataTable InsertCompanyShipping(M_CompanyShippingModel mModelShip, M_CompanyModel mModel,string PcName)
        {
            DataTable dt = new DataTable();
            BaseDL dl = new BaseDL();
            if (mModelShip != null)
            {
                SqlParameter[] prmship = new SqlParameter[11];
                if (!string.IsNullOrWhiteSpace(mModel.CompanyCD))
                {
                    prmship[0] = new SqlParameter("@CompanyCD", SqlDbType.VarChar) { Value = mModel.CompanyCD };
                }
                else
                {
                    prmship[0] = new SqlParameter("@CompanyCD", SqlDbType.VarChar) { Value = System.DBNull.Value };
                }
                if (mModelShip.ShippingID != 0)
                {
                    prmship[1] = new SqlParameter("@ShippingID", SqlDbType.VarChar) { Value = mModelShip.ShippingID };
                }
                else
                {
                    prmship[1] = new SqlParameter("@ShippingID", SqlDbType.VarChar) { Value = System.DBNull.Value };
                }
                if (!string.IsNullOrWhiteSpace(mModelShip.ShippingName))
                {
                    prmship[2] = new SqlParameter("@ShippingName", SqlDbType.VarChar) { Value = mModelShip.ShippingName };
                }
                else
                {
                    prmship[2] = new SqlParameter("@ShippingName", SqlDbType.VarChar) { Value = System.DBNull.Value };
                }
                if (!string.IsNullOrWhiteSpace(mModelShip.ZipCD1))
                { 
                prmship[3] = new SqlParameter("@ZipCD1", SqlDbType.VarChar) { Value = mModelShip.ZipCD1 };
                }
                else
                {
                 prmship[3] = new SqlParameter("@ZipCD1", SqlDbType.VarChar) { Value = System.DBNull.Value };
                }

                if (!string.IsNullOrWhiteSpace(mModelShip.ZipCD2))
                {
                 prmship[4] = new SqlParameter("@ZipCD2", SqlDbType.VarChar) { Value = mModelShip.ZipCD2 };
                }
                else
                {
                    prmship[4] = new SqlParameter("@ZipCD2", SqlDbType.VarChar) { Value = System.DBNull.Value };

                }
                if(!string.IsNullOrWhiteSpace(mModelShip.Address1))
                { 
                prmship[5] = new SqlParameter("@Address1", SqlDbType.VarChar) { Value = mModelShip.Address1 };
                }
                else
                {
                    prmship[5] = new SqlParameter("@Address1", SqlDbType.VarChar) { Value = System.DBNull.Value };
                }
                if (!string.IsNullOrWhiteSpace(mModelShip.Address2))
                { 
                prmship[6] = new SqlParameter("@Address2", SqlDbType.VarChar) { Value = mModelShip.Address2 };
                }
                else
                {
                    prmship[6] = new SqlParameter("@Address2", SqlDbType.VarChar) { Value = System.DBNull.Value };
                }
                if(!string.IsNullOrWhiteSpace(mModelShip.TelephoneNO) )
                { 
                prmship[7] = new SqlParameter("@TelephoneNO", SqlDbType.VarChar) { Value = mModelShip.TelephoneNO };
                }
                else
                {
                 prmship[7] = new SqlParameter("@TelephoneNO", SqlDbType.VarChar) { Value = System.DBNull.Value };

                }
                if (!string.IsNullOrWhiteSpace(mModelShip.FaxNO))
                { 
                prmship[8] = new SqlParameter("@FaxNO", SqlDbType.VarChar) { Value = mModelShip.FaxNO };
                }
                else
                {
                    prmship[8] = new SqlParameter("@FaxNO", SqlDbType.VarChar) { Value = System.DBNull.Value };

                }
                prmship[9] = new SqlParameter("@InsertOperator", SqlDbType.VarChar) { Value = mModelShip.InsertOperator };
                prmship[10] = new SqlParameter("@AccessPC", SqlDbType.VarChar) { Value = PcName };
                dl.InsertUpdateDeleteData("M_CompanyShipping_Insert", prmship);
            }
            return dt;

        }

        public DataTable InsertCompanyTag (M_CompanyTagModel mModelTag, M_CompanyModel mModel,string PcName)
        {
            DataTable dt = new DataTable();
            BaseDL dl = new BaseDL();
            if (mModelTag != null)
            {
                SqlParameter[] prmtag = new SqlParameter[4];
                prmtag[0] = new SqlParameter("@CompanyCD", SqlDbType.VarChar) { Value = mModel.CompanyCD };

                if (!string.IsNullOrWhiteSpace(mModelTag.Tag))
                {
                    prmtag[1] = new SqlParameter("@Tag", SqlDbType.VarChar) { Value = mModelTag.Tag };
                }
                else
                {
                    prmtag[1] = new SqlParameter("@Tag", SqlDbType.VarChar) { Value = System.DBNull.Value };
                }
                prmtag[2] = new SqlParameter("@InsertOperator", SqlDbType.VarChar) { Value = mModelTag.InsertOperator };
                prmtag[3] = new SqlParameter("@AccessPC", SqlDbType.VarChar) { Value = PcName };
                dl.InsertUpdateDeleteData("M_CompanyTag_Insert", prmtag);
            }
            return dt;
        }

        public DataTable InsertCompanyBrand (M_BrandModel mBrand, M_CompanyModel mModel,string PcName)
        {
            DataTable dt = new DataTable();
            BaseDL dl = new BaseDL();
            if (mBrand != null)
            {
                SqlParameter[] prmBrand = new SqlParameter[4];
                prmBrand[0] = new SqlParameter("@CompanyCD", SqlDbType.VarChar) { Value = mModel.CompanyCD };
                if (!string.IsNullOrWhiteSpace(mBrand.BrandName))
                {
                    prmBrand[1] = new SqlParameter("@BrandName", SqlDbType.VarChar) { Value = mBrand.BrandName };
                }
                else
                {
                    prmBrand[1] = new SqlParameter("@BrandName", SqlDbType.VarChar) { Value= System.DBNull.Value};
                }
                prmBrand[2] = new SqlParameter("@InsertOperator", SqlDbType.VarChar) { Value = mBrand.InsertOperator };
                prmBrand[3] = new SqlParameter("@AccessPC", SqlDbType.VarChar) { Value = PcName };
                dl.InsertUpdateDeleteData("M_CompanyBrand_Insert", prmBrand);

            }
            return dt;
        }


        public DataTable Check_Duplicate_CompanyCD (M_CompanyModel mModel)
        {
            DataTable dt = new DataTable();
            BaseDL dl = new BaseDL();
            SqlParameter[] prmName= new SqlParameter[1];
            prmName[0] = new SqlParameter("@CompanyCD", SqlDbType.VarChar) { Value = mModel.CompanyCD };
           dt= dl.SelectData("M_Company_SelectBy_CompanyCD", prmName);
            return dt;
        }



        public DataTable Message_Select (string key, string ID )
        {
            DataTable dt = new DataTable();
            BaseDL dl = new BaseDL();
            SqlParameter[] prmMsg = new SqlParameter[2];
            prmMsg[0] = new SqlParameter("@key", SqlDbType.VarChar) { Value = key };
            prmMsg[1] = new SqlParameter("@msgType", SqlDbType.VarChar) { Value = ID };
           dt= dl.SelectData("Message_Select", prmMsg);
            return dt;
        }

        public DataTable InsertCompany(M_CompanyModel mModel)
        {
            throw new NotImplementedException();
        }
        public DataTable CompanyUpdateView_select()
        {
            
            DataTable dt = new DataTable();
            BaseDL dl = new BaseDL();
            //SqlParameter[] prmMsg = new SqlParameter[2];
            //prmMsg[0] = new SqlParameter("@key", SqlDbType.VarChar) { Value = key };
            //prmMsg[1] = new SqlParameter("@msgType", SqlDbType.VarChar) { Value = ID };
            dt = dl.SelectData("CompanyUpdateView_Select", null);
           
            return dt;

        }
        public Boolean CompanyUpdate_View_Delete(string id, string AccessPC)
        {
            BaseDL bdl = new BaseDL();
            bool result = false;
            DataTable dt = new DataTable();
            SqlParameter[] prm = new SqlParameter[2];

            prm[0] = new SqlParameter("@companyCD", SqlDbType.VarChar) { Value = id };

            prm[1] = new SqlParameter("@AccessPC", SqlDbType.VarChar) { Value = AccessPC };

            dt = bdl.SelectData("CompanyUpdateView_Delete", prm);
            if (dt.Rows.Count > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }

        public DataTable CompanyUpdate_View_Edit(string id)
        {
            BaseDL dl = new BaseDL();
            DataTable dt = new DataTable();
            SqlParameter prms = new SqlParameter();
            prms = new SqlParameter("@CompanyCD", SqlDbType.VarChar) { Value = id };
            dt = dl.SelectData("CompanyUpdateView_Edit", prms);

            return dt;
        }
        
    }
    }
  

