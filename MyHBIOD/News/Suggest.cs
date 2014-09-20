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
    public class Suggest
    {
      
        
      
        MyExecuteData mExec;
        MyGetData mGet;

        public Suggest()
        {
            mExec = new MyExecuteData();
            mGet = new MyGetData();
        }

        public Suggest(string KeyConnect_InConfig)
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
                DataSet mSet = mGet.GetDataSet("Sp_Suggest_Select", mPara, mValue);
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
        /// <para>Type = 3: Lấy tất cả</para>
        /// </param>
        /// <param name="Para_1"></param>
        /// <returns></returns>
        public DataTable Select(int Type)
        {
            try
            {
                string[] mPara = { "Type"};
                string[] mValue = { Type.ToString() };
                return mGet.GetDataTable("Sp_Suggest_Select", mPara, mValue);
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
        /// <para>Type = 1: Lấy thông tin chi tiết 1 Record (Para_1 = SuggestID)</para>
        /// <para>Type = 2: Lấy tất cả các gợi ý đã active theo câu hỏi (Para_1 = QuestionID) </para>
        /// </param>
        /// <param name="Para_1"></param>
        /// <returns></returns>
        public DataTable Select(int Type, string Para_1)
        {
            try
            {
                string[] mPara = { "Type", "Para_1" };
                string[] mValue = { Type.ToString(), Para_1 };
                return mGet.GetDataTable("Sp_Suggest_Select", mPara, mValue);
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
                if (mExec.ExecProcedure("Sp_Suggest_Insert", mpara, mValue) > 0)
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

        public bool Active(int Type, bool IsActive, string XMLContent)
        {
            try
            {
                string[] mpara = { "Type", "IsActive", "XMLContent" };
                string[] mValue = { Type.ToString(), IsActive.ToString(), XMLContent };
                if (mExec.ExecProcedure("Sp_Suggest_Active", mpara, mValue) > 0)
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
                if (mExec.ExecProcedure("Sp_Suggest_Delete", mpara, mValue) > 0)
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
                if (mExec.ExecProcedure("Sp_Suggest_Update", mpara, mValue) > 0)
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
               
        public int TotalRow(int? Type, string SearchContent, int QuestionID,bool? IsActive)
        {
            try
            {
                string[] mpara = { "Type", "SearchContent", "QuestionID", "IsActive", "IsTotalRow" };
                string[] mValue = { Type.ToString(), SearchContent, QuestionID.ToString(), (IsActive == null ? null : IsActive.ToString()), true.ToString() };
                return (int)mGet.GetExecuteScalar("Sp_Suggest_Search", mpara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Search(int? Type, int BeginRow, int EndRow, string SearchContent, int QuestionID, bool? IsActive, string OrderBy)
        {
            try
            {
                string[] mpara = { "Type", "BeginRow", "EndRow", "SearchContent", "QuestionID", "IsActive", "OrderBy", "IsTotalRow" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), SearchContent, QuestionID.ToString(), (IsActive == null ? null : IsActive.ToString()), OrderBy, false.ToString() };
                return mGet.GetDataTable("Sp_Suggest_Search", mpara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
