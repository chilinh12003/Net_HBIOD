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
    public partial class Ad_CheckSubInfo : System.Web.UI.Page
    {
        public GetRole mGetRole;
        public int PageIndex = 1;
        Subscriber mSub = new Subscriber();

        private void BindCombo(int type)
        {
            try
            {
                switch (type)
                {
                    case 1:
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void BindData()
        {
            Admin_Paging1.ResetLoadData();
        }

        private bool CheckPermission()
        {
            try
            {
                if (mGetRole.ViewRole == false)
                {
                    Response.Redirect(mGetRole.URLNotView, false);
                    return false;
                }

            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.CheckPermissionError, "Chilinh");
                return false;
            }
            return true;
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            bool IsRedirect = false;
            try
            {
                //Phân quyền
                if (ViewState["Role"] == null)
                {
                    mGetRole = new GetRole(MySetting.AdminSetting.ListPage.CheckDetailInfo, Member.MemberGroupID());
                }
                else
                {
                    mGetRole = (GetRole)ViewState["Role"];
                }

                if (!CheckPermission())
                {
                    IsRedirect = true;
                }
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.LoadDataError, "Chilinh");
            }
            if (IsRedirect)
            {
                Response.End();
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                MyAdmin.MasterPages.Admin mMaster = (MyAdmin.MasterPages.Admin)Page.Master;
                mMaster.str_PageTitle = mGetRole.PageName;

                if (!IsPostBack)
                {
                    ViewState["SortBy"] = string.Empty;
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
                return 10;
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
                string MSISDN = tbx_SearchContent.Value;
                int PID = 0;

                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref MSISDN, ref mTelco, "84");

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    return new DataTable();
                }
                PID = MyPID.GetPIDByPhoneNumber(MSISDN, MySetting.AdminSetting.MaxPID);

                PageIndex = (Admin_Paging1.mPaging.CurrentPageIndex - 1) * Admin_Paging1.mPaging.PageSize + 1;

                DataTable mTable = mSub.Select(11, PID.ToString(), MSISDN);

               
                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void lbtn_Sort_Click(object sender, EventArgs e)
        {
            try
            {
                //lbtn_Sort_1.CssClass = "Sort";
                //lbtn_Sort_2.CssClass = "Sort";
                //lbtn_Sort_3.CssClass = "Sort";
                //lbtn_Sort_4.CssClass = "Sort";
                //lbtn_Sort_5.CssClass = "Sort";
                //lbtn_Sort_6.CssClass = "Sort";
                //lbtn_Sort_7.CssClass = "Sort";

                LinkButton mLinkButton = (LinkButton)sender;
                ViewState["SortBy"] = mLinkButton.CommandArgument;

                if (mLinkButton.CommandArgument.IndexOf(" ASC") >= 0)
                {
                    mLinkButton.CssClass = "SortActive_Up";
                    mLinkButton.CommandArgument = mLinkButton.CommandArgument.Replace(" ASC", " DESC");
                }
                else
                {
                    mLinkButton.CssClass = "SortActive_Down";
                    mLinkButton.CommandArgument = mLinkButton.CommandArgument.Replace(" DESC", " ASC");
                }

                BindData();
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.SortError, "Chilinh");
            }
        }

        protected void btn_Search_Click(object sender, EventArgs e)
        {
            try
            {
                string MSISDN = tbx_SearchContent.Value;
                MyConfig.Telco mTelco = MyConfig.Telco.Nothing;
                MyCheck.CheckPhoneNumber(ref MSISDN, ref mTelco, "84");

                if (mTelco != MyConfig.Telco.Vinaphone)
                {
                    MyMessage.ShowError("Số điện thoại không chính xác, xin vui lòng kiểm tra lại");
                    return;
                }
                tbx_SearchContent.Value = MSISDN;
                BindData();
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.SeachError, "Chilinh");
            }
        }

    }
}