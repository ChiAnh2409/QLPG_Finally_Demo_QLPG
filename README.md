# Demo hoàn tất các chức năng cơ bản Lần 15 (3/1/2024).
# WebApp Quản Lý Phòng Gym
Use C# ASP.NET, SQL server.
+ Dự án bao gồm:
  Trang giao diện dành cho mọi người xem thông tin chung về Phòng Gym.
 - Trang giao diện đăng ký tập thử 1 ngày miễn phí cho khách hàng. (kiểm tra định dạng dữ liệu nhập vào như: chỉ nhập chữ cho phần tên, 10 số cho sđt, chỉ nhập chữ và số + @gmail.com)
 - Trang quản lý bao gồm: các chức năng thêm, xóa, sửa, tìm kiếm theo tên, phân quyền : quản lý (bảng doanh thu theo từng tháng), tiếp tân (danh sách khách hàng đăng ký tập thử) , cskh (dang sách các gói tập hết hạn của hội viên).
 - Quản lý thành viên: khách hàng nút bấm đăng ký ngay trên form đăng ký tập thử sẽ được lưu trực tiếp vào bảng thành viên (lưu cả ngày ĐK). Đặc biệt thành viên đã thành hội viên thì không thể xóa khỏi csdl.
 - Quản lý hội viên: hiển thị thông tin cơ bản và tình trạng tham gia gói tập của hội viên (tên hội viên được chọn từ bảng thành viên),
   thêm cột điểm danh và gia hạn: điểm danh và xem chi tiết điểm danh của hội viên kèm theo tên gói tập (đã xong), gia hạn gói tập cũ dựa vào ngày kết thúc của gói tập đó, không còn xét dựa vào tình trạng của hội viên nữa (xong). Đặc biệt xóa hội viên chỉ được khi thêm mới hội viên mà chưa đăng ký gói tập.
   Tính mức độ thường xuyên đi tập của khách hàng theo gói (tuần: 7/7 buổi/ngày, tháng: 30/30, 3 tháng 90/90), đăng ký gói tập chưa tới ngày bắt đầu thì ẩn tác vụ đi trong phần hành động của điểm danh và gia hạn.
 - Quản lý gói tập: gói tập (chi phí, thời gian hiệu lực). 
 - Quản lý đăng ký gói tập: biết được hội viên A đã đăng ký được bao nhiêu gói tập và có thể dựa vào đó để tính thống kê doanh thu.
 - Quản lý người dùng bao gồm: thông tin của người dùng cơ bảng (admin có thể phân quyền đăng nhập)
 - Quản lý lịch trình: điểm danh sẽ lưu lại thông tin ngày giờ mà hội viên đến phòng tập.
 - Gia hạn gói tập: hội viên muốn tập gói tập cũ thì người quản lý sẽ gia hạn theo tên gói tập cũ và tên hội viên đó. Sau khi gia hạn thì tình trạng của hội viên sẽ là hoạt động.
 - Điểm danh cho hội viên: khi đã điểm danh thành công cho đăng gói tập nào thì sẽ hiển thị cụm từ "đã điểm danh" trong phần hành động, xem chi tiết buổi tập theo đăng ký gói tập của hội viên đó.
 - Thống kê doanh thu: đã hoàn thiện xong chức năng. (hiển thị được sơ đồ cột và bảng dữ liệu cùng năm đã chọn)
 - Các phần quan trọng như: số lượng KH đăng ký tập thử, số lượng hội viên đăng ký mới trong ngày, số lượng hội viên hết hạn gói tập, tỷ số chuyển đổi,...
 - Thông báo đăng ký tập thử miễn phí với nội dung của khách hàng gồm: tên, gói, sđt, ngày đăng ký gói tập.
 - Xuất file excel thống kê doanh thu từng tháng theo năm (chọn xem theo năm và xuất theo năm chọn luôn)
Giao diện trang chủ ![image](https://github.com/ChiAnh2409/QLPG_Demo_fisrt/assets/118975118/f173eeff-284f-4727-8543-d63d6874fedc)
Giao diện các phần tập ![image](https://github.com/ChiAnh2409/QLPG_Demo_second/assets/118975118/de394481-8dcc-4a26-bc6e-65f3901ba47b)
Giao diện các thiết bị tại phòng Gym ![image](https://github.com/ChiAnh2409/QLPG_Demo_fisrt/assets/118975118/ff3c5560-5860-4b36-9114-07acbed6d228)
Giao diện thông tin giai đoạn thành lập ![image](https://github.com/ChiAnh2409/QLPG_Demo_fisrt/assets/118975118/534e888a-7a6a-48ab-b000-24e9df4287d2)
Giao diện thông tin Staff ![image](https://github.com/ChiAnh2409/QLPG_Demo_fisrt/assets/118975118/8acb5e8e-7b17-4e00-9813-dd609c4bae43)
Giao diện đăng ký tập thử ![image](https://github.com/ChiAnh2409/QLPG_Demo_fisrt/assets/118975118/1fa3f656-d844-448d-a085-b40ae1409e00)
Hiển thị thông báo đăng ký thành công: ![image](https://github.com/ChiAnh2409/QLPG_Demo_fisrt/assets/118975118/6fa20a06-e6a6-4b1a-bfd9-40b731206914)

Giao diện khuyến mãi ưu đãi đặc biệt ![image](https://github.com/ChiAnh2409/QLPG_Demo_second/assets/118975118/8c6463ef-4f03-49ba-9edf-fadf221450e3)
Giao diện thông tin bổ ích ![image](https://github.com/ChiAnh2409/QLPG_Demo_fisrt/assets/118975118/cca3ca4d-612b-42ba-a9af-1d08306714dc)
Click vào để xem chi tiết ![image](https://github.com/ChiAnh2409/QLPG_Demo_fisrt/assets/118975118/ae89541a-5dca-4909-bea7-b1e210357968)

Chi tiết Treadmill Exercise bao gồm: 
 - Thực đơn ăn uống phù hợp và lợi ích của Treadmill Exercise  ![image](https://github.com/ChiAnh2409/QLPG_Demo_second/assets/118975118/dadb3dc8-9286-41d0-be0a-4be018193390)
 ![image](https://github.com/ChiAnh2409/QLPG_Demo_second/assets/118975118/0c7aeb57-0535-4785-bd0a-1492ef25742c)

Giao diện login 
![image](https://github.com/ChiAnh2409/QLPG_Demo_fisrt/assets/118975118/5d47ceaa-959b-4c3e-8420-466691089a8b)

Giao diện trang quản lý 
  - Thông tin quản lý:
    <img width="464" alt="image" src="https://github.com/ChiAnh2409/QLPG_Demo_finally/assets/118975118/655620f1-a322-4dd0-94e5-3dbeece7384b">
  
       Giao diện thống kê theo năm: ![image](https://github.com/ChiAnh2409/QLPG_Demo_finally_5/assets/118975118/1b69a3da-b175-42be-ae3a-4fbb7a5fbf6c)
    
       Giao diện bảng doanh thu theo tháng: ![image](https://github.com/ChiAnh2409/QLPG_Demo_finally_5/assets/118975118/0da377f2-887e-42c4-91d6-eea2b721334e)
    
  - Trang quản lý của lễ tân:   <img width="471" alt="image" src="https://github.com/ChiAnh2409/QLPG_Demo_finally/assets/118975118/b15e0ea8-3229-4ef6-b608-b7f774e80cf2">
  - Trang quản lý của CSKH: <img width="471" alt="image" src="https://github.com/ChiAnh2409/QLPG_Demo_finally/assets/118975118/776cec64-53dd-47c8-bf44-42055a8dfbbb">

  - Thông tin quan trọng: <img width="472" alt="image" src="https://github.com/ChiAnh2409/QLPG_Demo_fifth/assets/118975118/5689925e-552f-43c5-9ee5-f2ea5e0fe0de">
  
Giao diện trang quản lý gói tập 
Giao diện trang Đăng Ký Gói Tập ![image](https://github.com/ChiAnh2409/QLPG_Demo_finally_5/assets/118975118/76efad40-bbfc-46cd-98e4-3dad57ea2e28)

Giao diện trang quản lý Thành Viên ![image](https://github.com/ChiAnh2409/QLPG_Demo_second/assets/118975118/a3155311-d8d4-4866-a1ae-27d47654ff2d)
Giao diện trang quản lý Hội Viên
- hội viên nào có tình trạng là "hết hạn" có nghĩa là gói tập đã hết hạn.
   ![image](https://github.com/ChiAnh2409/QLPG_Demo_finally_5/assets/118975118/8de9fa3b-3893-4b96-b186-c103f00cfebe)
- Trang gia diện điểm danh và gia hạn : ![image](https://github.com/ChiAnh2409/QLPG_Demo_finally_5/assets/118975118/7bea4aa4-9d79-49ae-817c-b100f06929f1)

Giao diện gia hạn gói tập cho hội viên ![image](https://github.com/ChiAnh2409/QLPG_Demo_third/assets/118975118/dc795e19-fa04-4a67-8991-ca8519ca0d6f)

Giao diện điểm danh của hội viên ![image](https://github.com/ChiAnh2409/QLPG_Demo_second/assets/118975118/0734c8c3-93ac-4b4f-bb33-0284d4dedad7)

Giao diện trang quản lý Người Dùng ![image](https://github.com/ChiAnh2409/QLPG_Demo_second/assets/118975118/8ee2257e-ba4a-4fb2-ba89-5459ed1e55c2)




