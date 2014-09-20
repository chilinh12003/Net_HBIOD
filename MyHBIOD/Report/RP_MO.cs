using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MyConnect.SQLServer;
using MyUtility;
using System.Web;
using System.ComponentModel;


namespace MyHBIOD.Report
{
    public class RP_MO
    {
        MyExecuteData mExec;
        MyGetData mGet;
        string KeyConnect_InConfig = string.Empty;

        public RP_MO()
        {
            mExec = new MyExecuteData();
            mGet = new MyGetData();
        }

        public RP_MO(string KeyConnect_InConfig)
        {
            this.KeyConnect_InConfig = KeyConnect_InConfig;
            if (string.IsNullOrEmpty(this.KeyConnect_InConfig))
            {
                mExec = new MyExecuteData();
                mGet = new MyGetData();
            }
            else
            {
                mExec = new MyExecuteData(KeyConnect_InConfig);
                mGet = new MyGetData(KeyConnect_InConfig);
            }
        }

        /// <summary>
        /// Kiểu MO người dùng nhắn tin lên
        /// </summary>
        public enum MOType
        {
            [DescriptionAttribute("Nothing")]
            Nothing = 0,
            [DescriptionAttribute("MO Sai cú pháp")]
            Invalid = 1,
            [DescriptionAttribute("MO Đăng ký")]
            Register = 2,
            [DescriptionAttribute("MO Hủy")]
            Deregister = 3,
            [DescriptionAttribute("MO Mua dữ kiện")]
            BuyContent = 4,
            [DescriptionAttribute("MO Dự đoán")]
            Answer = 5,
            [DescriptionAttribute("MO khác")]
            Other = 6,            
        }

        public DateTime ReportDay = DateTime.MinValue;
        public MOType mMOType = MOType.Nothing;
        public string Keyword = string.Empty;
        public double SubCorrect = 0;
        public double MOCorrect = 0;
        public double MOChargeSuccess = 0;
        public double MOSale=0;

        public bool IsNull
        {
            get
            {
                if (ReportDay == DateTime.MinValue || mMOType == MOType.Nothing)
                    return true;
                else
                    return false;
            }
        }

        public void Clear()
        {
            ReportDay = DateTime.MinValue;
            mMOType = MOType.Nothing;
            Keyword = string.Empty;
            SubCorrect = 0;
            MOCorrect = 0;
            MOChargeSuccess = 0;
            MOSale = 0;
        }

        public RP_MO Convert(DataRow mRow)
        {
            try
            {
                mExec = new MyExecuteData(KeyConnect_InConfig);
                mGet = new MyGetData(KeyConnect_InConfig);

                RP_MO mObj = new RP_MO(this.KeyConnect_InConfig);
                if (mRow == null)
                    return mObj;

                mObj.ReportDay = mRow["ReportDay"] != DBNull.Value ? (DateTime)mRow["ReportDay"] : DateTime.Now;
                mObj.mMOType = mRow["MOTypeID"] != DBNull.Value ? (MOType)(int)mRow["MOTypeID"] : MOType.Nothing;
                mObj.Keyword = mRow["Keyword"] != DBNull.Value ? mRow["Keyword"].ToString() : string.Empty;
                mObj.SubCorrect = mRow["SubCorrect"] != DBNull.Value ? (double)mRow["SubCorrect"] : 0;
                mObj.MOCorrect = mRow["MOCorrect"] != DBNull.Value ? (double)mRow["MOCorrect"] : 0;
                mObj.MOChargeSuccess = mRow["MOChargeSuccess"] != DBNull.Value ? (double)mRow["MOChargeSuccess"] : 0;
                mObj.MOSale = mRow["MOSale"] != DBNull.Value ? (double)mRow["MOSale"] : 0;

                return mObj;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<RP_MO> Convert(DataTable mTable)
        {
            try
            {
                List<RP_MO> mList = new List<RP_MO>();

                foreach (DataRow mRow in mTable.Rows)
                {
                    mList.Add(Convert(mRow));
                }
                return mList;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void AddNewRow(ref DataTable mTable)
        {
            try
            {
                if (mTable == null || mTable.Columns.Count < 1)
                {
                    mTable = CreateDataSet().Tables[0].Clone();
                }

                DataRow mRow = mTable.NewRow();
                mRow["ReportDay"] = this.ReportDay;
                mRow["MOTypeID"] = (int)this.mMOType;
                mRow["Keyword"] = this.Keyword;
                mRow["SubCorrect"] = this.SubCorrect;
                mRow["MOCorrect"] = this.MOCorrect;
                mRow["MOChargeSuccess"] = this.MOChargeSuccess;
                mRow["MOSale"] = this.MOSale;
               
                mTable.Rows.Add(mRow);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataSet CreateDataSet()
        {
            try
            {
                string[] mPara = { "Type" };
                string[] mValue = { "0" };
                DataSet mSet = mGet.GetDataSet("Sp_RP_MO_Select", mPara, mValue);
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

        public DataTable Select(int Type, string Para_1)
        {
            try
            {
                string[] mPara = { "Type", "Para_1" };
                string[] mValue = { Type.ToString(), Para_1 };
                return mGet.GetDataTable("Sp_RP_MO_Select", mPara, mValue);
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
        /// <para>Type = 2: Lấy theo ngày (Para_1 = BeginDate, Para_2 = Endate)</para>
        /// </param>
        /// <param name="Para_1"></param>
        /// <param name="Para_2"></param>
        /// <returns></returns>
        public DataTable Select(int Type, string Para_1, string Para_2)
        {
            try
            {
                string[] mPara = { "Type", "Para_1", "Para_2" };
                string[] mValue = { Type.ToString(), Para_1, Para_2 };
                return mGet.GetDataTable("Sp_RP_MO_Select", mPara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Select(int Type, string Para_1, string Para_2, string Para_3)
        {
            try
            {
                string[] mPara = { "Type", "Para_1", "Para_2", "Para_3" };
                string[] mValue = { Type.ToString(), Para_1, Para_2, Para_3 };
                return mGet.GetDataTable("Sp_RP_MO_Select", mPara, mValue);
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
                if (mExec.ExecProcedure("Sp_RP_MO_Insert", mpara, mValue) > 0)
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
                if (mExec.ExecProcedure("Sp_RP_MO_Update", mpara, mValue) > 0)
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

        public int TotalRow(int? Type, DateTime BeginDate, DateTime EndDate)
        {
            try
            {
                string str_BeginDate = null;
                string str_EndDate = null;

                if (BeginDate != DateTime.MinValue && BeginDate != DateTime.MaxValue &&
                    EndDate != DateTime.MinValue && EndDate != DateTime.MaxValue)
                {
                    str_BeginDate = BeginDate.ToString(MyConfig.DateFormat_InsertToDB);
                    str_EndDate = EndDate.ToString(MyConfig.DateFormat_InsertToDB);
                }
                string[] mPara = { "Type", "BeginDate", "EndDate", "IsTotalRow" };
                string[] mValue = { Type.ToString(), str_BeginDate, str_EndDate, true.ToString() };

                return (int)mGet.GetExecuteScalar("Sp_RP_MO_Search", mPara, mValue);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }

        public DataTable Search(int? Type, int BeginRow, int EndRow, DateTime BeginDate, DateTime EndDate, string OrderBy)
        {
            try
            {
                string str_BeginDate = null;
                string str_EndDate = null;

                if (BeginDate != DateTime.MinValue && BeginDate != DateTime.MaxValue &&
                    EndDate != DateTime.MinValue && EndDate != DateTime.MaxValue)
                {
                    str_BeginDate = BeginDate.ToString(MyConfig.DateFormat_InsertToDB);
                    str_EndDate = EndDate.ToString(MyConfig.DateFormat_InsertToDB);
                }

                string[] mpara = { "Type", "BeginRow", "EndRow", "BeginDate", "EndDate", "OrderBy", "IsTotalRow" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), str_BeginDate, str_EndDate, OrderBy, false.ToString() };
                DataTable mTable = mGet.GetDataTable("Sp_RP_MO_Search", mpara, mValue);

                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }

}