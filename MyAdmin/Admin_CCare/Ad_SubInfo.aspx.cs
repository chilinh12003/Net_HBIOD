using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MyUtility;
using MyHBIOD.Permission;
using MyHBIOD.Service;
using MyHBIOD.Sub;

namespace MyAdmin.Admin_CCare
{
    public partial class Ad_SubInfo : System.Web.UI.Page
    {

        MOLog mMOLog = new MOLog();
        ChargeLog mChargeLog = new ChargeLog();
        Subscriber mSub = new Subscriber();
        public int PageIndex_Sub = 1;
        public int PageIndex_BuyContent = 1;
        public int PageIndex_MOLog = 1;

        public string MSISDN = string.Empty;
        public int PID = 0;

        DateTime BeginDate_StartServcie = new DateTime(2014, 08, 01);
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                MSISDN = Request.QueryString["msisdn"] == null ? string.Empty : Request.QueryString["msisdn"];
                PID = MyPID.GetPIDByPhoneNumber(MSISDN, MySetting.AdminSetting.MaxPID);

                LoadSub();
                Admin_Paging_VNP_MoLog.rpt_Data = rpt_MOLog;
                Admin_Paging_VNP_MoLog.GetData_Callback_Change += new Admin_Control.Admin_Paging_VNP.GetData_Callback(Admin_Paging_VNP_MOLog_GetTotalPage_Callback_Change);
                Admin_Paging_VNP_MoLog.GetTotalPage_Callback_Change += new Admin_Control.Admin_Paging_VNP.GetTotalPage_Callback(Admin_Paging_VNP_MOLog_GetData_Callback_Change);

                Admin_Paging_VNP_BuyContent.rpt_Data = rpt_BuyContent;
                Admin_Paging_VNP_BuyContent.GetData_Callback_Change += new Admin_Control.Admin_Paging_VNP.GetData_Callback(Admin_Paging_VNP_BuyContent_GetTotalPage_Callback_Change);
                Admin_Paging_VNP_BuyContent.GetTotalPage_Callback_Change += new Admin_Control.Admin_Paging_VNP.GetTotalPage_Callback(Admin_Paging_VNP_BuyContent_GetData_Callback_Change);
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);
            }
        }

        private void LoadSub()
        {
            try
            {
                 rpt_Sub.DataSource = mSub.Select(11, PID.ToString(), MSISDN.ToString());
                 rpt_Sub.DataBind();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        int Admin_Paging_VNP_MOLog_GetData_Callback_Change()
        {
            try
            {
                int SearchType = 0;
                string SearchContent = MSISDN;
              

                DateTime BeginDate = BeginDate_StartServcie;
                DateTime EndDate = DateTime.Now;


                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref SearchContent, ref mTelco, "84");

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    return 0;
                }
                


                return mMOLog.TotalRow(SearchType, SearchContent, PID, 0, 0, 0, BeginDate, EndDate);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        DataTable Admin_Paging_VNP_MOLog_GetTotalPage_Callback_Change()
        {
            try
            {
                string SortBy = "LogID DESC";
                int SearchType = 0;

                string SearchContent = MSISDN;

              

                DateTime BeginDate = BeginDate_StartServcie;
                DateTime EndDate = DateTime.Now;

                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref SearchContent, ref mTelco, "84");

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    return new DataTable();
                }
               

                PageIndex_MOLog = (Admin_Paging_VNP_MoLog.mPaging.CurrentPageIndex - 1) * Admin_Paging_VNP_MoLog.mPaging.PageSize + 1;

                DataTable mTable = mMOLog.Search(SearchType, Admin_Paging_VNP_MoLog.mPaging.BeginRow, Admin_Paging_VNP_MoLog.mPaging.EndRow, SearchContent, PID, 0, 0, 0, BeginDate, EndDate, SortBy);

                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        int Admin_Paging_VNP_BuyContent_GetData_Callback_Change()
        {
            try
            {
                int SearchType = 0;

                string SearchContent = MSISDN;


                DateTime BeginDate = BeginDate_StartServcie;
                DateTime EndDate = DateTime.Now;

                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref SearchContent, ref mTelco, "84");

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    return 0;
                }
               

                return mChargeLog.TotalRow_SelectType(SearchType, SearchContent, PID, 0, 0, 0, 0, BeginDate, EndDate, 3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        DataTable Admin_Paging_VNP_BuyContent_GetTotalPage_Callback_Change()
        {
            try
            {
                string SortBy = "LogID DESC";
                int SearchType = 0;

                string SearchContent = MSISDN;

               

                DateTime BeginDate = BeginDate_StartServcie;
                DateTime EndDate = DateTime.Now;


                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref SearchContent, ref mTelco, "84");

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    return new DataTable();
                }
               


                PageIndex_BuyContent = (Admin_Paging_VNP_BuyContent.mPaging.CurrentPageIndex - 1) * Admin_Paging_VNP_BuyContent.mPaging.PageSize + 1;

                DataTable mTable = mChargeLog.Search_SelectType(SearchType, Admin_Paging_VNP_BuyContent.mPaging.BeginRow, Admin_Paging_VNP_BuyContent.mPaging.EndRow, SearchContent, PID, 0, 0, 0, 0, BeginDate, EndDate, 3, SortBy);

                foreach (DataRow mRow in mTable.Rows)
                {
                    if ((int)mRow["ChargeStatusID"] == 0)
                    {
                        mRow["ChargeStatusName"] = "Thành công";
                    }
                    else
                    {
                        mRow["ChargeStatusName"] = "Không thành công";
                    }

                }
                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}