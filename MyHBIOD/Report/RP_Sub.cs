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
    public class RP_Sub
    {
        MyExecuteData mExec;
        MyGetData mGet;
        string KeyConnect_InConfig = string.Empty;


        public DateTime ReportDay = DateTime.MinValue;
        public double SubTotal = 0;
        public double SubNew = 0;
        public double SubSuccess = 0;
       
        public double SubAll= 0;
        public double SubSMS = 0;
        public double SubUSSD = 0;
        public double SubIVR = 0;
        public double SubWEB = 0;
        public double SubWAP = 0;
        public double SubAPP = 0;
        public double SubCSKH = 0;

        public double UnsubTotal = 0;
        public double UnsubNew = 0;
        public double UnsubSuccess = 0;
        public double UnsubSelf = 0;
        public double UnsubExtend = 0;
        public double UnSubSMS = 0;
        public double UnSubUSSD = 0;
        public double UnSubIVR = 0;
        public double UnSubWEB = 0;
        public double UnSubWAP = 0;
        public double UnSubAPP = 0;
        public double UnSubCSKH = 0;

        public double UseByDay = 0;
        public double UseByMonth = 0;
        public double RenewTotal = 0;
        public double RenewSuccess = 0;
        public double RenewFail = 0;
        public double RenewRate = 0;
        public double UseNot = 0;
        public double UseVeryFew = 0;
        public double UseFew = 0;
        public double UseModerate = 0;
        public double UseMuch = 0;
        public double UserVeryMuch = 0;
        public double SaleReg = 0;
        public double SaleRenew = 0;
        public double SaleContent = 0;
        public double SaleTotal = 0;
        public double RateUseByDay = 0;
        public double RateUseByMonth = 0;
        public double RateSaleDay = 0;
        public double RateSaleWeek = 0;
        public double RateSaleMonth = 0;
        public double RateComplete = 0;
        public string Note =string.Empty;

        /// <summary>
        /// Xóa hết dữ liệu thống kê
        /// </summary>
        public void Clear()
        {
            SubTotal = 0;
            SubNew = 0;
            SubSuccess = 0;
            SubAll = 0;
            SubSMS = 0;
            SubUSSD = 0;
            SubIVR = 0;
            SubWEB = 0;
            SubWAP = 0;
            SubAPP = 0;
            SubCSKH = 0;
            UnsubTotal = 0;
            UnsubNew = 0;
            UnsubSuccess = 0;
            UnsubSelf = 0;
            UnsubExtend = 0;
            UnSubSMS = 0;
            UnSubUSSD = 0;
            UnSubIVR = 0;
            UnSubWEB = 0;
            UnSubWAP = 0;
            UnSubAPP = 0;
            UnSubCSKH = 0;
            UseByDay = 0;
            UseByMonth = 0;
            RenewTotal = 0;
            RenewSuccess = 0;
            RenewFail = 0;
            RenewRate = 0;
            UseNot = 0;
            UseVeryFew = 0;
            UseFew = 0;
            UseModerate = 0;
            UseMuch = 0;
            UserVeryMuch = 0;
            SaleReg = 0;
            SaleRenew = 0;
            SaleContent = 0;
            SaleTotal = 0;
            RateUseByDay = 0;
            RateUseByMonth = 0;
            RateSaleDay = 0;
            RateSaleWeek = 0;
            RateSaleMonth = 0;
            RateComplete = 0;
            Note = string.Empty;
        }

        public RP_Sub Convert(DataTable mTable)
        {
            try
            {
                mExec = new MyExecuteData(KeyConnect_InConfig);
                mGet = new MyGetData(KeyConnect_InConfig);

                if (mTable == null || mTable.Rows.Count < 1)
                    return new RP_Sub(this.KeyConnect_InConfig);

                RP_Sub mObj = new RP_Sub(this.KeyConnect_InConfig);

                DataRow mRow = mTable.Rows[0];
                mObj.ReportDay= mRow["ReportDay"] != DBNull.Value ? (DateTime)mRow["ReportDay"] : DateTime.Now;
                mObj.SubTotal = mRow["SubTotal"] != DBNull.Value ? (double)mRow["SubTotal"] : 0;
                mObj.SubNew = mRow["SubNew"] != DBNull.Value ? (double)mRow["SubNew"] : 0;
                mObj.SubSuccess = mRow["SubSuccess"] != DBNull.Value ? (double)mRow["SubSuccess"] : 0;
                mObj.SubSMS = mRow["SubSMS"] != DBNull.Value ? (double)mRow["SubSMS"] : 0;
                mObj.SubUSSD = mRow["SubUSSD"] != DBNull.Value ? (double)mRow["SubUSSD"] : 0;
                mObj.SubIVR = mRow["SubIVR"] != DBNull.Value ? (double)mRow["SubIVR"] : 0;
                mObj.SubWEB = mRow["SubWEB"] != DBNull.Value ? (double)mRow["SubWEB"] : 0;
                mObj.SubWAP = mRow["SubWAP"] != DBNull.Value ? (double)mRow["SubWAP"] : 0;
                mObj.SubAPP = mRow["SubAPP"] != DBNull.Value ? (double)mRow["SubAPP"] : 0;
                mObj.SubCSKH = mRow["SubCSKH"] != DBNull.Value ? (double)mRow["SubCSKH"] : 0;
                mObj.UnsubTotal = mRow["UnsubTotal"] != DBNull.Value ? (double)mRow["UnsubTotal"] : 0;
                mObj.UnsubSelf = mRow["UnsubSelf"] != DBNull.Value ? (double)mRow["UnsubSelf"] : 0;
                mObj.UnsubExtend = mRow["UnsubExtend"] != DBNull.Value ? (double)mRow["UnsubExtend"] : 0;
                mObj.UnSubSMS = mRow["UnSubSMS"] != DBNull.Value ? (double)mRow["UnSubSMS"] : 0;
                mObj.UnSubUSSD = mRow["UnSubUSSD"] != DBNull.Value ? (double)mRow["UnSubUSSD"] : 0;
                mObj.UnSubIVR = mRow["UnSubIVR"] != DBNull.Value ? (double)mRow["UnSubIVR"] : 0;
                mObj.UnSubWEB = mRow["UnSubWEB"] != DBNull.Value ? (double)mRow["UnSubWEB"] : 0;
                mObj.UnSubWAP = mRow["UnSubWAP"] != DBNull.Value ? (double)mRow["UnSubWAP"] : 0;
                mObj.UnSubAPP = mRow["UnSubAPP"] != DBNull.Value ? (double)mRow["UnSubAPP"] : 0;
                mObj.UnSubCSKH = mRow["UnSubCSKH"] != DBNull.Value ? (double)mRow["UnSubCSKH"] : 0;
                mObj.UseByDay = mRow["UseByDay"] != DBNull.Value ? (double)mRow["UseByDay"] : 0;
                mObj.UseByMonth = mRow["UseByMonth"] != DBNull.Value ? (double)mRow["UseByMonth"] : 0;
                mObj.RenewTotal = mRow["RenewTotal"] != DBNull.Value ? (double)mRow["RenewTotal"] : 0;
                mObj.RenewSuccess = mRow["RenewSuccess"] != DBNull.Value ? (double)mRow["RenewSuccess"] : 0;
                mObj.RenewFail = mRow["RenewFail"] != DBNull.Value ? (double)mRow["RenewFail"] : 0;
                mObj.RenewRate = mRow["RenewRate"] != DBNull.Value ? (double)mRow["RenewRate"] : 0;
                mObj.UseNot = mRow["UseNot"] != DBNull.Value ? (double)mRow["UseNot"] : 0;
                mObj.UseVeryFew = mRow["UseVeryFew"] != DBNull.Value ? (double)mRow["UseVeryFew"] : 0;
                mObj.UseFew = mRow["UseFew"] != DBNull.Value ? (double)mRow["UseFew"] : 0;
                mObj.UseModerate = mRow["UseModerate"] != DBNull.Value ? (double)mRow["UseModerate"] : 0;
                mObj.UseMuch = mRow["UseMuch"] != DBNull.Value ? (double)mRow["UseMuch"] : 0;
                mObj.UserVeryMuch = mRow["UserVeryMuch"] != DBNull.Value ? (double)mRow["UserVeryMuch"] : 0;
                mObj.SaleReg = mRow["SaleReg"] != DBNull.Value ? (double)mRow["SaleReg"] : 0;
                mObj.SaleRenew = mRow["SaleRenew"] != DBNull.Value ? (double)mRow["SaleRenew"] : 0;
                mObj.SaleContent = mRow["SaleContent"] != DBNull.Value ? (double)mRow["SaleContent"] : 0;
                mObj.SaleTotal = mRow["SaleTotal"] != DBNull.Value ? (double)mRow["SaleTotal"] : 0;
                mObj.RateUseByDay = mRow["RateUseByDay"] != DBNull.Value ? (double)mRow["RateUseByDay"] : 0;
                mObj.RateUseByMonth = mRow["RateUseByMonth"] != DBNull.Value ? (double)mRow["RateUseByMonth"] : 0;
                mObj.RateSaleDay = mRow["RateSaleDay"] != DBNull.Value ? (double)mRow["RateSaleDay"] : 0;
                mObj.RateSaleWeek = mRow["RateSaleWeek"] != DBNull.Value ? (double)mRow["RateSaleWeek"] : 0;
                mObj.RateSaleMonth = mRow["RateSaleMonth"] != DBNull.Value ? (double)mRow["RateSaleMonth"] : 0;
                mObj.RateComplete = mRow["RateComplete"] != DBNull.Value ? (double)mRow["RateComplete"] : 0;

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

                DataRow mRow = mTable.NewRow();
                mRow["ReportDay"] = this.ReportDay;
                mRow["SubTotal"] = this.SubTotal;
                mRow["SubNew"] = this.SubNew;
                mRow["SubSuccess"] = this.SubSuccess;
                mRow["SubSMS"] = this.SubSMS;
                mRow["SubUSSD"] = this.SubUSSD;
                mRow["SubIVR"] = this.SubIVR;
                mRow["SubWEB"] = this.SubWEB;
                mRow["SubWAP"] = this.SubWAP;
                mRow["SubAPP"] = this.SubAPP;
                mRow["SubCSKH"] = this.SubCSKH;
                mRow["UnsubTotal"] = this.UnsubTotal;
                mRow["UnsubNew"] = this.UnsubNew;
                mRow["UnsubSuccess"] = this.UnsubSuccess;
                mRow["UnsubSelf"] = this.UnsubSelf;
                mRow["UnsubExtend"] = this.UnsubExtend;
                mRow["UnSubSMS"] = this.UnSubSMS;
                mRow["UnSubUSSD"] = this.UnSubUSSD;
                mRow["UnSubIVR"] = this.UnSubIVR;
                mRow["UnSubWEB"] = this.UnSubWEB;
                mRow["UnSubWAP"] = this.UnSubWAP;
                mRow["UnSubAPP"] = this.UnSubAPP;
                mRow["UnSubCSKH"] = this.UnSubCSKH;
                mRow["UseByDay"] = this.UseByDay;
                mRow["UseByMonth"] = this.UseByMonth;
                mRow["RenewTotal"] = this.RenewTotal;
                mRow["RenewSuccess"] = this.RenewSuccess;
                mRow["RenewFail"] = this.RenewFail;
                mRow["RenewRate"] = this.RenewRate;
                mRow["UseNot"] = this.UseNot;
                mRow["UseVeryFew"] = this.UseVeryFew;
                mRow["UseFew"] = this.UseFew;
                mRow["UseModerate"] = this.UseModerate;
                mRow["UseMuch"] = this.UseMuch;
                mRow["UserVeryMuch"] = this.UserVeryMuch;
                mRow["SaleReg"] = this.SaleReg;
                mRow["SaleRenew"] = this.SaleRenew;
                mRow["SaleContent"] = this.SaleContent;
                mRow["SaleTotal"] = this.SaleTotal;
                mRow["RateUseByDay"] = this.RateUseByDay;
                mRow["RateUseByMonth"] =this.RateUseByMonth;
                mRow["RateSaleDay"] = this.RateSaleDay;
                mRow["RateSaleWeek"] = this.RateSaleWeek;
                mRow["RateSaleMonth"] = this.RateSaleMonth;
                mRow["RateComplete"] = this.RateComplete;
                mRow["Note"] = this.Note;

                mTable.Rows.Add(mRow);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool IsNull
        {
            get
            {
                if (ReportDay == DateTime.MinValue || SubTotal == 0)
                    return true;
                else
                    return false;
            }
        }

        public RP_Sub()
        {
            mExec = new MyExecuteData();
            mGet = new MyGetData();
        }

        public RP_Sub(string KeyConnect_InConfig)
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

        public DataSet CreateDataSet()
        {
            try
            {
                string[] mPara = { "Type" };
                string[] mValue = { "0" };
                DataSet mSet = mGet.GetDataSet("Sp_RP_Sub_Select", mPara, mValue);
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
                return mGet.GetDataTable("Sp_RP_Sub_Select", mPara, mValue);
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
        /// <para>Type = 3: Lấy theo ngày cho report email (Para_1 = BeginDate, Para_2 = Endate)</para>
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
                return mGet.GetDataTable("Sp_RP_Sub_Select", mPara, mValue);
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
                string[] mPara = { "Type", "Para_1", "Para_2","Para_3" };
                string[] mValue = { Type.ToString(), Para_1, Para_2, Para_3 };
                return mGet.GetDataTable("Sp_RP_Sub_Select", mPara, mValue);
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
                if (mExec.ExecProcedure("Sp_RP_Sub_Insert", mpara, mValue) > 0)
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
                if (mExec.ExecProcedure("Sp_RP_Sub_Update", mpara, mValue) > 0)
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
                string[] mValue = { Type.ToString(),str_BeginDate, str_EndDate, true.ToString() };

                return (int)mGet.GetExecuteScalar("Sp_RP_Sub_Search", mPara, mValue);
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
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(),  str_BeginDate, str_EndDate, OrderBy, false.ToString() };
                DataTable mTable = mGet.GetDataTable("Sp_RP_Sub_Search", mpara, mValue);

                return mTable;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
