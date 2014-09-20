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
    public class Service
    {

        public enum ServiceType
        {
            [DescriptionAttribute("Loại dịch vụ")]
            Nothing = 0,
            [DescriptionAttribute("Dịch vụ bình thường")]
            Normal = 1,

        }

      
        MyExecuteData mExec;
        MyGetData mGet;

        public Service()
        {
            mExec = new MyExecuteData();
            mGet = new MyGetData();
        }

        public Service(string KeyConnect_InConfig)
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
                DataSet mSet = mGet.GetDataSet("Sp_Service_Select", mPara, mValue);
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
        /// <para>TYpe = 2: Lất tất cả đã được kích thoạt</para>
        /// <para>TYpe = 3: Lất tất cả </para>
        /// </param>
        /// <param name="Para_1"></param>
        /// <returns></returns>
        public DataTable Select(int Type)
        {
            try
            {
                string[] mPara = { "Type"};
                string[] mValue = { Type.ToString() };

                DataTable mTable = new DataTable();
                if (Type == 3)
                {
                    if (MyCurrent.CurrentPage.Session != null && MyCurrent.CurrentPage.Session[MySetting.AdminSetting.ParaSave.Service] != null)
                        mTable = ((DataTable)MyCurrent.CurrentPage.Session[MySetting.AdminSetting.ParaSave.Service]).Copy();
                    else
                    {
                        mTable = mGet.GetDataTable("Sp_Service_Select", mPara, mValue);
                        MyCurrent.CurrentPage.Session[MySetting.AdminSetting.ParaSave.Service] = mTable;
                    }
                }
                else
                {
                    mTable = mGet.GetDataTable("Sp_Service_Select", mPara, mValue);
                }
                return mTable;
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
        /// <para>Type = 1: Lấy chi tiết 1 Record (Para_1 = ServiceID)</para>
        /// </param>
        /// <param name="Para_1"></param>
        /// <returns></returns>
        public DataTable Select(int Type, string Para_1)
        {
            try
            {
                string[] mPara = { "Type", "Para_1" };
                string[] mValue = { Type.ToString(), Para_1 };

                DataTable mTable = new DataTable();

                mTable = mGet.GetDataTable("Sp_Service_Select", mPara, mValue);

                return mTable;
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
                if (mExec.ExecProcedure("Sp_Service_Insert", mpara, mValue) > 0)
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
                if (mExec.ExecProcedure("Sp_Service_Delete", mpara, mValue) > 0)
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
                if (mExec.ExecProcedure("Sp_Service_Update", mpara, mValue) > 0)
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
                if (mExec.ExecProcedure("Sp_Service_Active", mpara, mValue) > 0)
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

        public int TotalRow(int? Type, string SearchContent, bool? IsActive)
        {
            try
            {
                string[] mPara = { "Type", "SearchContent",  "IsActive", "IsTotalRow" };
                string[] mValue = { Type.ToString(), SearchContent, (IsActive == null ? null : IsActive.ToString()), true.ToString() };

                return (int)mGet.GetExecuteScalar("Sp_Service_Search", mPara, mValue);
            }
            catch (SqlException ex)
            {
                throw ex;
            }
        }


        public DataTable Search(int? Type, int BeginRow, int EndRow, string SearchContent, bool? IsActive, string OrderBy)
        {
            try
            {
                string[] mpara = { "Type", "BeginRow", "EndRow", "SearchContent", "IsActive",  "OrderBy","IsTotalRow" };
                string[] mValue = { Type.ToString(), BeginRow.ToString(), EndRow.ToString(), SearchContent, (IsActive == null ? null : IsActive.ToString()), OrderBy, false.ToString() };
                return mGet.GetDataTable("Sp_Service_Search", mpara, mValue);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
