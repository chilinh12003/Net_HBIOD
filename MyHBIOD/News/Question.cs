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
    public class Question
    {
        public enum Status
        {
            [DescriptionAttribute("Nothing")]
            Nothing = 0,
            [DescriptionAttribute("Mới tạo")]
            New = 1,
            [DescriptionAttribute("Đang choi")]
            Playing = 2,
            [DescriptionAttribute("Kết thúc")]
            Finish = 3,           
        }

      
        MyExecuteData mExec;
        MyGetData mGet;

        public Question()
        {
            mExec = new MyExecuteData();
            mGet = new MyGetData();
        }

        public Question(string KeyConnect_InConfig)
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
                DataSet mSet = mGet.GetDataSet("Sp_Question_Select", mPara, mValue);
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
        /// <para>Type = 1: Lấy thông tin chi tiết 1 Record (Para_1 = QuestionID)</para>
        /// <para>Type = 2: Lấy câu hỏi theo ngày (Para_1 = PlayDate) </para>
        /// <para>Type = 3: Lấy 1 câu hỏi mới nhất đang chạy (Para_1 = StatusID)</para>
        /// 
        /// </param>
        /// <param name="Para_1"></param>
        /// <returns></returns>
        public DataTable Select(int Type, string Para_1)
        {
            try
            {
                string[] mPara = { "Type", "Para_1" };
                string[] mValue = { Type.ToString(), Para_1 };
                return mGet.GetDataTable("Sp_Question_Select", mPara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Type">
        /// <para>Type = 4: Lấy câu hỏi theo Status va theo ngày (Para_1 = StatusID, Para_2 = PlayDate) </para>
        /// </param>
        /// <param name="Para_1"></param>
        /// <param name="Para_2"></param>
        /// <returns></returns>
 public DataTable Select(int Type, string Para_1, string Para_2)
        {
            try
            {
                string[] mPara = { "Type", "Para_1","Para_2" };
                string[] mValue = { Type.ToString(), Para_1, Para_2 };
                return mGet.GetDataTable("Sp_Question_Select", mPara, mValue);
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
                if (mExec.ExecProcedure("Sp_Question_Insert", mpara, mValue) > 0)
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
                if (mExec.ExecProcedure("Sp_Question_Delete", mpara, mValue) > 0)
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
                if (mExec.ExecProcedure("Sp_Question_Update", mpara, mValue) > 0)
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
               
        public int TotalRow(int? Type, string SearchContent, Status StatusID)
        {
            try
            {
                string[] mpara = { "Type", "SearchContent","StatusID", "IsTotalRow" };
                string[] mValue = { Type.ToString(), SearchContent,((int)StatusID).ToString(), true.ToString() };
                return (int)mGet.GetExecuteScalar("Sp_Question_Search", mpara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Search(int? Type, int BeginRow, int EndRow, string SearchContent, Status StatusID,  string OrderBy)
        {
            try
            {
                string[] mpara = { "Type", "BeginRow", "EndRow", "SearchContent","StatusID",  "OrderBy", "IsTotalRow" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), SearchContent,((int)StatusID).ToString(),  OrderBy, false.ToString() };
                return mGet.GetDataTable("Sp_Question_Search", mpara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
