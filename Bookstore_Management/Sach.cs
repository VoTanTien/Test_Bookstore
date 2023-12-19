using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Bookstore_Management
{
    public class Sach
    {
        private string maSach;
        private string theLoai;
        private string tenSach;
        private int soLuong;
        private string giaBan;
        private string ngayPhatHanh;
        private string tenAnh;

        public string MaSach { get => maSach; set => maSach = value; }
        public string TheLoai { get => theLoai; set => theLoai = value; }
        public string TenSach { get => tenSach; set => tenSach = value; }
        public int SoLuong { get => soLuong; set => soLuong = value; }
        public string GiaBan { get => giaBan; set => giaBan = value; }
        public string NgayPhatHanh { get => ngayPhatHanh; set => ngayPhatHanh = value; }
        public string TenAnh { get => tenAnh; set => tenAnh = value; }

        public Sach() { }
        public Sach(string masach, string theloai, string tensach, int soluong, string giaban, string ngayphathanh, string tenanh)
        {
            this.MaSach = masach;
            this.TheLoai = theloai;
            this.TenSach = tensach;
            this.SoLuong = soluong;
            this.GiaBan = giaban;
            this.NgayPhatHanh = ngayphathanh;
            this.TenAnh = tenanh;
        }

        public Sach(DataRow row)
        {
            this.MaSach = row["MaSach"].ToString();
            this.TheLoai = row["TheLoai"].ToString();
            this.TenSach = row["TenSach"].ToString();
            this.SoLuong = (int)row["SoLuong"];
            this.GiaBan = row["GiaBan"].ToString();
            this.NgayPhatHanh = row["NgayPhatHanh"].ToString();
            this.TenAnh = row["TenAnh"].ToString();
        }
    }
}
