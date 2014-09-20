using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MyConnect.SQLServer;
using MyUtility;
using System.Web;
using System.ComponentModel;


namespace MyHBIOD.Service
{
    public class Winner
    {
       
        MyExecuteData mExec;
        MyGetData mGet;

        public Winner()
        {
            mExec = new MyExecuteData();
            mGet = new MyGetData();
        }

        public Winner(string KeyConnect_InConfig)
        {
            mExec = new MyExecuteData(KeyConnect_InConfig);
            mGet = new MyGetData(KeyConnect_InConfig);
        }

        public DataSet CreateDataSet()
        {
            try
            {
                string[] mPara = { "Type" };
                string[] mValue = { "0" };
                DataSet mSet = mGet.GetDataSet("Sp_Winner_Select", mPara, mValue);
                if (mSet != null && mSet.Tables.Count >= 1)
                {
                    mSet.DataSetName = "Parent";
                    mSet.Tables[0].TableName = "Child";
                }
                return mSet;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lấy tổng số dòng cho phân trang
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="SearchContent"></param>
        /// <param name="OrderBy"></param>
        /// <param name="IsActive"></param>
        /// <returns></returns>
        public int TotalRow(int? Type, string SearchContent, int QuestionID, int SuggestID)
        {
            try
            {
                string[] mPara = { "Type", "SearchContent", "QuestionID", "SuggestID", "IsTotalRow" };
                string[] mValue = { Type.ToString(), SearchContent, QuestionID.ToString(), SuggestID.ToString(), true.ToString() };

                return (int)mGet.GetExecuteScalar("Sp_Winner_Search", mPara, mValue);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Lẫy dữ liệu phân trang
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="BeginRow"></param>
        /// <param name="EndRow"></param>
        /// <param name="SearchContent"></param>
        /// <param name="OrderBy"></param>
        /// <param name="IsActive"></param>
        /// <returns></returns>
        public DataTable Search(int? Type, int BeginRow, int EndRow, string SearchContent,  int QuestionID, int SuggestID, string OrderBy)
        {
            try
            {
                string[] mpara = { "Type", "BeginRow", "EndRow", "SearchContent", "QuestionID", "SuggestID", "OrderBy", "IsTotalRow" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), SearchContent,QuestionID.ToString(), SuggestID.ToString(), OrderBy, false.ToString() };
                return mGet.GetDataTable("Sp_Winner_Search", mpara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
