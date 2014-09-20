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
            Default = 100, Invalid = 101, Help = 102, SystemError = 103, Fail = 104, GetOTPSuccess = 105, GetOTPNotReg = 106,
            PushMT =
                107,
            // -----ĐĂNG KÝ DỊCH VỤ
            /// <summary>
            ///  Đăng ký mới thành công
            /// </summary>
            RegNewSuccess = 200,

            /// <summary>
            ///  Đăng ký lai thành công và miễn phí
            /// </summary>
            RegAgainSuccessFree = 201,
            /// <summary>
            ///  Đăng ký lại thành công không miễn phí  = đăng ký lại nhưng hết thời
            ///  gian khuyến mại
            /// </summary>
            RegAgainSuccessNotFree = 202,

            /// <summary>
            ///  Đăng ký rồi nhưng lại đăng ký tiếp vần còn trong thời gian khuyến mại
            /// </summary>
            RegRepeatFree = 203,

            /// <summary>
            ///  Đắng ký lặp trong thời gian hết khuyến mại
            /// </summary>
            RegRepeatNotFree = 204,

            /// <summary>
            ///  Đăng ký nhưng tải khoản khách hàng không đủ tiền
            /// </summary>
            RegNotEnoughMoney = 205,
            /// <summary>
            ///  Đăng ký không thành công
            /// </summary>
            RegFail = 206,

            /// <summary>
            ///  DK nhưng hệ thống bị lỗi
            /// </summary>
            RegSystemError = 207,


            // -----HỦY DỊCH VỤ
            /// <summary>
            ///  Hủy thành công dịch vụ
            /// </summary>
            DeregSuccess = 300,

            /// <summary>
            ///  Huy khi mà chưa đăng ký dịch vụ
            /// </summary>
            DeregNotRegister = 301,

            /// <summary>
            ///  Hủy không thành công do lỗi hệ thống...
            /// </summary>
            DeregFail = 302,

            DeregSystemError = 303,

            /// <summary>
            ///  Hủy khi gia hạn không thành công
            /// </summary>
            DeregExtendFail = 304,

            /// <summary>
            ///  Nội dung MT DK FREE thành công từ kênh CCOS của Vinaphone
            /// </summary>
            RegCCOSSuccessFree = 305,

            /// <summary>
            ///  Nội dung MT DK Tính phí thành công từ kênh CCOS của Vinaphone
            /// </summary>
            RegCCOSSuccessNotFree = 306,

            // ----MUA DỮ KIỆN

            /// <summary>
            ///  Mua dữ kiện khi chưa đăng ký
            /// </summary>
            BuySugNotReg = 400,

            /// <summary>
            ///  Mua gợi ý nhưng không đủ tiền
            /// </summary>
            BuySugNotEnoughMoney = 401,

            /// <summary>
            ///  Mua dữ kiện khi phiên chơi chưa bắt đầu hoặc đã kết thúc
            /// </summary>
            BuySugExpire = 402,

            /// <summary>
            ///  Mua dữ kiện khi vượt quá 20 lần trong 1 ngày
            /// </summary>
            BuySugLimit = 403,

            /// <summary>
            ///  Mua dữ kiện không thành công do lỗi hệ thống hoặc một lý do
            ///  nào khác
            /// </summary>
            BuySugFail = 404,

            /// <summary>
            ///  Mua dữ kiện khi đã trả lời đúng trước đó
            /// </summary>
            BuySugAnswerRight = 405,

            /// <summary>
            ///  Mua thành công và trả về dữ kiện
            /// </summary>
            BuySugSuccess = 406,

            /// <summary>
            ///  Bản tin nhắc nhở khi mua dữ kiện  = nếu có
            /// </summary>
            BuySugNotify = 407,

            /// <summary>
            ///  Mua dữ kiện khi thuê bao gia hạn không thành công
            /// </summary>
            BuySugNotExtend = 408,

            // -----TRẢ LỜI 
            /// <summary>
            ///  trả lời khi chưa mua dữ kiện
            /// </summary>
            AnswerNotBuy = 500,

            /// <summary>
            ///  Trả lời khi chưa đăng ký dịch vụ
            /// </summary>
            AnswerNotReg = 501,

            /// <summary>
            ///  Dự đoán vượt quá số lần cho phép, mỗi 1 lần mua chỉ được
            ///  dự đoán 1 lần Và dự đoán là cho lần mua gần nhất
            /// </summary>
            AnswerLimit = 502,

            /// <summary>
            ///  Trả lời khi phiên chưa diễn ra
            /// </summary>
            AnswerExpire = 503,

            /// <summary>
            ///  Dự đoán sai
            /// </summary>
            AnswerWrong = 504,

            /// <summary>
            ///  Dự đoán không thành công do phát sinh lỗi hoặc lý do khác...
            /// </summary>
            AnswerFail = 505,

            /// <summary>
            ///  Dự đoán chính xác với kết quả
            /// </summary>
            AnswerSuccess = 506,

            /// <summary>
            ///  Thông báo về phiên chơi mới
            /// </summary>
            NotifyNewSession = 600,

            /// <summary>
            ///  thông báo về người chiến thằng
            /// </summary>
            NotifyWinner = 601,

            /// <summary>
            ///  Tin tức hàng này
            /// </summary>
            NewsDaily = 602,

            /// <summary>
            ///  Thông báo khi gia hạn thành công
            /// </summary>
            NotifyRenewSuccess = 603,

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
