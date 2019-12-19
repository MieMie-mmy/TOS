using System.Data.SqlClient;
using System.Web;
using System.Data;
using TOS_Model;
using TOS_DL;


namespace Company_Entry_BL
{
    public class Company_EntryBL
    {
        public DataTable InsertCompany(M_CompanyModel mModel)
        {

            DataTable dt = new DataTable();
            BaseDL dl = new BaseDL();


            //if (!string.IsNullOrWhiteSpace(mModel))
            //{

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
            prms[12] = new SqlParameter("@RnkFlag", SqlDbType.VarChar) { Value = mModel.RankingFlg };
            prms[13] = new SqlParameter("@insertOperator", SqlDbType.VarChar) { Value = mModel.InsertOperator };
            dl.InsertUpdateDeleteData("M_Company_Insert", prms);




            


            //}
            return dt;
        }
        public DataTable InsertCompanyShipping(M_CompanyShippingModel mModelShip, M_CompanyModel mModel)
        {
            DataTable dt = new DataTable();
            BaseDL dl = new BaseDL();

            SqlParameter[] prmship = new SqlParameter[1];
            prmship[0] = new SqlParameter("@CompanyCD", SqlDbType.VarChar) { Value = mModel.CompanyCD };
            prmship[1] = new SqlParameter("@ShippingID", SqlDbType.VarChar) { Value = mModelShip.ShippingID };
            prmship[2] = new SqlParameter("@ShippingName", SqlDbType.VarChar) { Value = mModelShip.ShippingName };
            prmship[3] = new SqlParameter("@ZipCD1", SqlDbType.VarChar) { Value = mModelShip.ZipCD1 };
            prmship[4] = new SqlParameter("@ZipCD2", SqlDbType.VarChar) { Value = mModelShip.ZipCD2 };
            prmship[5] = new SqlParameter("@Address1", SqlDbType.VarChar) { Value = mModelShip.Address1 };
            prmship[6] = new SqlParameter("@Address2", SqlDbType.VarChar) { Value = mModelShip.Address2 };
            prmship[9] = new SqlParameter("@TelephoneNO", SqlDbType.VarChar) { Value = mModelShip.TelephoneNO };
            prmship[10] = new SqlParameter("@FaxNO", SqlDbType.VarChar) { Value = mModelShip.FaxNO };
            prmship[11] = new SqlParameter("@InsertOperator", SqlDbType.VarChar) { Value = mModelShip.InsertOperator };

            dl.InsertUpdateDeleteData("M_CompanyShipping_Insert", prmship);



            return dt;

        }
    }
  }
