using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using MyUtility;
namespace MySetting
{
    public class AdminSetting
    {

        public enum ListPage
        {
            [DescriptionAttribute("Quản trị thể loại")]
            Categories,
            [DescriptionAttribute("Quản trị Menu")]
            MenuAdmin,
            [DescriptionAttribute("Cấu hình hệ thống")]
            SystemConfig,
            [DescriptionAttribute("Nhóm thành viên")]
            MemberGroup,
            [DescriptionAttribute("Quản trị tài khoản")]
            Member,
            [DescriptionAttribute("Phần quyền")]
            Permission,
            [DescriptionAttribute("Log thành viên")]
            MemberLog,
            [DescriptionAttribute("Đổi mật khẩu")]
            ChangePass,
            [DescriptionAttribute("Quản trị Đối tác")]
            Partner,
            [DescriptionAttribute("Quản trị Thể loại")]
            Category,
            [DescriptionAttribute("Tin tức")]
            News,
            [DescriptionAttribute("Quản trị Dịch vụ")]
            Service,

            [DescriptionAttribute("Lịch sử Mua dữ kiện, Dự đoán trong ngày")]
            Play,
            [DescriptionAttribute("Danh sách người chiến thắng")]
            Winner,

            [DescriptionAttribute("Thống kế cho dữ kiện trong ngày")]
            SuggestLog,

            [DescriptionAttribute("Số lượng thuê bao đăng ký dịch vụ")]
            ReportCountSub,


            [DescriptionAttribute("Số lượng thuê bao hủy dịch vụ")]
            ReportCountUnSub,

            [DescriptionAttribute("Lịch sử Đăng ký/Hủy/Gia hạn")]
            MOLog,
            [DescriptionAttribute("Báo cáo KPI thuê bao")]
            KPI_Sub,
            [DescriptionAttribute("Báo cáo KPI MO")]
            KPI_MO,
            [DescriptionAttribute("Lịch sử trả MT")]
            ReportMTHisTory,


            [DescriptionAttribute("Lịch sử trừ tiền")]
            ChargeLog,

            [DescriptionAttribute("Lịch sử đăng ký/huỷ dịch vụ của thuê bao")]
            History_Reg_Dereg,

            [DescriptionAttribute("Lịch sử gia hạn dịch vụ của thuê bao")]
            History_Renew,

            [DescriptionAttribute("Lịch sử gia mua nội dung của thuê bao")]
            History_BuyContent,

            [DescriptionAttribute("Lịch sử MO/MT của thuê bao")]
            History_MO_MT,
            [DescriptionAttribute("Lịch sử tương tác, sử dụng dịch vụ của thuê bao")]
            History_Interaction,
            [DescriptionAttribute("Thông tin sử dụng dịch vụ của thuê bao")]
            CheckDetailInfo,
            [DescriptionAttribute("Chỉ số KPI")]
            KPI,

            [DescriptionAttribute("Thống kê sản lượng")]
            ChargeLogByDay,

            [DescriptionAttribute("Thống kê sản lượng theo giá")]
            ChargeLogByDay_Price,

            [DescriptionAttribute("Thống kê sản lượng theo giá")]
            RPPartnerPrice,
            [DescriptionAttribute("Thống kê sản lượng theo ngày")]
            RPPartnerDay,

            [DescriptionAttribute("Thống kê thuê bao")]
            RPPartnerSub,

            [DescriptionAttribute("Gửi lại MT cho khách hàng")]
            ResendMT,

            [DescriptionAttribute("Đăng ký/Hủy đăng ký")]
            Register,
           
            [DescriptionAttribute("Thống kê lượt Đăng ký/Hủy")]
            ReportSubByDay,
        }

        public struct ParaSave
        {
            /// <summary>
            /// Lưu trữ thông tin Serivice vào session
            /// </summary>
            public static string Service = "Service";

        }

        public static string MySQLConnection_Gateway
        {
            get
            {
                return "MySQLConnection_Gateway";
            }
        }

        public static string ShoreCode
        {
            get
            {
                string Temp = MyConfig.GetKeyInConfigFile("ShoreCode");
                if (string.IsNullOrEmpty(Temp))
                    return "1566";
                else
                    return Temp;
            }
        }
        
        public static int MaxPID
        {
            get
            {
                return 50;
            }
        }

        /// <summary>
        /// Key dùng để mã hóa tạo chữ ký khi call WS đăng ký dịch vụ
        /// </summary>
        public static string RegWSKey = "wre34WD45F";

        /// <summary>
        /// Key dùng để mã hóa dữ liệu nhạy cảm
        /// </summary>
        public static string SpecialKey = "ChIlINh154";

        public static string AllowIPFile
        {
            get
            {
                string Temp = MyConfig.GetKeyInConfigFile("AllowIPFile");
                if (string.IsNullOrEmpty(Temp))
                    return @"~/App_Data/AllowIP.xml";
                else
                    return Temp;
            }
        }

        /// <summary>
        /// Tắt chức năng kiểm tra IP
        /// </summary>
        public static bool DisableCheckIP
        {
            get
            {
                string Temp = MyConfig.GetKeyInConfigFile("DisableCheckIP");
                if (string.IsNullOrEmpty(Temp))
                {

                    return false;
                }
                else
                {
                    Temp = Temp.Trim();
                    bool bValue = false;
                    bool.TryParse(Temp,out bValue);
                    return bValue;
                }
            }
        }
    }
}
