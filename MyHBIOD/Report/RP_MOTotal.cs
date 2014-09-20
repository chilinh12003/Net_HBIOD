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
    public class RP_MOTotal
    {
        MyExecuteData mExec;
        MyGetData mGet;
        string KeyConnect_InConfig = string.Empty;

        public RP_MOTotal()
        {
            mExec = new MyExecuteData();
            mGet = new MyGetData();
        }

        public RP_MOTotal(string KeyConnect_InConfig)
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
       

        public DateTime ReportDay = DateTime.MinValue;
        public double MOTotal = 0;
        public double MOCorrectTotal = 0;
        public double SubCorrectTotal = 0;
        public double MOCorrectRate=0;
        public double MOSaleTotal=0;
        public double Sub1Correct=0;
        public double Sub2Correct=0;
        public double Sub4Correct=0;
        public double Sub5Correct=0;
        public double Sub10Correct=0;
        public double RateSubByDay=0;
        public double RateSaleByDay=0;
        public string Note=string.Empty;

        /// <summary>
        /// Tổng sub tham giá gửi MT trong ngày
        /// </summary>
        public double SubTotal = 0;
        public bool IsNull
        {
            get
            {
                if (ReportDay == DateTime.MinValue || MOTotal == 0)
                    return true;
                else
                    return false;
            }
        }

        public void Clear()
        {
            ReportDay = DateTime.MinValue;
            MOTotal = 0;
            MOCorrectTotal = 0;
            SubCorrectTotal = 0;
            MOCorrectRate = 0;
            MOSaleTotal = 0;
            Sub1Correct = 0;
            Sub2Correct = 0;
            Sub4Correct = 0;
            Sub5Correct = 0;
            Sub10Correct = 0;
            RateSubByDay = 0;
            RateSaleByDay = 0;
            Note = string.Empty;
        }

        public RP_MOTotal Convert(DataTable mTable)
        {
            try
            {
                mExec = new MyExecuteData(KeyConnect_InConfig);
                mGet = new MyGetData(KeyConnect_InConfig);

                if (mTable == null || mTable.Rows.Count < 1)
                    return new RP_MOTotal(this.KeyConnect_InConfig);

                RP_MOTotal mObj = new RP_MOTotal(this.KeyConnect_InConfig);               
                DataRow mRow = mTable.Rows[0];
                mObj.ReportDay = mRow["ReportDay"] != DBNull.Value ? (DateTime)mRow["ReportDay"] : DateTime.Now;
                mObj.MOTotal = mRow["MOTotal"] != DBNull.Value ? (double)mRow["MOTotal"] : 0;
                mObj.MOCorrectTotal = mRow["MOCorrectTotal"] != DBNull.Value ? (double)mRow["MOCorrectTotal"] : 0;
                mObj.SubCorrectTotal = mRow["SubCorrectTotal"] != DBNull.Value ? (double)mRow["SubCorrectTotal"] : 0;
                mObj.MOCorrectRate = mRow["MOCorrectRate"] != DBNull.Value ? (double)mRow["MOCorrectRate"] : 0;
                mObj.MOSaleTotal = mRow["MOSaleTotal"] != DBNull.Value ? (double)mRow["MOSaleTotal"] : 0;
                mObj.Sub1Correct = mRow["Sub1Correct"] != DBNull.Value ? (double)mRow["Sub1Correct"] : 0;
                mObj.Sub2Correct = mRow["Sub2Correct"] != DBNull.Value ? (double)mRow["Sub2Correct"] : 0;
                mObj.Sub4Correct = mRow["Sub4Correct"] != DBNull.Value ? (double)mRow["Sub4Correct"] : 0;
                mObj.Sub5Correct = mRow["Sub5Correct"] != DBNull.Value ? (double)mRow["Sub5Correct"] : 0;
                mObj.Sub10Correct = mRow["Sub10Correct"] != DBNull.Value ? (double)mRow["Sub10Correct"] : 0;
                mObj.RateSubByDay = mRow["RateSubByDay"] != DBNull.Value ? (double)mRow["RateSubByDay"] : 0;
                mObj.RateSaleByDay = mRow["RateSaleByDay"] != DBNull.Value ? (double)mRow["RateSaleByDay"] : 0;              

                mObj.Note = mRow["Note"] != DBNull.Value ? mRow["Note"].ToString() : string.Empty;

                return mObj;
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
                Note = string.Empty;
                DataRow mRow = mTable.NewRow();
                mRow["ReportDay"] = this.ReportDay;
                mRow["MOTotal"] = this.MOTotal;
                mRow["MOCorrectTotal"] = this.MOCorrectTotal;
                mRow["SubCorrectTotal"] = this.SubCorrectTotal;
                mRow["MOCorrectRate"] = this.MOCorrectRate;
                mRow["MOSaleTotal"] = this.MOSaleTotal;
                mRow["Sub1Correct"] = this.Sub1Correct;
                mRow["Sub2Correct"] = this.Sub2Correct;
                mRow["Sub4Correct"] = this.Sub4Correct;
                mRow["Sub5Correct"] = this.Sub5Correct;
                mRow["Sub10Correct"] = this.Sub10Correct;
                mRow["RateSubByDay"] = this.RateSubByDay;
                mRow["RateSaleByDay"] = this.RateSaleByDay;
                mRow["Note"] = this.Note;

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
                DataSet mSet = mGet.GetDataSet("Sp_RP_MOTotal_Select", mPara, mValue);
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
                return mGet.GetDataTable("Sp_RP_MOTotal_Select", mPara, mValue);
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
                return mGet.GetDataTable("Sp_RP_MOTotal_Select", mPara, mValue);
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
                return mGet.GetDataTable("Sp_RP_MOTotal_Select", mPara, mValue);
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
                if (mExec.ExecProcedure("Sp_RP_MOTotal_Insert", mpara, mValue) > 0)
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
                if (mExec.ExecProcedure("Sp_RP_MOTotal_Update", mpara, mValue) > 0)
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

                return (int)mGet.GetExecuteScalar("Sp_RP_MOTotal_Search", mPara, mValue);
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
                DataTable mTable = mGet.GetDataTable("Sp_RP_MOTotal_Search", mpara, mValue);

                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



    }

}