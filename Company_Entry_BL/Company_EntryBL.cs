using System.Data.SqlClient;
using System.Web;
using System.Data;
using TOS_Model;
using TOS_DL;
using System.Collections.Generic;

namespace Company_Entry_BL
{
    public class Company_EntryBL
    {
        public DataTable InsertCompany(M_CompanyModel mModel)
        {

            DataTable dt = new DataTable();
            BaseDL dl = new BaseDL();


            if (mModel !=null)
            {

                SqlParameter[] prms = new SqlParameter[14];
            prms[0] = new SqlParameter("@companyCD", SqlDbType.VarChar) { Value = mModel.CompanyCD };
            prms[1] = new SqlParameter("@companyName", SqlDbType.VarChar) { Value = mModel.CompanyName };
            prms[2] = new SqlParameter("@Passwd", SqlDbType.VarChar) { Value = mModel.Password };
            prms[3] = new SqlParameter("@User", SqlDbType.VarChar) { Value = mModel.UserRole };
            prms[4] = new SqlParameter("@ShortName", SqlDbType.VarChar) { Value = mModel.ShortName };
            prms[5] = new SqlParameter("@Zip1", SqlDbType.VarChar) { Value = mModel.ZipCD1 };
            prms[6] = new SqlParameter("@Zip2", SqlDbType.VarChar) { Value = mModel.ZipCD2 };
            prms[7] = new SqlParameter("@Add1", SqlDbType.VarChar) { Value = mModel.Address1 };
            prms[8] = new SqlParameter("@Add2", SqlDbType.VarChar) { Value = mModel.Address2 };
            prms[9] = new SqlParameter("@phno", SqlDbType.VarChar) { Value = mModel.TelephoneNo };
            prms[10] = new SqlParameter("@faxNo", SqlDbType.VarChar) { Value = mModel.FaxNo };
            prms[11] = new SqlParameter("@PresiName", SqlDbType.VarChar) { Value = mModel.PresidentName };
            if(mModel.RankingFlg != 0)
                { 
                 prms[12] = new SqlParameter("@RnkFlag", SqlDbType.VarChar) { Value = mModel.RankingFlg };
                }
                else
                {
                    prms[12] = new SqlParameter("@RnkFlag", SqlDbType.VarChar) { Value = System.DBNull.Value };
                }
                prms[13] = new SqlParameter("@insertOperator", SqlDbType.VarChar) { Value = mModel.InsertOperator };
            dl.InsertUpdateDeleteData("M_Company_Insert", prms);
            }
            return dt;
        }
        public DataTable InsertCompanyShipping(M_CompanyShippingModel mModelShip, M_CompanyModel mModel)
        {
            DataTable dt = new DataTable();
            BaseDL dl = new BaseDL();
            if (mModelShip != null)
            {
                SqlParameter[] prmship = new SqlParameter[10];
                prmship[0] = new SqlParameter("@CompanyCD", SqlDbType.VarChar) { Value = mModel.CompanyCD };
                prmship[1] = new SqlParameter("@ShippingID", SqlDbType.VarChar) { Value = mModelShip.ShippingID };
                prmship[2] = new SqlParameter("@ShippingName", SqlDbType.VarChar) { Value = mModelShip.ShippingName };
                if (mModelShip.ZipCD1 != null)
                { 
                prmship[3] = new SqlParameter("@ZipCD1", SqlDbType.VarChar) { Value = mModelShip.ZipCD1 };
                }
                else
                {
                 prmship[3] = new SqlParameter("@ZipCD1", SqlDbType.VarChar) { Value = System.DBNull.Value };
                }

                if (mModelShip.ZipCD2 != null)
                {
                 prmship[4] = new SqlParameter("@ZipCD2", SqlDbType.VarChar) { Value = mModelShip.ZipCD2 };
                }
                else
                {
                    prmship[4] = new SqlParameter("@ZipCD2", SqlDbType.VarChar) { Value = System.DBNull.Value };

                }
                if(mModelShip.Address1 !=null)
                { 
                prmship[5] = new SqlParameter("@Address1", SqlDbType.VarChar) { Value = mModelShip.Address1 };
                }
                else
                {
                    prmship[5] = new SqlParameter("@Address1", SqlDbType.VarChar) { Value = System.DBNull.Value };
                }
                if (mModelShip.Address2 !=null)
                { 
                prmship[6] = new SqlParameter("@Address2", SqlDbType.VarChar) { Value = System.DBNull.Value };
                }
                else
                {
                    prmship[6] = new SqlParameter("@Address2", SqlDbType.VarChar) { Value = mModelShip.Address2 };
                }
                if(mModelShip.TelephoneNO != null)
                { 
                prmship[7] = new SqlParameter("@TelephoneNO", SqlDbType.VarChar) { Value = mModelShip.TelephoneNO };
                }
                else
                {
                 prmship[7] = new SqlParameter("@TelephoneNO", SqlDbType.VarChar) { Value = System.DBNull.Value };

                }
                if (mModelShip.FaxNO !=null)
                { 
                prmship[8] = new SqlParameter("@FaxNO", SqlDbType.VarChar) { Value = mModelShip.FaxNO };
                }
                else
                {
                    prmship[8] = new SqlParameter("@FaxNO", SqlDbType.VarChar) { Value = System.DBNull.Value };

                }
                prmship[9] = new SqlParameter("@InsertOperator", SqlDbType.VarChar) { Value = mModelShip.InsertOperator };
                dl.InsertUpdateDeleteData("M_CompanyShipping_Insert", prmship);
            }
            return dt;

        }

        public DataTable InsertCompanyTag (M_CompanyTagModel mModelTag, M_CompanyModel mModel)
        {
            DataTable dt = new DataTable();
            BaseDL dl = new BaseDL();
            if (mModelTag != null)
            {
                SqlParameter[] prmtag = new SqlParameter[3];
                prmtag[0] = new SqlParameter("@CompanyCD", SqlDbType.VarChar) { Value = mModel.CompanyCD };
                prmtag[1] = new SqlParameter("@Tag", SqlDbType.VarChar) { Value = mModelTag.Tag };
                prmtag[2] = new SqlParameter("@InsertOperator", SqlDbType.VarChar) { Value = mModelTag.InsertOperator };
                dl.InsertUpdateDeleteData("M_CompanyTag_Insert", prmtag);
            }
            return dt;
        }

        public DataTable InsertCompanyBrand (M_BrandModel mBrand, M_CompanyModel mModel)
        {
            DataTable dt = new DataTable();
            BaseDL dl = new BaseDL();
            if (mBrand != null)
            {
                SqlParameter[] prmBrand = new SqlParameter[3];
                prmBrand[0] = new SqlParameter("@CompanyCD", SqlDbType.VarChar) { Value = mModel.CompanyCD };
                if (mBrand.BrandName != null)
                {
                    prmBrand[1] = new SqlParameter("@BrandName", SqlDbType.VarChar) { Value = mBrand.BrandName };
                }
                else
                {
                    prmBrand[1] = new SqlParameter("@BrandName", SqlDbType.VarChar) { Value= System.DBNull.Value};
                }
                prmBrand[2] = new SqlParameter("@InsertOperator", SqlDbType.VarChar) { Value = mBrand.InsertOperator };
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
            dl.SelectData("M_Company_SelectBy_CompanyCD", prmName);
            return dt;
        }
    }
  }
