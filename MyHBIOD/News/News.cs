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
        /// <summary>
        /// Cung Hoàn đạo
        /// </summary>
        public enum Zodiac
        {
            [DescriptionAttribute("Tất cả")]
            Nothing = 0,
            [DescriptionAttribute("Bạch dương")]
            BachDuong = 1,
            [DescriptionAttribute("Kim Ngưu")]
            KimNguu = 2,
            [DescriptionAttribute("Song Tử")]
            SongTu = 3,
            [DescriptionAttribute("Cự Giải")]
            Cugiai = 4,
            [DescriptionAttribute("Sư Tử")]
            Sutu = 5,
            [DescriptionAttribute("Xử nữ")]
            Xunu = 6,
            [DescriptionAttribute("Hổ Cáp")]
            HoCap = 7,
            [DescriptionAttribute("Nhân Mã")]
            NhanMa = 8,
            [DescriptionAttribute("Ma Kết")]
            MaKet = 9,
            [DescriptionAttribute("Bảo Bình")]
            BaoBinh = 10,
            [DescriptionAttribute("Song Ngư")]
            SongNgu = 11,
            [DescriptionAttribute("Thiên Bình")]
            ThienBinh = 12,
        }
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
            [DescriptionAttribute("Tin mới")]
            New = 1,
            [DescriptionAttribute("Đang gửi")]
            Sending = 2,
            [DescriptionAttribute("Đã gửi")]
            Complete = 3,
            [DescriptionAttribute("Chờ duyệt")]
            Waiting = 4,
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
               
        public int TotalRow(int? Type, string SearchContent, Status StatusID, NewsType NewsTypeID)
        {
            try
            {
                string[] mpara = { "Type", "SearchContent","StatusID","NewsTypeID", "IsTotalRow" };
                string[] mValue = { Type.ToString(), SearchContent,((int)StatusID).ToString(),((int)NewsTypeID).ToString(), true.ToString() };
                return (int)mGet.GetExecuteScalar("Sp_News_Search", mpara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Search(int? Type, int BeginRow, int EndRow, string SearchContent, Status StatusID, NewsType NewsTypeID,  string OrderBy)
        {
            try
            {
                string[] mpara = { "Type", "BeginRow", "EndRow", "SearchContent","StatusID", "NewsTypeID",  "OrderBy", "IsTotalRow" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), SearchContent,((int)StatusID).ToString(), ((int)NewsTypeID).ToString(), OrderBy, false.ToString() };
                return mGet.GetDataTable("Sp_News_Search", mpara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
