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
using MyHBIOD.Report;
namespace MyAdmin.Admin_Report
{
    public partial class Ad_KPI_MO : System.Web.UI.Page
    {
        public GetRole mGetRole;
        public int PageIndex = 1;
        RP_MO mRP_MO = new RP_MO();
        RP_MOTotal mRP_MOTotal = new RP_MOTotal();

        /// <summary>
        /// Lấy danh sách các Row trong table RP_MO theo ngày cần report
        /// </summary>
        DataTable mTable_RP_MO
        {
            get
            {
                try
                {
                    if (ViewState["RP_MO"] != null && ((DataTable)ViewState["RP_MO"]).Rows.Count > 0)
                        return (DataTable)ViewState["RP_MO"];
                    else
                    {
                        DateTime BeginDate = tbx_FromDate.Value.Length > 0 ? DateTime.ParseExact(tbx_FromDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;
                        DateTime EndDate = tbx_ToDate.Value.Length > 0 ? DateTime.ParseExact(tbx_ToDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;

                        DataTable mTable = mRP_MO.Select(2, BeginDate.ToString(MyConfig.DateFormat_InsertToDB), EndDate.ToString(MyConfig.DateFormat_InsertToDB));
                        ViewState["RP_MO"] = mTable;
                        return mTable;
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            set
            {
                ViewState["RP_MO"] = value;
            }
        }

        public string GetValue(DateTime ReportDay, RP_MO.MOType mMOType)
        {
            string Format = "<td style=\"border-style: none solid solid none; border-width: 1px; border-color: #000000;\">{0}</td>" +
                                  "<td style=\"border-style: none solid solid none; border-width: 1px; border-color: #000000;\">{1}</td>" +
                                  "<td style=\"border-style: none solid solid none; border-width: 1px; border-color: #000000;\">{2}</td>" +
                                  "<td style=\"border-style: none solid solid none; border-width: 1px; border-color: #000000;\">{3}</td>" +
                                  "<td style=\"border-style: none solid solid none; border-width: 1px; border-color: #000000;\">{4}</td>"
                                  ;
            
            try
            {
                DataTable mTable = mTable_RP_MO.Copy();

                System.Text.StringBuilder mBuilder = new System.Text.StringBuilder(string.Empty);
                foreach (DataRow mRow in mTable.Rows)
                {
                    if ((DateTime)mRow["ReportDay"] == ReportDay && (int)mRow["MOTypeID"] == (int)mMOType)
                    {

                        return string.Format(Format, new string[] {    MyEnum.StringValueOf(mMOType),
                                                                                ((double)mRow["SubCorrect"]).ToString(MyConfig.IntFormat),
                                                                                ((double)mRow["MOCorrect"]).ToString(MyConfig.IntFormat),
                                                                                ((double)mRow["MOChargeSuccess"]).ToString(MyConfig.IntFormat),
                                                                                ((double)mRow["MOSale"]).ToString(MyConfig.IntFormat)
                                                                            });
                    }
                }

            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex);
            }

            return string.Format(Format, new string[] {"","","","",""});
        }
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
                    mGetRole = new GetRole(MySetting.AdminSetting.ListPage.KPI_MO, Member.MemberGroupID());
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

                    tbx_FromDate.Value = MyConfig.StartDayOfMonth.ToString(MyConfig.ShortDateFormat);
                    tbx_ToDate.Value = DateTime.Now.ToString(MyConfig.ShortDateFormat);

                    DataTable mTable = mTable_RP_MO;
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

                DateTime BeginDate = tbx_FromDate.Value.Length > 0 ? DateTime.ParseExact(tbx_FromDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;
                DateTime EndDate = tbx_ToDate.Value.Length > 0 ? DateTime.ParseExact(tbx_ToDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;


                return mRP_MOTotal.TotalRow(SearchType, BeginDate, EndDate);
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


                DateTime BeginDate = tbx_FromDate.Value.Length > 0 ? DateTime.ParseExact(tbx_FromDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;
                DateTime EndDate = tbx_ToDate.Value.Length > 0 ? DateTime.ParseExact(tbx_ToDate.Value, "dd/MM/yyyy", null) : DateTime.MinValue;

                PageIndex = (Admin_Paging1.mPaging.CurrentPageIndex - 1) * Admin_Paging1.mPaging.PageSize + 1;

                return mRP_MOTotal.Search(SearchType, Admin_Paging1.mPaging.BeginRow, Admin_Paging1.mPaging.EndRow, BeginDate, EndDate, SortBy);
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
                mTable_RP_MO = null;

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
                mTable_RP_MO = null;
                BindData();
            }
            catch (Exception ex)
            {
                MyLogfile.WriteLogError(ex, true, MyNotice.AdminError.SeachError, "Chilinh");
            }
        }

    }
}