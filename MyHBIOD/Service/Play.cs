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
    public class Play
    {
        public enum Status
        {
            [DescriptionAttribute("NULL")]
            /// <summary>
            /// 
            /// </summary>
            Nothing=0, 
            [DescriptionAttribute("Dự đoán đúng")]
            /// <summary>
            /// 
            /// </summary>
            CorrectAnswer=100,
            [DescriptionAttribute("Dự đoán sai")]
            /// <summary>
            /// 
            /// </summary>
            IncorrectAnswer=101,
            [DescriptionAttribute("Mua dữ kiện")]
            /// <summary>
            /// 
            /// </summary>
            BuySuggest=200,
        }
        public enum PlayType
        {
            [DescriptionAttribute("NULL")]
            /// <summary>
            /// 
            /// </summary>
            Nothing = 0,
            [DescriptionAttribute("Mua dữ kiện")]
            /// <summary>
            /// 
            /// </summary>
            BuySuggest = 1,
            [DescriptionAttribute("Dự đoán")]
            /// <summary>
            /// 
            /// </summary>
            Answer = 2,
        }

        MyExecuteData mExec;
        MyGetData mGet;

        public Play()
        {
            mExec = new MyExecuteData();
            mGet = new MyGetData();
        }

        public Play(string KeyConnect_InConfig)
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
                DataSet mSet = mGet.GetDataSet("Sp_Play_Select", mPara, mValue);
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
        /// Lấy dữ liệu Play
        /// </summary>
        /// <param name="Type">Cách thức lấy
        /// <para>Type = 0: Lấy dữ liệu mẫu</para>
        /// <para>Type = 1: Lấy thông tin chi tiết 1 record (Para_1 = PlayID)</para>
        /// <para>Type = 2: Lấy danh sách đối tác đã được kích hoạt (dành cho combobox)</para>
        /// <para>Type = 3: Lấy các đối tác theo PlayType (Para_1)</para>
        ///</param>
        /// <param name="Para_1"></param>
        /// <returns></returns>
        public DataTable Select(int? Type, string Para_1)
        {
            try
            {
                string[] mpara = { "Type", "Para_1" };
                string[] mValue = { Type.ToString(), Para_1 };
                return mGet.GetDataTable("Sp_Play_Select", mpara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
        public bool Active(int Type, bool IsActive, string XMLContent)
        {
            try
            {
                string[] mpara = { "Type", "IsActive", "XMLContent" };
                string[] mValue = { Type.ToString(), IsActive.ToString(), XMLContent };
                if (mExec.ExecProcedure("Sp_Play_Active", mpara, mValue) > 0)
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

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="PlayID"></param>
        /// <param name="XMLContent"></param>
        /// <returns></returns>
        public bool Delete(int Type, string XMLContent)
        {
            try
            {
                string[] mpara = { "Type", "XMLContent" };
                string[] mValue = { Type.ToString(), XMLContent };
                if (mExec.ExecProcedure("Sp_Play_Delete", mpara, mValue) > 0)
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


        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type">Type = 0</param>
        /// <param name="XMLContent"></param>
        /// <returns></returns>
        public bool Update(int Type, string XMLContent)
        {
            try
            {
                string[] mpara = { "Type", "XMLContent" };
                string[] mValue = { Type.ToString(), XMLContent };
                if (mExec.ExecProcedure("Sp_Play_Update", mpara, mValue) > 0)
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

        /// <summary>
        /// Insert dữ liệu và trả về 1 table cần lấy sau khi insert
        /// </summary>
        /// <param name="Type">Type = 0 </param>
        /// <param name="XMLContent"></param>
        /// <returns></returns>
        public bool Insert(int Type, string XMLContent)
        {
            try
            {
                string[] mpara = { "Type", "XMLContent" };
                string[] mValue = { Type.ToString(), XMLContent };

                if (mExec.ExecProcedure("Sp_Play_Insert", mpara, mValue) > 0)
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

        /// <summary>
        /// Lấy tổng số dòng cho phân trang
        /// </summary>
        /// <param name="Type"></param>
        /// <param name="SearchContent"></param>
        /// <param name="OrderBy"></param>
        /// <param name="IsActive"></param>
        /// <returns></returns>
        public int TotalRow(int? Type, string SearchContent,int PID, int PlayTypeID, int StatusID, int QuestionID, int SuggestID)
        {
            try
            {
                string[] mPara = { "Type", "SearchContent","PID", "PlayTypeID", "StatusID", "QuestionID", "SuggestID", "IsTotalRow" };
                string[] mValue = { Type.ToString(), SearchContent,PID.ToString(), PlayTypeID.ToString(), StatusID.ToString(),QuestionID.ToString(),SuggestID.ToString(), true.ToString() };

                return (int)mGet.GetExecuteScalar("Sp_Play_Search", mPara, mValue);

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
        public DataTable Search(int? Type, int BeginRow, int EndRow, string SearchContent,int PID, int PlayTypeID, int StatusID, int QuestionID, int SuggestID, string OrderBy)
        {
            try
            {
                string[] mpara = { "Type", "BeginRow", "EndRow", "SearchContent","PID", "PlayTypeID", "StatusID", "QuestionID", "SuggestID", "OrderBy", "IsTotalRow" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), SearchContent,PID.ToString(), PlayTypeID.ToString(), StatusID.ToString(), QuestionID.ToString(), SuggestID.ToString(), OrderBy, false.ToString() };
                DataTable mTable =  mGet.GetDataTable("Sp_Play_Search", mpara, mValue);
                DataColumn mCol_PlayTypeName = new DataColumn("PlayTypeName", typeof(string));
                DataColumn mCol_StatusName = new DataColumn("StatusName", typeof(string));
                mTable.Columns.Add(mCol_PlayTypeName);
                mTable.Columns.Add(mCol_StatusName);

                foreach (DataRow mRow in mTable.Rows)
                {
                   
                        mRow["StatusName"] = MyEnum.StringValueOf((Status)(int)mRow["StatusID"]);
                        mRow["PlayTypeName"] = MyEnum.StringValueOf((PlayType)(int)mRow["PlayTypeID"]);
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
