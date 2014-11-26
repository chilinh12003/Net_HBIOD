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
    public class DefineMT
    {

        public enum MTType
        {
            Default = 100, Invalid = 101, Help = 102, SystemError = 103, Fail = 104,
            PushMT = 105, Reminder = 106,ReSendMT=107,

            // -----YÊU CẦU TẢI NỘI DUNG
            /// <summary>
            ///  Yêu cầu tải nội dung từ khách hàng thanh cong
            /// </summary>
            RequestSuccess = 200,

            /// <summary>
            ///  Không có tin tức nào để trả về cho khách hàng
            /// </summary>
            RequestNoNews = 201,

            /// <summary>
            ///  Mua nội dung không thành công vì một lý do nào đó
            /// </summary>
            RequestFail = 202,

            // ----- CONFIRM DOWNLOAD
            /// <summary>
            ///  KH xác nhận nội dung cần tải thành công
            /// </summary>
            ConfirmSuccess = 300,

            /// <summary>
            ///  KH không đủ tiền để tải nội dung
            /// </summary>
            ConfirmNotEnoughMoney = 301,

            /// <summary>
            ///  Nội dung xác nhận download không tồn tại
            /// </summary>
            ConfirmInvalidContent = 302,

            /// <summary>
            ///  Confirm download không thành công
            /// </summary>
            ConfirmFail = 303,
            // -----THÔNG BÁO

            /// <summary>
            ///  Thông báo sau khi khách hàng không confirm trong 1 khoảng thời gian nhất định
            /// </summary>
            NotifyExpire = 400,
        }
     MyExecuteData mExec;
        MyGetData mGet;

        public DefineMT()
        {
            mExec = new MyExecuteData();
            mGet = new MyGetData();
        }

        public DefineMT(string KeyConnect_InConfig)
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
                DataSet mSet = mGet.GetDataSet("Sp_DefineMT_Select", mPara, mValue);
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
        /// 
        /// </summary>
        /// <param name="Type">Cách thức lấy
        /// <para>Type = 1: Lấy chi tiết 1 Record (Para_1 = DefintMTID)</para>
        /// </param>
        /// <param name="Para_1"></param>
        /// <returns></returns>
        public DataTable Select(int Type, string Para_1)
        {
            try
            {
                string[] mPara = { "Type", "Para_1" };
                string[] mValue = { Type.ToString(), Para_1 };
                return mGet.GetDataTable("Sp_DefineMT_Select", mPara, mValue);
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
                if (mExec.ExecProcedure("Sp_DefineMT_Insert", mpara, mValue) > 0)
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
                if (mExec.ExecProcedure("Sp_DefineMT_Delete", mpara, mValue) > 0)
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
                if (mExec.ExecProcedure("Sp_DefineMT_Update", mpara, mValue) > 0)
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
                if (mExec.ExecProcedure("Sp_DefineMT_Active", mpara, mValue) > 0)
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

        public int TotalRow(int? Type, string SearchContent,int MTTypeID, bool? IsActive)
        {
            try
            {
                string[] mPara = { "Type", "SearchContent", "MTTypeID", "IsActive", "IsTotalRow" };
                string[] mValue = { Type.ToString(), SearchContent, MTTypeID.ToString(),(IsActive == null ? null : IsActive.ToString()), true.ToString() };

                return (int)mGet.GetExecuteScalar("Sp_DefineMT_Search", mPara, mValue);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }


        public DataTable Search(int? Type, int BeginRow, int EndRow, string SearchContent,int MTTypeID, bool? IsActive, string OrderBy)
        {
            try
            {
                string[] mpara = { "Type", "BeginRow", "EndRow", "SearchContent","MTTypeID", "IsActive",  "OrderBy","IsTotalRow" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), SearchContent, MTTypeID.ToString(), (IsActive == null ? null : IsActive.ToString()), OrderBy, false.ToString() };
                return mGet.GetDataTable("Sp_DefineMT_Search", mpara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
