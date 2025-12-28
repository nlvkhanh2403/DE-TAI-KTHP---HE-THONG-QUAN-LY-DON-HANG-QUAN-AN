using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DeTaiQLDH
{
    public class QuanLyDonHang
    {
        private List<DonHang> ListDonHang = null;
        private readonly string filePath = "DonHang.txt";
        private readonly string backupPath = "DonHang.bak";

        //Gọi danh sách đơn hàng ra
        public QuanLyDonHang()
        {
            ListDonHang = new List<DonHang>();
            DocFile();
        }

        private int GenerateID()
        {
            int max = 1;
            if (ListDonHang != null && ListDonHang.Count > 0)
            {
                max = ListDonHang[0].ID;
                foreach (DonHang dh in ListDonHang)
                {
                    if (max < dh.ID)
                    {
                        max = dh.ID;
                    }
                }
                max++;
            }
            return max;
        }

        /// <summary>
        /// Tạo một đơn hàng mới thông qua việc nhập thông tin khách hàng, 
        /// Chọn món ăn và xác định hình thức thanh toán.
        /// </summary>
        /// <remarks>
        /// Hàm sẽ tự động khởi tạo đối tượng <see cref="DonHang"/>, 
        /// Hiển thị menu món ăn từ <see cref="MenuMon"/>, 
        /// Cho phép người dùng nhập mã món muốn chọn và lưu lại đơn hàng mới vào danh sách.
        /// </remarks>        
        public void TaoDonHang()
        {
            Console.WriteLine("\n===== TẠO ĐƠN HÀNG MỚI =====");
            DonHang dh = new DonHang();
            dh.ID = GenerateID();

            Console.Write("Nhập tên khách hàng: ");
            dh.CustomerName = Console.ReadLine()!;

            dh.NgayTaoDon = DateTime.Now;// Khởi tạo ngày tạo đơn hàng là ngày hiện tại

            //Gọi menu từ class MenuMonAn
            MenuMon menuMon = new MenuMon();
            menuMon.HienThiMenu();

            // Cho người dùng chọn món
            Console.Write("\nNhập mã món muốn chọn (ngăn bằng dấu cách): ");
            string input = Console.ReadLine()!;
            string[] choice = input.Split(" ");

            // Thêm món được chọn vào đơn hàng
            List<MonAn> danhSachMon = menuMon.LayDanhSachMon();

            foreach (string maMon in choice)
            {
                MonAn? mon = danhSachMon.FirstOrDefault(m => m.IDMon.Equals(maMon, StringComparison.OrdinalIgnoreCase));

                if (mon != null)  //Kiểm tra món có tồn tại không
                {
                    dh.ThemMon(mon.Value);  //Thêm món vào danh sách đơn hàng
                }
                else
                {
                    Console.WriteLine($"Mã món '{maMon}' không tồn tại trong menu!");
                }
            }

            //Chọn hình thức thanh toán
            Console.Write("Chọn hình thức thanh toán (1: Tiền mặt, 2: Chuyển khoản): ");
            string phuongThuc = Console.ReadLine()!;
            dh.PhuongThucThanhToan = phuongThuc == "2";//Nếu nhập 2 đặt PhuongThucThanhToan = true (chuyển khoản), nếu nhập 1 là false (tiền mặt)                                          
            dh.TrangThai = DonHang.TrangThaiDonHang.DangXuLy; //Mặc định trạng thái là "Đang xử lý";

            //Thêm đơn hàng vừa tạo vào danh sách quản lý
            ListDonHang.Add(dh);
            GhiFile();
            Console.WriteLine("\nĐơn hàng đã được tạo thành công!");
            dh.HienThiDonHang(); //in thông báo thành công và hiển thị thông tin chi tiết đơn hàng ra màn hình
        }


        /// <summary>
        /// Cập nhật thông tin của một đơn hàng dựa trên ID người dùng nhập.
        /// </summary>
        /// <remarks>
        /// Hàm này cho phép người dùng thay đổi tên khách hàng, danh sách món hoặc trạng thái đơn hàng.  
        /// - Có kiểm tra đầu vào hợp lệ bằng <see cref="int.TryParse(string, out int)"/>.  
        /// - Có xác thực sự tồn tại của đơn hàng trong danh sách <see cref="ListDonHang"/>.  
        /// - Sau khi cập nhật, hàm ghi đè lại dữ liệu vào danh sách và có thể bổ sung lưu ra file (.txt hoặc .csv).  
        /// - Nên kết hợp với sao lưu file (.bak) để tránh mất dữ liệu khi có lỗi.
        /// </remarks>
        
        public void CapNhatDonHang()
        {
            Console.WriteLine("\n===== CẬP NHẬT ĐƠN HÀNG =====");
            HienThiDanhSachDonHang.HienThi(ListDonHang); //Hiển thị danh sách đơn hàng hiện có
            Console.Write("Nhập ID đơn hàng cần cập nhật: ");
            if (!int.TryParse(Console.ReadLine(), out int idCanCapNhat))//Chuyển chuỗi ng dùng nhập thành số nguyên, nếu người dùng nhập không phải là số nguyên hợp lệ, thì báo lỗi và dừng việc cập nhật đơn hàng.
            {
                Console.WriteLine("ID không hợp lệ!");
                return;
            }

            DonHang donHangCanSua = ListDonHang.Find(dh => dh.ID == idCanCapNhat);
            if (donHangCanSua == null)
            {
                Console.WriteLine("Không tìm thấy đơn hàng!");
                return;
            }

            // Nếu như đơn hàng tồn tại thì
            Console.WriteLine("\n===== THÔNG TIN ĐƠN HÀNG CẦN CẬP NHẬT =====");
            Console.WriteLine($"Mã đơn hàng: {donHangCanSua.ID}           ");// In ra mã đơn hàng
            Console.WriteLine($"Khách hàng: {donHangCanSua.CustomerName}  ");// In tên khách hàng
            Console.WriteLine($"Ngày tạo: {donHangCanSua.NgayTaoDon}      ");// In ngày tạo
            Console.WriteLine($"Trạng thái: {donHangCanSua.TrangThai}     ");// In trạng thái đơn hàng
            Console.WriteLine("Danh sách món:                             ");// In danh sách món đã tạo
            foreach (var mon in donHangCanSua.DanhSachMon)//Duyệt qua từng món ăn trong danh sách món
                Console.WriteLine($"- {mon.TenMon} ({mon.Gia:N0} VND)");//Rồi in ra tên từng món ăn
            Console.WriteLine("=============================================");
            Console.WriteLine("Bạn muốn cập nhật phần nào?");//Sau khi in xong danh sách món thì hỏi ng dùng muốn cập nhật phần nào
            Console.WriteLine("1. Tên khách hàng");//Cập nhật lại tên
            Console.WriteLine("2. Danh sách món");//Cập nhật lại danh sách món
            Console.WriteLine("3. Trạng thái đơn hàng");//Cập nhật lại trạng thái của đơn hàng
            Console.Write("Chọn: ");
            string userchoice = Console.ReadLine();//Đọc lựa chọn người dùng nhập vào từ bàn phím

            switch (userchoice)
            {
                case "1"://Chọn cập nhật lại tên KH
                    Console.Write("Nhập tên khách hàng mới: ");
                    string newName = Console.ReadLine();//Lưu tên khách hàng mới mà ng dùng vừa nhập vào biến newName.
                    if (!string.IsNullOrWhiteSpace(newName))//Nếu như tên KH vừa nhập không phải khoảng trắng, không rỗng 
                    {
                        donHangCanSua.CustomerName = newName;//Thì cập nhật lại tên KH trong đơn hàng bằng tên mới ng dùng vừa nhập.
                        Console.WriteLine("Cập nhật tên khách hàng thành công!");
                    }
                    else Console.WriteLine("Tên không được để trống!");//Nếu đk sai thì in dòng này
                    break;

                case "2"://Cập nhật danh sách món
                    MenuMon menuMon = new MenuMon();
                    menuMon.HienThiMenu();
                    donHangCanSua.DanhSachMon.Clear();//Xóa toàn bộ các món cũ trong đơn hàng trước khi thêm món mới.
                    Console.Write("Nhập danh sách món mới: ");
                    string danhSachMoi = Console.ReadLine();
                    string[] mangMaMon = danhSachMoi.Split(" ");//Chia chuỗi danhSachMoi thành mảng các mã món riêng biệt bằng dấu cách
                    List<MonAn> danhSachMon = menuMon.LayDanhSachMon();//Lấy danh sách món ăn hiện có từ menuMon và lưu vào biến danhSachMon
                    foreach (string monMoi in mangMaMon)
                    {
                        MonAn? mon = danhSachMon.FirstOrDefault(m => m.IDMon.Equals(monMoi, StringComparison.OrdinalIgnoreCase));
                        if (mon != null)  //Kiểm tra món có tồn tại không
                        {
                            donHangCanSua.ThemMon(mon.Value);  //Thêm món vào danh sách đơn hàng
                        }
                        else
                        {
                            Console.WriteLine($"Mã món '{monMoi}' không tồn tại trong menu!");
                        }
                    }
                    Console.WriteLine("Cập nhật danh sách món thành công!");//In thông báo cập nhật thành công
                    break;

                case "3": //Cập nhật trạng thái đơn hàng
                    Console.WriteLine("Chọn trạng thái mới:");
                    Console.WriteLine("1. Hoàn tất");
                    Console.WriteLine("2. Đã hủy");
                    Console.WriteLine("3. Đang xử lý");
                    Console.Write("Chọn: ");
                    string trangthai = Console.ReadLine();//Đọc chuỗi người dùng nhập từ bàn phím và lưu vào biến status.
                    switch (trangthai)                    {
                        case "1":
                            donHangCanSua.TrangThai = DonHang.TrangThaiDonHang.HoanTat;
                            break;
                        case "2":
                            donHangCanSua.TrangThai = DonHang.TrangThaiDonHang.DaHuy;
                            break;
                        case "3":
                            donHangCanSua.TrangThai = DonHang.TrangThaiDonHang.DangXuLy;
                            break;
                        default:
                            Console.WriteLine("Lựa chọn không hợp lệ!");
                            return;
                    }
                    Console.WriteLine("Cập nhật trạng thái thành công!");//Sau khi cập nhật hợp lệ, chương trình in thông báo xác nhận cho ng dùng.
                    break;

                default:
                    Console.WriteLine("Lựa chọn không hợp lệ!");//Trường hợp người dùng nhập ngoài “1”, “2”, “3” ở switch (trangthai) ban đầu, thì chương trình báo lỗi “Lựa chọn không hợp lệ!”.
                    break;
            }
            // Ghi lại thay đổi vào danh sách
            GhiFile();
            Console.WriteLine("Cập nhật đơn hàng hoàn tất!");
        }

        //Xóa đơn hàng
        public void XoaDonHang()
        {
            Console.WriteLine("\n===== XÓA ĐƠN HÀNG =====");
            Console.Write("\n Nhập mã đơn hàng cần xóa: ");
            int maXoa = int.Parse(Console.ReadLine());
            var donCanXoa = ListDonHang.FirstOrDefault(d => d.ID == maXoa);

            if (donCanXoa != null)
            {
                Console.Write($"Bạn có chắc chắn muốn xóa đơn hàng {maXoa} của {donCanXoa.CustomerName} không? (y/n): ");
                string confirm = Console.ReadLine();

                if (confirm == "y")
                {
                    ListDonHang.Remove(donCanXoa);
                    Console.WriteLine("Đơn hàng đã được xóa thành công!");
                }
                else
                {
                    Console.WriteLine("Hủy thao tác xóa đơn hàng");
                }
            }
            else
            {
                Console.WriteLine("Không tìm thấy đơn hàng");
            }
        }

        /// <summary>
        /// Hiển thị toàn bộ danh sách đơn hàng hiện có trong hệ thống.
        /// </summary>
        /// <remarks>
        /// Phương thức sẽ kiểm tra xem danh sách <see cref="ListDonHang"/> có rỗng hay không.  
        /// Nếu không có đơn hàng nào, hệ thống sẽ hiển thị thông báo tương ứng.  
        /// Nếu có, phương thức sẽ gọi hàm <see cref="HienThiDanhSachDonHang.HienThi(List{DonHang})"/> 
        /// Để hiển thị chi tiết từng đơn hàng.
        /// </remarks>
        public void HienThiTatCaDonHang()
        {
            if (ListDonHang.Count == 0)
            {
                Console.WriteLine("\nHiện chưa có đơn hàng nào trong hệ thống.");
                return;
            }
            HienThiDanhSachDonHang.HienThi(ListDonHang); //Gọi phần hiển thị danh sách
        }

        /// <summary>
        /// Tìm kiếm đơn hàng trong danh sách theo các tiêu chí khác nhau như:
        /// Mã đơn hàng, tên khách hàng hoặc tên món ăn.
        /// </summary>
        /// <remarks>
        /// Phương thức hiển thị menu cho người dùng lựa chọn kiểu tìm kiếm,
        /// Sau đó lọc danh sách đơn hàng <see cref="ListDonHang"/> tương ứng.
        /// Nếu không tìm thấy kết quả phù hợp, thông báo sẽ được hiển thị.
        /// </remarks>
        public void TimKiemDonHang()
        {
            Console.WriteLine("===TÌM KIẾM ĐƠN HÀNG===");
            Console.WriteLine("1. Tìm kiếm theo MÃ ĐƠN HÀNG");
            Console.WriteLine("2. Tìm kiếm theo TÊN KHÁCH HÀNG");
            Console.WriteLine("3. Tìm kiếm theo TỔNG TIỀN");
            Console.Write("Chọn chức năng: ");
            string luachon = Console.ReadLine();

            if (!int.TryParse(luachon, out int luaChon))
            {
                Console.WriteLine("Lựa chọn không hợp lệ!");
                return;
            }

            List<DonHang> ketQua = new List<DonHang>();

            switch (luaChon)
            {
                case 1:
                    Console.Write("Nhập mã ID đơn hàng cần tìm: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    ketQua = TimKiem(id);
                    break;
                case 2:
                    Console.Write("Nhập tên khách hàng cần tìm: ");
                    string keyword = Console.ReadLine()!;
                    ketQua = TimKiem(keyword);
                    break;
                case 3:
                    Console.Write("Nhập tổng tiền cần tìm: ");
                    if (decimal.TryParse(Console.ReadLine(), out decimal tongtien))
                    ketQua = TimKiem(tongtien);
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ!");
                    return;
            }

            if (ketQua.Count == 0)
                Console.WriteLine("Không tìm thấy đơn hàng nào phù hợp!");
            else
            {
                Console.WriteLine($"\nTìm thấy {ketQua.Count} đơn hàng:");
                foreach (var dh in ketQua)
                    dh.HienThiDonHang();
            }
        }

        private List<DonHang> TimKiem(int id) //Tìm theo mã ID
        {
            return ListDonHang.Where(dh => dh.ID == id).ToList();
        }
        private List<DonHang> TimKiem(string keyword) //Tìm theo tên khách hàng
        {
            return ListDonHang
                    .Where(dh => dh.CustomerName.Contains(keyword, StringComparison.OrdinalIgnoreCase)
                        || dh.ID.ToString() == keyword)
                    .ToList();
        }

        private List<DonHang> TimKiem(decimal tongTien) //Tìm theo tổng tiền
        {
            SapXepTheoTongTien(); // Sắp xếp danh sách trước
            List<DonHang> ketQua = new List<DonHang>();
            int left = 0, right = ListDonHang.Count - 1;
            while (left <= right)
            {
                int mid = (left + right) / 2;
                decimal midValue = ListDonHang[mid].TongTien();

                if (Math.Abs(midValue - tongTien) < 0.001m) // So sánh với độ chính xác nhỏ
                {
                    ketQua.Add(ListDonHang[mid]);
                    //Tìm sang hai bên xem có đơn nào cùng tiền không
                    int  templeft = mid - 1;
                    while (templeft >= 0 && Math.Abs(ListDonHang[templeft].TongTien() - tongTien) < 0.001m)
                    {
                        ketQua.Add(ListDonHang[templeft--]);
                    }

                    int tempright = mid + 1;
                    while (tempright < ListDonHang.Count && Math.Abs(ListDonHang[tempright].TongTien() - tongTien) < 0.001m)
                    {
                        ketQua.Add(ListDonHang[tempright++]);
                    }
                    break;
                    }
                else if (midValue < tongTien)
                left = mid + 1;
                else
                right = mid - 1;
            }
            return ketQua;
        }

        /// <summary>
        /// Sắp xếp đơn hàng trong danh sách theo các tiêu chí khác nhau như:
        /// Mã đơn hàng, tên khách hàng hoặc tổng tiền món ăn.
        /// </summary>
        /// <remarks>
        /// Phương thức hiển thị menu cho người dùng lựa chọn kiểu tìm kiếm,
        /// Sau đó lọc danh sách đơn hàng <see cref="ListDonHang"/> tương ứng.
        /// Nếu không tìm thấy kết quả phù hợp, thông báo sẽ được hiển thị.
        /// </remarks>
        public void SapXepDonHang()
        {
            if (ListDonHang.Count == 0)
            {
                Console.WriteLine("\nHiện chưa có đơn hàng nào để sắp xếp.");
                return;
            }
            Console.WriteLine("\n=== SẮP XẾP ĐƠN HÀNG ===");
            Console.WriteLine("1. Theo TRẠNG THÁI ĐƠN HÀNG");   //Bubble 
            Console.WriteLine("2. Theo TÊN KHÁCH HÀNG (A -> Z)"); //Bubble
            Console.WriteLine("3. Theo TỔNG TIỀN (tăng dần)"); //Insertion
            Console.Write("Nhập lựa chọn: ");
            string luaChon = Console.ReadLine()!;
            switch (luaChon)
            {
                case "1":
                    SapXepTheoTrangThai();
                    Console.WriteLine("→ Đã sắp xếp theo trạng thái đơn hàng.");
                    break;
                case "2":
                    SapXepTheoTenKH();
                    Console.WriteLine("→ Đã sắp xếp theo tên khách hàng (A–Z).");
                    break;
                case "3":
                    SapXepTheoTongTien();
                    Console.WriteLine("→ Đã sắp xếp theo tổng tiền (tăng dần).");
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ!");
                    return;
            }
            // Sau khi sắp xếp xong thì hiển thị lại danh sách
            HienThiTatCaDonHang();
        }

        private void SapXepTheoTrangThai()
        {
            for (int i = 0; i < ListDonHang.Count - 1; i++)
            {
                for (int j = 0; j < ListDonHang.Count - i - 1; j++)
                {
                    if (ListDonHang[j].TrangThai > ListDonHang[j + 1].TrangThai)
                    {
                        var temp = ListDonHang[j];
                        ListDonHang[j] = ListDonHang[j + 1];
                        ListDonHang[j + 1] = temp;
                    }
                }
            }
        }

        private void SapXepTheoTenKH()
        {
            for (int i = 0; i < ListDonHang.Count - 1; i++)
            {
                for (int j = 0; j < ListDonHang.Count - i - 1; j++)
                {
                    if (string.Compare(ListDonHang[j].CustomerName, ListDonHang[j + 1].CustomerName, true) > 0)
                    {
                        var temp = ListDonHang[j];
                        ListDonHang[j] = ListDonHang[j + 1];
                        ListDonHang[j + 1] = temp;
                    }
                }
            }
        }
        private void SapXepTheoTongTien()
        {
            for (int i = 1; i < ListDonHang.Count; i++)
            {
                var key = ListDonHang[i];
                int j = i - 1;

                while (j >= 0 && ListDonHang[j].TongTien() > key.TongTien())
                {
                    ListDonHang[j + 1] = ListDonHang[j];
                    j--;
                }
                ListDonHang[j + 1] = key;
            }
        }

        public void ThongKeBaoCao()
        {
            Console.WriteLine("\n===== THỐNG KÊ – BÁO CÁO ĐƠN HÀNG =====");
            if (ListDonHang.Count == 0)
            {
                Console.WriteLine("Chưa có dữ liệu để thống kê!");
                return;
            }
            TaoBangThongKeTrangThai();
            Console.WriteLine("+----------BẢNG THỐNG KÊ----------+");
            Console.WriteLine("|----------------+----------------|");
            Console.WriteLine("|   Trạng thái   |  Số lượng đơn  |");
            Console.WriteLine("|----------------+----------------|");

            for (int i = 0; i < 3; i++)
            {
                string tenTrangThai = i switch
                {
                    0 => "Đang xử lý",
                    1 => "Hoàn tất",
                    2 => "Đã hủy",
                    _ => "?"
                };
                Console.WriteLine($"| {tenTrangThai,-14} | {bangThongKeTrangThai[i,1],-15}|");
                Console.WriteLine("+----------------+----------------+");

            }
            decimal tongDoanhThu = ListDonHang
                .Where(d => d.TrangThai == DonHang.TrangThaiDonHang.HoanTat)
                .Sum(d => d.TongTien());
            Console.WriteLine($"Tổng số đơn hàng   : {ListDonHang.Count}");
            Console.WriteLine($"Tổng doanh thu     : {tongDoanhThu:N0} VND");

            ThongKeMonBanChay();
        }

        private void DemSoLuongTheoTrangThai(
            out int dangXuLy,
            out int hoanTat,
            out int daHuy)
        {
            dangXuLy = 0;
            hoanTat = 0;
            daHuy = 0;
            foreach (var dh in ListDonHang)
            {
                  switch (dh.TrangThai)
                  {
                        case DonHang.TrangThaiDonHang.DangXuLy:
                            dangXuLy++;
                            break;
                        case DonHang.TrangThaiDonHang.HoanTat:
                            hoanTat++;
                            break;
                        case DonHang.TrangThaiDonHang.DaHuy:
                            daHuy++;
                            break;
                  }
            }
        }

        private int[,] bangThongKeTrangThai = new int[3, 2]; //mảng 2 chiều có 3 hàng và 2 cột
        private void TaoBangThongKeTrangThai()
        {
            int dxl, ht, dh;
            DemSoLuongTheoTrangThai(out dxl, out ht, out dh);

            bangThongKeTrangThai[0, 0] = (int)DonHang.TrangThaiDonHang.DangXuLy; ; //Vị trí hàng 1, cột 1 => Đang xử lý. Số 0 tương ứng với vị trí của enum
            bangThongKeTrangThai[0, 1] = dxl;

            bangThongKeTrangThai[1, 0] = (int)DonHang.TrangThaiDonHang.HoanTat; //Vị trí hàng 2, cột 1 => Hoàn tất
            bangThongKeTrangThai[1, 1] = ht;

            bangThongKeTrangThai[2, 0] = (int)DonHang.TrangThaiDonHang.DaHuy; //Vị trí hàng 3, cột 1 => Đã hủy
            bangThongKeTrangThai[2, 1] = dh;
        }

        private void ThongKeMonBanChay()
        {
            Console.WriteLine("\n--- THỐNG KÊ MÓN BÁN CHẠY ---");

            Dictionary<string, int> thongKeMon = new Dictionary<string, int>();

            foreach (var dh in ListDonHang)
            {
                foreach (var mon in dh.DanhSachMon)
                {
                    if (thongKeMon.ContainsKey(mon.TenMon))
                        thongKeMon[mon.TenMon]++;
                    else
                        thongKeMon[mon.TenMon] = 1;
                }
            }

            if (thongKeMon.Count == 0)
            {
                Console.WriteLine("Chưa có dữ liệu món ăn.");
                return;
            }

            foreach (var item in thongKeMon.OrderByDescending(x => x.Value))
            {
                Console.WriteLine($"{item.Key,-25} : {item.Value} lần");
            }
        }

        //XỬ LÝ FILE
        //Ghi danh sách đơn hàng ra file
        public void XuLyFile()
        {
            Console.WriteLine("\n===== XỬ LÝ FILE DỮ LIỆU =====");
            Console.WriteLine("1. Ghi dữ liệu ra file");
            Console.WriteLine("2. Đọc dữ liệu từ file");
            Console.WriteLine("3. Phục hồi từ file backup");
            Console.Write("Chọn chức năng: ");
            string choice = Console.ReadLine()!;
            switch (choice)
            {
                case "1":
                    GhiFile();
                    break;
                case "2":
                    DocFile();
                    break;
                case "3":
                    PhucHoiBackup();
                    break;
                default:
                    Console.WriteLine("Lựa chọn không hợp lệ!");
                    return;
            }
        }

        private void GhiFile()
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Copy(filePath, backupPath, true);
                    Console.WriteLine("Đã tạo file sao lưu thành công!");
                }
                using (StreamWriter writer = new StreamWriter(filePath, false, System.Text.Encoding.UTF8))
                {
                    foreach (var dh in ListDonHang)
                    {
                        string danhSachMon = string.Join(";", dh.DanhSachMon.Select(m => m.TenMon));
                        writer.WriteLine($"{dh.ID},{dh.CustomerName},{dh.NgayTaoDon},{(int)dh.TrangThai},{danhSachMon},{dh.TongTien()}");
                    }
                }
                Console.WriteLine("Ghi dữ liệu ra file thành công!");
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Lỗi: Không có quyền truy cập file!");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Lỗi khi ghi file: {ex.Message}");
            }
        }

        //Đọc dữ liệu từ file khi khởi động
        private void DocFile()
        {
            MenuMon menuMon = new MenuMon();
            List<MonAn> danhSachMon = menuMon.LayDanhSachMon();
            try //Bắt đầu khối try để bắt lỗi đọc file
            {
                if (!File.Exists(filePath)) // Kiểm tra xem file có tồn tại không
                {
                    Console.WriteLine("File dữ liệu không tồn tại. Danh sách trống."); // Thông báo nếu không có file
                    return; // Dừng hàm
                }

                ListDonHang.Clear(); // Xóa danh sách hiện tại trước khi đọc dữ liệu mới
                using (StreamReader reader = new StreamReader(filePath)) // Mở file để đọc
                {
                    string line; // Biến chứa từng dòng đọc được
                    while ((line = reader.ReadLine()) != null) // Đọc đến khi hết file
                    {
                        string[] parts = line.Split(','); // Tách dòng thành các phần tử bằng dấu phẩy
                        if (parts.Length >= 6) // Đảm bảo có đủ 6 trường dữ liệu
                        {
                            DonHang dh = new DonHang // Tạo đối tượng đơn hàng mới
                            {
                                ID = int.Parse(parts[0]),
                                CustomerName = parts[1],
                                NgayTaoDon = DateTime.Parse(parts[2]),
                                TrangThai = (DonHang.TrangThaiDonHang)int.Parse(parts[3]),
                            };
                            dh.DanhSachMon = parts[4]
                                .Split(';')
                                .Select(tenMon =>
                                {
                                    var monKhop = danhSachMon.FirstOrDefault(m => m.TenMon.Equals(tenMon, StringComparison.OrdinalIgnoreCase));
                                    return new MonAn
                                    {
                                        TenMon = tenMon,
                                        Gia = monKhop.TenMon != null ? monKhop.Gia : 0
                                    };
                                })
                                .ToList();
                            ListDonHang.Add(dh); // Thêm đơn hàng vào danh sách
                        }
                    }
                }
                Console.WriteLine("Đọc dữ liệu từ file thành công!"); //Thông báo đọc thành công
            }
            catch (FileNotFoundException) //Lỗi file không tồn tại
            {
                Console.WriteLine("Lỗi: File không tồn tại!");
            }
            catch (UnauthorizedAccessException) //Lỗi không có quyền đọc file
            {
                Console.WriteLine("Lỗi: Không có quyền đọc file!");
            }
            catch (FormatException) //Lỗi sai định dạng dữ liệu
            {
                Console.WriteLine("Lỗi: Dữ liệu trong file sai định dạng!");
            }
            catch (Exception ex) //Các lỗi khác
            {
                Console.WriteLine($"Lỗi khi đọc file: {ex.Message}");
            }
        }

        //Phục hồi từ file backup (.bak)
        private void PhucHoiBackup()
        {
            try //Bắt đầu khối try để bắt lỗi phục hồi
            {
                if (File.Exists(backupPath)) //Kiểm tra file .bak có tồn tại không
                {
                    File.Copy(backupPath, filePath, true); // Sao chép file .bak đè lên file chính
                    Console.WriteLine("Phục hồi từ file sao lưu thành công!"); // Thông báo phục hồi thành công
                }
                else
                {
                    Console.WriteLine("File backup không tồn tại!");
                }
            }
            catch (UnauthorizedAccessException)
            {
                Console.WriteLine("Lỗi: Không có quyền truy cập file!");
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Lỗi khi phục hồi file backup: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi không xác định khi phục hồi backup: {ex.Message}");
            }
        }
    }
}




