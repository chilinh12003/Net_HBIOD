﻿using System;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using MyUtility;
using MyHBIOD;
using MyHBIOD.Service;
using MyHBIOD.Sub;

namespace MyCCare.Admin_CCare
{
    public partial class Ad_HistoryCharge : System.Web.UI.Page
    {
        public int PageIndex = 1;
        ChargeLog mChargeLog = new ChargeLog();

        private void BindCombo(int type)
        {
            try
            {
                switch (type)
                {
                    case 1:
                        Service mService = new Service();
                        sel_Service.DataSource = mService.Select(3, string.Empty);
                        sel_Service.DataTextField = "ServiceName";
                        sel_Service.DataValueField = "ServiceID";
                        sel_Service.DataBind();
                        sel_Service.Items.Insert(0, new ListItem("--Chọn gói cước--", "0"));
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                MyCCare.MasterPages.Admin mMaster = (MyCCare.MasterPages.Admin)Page.Master;
                mMaster.Title = "GUI - Lịch sử trừ cước";

                if (!IsPostBack)
                {
                    BindCombo(1);
                    ViewState["SortBy"] = string.Empty;
                    tbx_MSISDN.Value = MySetting.AdminSetting.MSISDN;

                    tbx_FromDate.Value = MySetting.AdminSetting.BeginDate;
                    tbx_ToDate.Value = MySetting.AdminSetting.EndDate;
                }
                else
                {
                    MySetting.AdminSetting.BeginDate = tbx_FromDate.Value;
                    MySetting.AdminSetting.EndDate = tbx_ToDate.Value;
                }
                Admin_Paging1.rpt_Data = rpt_Data;
                Admin_Paging1.GetData_Callback_Change += new MyAdmin.Admin_Control.Admin_Paging.GetData_Callback(Admin_Paging1_GetData_Callback_Change);
                Admin_Paging1.GetTotalPage_Callback_Change += new MyAdmin.Admin_Control.Admin_Paging.GetTotalPage_Callback(Admin_Paging1_GetTotalPage_Callback_Change);
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.LoadDataError, "Chilinh");
            }
        }

        int Admin_Paging1_GetTotalPage_Callback_Change()
        {
            try
            {
                int SearchType = 0;
                string SortBy = ViewState["SortBy"].ToString();
                string SearchContent = tbx_MSISDN.Value;

                int PID = 0;

                DateTime BeginDate = tbx_FromDate.Value.Length > 0 ? DateTime.ParseExact(tbx_FromDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;
                DateTime EndDate = tbx_ToDate.Value.Length > 0 ? DateTime.ParseExact(tbx_ToDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;
                EndDate = EndDate.AddDays(1);

                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref SearchContent, ref mTelco, "84");

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    return 0;
                }
                PID = MyPID.GetPIDByPhoneNumber(SearchContent, MySetting.AdminSetting.MaxPID);

                int ServiceID = 0;
                if (sel_Service.SelectedIndex > 0)
                {
                    int.TryParse(sel_Service.Value, out ServiceID);
                }

                return mChargeLog.TotalRow_SelectType(SearchType, SearchContent, PID,ServiceID, 0, 0, 0, BeginDate, EndDate, 3);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        DataTable Admin_Paging1_GetData_Callback_Change()
        {
            try
            {
                int SearchType = 0;
                string SortBy = ViewState["SortBy"].ToString();
                string SearchContent = tbx_MSISDN.Value;

                int PID = 0;

                DateTime BeginDate = tbx_FromDate.Value.Length > 0 ? DateTime.ParseExact(tbx_FromDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;
                DateTime EndDate = tbx_ToDate.Value.Length > 0 ? DateTime.ParseExact(tbx_ToDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;
                EndDate = EndDate.AddDays(1);

                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref SearchContent, ref mTelco, "84");

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    return new DataTable();
                }
                PID = MyPID.GetPIDByPhoneNumber(SearchContent, MySetting.AdminSetting.MaxPID);

                PageIndex = (Admin_Paging1.mPaging.CurrentPageIndex - 1) * Admin_Paging1.mPaging.PageSize + 1;
                int ServiceID = 0;
                if (sel_Service.SelectedIndex > 0)
                {
                    int.TryParse(sel_Service.Value, out ServiceID);
                }
                DataTable mTable = mChargeLog.Search_SelectType(SearchType, Admin_Paging1.mPaging.BeginRow, Admin_Paging1.mPaging.EndRow, SearchContent, PID,ServiceID, 0, 0, 0, BeginDate, EndDate, 3, SortBy);
                DataColumn mCol_1 = new DataColumn("ActionName", typeof(string));
                mTable.Columns.Add(mCol_1);

                foreach (DataRow mRow in mTable.Rows)
                {
                    if ((int)mRow["ChargeTypeID"] == (int)ChargeLog.ChargeType.CONTENT)
                    {
                        mRow["ActionName"] = "Mua nội dung";
                    }
                    else if ((int)mRow["ChargeTypeID"] == (int)ChargeLog.ChargeType.RENEW)
                    {
                        mRow["ActionName"] = "Gia hạn";
                    }

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

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                string MSISDN = tbx_MSISDN.Value;
                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref MSISDN, ref mTelco, "84");

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    MyMessage.ShowError("Số điện thoại không chính xác, xin vui lòng kiểm tra lại");
                    return;
                }
                tbx_MSISDN.Value = MSISDN;
                MySetting.AdminSetting.MSISDN = MSISDN;
                Admin_Paging1.ResetLoadData();
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.SeachError, "Chilinh");
            }
        }

    }
}