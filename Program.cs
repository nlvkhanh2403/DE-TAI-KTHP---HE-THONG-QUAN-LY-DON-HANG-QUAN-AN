using System;
using System.Security.Cryptography;
using System.Text;

namespace DeTaiQLDH
{
    public class Menu
    {
        public const string TIEU_DE = "ĐỀ TÀI KẾT THÚC HỌC PHẦN";

        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            QuanLyDonHang quanLyDonHang = new QuanLyDonHang();

            while (true)
            {
                MenuConsole();

                Console.Write("Nhập tùy chọn: ");
                string input = Console.ReadLine()!;
                if (!int.TryParse(input, out int key))
                {
                    Console.WriteLine("Vui lòng nhập số hợp lệ!");
                    continue;
                }

                switch (key)
                {
                    case 1:
                        quanLyDonHang.TaoDonHang();
                        break;
                    case 2:
                        quanLyDonHang.CapNhatDonHang();
                        break;
                    case 3:
                        quanLyDonHang.XoaDonHang();
                        break;
                    case 4:
                        quanLyDonHang.HienThiTatCaDonHang();
                        break;
                    case 5:
                        quanLyDonHang.TimKiemDonHang();
                        break;
                    case 6:
                        quanLyDonHang.SapXepDonHang();
                        break;
                    case 7:
                        quanLyDonHang.ThongKeBaoCao();
                        break;
                    case 8:
                        quanLyDonHang.XuLyFile();
                        break;
                    case 0:
                        Console.WriteLine("Thoát chương trình. Tạm biệt!");
                        return;
                    default:
                        Console.WriteLine("Lựa chọn không hợp lệ!");
                        break;
                }
                Console.WriteLine("\nNhấn Enter để tiếp tục...");
                Console.ReadLine();
                Console.Clear();
            }
        }

        static void MenuConsole()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            int windowWidth = Console.WindowWidth;
            int boxWidth = TIEU_DE.Length + 6;
            int padding = (windowWidth - boxWidth) / 2;
            string horizontalLine = new string('*', boxWidth);
 

            Console.WriteLine(new string(' ', padding) + horizontalLine);
            Console.WriteLine(new string(' ', padding) + $"*  {TIEU_DE}  *");
            Console.WriteLine(new string(' ', padding) + horizontalLine);
            Console.WriteLine("                                                           Giảng viên bộ môn: Nguyễn Thành Huy");
            Console.WriteLine("                                                           Nhóm sinh viên thực hiện: Nhóm B");
            Console.WriteLine(new string(' ', padding - 15) + "╔═════════════════════════════════════════════════════════════╗");
            Console.WriteLine(new string(' ', padding - 15) + "║        =======> QUẢN LÝ ĐƠN HÀNG QUÁN ĂN <=======           ║");
            Console.WriteLine(new string(' ', padding - 15) + "╠═════════════════════════════════════════════════════════════╣");
            Console.WriteLine(new string(' ', padding - 15) + "║                                                             ║");
            Console.WriteLine(new string(' ', padding - 15) + "║  1. Tạo đơn hàng               ║  2. Cập nhật đơn hàng      ║");
            Console.WriteLine(new string(' ', padding - 15) + "║  3. Xóa đơn hàng               ║  4. Hiển thị đơn hàng      ║");
            Console.WriteLine(new string(' ', padding - 15) + "║  5. Tìm kiếm đơn hàng          ║  6. Sắp xếp đơn hàng       ║");
            Console.WriteLine(new string(' ', padding - 15) + "║  7. Thống kê - Báo cáo         ║  8. Xử lý File             ║");
            Console.WriteLine(new string(' ', padding - 15) + "║                                                             ║");
            Console.WriteLine(new string(' ', padding - 15) + "║                 0. Thoát chương trình an toàn               ║");
            Console.WriteLine(new string(' ', padding - 15) + "║                                                             ║");
            Console.WriteLine(new string(' ', padding - 15) + "╚═════════════════════════════════════════════════════════════╝");
            Console.ResetColor();
        }
    }
}