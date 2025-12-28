using Microsoft.VisualBasic.FileIO;
namespace DeTaiQLDH
{
    public struct MonAn//Khai báo class MonAn
    {
        public string IDMon { get; set; }// Thuộc tính lưu ID món ăn
        public string TenMon { get; set; }// Thuộc tính lưu tên món ăn
        public decimal Gia { get; set; }// Thuộc tính lưu giá món ăn
        public MonAn(string ma, string tenMon, decimal gia) 
        //Hàm tự động chạy khi tạo đối tượng MonAn.
        // Nhận 3 tham số (ma, tenMon, gia) để gán cho các thuộc tính
        {
            IDMon = ma;
            TenMon = tenMon;
            Gia = gia;
        }
    }
    public class MenuMon
    {
        List<MonAn> menu = new List<MonAn>()
        {
            new MonAn("M01", "Cơm gà xối mỡ", 35_000m),
            new MonAn("M02", "Cơm sườn nướng mật ong", 40_000m),
            new MonAn("M03", "Mì xào bò", 30_000m),
            new MonAn("M04", "Bún thịt nướng", 35_000m),
            new MonAn("M05", "Cơm chiên hải sản", 40_000m),
            new MonAn("M06", "Trà tắc mật ong", 20_000m),
            new MonAn("M07", "Trà đào cam sả", 20_000m),
            new MonAn("M08", "Nước suối", 10_000m),
            new MonAn("M09", "Coca", 15_000m),
            new MonAn("M10", "Trà đá", 5_000m),
        };

        public void HienThiMenu()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int sttWidth = 5;
            int idWidth = 6;
            int tenWidth = 25;
            int giaWidth = 12;
            int tableWidth = sttWidth + idWidth + tenWidth + giaWidth + 9;

            // ===== TIÊU ĐỀ =====
            string title = "DANH SÁCH MÓN ĂN";
            Console.WriteLine(new string('-', tableWidth + 2));

            int padding = (tableWidth - title.Length) / 2;
            Console.WriteLine(
                "|" +
                new string(' ', padding) +
                title +
                new string(' ', tableWidth - title.Length - padding) +
                "|"
            );

            Console.WriteLine(new string('-', tableWidth));
            Console.WriteLine(
                "| " +
                "STT".PadRight(sttWidth) + " | " +
                "Mã món".PadRight(idWidth) + " | " +
                "Tên món".PadRight(tenWidth) + " | " +
                "Giá (VND)".PadLeft(giaWidth) + " |"
            );

            Console.WriteLine(new string('-', tableWidth));

            int stt = 1;
            foreach (var mon in menu)
            {
                Console.WriteLine(
                    "| " +
                    stt.ToString().PadRight(sttWidth) + " | " +
                    mon.IDMon.PadRight(idWidth) + " | " +
                    mon.TenMon.PadRight(tenWidth) + " | " +
                    mon.Gia.ToString("N0").PadLeft(giaWidth) + " |"
                );
                stt++;
            }

            Console.WriteLine(new string('-', tableWidth));
        }

        public List<MonAn> LayDanhSachMon()
        {
            return menu;
        }
    }
}