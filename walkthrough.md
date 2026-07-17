# Walkthrough: Hướng dẫn vận hành ứng dụng Auto Overtime Scheduler

Ứng dụng C# WinForms xếp lịch tăng ca tự động đã hoàn thành việc xây dựng và biên dịch thành công. Dưới đây là tóm tắt cấu trúc mã nguồn, tính năng và cách thức vận hành ứng dụng.

---

## 1. Cấu trúc Mã nguồn Đã Triển khai

*   [Employee.cs](file:///c:/Users/IRS03-415/Desktop/7139/PathofBuilding/OvertimeScheduler/Models/Employee.cs): Lớp mô tả nhân viên, tích hợp bộ nạp tự động từ tệp Excel [ListNhânViên.xlsx](file:///c:/Users/IRS03-415/Desktop/7139/PathofBuilding/ListNhânViên.xlsx). Chứa Mã NV, Họ tên, Chức danh, danh sách khoảng báo nghỉ phép (`LeavePeriods`) và danh sách giờ làm thêm gán cố định (`FixedOvertimeHours`).
*   [CompanyHoliday.cs](file:///c:/Users/IRS03-415/Desktop/7139/PathofBuilding/OvertimeScheduler/Models/CompanyHoliday.cs): Lớp mô tả ngày nghỉ lễ công ty.
*   [ScheduleEntry.cs](file:///c:/Users/IRS03-415/Desktop/7139/PathofBuilding/OvertimeScheduler/Models/ScheduleEntry.cs): Cấu trúc lưu trữ lịch xếp ca theo từng ngày cho 3 ca trực: "Ngày", "Đêm", và "Hành chính".
*   [SchedulerEngine.cs](file:///c:/Users/IRS03-415/Desktop/7139/PathofBuilding/OvertimeScheduler/Services/SchedulerEngine.cs): Thuật toán xếp lịch tự động công bằng theo khối ngày làm việc, tính lũy tích lũy tổng giờ tăng ca. Ưu tiên phân bổ các nhân viên có ít ca trực nhất, kiểm tra khoảng nghỉ phép và điền trước các giờ gán cố định. Hỗ trợ cấu hình động số người/ca và ngày nghỉ lễ công ty.
*   [ExcelService.cs](file:///c:/Users/IRS03-415/Desktop/7139/PathofBuilding/OvertimeScheduler/Services/ExcelService.cs): Thực hiện xuất lịch tuần ClosedXML theo đúng định dạng khối dọc trực quan (Stacked Block Layout) chuẩn nhà xưởng:
    *   Mỗi khối có dòng tiêu đề màu vàng.
    *   Gồm 4 cột ca làm việc: **CA 1 (6:00~14:00)** (Xanh dương), **CA 2 (14:00~22:00)** (Xám nhạt - để trống dữ liệu), **CA 3 (22:00~6:00)** (Cam) và **HÀNH CHÍNH** (Xanh lá).
    *   Tên nhân viên được IN HOA kèm số giờ làm thêm cố định nếu có (Ví dụ: `NGUYEN KIM CHI 12h`).
    *   Tự động kẻ ô viền mỏng (Thin Border) cho toàn bộ bảng.
*   [MessageParser.cs](file:///c:/Users/IRS03-415/Desktop/7139/PathofBuilding/OvertimeScheduler/Services/MessageParser.cs): Bộ phân tích cú pháp tin nhắn tiếng Việt báo nghỉ phép (ví dụ: "Bình nghỉ thứ 3", "Nam bận t6"...) bằng cách chuẩn hóa loại bỏ dấu và tìm kiếm nhân viên + thứ tương ứng.
*   [ZaloBotService.cs](file:///c:/Users/IRS03-415/Desktop/7139/PathofBuilding/OvertimeScheduler/Services/ZaloBotService.cs): Điều khiển trình duyệt Chrome qua Selenium WebDriver, đăng nhập Zalo Web, lắng nghe tin nhắn báo nghỉ để cập nhật lịch và tự gửi lại file Excel mới xuất vào nhóm.
*   [Form1.cs](file:///c:/Users/IRS03-415/Desktop/7139/PathofBuilding/OvertimeScheduler/Form1.cs) & [Form1.Designer.cs](file:///c:/Users/IRS03-415/Desktop/7139/PathofBuilding/OvertimeScheduler/Form1.Designer.cs): Giao diện kéo thả trực quan 4 Tab với thanh điều hướng khoảng ngày, nút tự động xếp lịch, Xuất Excel & Gửi Zalo, hiển thị trạng thái giờ đã xếp/còn lại. Giao diện được tối giản hoá và Top Bar được tinh gọn chiều cao xuống **45px** để tăng tối đa không gian hiển thị chính.
*   [OvertimeChart.cs](file:///c:/Users/IRS03-415/Desktop/7139/PathofBuilding/OvertimeScheduler/Forms/OvertimeChart.cs): Biểu đồ vẽ bằng đồ họa vector GDI+ (`System.Drawing`), biểu thị cột tổng số giờ tăng ca của từng vai trò và dây thể hiện số tiếng trung bình trên đầu người để kiểm tra tính công bằng.
*   [EmployeeEditForm.cs](file:///c:/Users/IRS03-415/Desktop/7139/PathofBuilding/OvertimeScheduler/Forms/EmployeeEditForm.cs) & [EmployeeEditForm.Designer.cs](file:///c:/Users/IRS03-415/Desktop/7139/PathofBuilding/OvertimeScheduler/Forms/EmployeeEditForm.Designer.cs): Cửa sổ popup mở ra khi click đúp thẻ nhân sự, cho phép nhập khoảng ngày báo nghỉ phép và gán giờ làm thêm cố định.

---

## 2. Cách thức Vận hành và Hướng dẫn Sử dụng

### 2.1. Chuẩn bị chạy ứng dụng
1. Mở thư mục dự án: [OvertimeScheduler](file:///c:/Users/IRS03-415/Desktop/7139/PathofBuilding/OvertimeScheduler).
2. Chạy ứng dụng bằng cách click đúp file **`run_app.bat`** ở thư mục gốc hoặc gõ lệnh:
   ```powershell
   dotnet run
   ```

### 2.2. Các thao tác trên giao diện chính
*   **Tải danh sách tự động**: Ứng dụng tự động đọc tệp [ListNhânViên.xlsx](file:///c:/Users/IRS03-415/Desktop/7139/PathofBuilding/ListNhânViên.xlsx) ở thư mục gốc khi chạy. 
*   **Tìm kiếm nhân sự**: Gõ vào ô tìm kiếm ở Sidebar để lọc nhanh. Sidebar chỉ vẽ tối đa 50 nhân viên đầu tiên để tối ưu hiệu năng.
*   **Tab 1 (BẢNG XẾP LỊCH)**: Kéo thẻ nhân sự đó từ Sidebar bên trái thả vào 1 trong 3 cột ca trực (**CA NGÀY**, **CA ĐÊM**, **HÀNH CHÍNH**). Bấm nút `[X]` để báo nghỉ phép nhanh cho nhân viên.
*   **Tab 2 (BIỂU ĐỒ THỐNG KÊ)**: Xem biểu đồ cột và dây GDI+ toàn màn hình thống kê số giờ làm thêm của từng vai trò.
*   **Tab 3 (NGÀY NGHỈ CÔNG TY - LIVE CALENDAR)**: Chọn ngày trên MonthCalendar, nhập tên lễ và Thêm/Xóa ngày nghỉ. Lịch ca và Excel sẽ tự động đồng bộ theo thời gian thực.
*   **Tab 4 (CÀI ĐẶT CẤU HÌNH)**: Nhóm toàn bộ thiết lập cấu hình:
    *   Checkbox `[ ] Thứ 7 làm thường` (khi bật, Thứ 7 không tính tăng ca và bỏ qua không tính toán hay xuất Excel).
    *   Ô chọn số `Người/ca` cấu hình tối đa nhân sự/ca xếp tự động (từ 1 đến 10).
    *   Ô chọn `Quỹ làm thêm giờ 1 tháng` của xưởng.
    *   Cấu hình Zalo Bot (Tên nhóm nhận lịch và nút Bật/Tắt Bot).

### 2.3. Vận hành Zalo Bot tự động xếp lịch qua tin nhắn
1. Vào **Tab 4 (Cài Đặt)**, nhập chính xác tên nhóm chat Zalo nhận lịch và bấm nút **[Bật Zalo Bot]**: Một cửa sổ trình duyệt Chrome sẽ mở ra truy cập Zalo Web. Quét mã QR để đăng nhập.
2. Bot sẽ tự động mở nhóm chat được định vị và lắng nghe tin nhắn báo nghỉ để tự xếp lại lịch, xuất file Excel mới và gửi phản hồi trực tiếp vào nhóm.

---

## 3. Hình ảnh thiết kế giao diện và Excel

#### Giao diện Ứng dụng đầy đủ tích hợp biểu đồ Combo Chart:
![Giao diện đầy đủ](/C:/Users/IRS03-415/.gemini/antigravity/brain/3b35b46a-04a7-43f4-acf7-8a0f0eaa0b89/winforms_final_mockup_1784263976481.jpg)

#### Bảng Excel Lịch Tuần trơn đơn giản xuất ra:
![Bảng Excel](/C:/Users/IRS03-415/.gemini/antigravity/brain/3b35b46a-04a7-43f4-acf7-8a0f0eaa0b89/excel_simple_mockup_1784267703612.jpg)
