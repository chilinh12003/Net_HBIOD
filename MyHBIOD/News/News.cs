using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MyConnect.SQLServer;
using MyUtility;
using System.Web;
using System.ComponentModel;

namespace MyHBIOD.News
{
    public class News
    {
       
        public enum NewsType
        {
            [DescriptionAttribute("Tất cả")]
            Nothing = 0,
            [DescriptionAttribute("Bản tin hàng ngày")]
            Push = 1,
            [DescriptionAttribute("Nhắc nhở")]
            Reminder = 2,
        }
        public enum Status
        {
            [DescriptionAttribute("Nothing")]
            Nothing = 0,
            [DescriptionAttribute("Kích hoạt")]
            Active = 1,
            [DescriptionAttribute("Hủy kích hoạt")]
            Deactive = 2,
        }

      
        MyExecuteData mExec;
        MyGetData mGet;

        public News()
        {
            mExec = new MyExecuteData();
            mGet = new MyGetData();
        }

        public News(string KeyConnect_InConfig)
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
                DataSet mSet = mGet.GetDataSet("Sp_News_Select", mPara, mValue);
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
        /// Lấy dữ liệu
        /// </summary>
        /// <param name="Type">Cách thức lấy dữ liệu
        /// <para>Type = 1: Lấy thông tin chi tiết 1 Record (Para_1 = NewsID)</para>
        /// <para>Type = 3: Lấy tin theo (Para_1 = NewsTypeID)</para>
        /// </param>
        /// <param name="Para_1"></param>
        /// <returns></returns>
        public DataTable Select(int Type, string Para_1)
        {
            try
            {
                string[] mPara = { "Type", "Para_1" };
                string[] mValue = { Type.ToString(), Para_1 };
                return mGet.GetDataTable("Sp_News_Select", mPara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Insert(int? Type, string XMLContent)
        {
            try
            {
                string[] mpara = { "Type", "XMLContent" };
                string[] mValue = { Type.ToString(), XMLContent };
                if (mExec.ExecProcedure("Sp_News_Insert", mpara, mValue) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public bool Delete(int? Type, string XMLContent)
        {
            try
            {
                string[] mpara = { "Type", "XMLContent" };
                string[] mValue = { Type.ToString(), XMLContent };
                if (mExec.ExecProcedure("Sp_News_Delete", mpara, mValue) > 0)
                {
                    return true;

                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public bool Update(int? Type, string XMLContent)
        {
            try
            {
                string[] mpara = { "Type", "XMLContent" };
                string[] mValue = { Type.ToString(), XMLContent };
                if (mExec.ExecProcedure("Sp_News_Update", mpara, mValue) > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }
               
        public int TotalRow(int? Type, string SearchContent,int ServiceID, Status StatusID, NewsType NewsTypeID)
        {
            try
            {
                string[] mpara = { "Type", "SearchContent","ServiceID","StatusID","NewsTypeID", "IsTotalRow" };
                string[] mValue = { Type.ToString(), SearchContent, ServiceID.ToString(),((int)StatusID).ToString(), ((int)NewsTypeID).ToString(), true.ToString() };
                return (int)mGet.GetExecuteScalar("Sp_News_Search", mpara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Search(int? Type, int BeginRow, int EndRow, string SearchContent,int ServiceID, Status StatusID, NewsType NewsTypeID,  string OrderBy)
        {
            try
            {
                string[] mpara = { "Type", "BeginRow", "EndRow", "SearchContent","ServiceID","StatusID", "NewsTypeID",  "OrderBy", "IsTotalRow" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), SearchContent, ServiceID.ToString(), ((int)StatusID).ToString(), ((int)NewsTypeID).ToString(), OrderBy, false.ToString() };
                return mGet.GetDataTable("Sp_News_Search", mpara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
