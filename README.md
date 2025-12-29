# DE-TAI-KTHP---HE-THONG-QUAN-LY-DON-HANG-QUAN-AN

**Cài đặt hệ thống**

Hệ thống được xây dựng dưới dạng ứng dụng Console sử dụng ngôn ngữ lập trình C#. Để chương trình hoạt động ổn định, máy tính cần đáp ứng các yêu cầu sau:
-	Hệ điều hành Windows.
-	Cài đặt .NET Framework hoặc .NET SDK phù hợp.
-	Phần mềm Visual Studio (phiên bản 2019 trở lên).
-	Máy tính có bàn phím để nhập liệu và thao tác với chương trình.
  
**Cách cài đặt và chạy chương trình**

Quy trình cài đặt và chạy chương trình được thực hiện theo các bước sau:

Bước 1: Mở phần mềm Visual Studio/Visual Studio Code

Bước 2: Mở project Hệ thống quản lý đơn hàng quán ăn.

Bước 3: Kiểm tra file Program.cs là file chứa phương thức Main() - điểm khởi đầu của chương trình.

Bước 4: Chạy chương trình.

Bước 5: Sau khi chạy thành công, giao diện menu chính của chương trình sẽ hiển thị trên màn hình console.

**Hướng dẫn sử dụng hệ thống**

Giao diện menu chính

Khi chương trình được khởi động, giao diện menu chính sẽ xuất hiện trên màn hình console. Menu hiển thị danh sách các chức năng quản lý đơn hàng dưới dạng các lựa chọn được đánh số. Người dùng thao tác bằng cách nhập số tương ứng với chức năng mong muốn và nhấn phím Enter để thực hiện.
 <img width="602" height="285" alt="image" src="https://github.com/user-attachments/assets/3aeb958e-b4ef-4ce6-b1db-ad64a49b8c2d" />

Hướng dẫn tạo đơn hàng

Người dùng chọn chức năng Tạo đơn hàng từ menu chính. Chương trình sẽ yêu cầu nhập tên khách hàng, sau đó hiển thị danh sách các món ăn có sẵn. Người dùng nhập mã các món ăn cần chọn để thêm vào đơn hàng và lựa chọn hình thức thanh toán. Sau khi hoàn tất, đơn hàng được lưu vào danh sách quản lý và có thể tiếp tục xử lý ở các chức năng khác. 

<img width="446" height="437" alt="image" src="https://github.com/user-attachments/assets/f0d46ed0-6834-4af9-8af3-9e762f45529f" />

Hướng dẫn cập nhật đơn hàng: 

Người dùng nhập mã đơn hàng cần cập nhật, sau đó có thể chỉnh sửa thông tin khách hàng, danh sách món ăn hoặc trạng thái đơn hàng. 

<img width="602" height="349" alt="image" src="https://github.com/user-attachments/assets/961945cc-256b-4263-a461-c6e73a608b55" />

Hướng dẫn xóa đơn hàng: 

Người dùng nhập mã đơn hàng cần xóa. Trước khi xóa, chương trình yêu cầu xác nhận nhằm tránh thao tác nhầm. Nếu xác nhận hợp lệ, đơn hàng sẽ được loại bỏ khỏi danh sách quản lý.

<img width="847" height="174" alt="image" src="https://github.com/user-attachments/assets/2a39d214-63fc-460e-9c68-d1635f929353" />

Hiển thị đơn hàng: 

Cho phép xem toàn bộ danh sách đơn hàng cùng các thông tin chi tiết như tên khách hàng, ngày tạo, trạng thái, danh sách món và tổng tiền.

 <img width="602" height="111" alt="image" src="https://github.com/user-attachments/assets/e188f158-b58a-4042-982c-aa56fd35eb5d" />


Hướng dẫn Tìm kiếm đơn hàng: 

Hỗ trợ tìm kiếm theo mã đơn hàng, tên khách hàng hoặc tổng tiền, giúp người dùng nhanh chóng tra cứu thông tin.

 <img width="602" height="332" alt="image" src="https://github.com/user-attachments/assets/b7d335e3-4959-4a35-9b06-bb5940cc2b04" />

Sắp xếp đơn hàng: 

Cho phép sắp xếp danh sách đơn hàng theo trạng thái, tên khách hàng (A-Z) hoặc tổng tiền tăng dần.

<img width="602" height="172" alt="image" src="https://github.com/user-attachments/assets/2c2c96e9-f03f-4d46-861e-2d46d67dadc4" />

Hướng dẫn thống kê - báo cáo: 

Chức năng thống kê - báo cáo giúp tổng hợp dữ liệu đơn hàng, bao gồm tổng số đơn, tổng doanh thu và phân loại đơn hàng theo trạng thái. Ngoài ra, chương trình còn thống kê các món ăn được bán nhiều nhất, hỗ trợ người dùng đánh giá hiệu quả hoạt động của cửa hàng.

<img width="337" height="392" alt="image" src="https://github.com/user-attachments/assets/425d8e38-1729-43a8-bdfc-cf526b8302f3" />


Xử lý file: 

Dữ liệu đơn hàng được lưu vào file text nhằm đảm bảo dữ liệu không bị mất khi chương trình kết thúc. Chương trình có cơ chế sao lưu file để phục hồi dữ liệu trong trường hợp xảy ra lỗi.

 <img width="232" height="107" alt="image" src="https://github.com/user-attachments/assets/57e90128-5073-46ef-b757-e609d539d59e" />

Thoát chương trình an toàn: 

Khi người dùng chọn chức năng thoát, chương trình sẽ kết thúc sau khi đảm bảo dữ liệu đã được lưu đầy đủ, tránh mất mát thông tin đơn hàng.

 <img width="245" height="47" alt="image" src="https://github.com/user-attachments/assets/417155ed-b83f-4526-aa05-ca8029c93a10" />

