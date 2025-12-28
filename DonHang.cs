using System.Reflection.Metadata;

namespace DeTaiQLDH
{
    public class DonHang
    {
        public int ID { get; set; }
        public string CustomerName { get; set; }
        public DateTime NgayTaoDon { get; set; } 
        public List<MonAn> DanhSachMon { get; set; }
        public bool PhuongThucThanhToan { get; set; }
        public TrangThaiDonHang TrangThai { get; set; }
        public DonHang()
        {
            DanhSachMon = new List<MonAn>();
        }

        public void ThemMon(MonAn mon)
        {
            DanhSachMon.Add(mon);
        }

        public decimal TongTruocThue()
        {
            return DanhSachMon.Sum(m => m.Gia);
        }

        public decimal TienVAT(decimal vat = 0.1m)
        {
            return TongTruocThue() * vat;
        }

        public decimal TongTien(decimal VAT = 0.1m)
        {
                decimal tong = DanhSachMon.Sum(m => m.Gia);
                return tong * (1 + VAT);// tính giá món + thuế VAT
        }

        public enum TrangThaiDonHang
        {
            DangXuLy,
            HoanTat,
            DaHuy
        }

        public void HienThiDonHang()
        {
            Console.WriteLine("\n ===== THÔNG TIN ĐƠN HÀNG =====");
            Console.WriteLine($"Mã đơn hàng: {ID}");
            Console.WriteLine($"Tên khách hàng: {CustomerName}");
            Console.WriteLine($"Ngày tạo đơn: {NgayTaoDon}");
            Console.WriteLine("\n---- Món đã chọn ----");
            foreach (var mon in DanhSachMon)
            {
                Console.WriteLine($"{mon.TenMon} - {mon.Gia:N0} VND");
            }
            Console.WriteLine($"VAT (10%): {TienVAT():N0}");
            Console.WriteLine($"Tổng tiền đơn hàng:{TongTien():N0} VND");
            Console.WriteLine($"Hình thức thanh toán: {(PhuongThucThanhToan ? "Chuyển khoản" : "Tiền mặt")}");
            Console.WriteLine("===============================================================================");
        }
    }
}

