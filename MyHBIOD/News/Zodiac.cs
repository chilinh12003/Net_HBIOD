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
    public class Zodiac
    {
        /// <summary>
        /// Cung Hoàn đạo
        /// </summary>
        public enum ZodiacList
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
    }
}
