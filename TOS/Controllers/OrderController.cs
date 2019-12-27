using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TOS_Model;
using Order_Input_BL;
using System.Data;
using System.Data.SqlClient;
using Newtonsoft.Json;
using Order_Portal_BL;
using Order_History_BL;
using System.IO;
using Group_Entry_BL;


namespace TOS.Controllers
{

    
    public class OrderController : Controller
    {
        Order_HistoryBL bl = new Order_HistoryBL();
        DataTable dt = new DataTable();

        // GET: Order_History
        public ActionResult Order_History()
        {
            return View();
        }
        [HttpPost]
        public string OH_GetFirstTable(string id)
        {
           
            //var OrderID = "101";//Get ID from Order Input
            dt = bl._SelectOrder(id);
            var Jsondata = JsonConvert.SerializeObject(dt);
            return Jsondata;
        }
       
        [HttpPost]
        public string OH_GetSecondTable(T_OrderHistorySearch data)
        {

            dt = bl._SelectOrderDetail(data);
            var Jsondata = JsonConvert.SerializeObject(dt);
            return Jsondata;
           
        }

        [HttpPost]
        public string Delete_OderHistoryDetailRow(string id)
        {
            var message = string.Empty;
            if(id==null)
            {
                message = "NOK";
            }
            else
            {
                var company = Session["CompanyCD"].ToString();
                string[] del_arr;
                var del_arr_o = "";
                var del_arr_a = "";
                
                del_arr = id.Split(',');

                    for (var i = 0; i < del_arr.Length; i++)
                    {
                        del_arr_o += (del_arr[i].Split('_'))[0] + ',';
                        del_arr_a += (del_arr[i].Split('_'))[1] + ',';
                    }
                
               
               
                var AccessPC = System.Environment.MachineName;

                message = bl._DeleteCheckedRow(company, del_arr_a.TrimEnd(','), del_arr_o.TrimEnd(','), AccessPC);

            }

            return JsonConvert.SerializeObject( message);
        }


        public ActionResult ExportReport()
        {
            DataSet ds = new DataSet();

            string savedFileName = "OrderHistory_" + (DateTime.Now).ToShortDateString() + ".pdf";
            var OrderID = "100";
            ds = bl._GetReportData(OrderID);
            Report.Order_History_Report ohrpt = new Report.Order_History_Report();
            ohrpt.Database.Tables["OH_Body"].SetDataSource(ds.Tables[1]);
            ohrpt.Database.Tables["OH_Header"].SetDataSource(ds.Tables[0]);
            Stream str = ohrpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
            Response.Buffer = false;
            Response.ClearContent();
            Response.ClearHeaders();
            str.Seek(0, SeekOrigin.Begin);
            return File(str, "application/pdf", savedFileName);
            

        }

        public ActionResult Order_Input(string id)
        {
            Order_InputBL obl = new Order_InputBL();
            M_JobTimeableModel Mjob = new M_JobTimeableModel();
            DataSet dst = new DataSet();
            //  string ItemCD = "cps-test,cd";
            dst = obl.Order_Input_M_Item_Data(id);
            DataSet dsnew = new DataSet();
            if (dst.Tables.Count > 0)
            {
                int tabcount = 0;
                int count = dst.Tables.Count;
                for (int i = 0; i < count; i++)
                {
                    if (dst.Tables[i].Rows.Count == 0)
                    {
                        dst.Tables.RemoveAt(i);
                        count--;
                        i--;
                    }
                    else
                    {
                        DataTable dtnew = dst.Tables[i];
                        dtnew.TableName = "Table" + tabcount;
                        tabcount++;
                        dsnew.Tables.Add(dst.Tables[i].Copy());
                    }
                }
            }
            Session["MakerItem"] = id;
            ViewBag.count = dsnew.Tables.Count;
            Session["dtsmitem"] = dsnew;
            if (Session["CompanyCD"] != null)
            {
                Mjob.CompanyCD = Session["CompanyCD"].ToString();
                ViewData["JobTime"] = obl.JobTimeTable_Select(Mjob);
                return View();
            }
            else
            {
                ViewData["JobTime"] = Mjob;
                return View();
            }

        }

        [HttpGet]
        public string ShippingName_Select()
        {
            DataTable dt = new DataTable();
            Order_InputBL oib = new Order_InputBL();
            string jsonresult;
            if (Session["CompanyCD"] != null)
            {
                string CompanyCD = Session["CompanyCD"].ToString();
                dt = oib.ShippingName_Select(CompanyCD);
            }

            jsonresult = JsonConvert.SerializeObject(dt);
            return jsonresult;
        }

        public string Order_Input_M_Item_Select(string id)
        {
            DataSet ds = new DataSet();
            //ds =ViewBag.dtsmitem as DataSet;
            ds = Session["dtsmitem"] as DataSet;
            //Order_InputBL oib = new Order_InputBL();

            //string ItemCD = "cps-test,BAQ005";
            //DataSet dst = new DataSet();
            //dst = oib.Order_Input_M_Item_Data(ItemCD);
            //if (id == null)
            //{
            //    string jsonresult;
            //    jsonresult = JsonConvert.SerializeObject(ds.Tables[0]);
            //    return jsonresult;

            //}
            //else
            //{
            if (ds.Tables.Count > Convert.ToInt32(id))
            {
                string jsonresult;
                jsonresult = JsonConvert.SerializeObject(ds.Tables["Table" + id]);
                return jsonresult;
            }
            else
            {
                string jsonresult;
                DataTable dt = new DataTable();
                dt.Columns.Add("MakerItemCD");
                dt.Columns.Add("ItemName");
                dt.Columns.Add("BrandName");
                dt.Columns.Add("ListPrice(InTax)");
                dt.Columns.Add("SalePrice(InTax)");
                dt.Columns.Add("Lot");
                jsonresult = JsonConvert.SerializeObject(dt);
                return jsonresult;
            }
            //  }

        }

        public string Order_Input_M_SKU_Select(string id)
        {
            //String MakerItem = Session["MakerItem"] as string;
            DataSet dsk = new DataSet();
            dsk = Session["dtsmitem"] as DataSet;
            Order_InputBL oib = new Order_InputBL();
            DataTable dt = new DataTable();
            //string[] itemcd = MakerItem.Split(',');
            int i = Convert.ToInt32(id);
            if (dsk.Tables.Count > i && dsk.Tables["Table" + id].Rows.Count > 0)
            {
                DataTable dttemp = dsk.Tables["Table" + id].Copy();
                string mcd = dttemp.Rows[0]["MakerItemCD"].ToString(); //itemcd[i].ToString();
                dt = oib.Order_Input_M_SKU(mcd);
                if (dt.Rows.Count > 0)
                {
                    string jsonresult;
                    jsonresult = JsonConvert.SerializeObject(dt);
                    return jsonresult;
                }

            }
            return null;

        }


        public ActionResult Order_Portal()
        {
            if (Session["CompanyCD"] != null)
            {
                Order_InputBL obl = new Order_InputBL();
                M_JobTimeableModel Mjob = new M_JobTimeableModel();
                Mjob.CompanyCD = Session["CompanyCD"].ToString();
                ViewData["JobTime"] = obl.JobTimeTable_Select(Mjob);

                //Order_PortalBL opbl = new Order_PortalBL();
                //DataTable dtorderpotal = opbl.Order_Portal_List_Select();

                return View();
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public string Get_Order_Portal_List()
        {
            Order_PortalBL opbl = new Order_PortalBL();
            return DataTableToJSONWithJSONNet(opbl.Order_Portal_List_Select());
        }

        public string DataTableToJSONWithJSONNet(DataTable table)
        {
            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(table);
            return JSONString;
        }

        [HttpGet]
        public JsonResult GetMessage()
        {
            // some service call to get data
            string output = string.Empty;
            Group_EntryBL gebl = new Group_EntryBL();
            DataTable dtIMsg = gebl.M_Message_Select("1001", "I");
            string message = string.Empty;
            if (dtIMsg.Rows.Count > 0)
            {
                TempData["Imsg"] = dtIMsg.Rows[0]["Message1"].ToString();
                output= dtIMsg.Rows[0]["Message1"].ToString();
            }
            return Json(output, JsonRequestBehavior.AllowGet);
        }

    }
}