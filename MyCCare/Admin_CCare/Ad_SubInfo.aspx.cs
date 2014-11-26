using System;
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
    public partial class Ad_SubInfo : System.Web.UI.Page
    {
        public int PageIndex = 1;
        Subscriber mSub = new Subscriber();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                MyCCare.MasterPages.Admin mMaster = (MyCCare.MasterPages.Admin)Page.Master;
                mMaster.Title = "GUI - Thông tin dịch vụ";

                if (!IsPostBack)
                {
                    ViewState["SortBy"] = string.Empty;
                    tbx_MSISDN.Value = MySetting.AdminSetting.MSISDN;
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
                string SortBy = ViewState["SortBy"].ToString();
                string SearchContent = tbx_MSISDN.Value;

                int PID = 0;

                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref SearchContent, ref mTelco, "84");

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    return 1;
                }
                PID = MyPID.GetPIDByPhoneNumber(SearchContent, MySetting.AdminSetting.MaxPID);

                DataTable mTable_Sub = mSub.Select(2, PID.ToString(), SearchContent);

              
                return mTable_Sub.Rows.Count;

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
                string SortBy = ViewState["SortBy"].ToString();
                string SearchContent = tbx_MSISDN.Value;

                int PID = 0;

                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref SearchContent, ref mTelco, "84");

                DataTable mTable = mSub.Select(0);
                Service mService = new Service();
                DataTable mTable_Service = mService.Select(3, null);
                
                mTable.Columns.Add(new DataColumn("ServiceName", typeof(string)));

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    return mTable;
                }
                else
                {
                    PID = MyPID.GetPIDByPhoneNumber(SearchContent, MySetting.AdminSetting.MaxPID);

                    mTable = mSub.Select(7, PID.ToString(), SearchContent);
                    mTable.Columns.Add(new DataColumn("ServiceName", typeof(string)));
                    mTable.Columns.Add(new DataColumn("StatusName", typeof(string)));

                    foreach (DataRow mRow in mTable.Rows)
                    {
                        mRow["StatusName"] = "Từng sử dụng";

                        mTable_Service.DefaultView.RowFilter = "ServiceID = '" + mRow["ServiceID"].ToString() + "'";

                        if (mTable_Service.DefaultView.Count > 0)
                            mRow["ServiceName"] = mTable_Service.DefaultView[0]["ServiceName"].ToString();
                        else
                            continue;
                    }
                    mTable_Service.DefaultView.RowFilter = string.Empty;
                }
                foreach(DataRow mRow_Service in mTable_Service.Rows)
                {
                    mTable.DefaultView.RowFilter = "ServiceID = " + mRow_Service["ServiceID"].ToString();
                    if (mTable.DefaultView.Count > 0)
                        continue;

                    DataRow mRow = mTable.NewRow();
                    mRow["ServiceID"] = mRow["ServiceID"];
                    mRow["ServiceName"] = mRow["ServiceName"];
                    mRow["StatusName"] = "Chưa từng sử dụng";
                    mRow["EffectiveDate"] = DBNull.Value;
                    mRow["ExpiryDate"] = DBNull.Value;
                    mTable.Rows.Add(mRow);
                }

                mTable.DefaultView.RowFilter = "";
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