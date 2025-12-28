using System.Collections.Generic;
using DeTaiQLDH;

namespace DeTaiQLDH
{
    //Hàm in toàn bộ thông tin đơn hàng ra màn hình
    public static class HienThiDanhSachDonHang
    {
        public static void HienThi(List<DonHang> listDonHang)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; // Viết được tiếng Việt
            Console.WriteLine("\n===== DANH SÁCH ĐƠN HÀNG =====");

            // Tính tổng đơn và tổng doanh thu
            int tongDon = listDonHang.Count;
            decimal tongDoanhThu = 0;
            foreach (var o in listDonHang)
            {
                tongDoanhThu += o.TongTien();
            }
            Console.WriteLine($"Tổng số đơn hàng: {tongDon}");
            Console.WriteLine($"Tổng doanh thu: {tongDoanhThu:N0} VND\n");

            // Tạo tiêu đề bảng
            Console.WriteLine(" {0,-5} | {1,-15} | {2,-20} | {3,-12} | {4,-15} | {5} ", // Điều chỉnh hình dạng bảng
                "ID", "Khách hàng", "Ngày tạo", "Tổng tiền", "Trạng thái", "Danh sách món"); // 6 cột tiêu đề chính
            Console.WriteLine(new string('-', 95)); // Đường kẻ gạch ngang phân cách giữa tiêu đề và dữ liệu 

            foreach (var o in listDonHang)
            {   // Thêm dữ liệu theo từng cột cho bảng
                string danhSachMon = "";
                for (int i = 0; i < o.DanhSachMon.Count; i++)
                {
                    danhSachMon += o.DanhSachMon[i].TenMon;
                    if (i < o.DanhSachMon.Count - 1)
                        danhSachMon += ", ";
                }
                Console.WriteLine(" {0,-5} | {1,-15} | {2,-20:dd/MM/yyyy HH:mm} | {3,-12:N0} | {4,-15} | {5} ",
                o.ID, o.CustomerName, o.NgayTaoDon, o.TongTien(), o.TrangThai, danhSachMon);
            }
        }
    }
}