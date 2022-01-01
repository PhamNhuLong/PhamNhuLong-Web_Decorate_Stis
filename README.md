# PhamNhuLong-Web_Decorate_Stis
Đồ án năm 3 môn FrameWork

# framework_IS220.M11_10
Đồ án web và framework: Cửa hàng trang trí Stis Decor

## Giới thiệu về đồ án
- Đồ án cửa hàng Decorate Stis là đồ án web đầu tiên của nhóm nên nhóm làm còn nhiều thiếu xót cũng như chưa được hoàn thiện nhiều
- Các chức năng chính của đồ án như:
	- Giỏ hàng
	- Admin quản lý
	- Mua hàng
	- Tìm kiếm sản phẩm
	- Lọc theo danh mục
	- ...
## Giới thiệu thành viên nhóm
- Phạm Như Long		: https://www.facebook.com/phamnhulong3.3/
- Lý Trần Thanh Thảo	: https://www.facebook.com/lttthao2601
- Hoàng Ngọc Thảo Quyên	: https://www.facebook.com/Quyenn.hntq.1611
- Bùi Bích Chăm		: https://www.facebook.com/bichcham201101
## Đánh giá theo đóng góp
- Long 30%
- Thảo 30%
- Quyên 25%
- Chăm 15%
## Hướng dẫn cài đặt
- Cài đặt Visual Studio 2019
- Cài đặt SQL Server 2019
  - Đăng nhập github
  - Truy cập https://github.com/PhamNhuLong/framework_IS220.M11_10.git và __clone__ code về máy
  - Tạo database CuaHangDecorate bằng SQL Server 2019
## Cài đặt các gói nuget packages sau:
- AspNetCoreHero.ToastNotification (1.1.0)
- Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore (5.0.12)
- Microsoft.AspNetCore.Identity.EntityFrameworkCore (5.0.12)
- Microsoft.AspNetCore.Identity.UI (5.0.12)
- Microsoft.AspNetCore.Mvc.Razor.RuntimeComplication (5.0.12)
- Microsoft.AspNetCore.session (2.2.0)
- Microsoft.EntityFrameworkCore.SqlServer (5.0.12)
- Microsoft.EntityFrameworkCore.Tool (5.0.12)
- Microsoft.Extension.Caching.Memory (5.0.0)
- Microsoft.VisualStudio.Web.CodeGeneration.Design (5.0.2)
- Newtonsoft.Json (13.0.1)
- PagedList.Core.Mvc (3.0.0)
## Tạo database và Insert dữ liệu
__Tạo database CuaHangDecorate__
```create database CuaHangDecorate;```  

__Tạo bảng Tài Khoản__
```CREATE   TABLE TAIKHOAN (
		MaTK int IDENTITY(1,1) PRIMARY KEY,
		SoDienThoai char(10),
		MatKhau varchar(20),
		RoleID int)
```  
__tạo bảng NHÂN VIÊN__
```CREATE TABLE NHANVIEN(
		MaNV int IDENTITY(1,1) PRIMARY KEY,
		TenNV nvarchar(50) NOT NULL,
		SoDienThoai char(10) NOT NULL,
		NgaySinh Date,
		GioiTinh char(3),
		DiaChi char(50) NOT NULL,
		NgayVaoLam Date NOT NULL,
		ChucVu char(25) NOT NULL)
 ```  
__Tạo bảng VOUCHER__
```CREATE TABLE VOUCHER(
		MaVoucher INT IDENTITY(1,1) PRIMARY KEY,
		TenVoucher nvarchar(40) NOT NULL,
		TiLeGiamGia float NOT NULL,
		NgayBatDau Date NOT NULL,
		NgayKetThuc Date NOT NULL)
 ```  
__Tạo bảng DANH MỤC SP__
```CREATE TABLE DANHMUCSP (
		MaDanhMuc int IDENTITY(1,1) PRIMARY KEY,
		TenDanhMuc nvarchar(50) NoT NULL,
		MoTa nvarchar(200))
```  
__Tạo bảng SẢN PHẨM__
```CREATE TABLE SANPHAM (
		MaSP int IDENTITY(1,1) PRIMARY KEY,
		TenSP nvarchar(50) NOT NULL,
		GiaTien Money,
		SoLuong int NOT NULL,
		MaDanhMuc int NOT NULL,
		MoTa nvarchar(200),
		MaNCC int NOT NULL
		)
 ```  
__Tạo bảng KHÁCH HÀNG__

```CREATE TABLE KHACHHANG(
		MaKH int IDENTITY(1,1) PRIMARY KEY,
		TenKH nvarchar(50) NOT NULL,
		SoDienThoai char(10) NOT NULL,
		NgaySinh Date,
		GioiTinh char(10),
		DiaChi char(50) NOT NULL,
		LoaiKH char(50) NOT NULL)
```  
__Tạo bảng ĐƠN ĐẶT HÀNG__

```CREATE TABLE DONDATHANG (
		MaDDH int IDENTITY(1,1) PRIMARY KEY,
		MaKH int NOT NULL,
		MaVoucher int,
		TongDonHang money,
		SoTienGiam money,
		ThanhTien money,
		MaNV int,
		NgayDatHang Date,
		MaNVC int)
```  
__Tạo bảng CHI TIẾT ĐẶT HÀNG__
```CREATE TABLE CTDH (
		MaDDH int NOT NULL,
		MaSP int NOT NULL,
		SoLuong int NOT NULL,
		GiaTien Money)
```  
__Tạo bảng Hình ảnh__
```CREATE TABLE HINHANH (
		MaHinhAnh int IDENTITY(1,1) PRIMARY KEY,
		LinkHinhAnh varchar(200),
		MaSP int)
```  
__Tạo bảng Nhà cung cấp__
```CREATE TABLE NHACUNGCAP (
		MaNCC int IDENTITY(1,1) PRIMARY KEY,
		TenNCC nvarchar(200) NOT NULL,
		DiaChi nvarchar(200) ,
		Email nvarchar(200) )
```  
__Tạo bảng Nhà vận chuyển__
```CREATE TABLE NHAVANCHUYEN (
		MaNVC int IDENTITY(1,1) primary key,
		TenNVC nvarchar (200),
		DiaChi nvarchar(200) ,
		Email nvarchar(200) )
```  
__Tạo bảng Role__
```CREATE  TABLE Roles (
		RoleID int IDENTITY(1,1) primary key,
		RoleName nvarchar (50),
		MoTa nvarchar(200) )
```  
__RÀNG BUỘC KHÓA NGOẠI__

--------Bảng tài khoản------------------------------------------  
```ALTER TABLE TAIKHOAN ADD CONSTRAINT FK_TAIKHOAN FOREIGN KEY (RoleID) REFERENCES Roles(RoleID);```
  
--BẢNG ĐƠN ĐẶT HÀNG--  
```ALTER TABLE DONDATHANG ADD CONSTRAINT FK_KHACHHANG FOREIGN KEY (MaKH) REFERENCES KHACHHANG(MaKH);
ALTER TABLE DONDATHANG ADD CONSTRAINT FK_VOUCHER FOREIGN KEY (MaVoucher) REFERENCES VOUCHER(MaVoucher);
ALTER TABLE DONDATHANG ADD CONSTRAINT FK_NHANVIEN FOREIGN KEY (MaNV) REFERENCES NHANVIEN(MaNV);
ALTER TABLE DONDATHANG ADD CONSTRAINT FK_NHAVANCHUYEN FOREIGN KEY (MaNVC) REFERENCES NHAVANCHUYEN(MaNVC);
```  
--BẢNG CHI TIẾT ĐẶT HÀNG--
```ALTER TABLE CTDH ADD CONSTRAINT FK_SANPHAM FOREIGN KEY (MaSP) REFERENCES SANPHAM(MaSP);
ALTER TABLE CTDH ADD CONSTRAINT FK_DONDATHANG FOREIGN KEY (MaDDH) REFERENCES DONDATHANG(MaDDH);
```  
--BẢNG SẢN PHẨM--
```
ALTER TABLE SANPHAM ADD CONSTRAINT FK_DANHMUCSP FOREIGN KEY (MaDanhMuc) REFERENCES DANHMUCSP(MaDanhMuc);
ALTER TABLE SANPHAM ADD CONSTRAINT FK_NHACUNGCAP FOREIGN KEY (MaNCC) REFERENCES NHACUNGCAP(MaNCC);
```  
--Bảng hình ảnh--  
```ALTER TABLE HINHANH ADD CONSTRAINT FK_SANPHAMM FOREIGN KEY (MaSP) REFERENCES SANPHAM(MaSP);```

__INERT DATA__
__--------------------------------------BẢNG ROLES--------------------------------__
```insert into Roles(RoleName,MoTa) values( N'Quản lý', N'Có chức năng quản lý website');
insert into Roles(RoleName,MoTa) values( N'Khách hàng', N'Có chức năng mua các sản phẩm');
insert into Roles(RoleName,MoTa) values( N'Nhân viên', N'Kiểm soát đơn hàng');
```  
__------------------------------------BẢNG TÀI KHOẢN-----------------------------__
```insert into TAIKHOAN(SoDienThoai,MatKhau,RoleID) values('0343988241','Cuahangdecor',1);
insert into TAIKHOAN(SoDienThoai,MatKhau,RoleID) values('0343988242','Cuahangdecor',1);
insert into TAIKHOAN(SoDienThoai,MatKhau,RoleID) values('0962534995','Cuahangdecor',2);
insert into TAIKHOAN(SoDienThoai,MatKhau,RoleID) values('0786667333','Cuahangdecor',2);
insert into TAIKHOAN(SoDienThoai,MatKhau,RoleID) values('0567779900','Cuahangdecor',2);
insert into TAIKHOAN(SoDienThoai,MatKhau,RoleID) values('0366789012','Cuahangdecor',2);
insert into TAIKHOAN(SoDienThoai,MatKhau,RoleID) values('0995551673','Cuahangdecor',3);
insert into TAIKHOAN(SoDienThoai,MatKhau,RoleID) values('0705620098','Cuahangdecor',3);
insert into TAIKHOAN(SoDienThoai,MatKhau,RoleID) values('0350099001','Cuahangdecor',3);
insert into TAIKHOAN(SoDienThoai,MatKhau,RoleID) values('0580218844','Cuahangdecor',3);
```  
__-----------------------------------BẢNG NHÂN VIÊN------------------------------__
```insert into NHANVIEN(TenNV, SoDienThoai, NgaySinh, GioiTinh, DiaChi, NgayVaoLam, ChucVu) values (N'Phạm Như Long','0990009990','2001-03-03','Nam','Long An','2020-11-20','Nhan vien ban hang');
insert into NHANVIEN(TenNV, SoDienThoai, NgaySinh, GioiTinh, DiaChi, NgayVaoLam, ChucVu) values (N'Hoàng Ngọc Thảo Quyên','0345678990','2001-09-11','Nu','Đong Nai','2020-11-20','Nhan vien ban hang');
insert into NHANVIEN(TenNV, SoDienThoai, NgaySinh, GioiTinh, DiaChi, NgayVaoLam, ChucVu) values (N'Bùi Bích Chăm','0990221112','2001-01-07','Nu','Kien Giang','2020-11-20','Nhan vien ban hang');
insert into NHANVIEN(TenNV, SoDienThoai, NgaySinh, GioiTinh, DiaChi, NgayVaoLam, ChucVu) values (N'Lý Trần Thanh Thảo','0770033111','2001-09-09','Nu','Binh Thuan','2020-11-20','Nhan vien ban hang');
insert into NHANVIEN(TenNV, SoDienThoai, NgaySinh, GioiTinh, DiaChi, NgayVaoLam, ChucVu) values (N'Nguyễn Ngọc Châu Pha','0522141338','2001-04-13','Nu','Quang Ngai','2021-01-25','Nhan vien ban hang');
insert into NHANVIEN(TenNV, SoDienThoai, NgaySinh, GioiTinh, DiaChi, NgayVaoLam, ChucVu) values (N'Lê Văn An','0776254420','2000-12-05','Nam','TPHCM','2021-01-25','Nhan vien ban hang');
insert into NHANVIEN(TenNV, SoDienThoai, NgaySinh, GioiTinh, DiaChi, NgayVaoLam, ChucVu) values (N'Nguyễn Minh Hiếu','0884211492','2000-05-07','Nam','TPHCM','2021-05-09','Nhan vien ban hang');
insert into NHANVIEN(TenNV, SoDienThoai, NgaySinh, GioiTinh, DiaChi, NgayVaoLam, ChucVu) values (N'Phạm Thị Thanh Lan','0363559470','1998-10-03','Nu','TPHCM','2021-05-09','Nhan vien ban hang');
insert into NHANVIEN(TenNV, SoDienThoai, NgaySinh, GioiTinh, DiaChi, NgayVaoLam, ChucVu) values (N'Bùi Quốc Bảo','0961527810','1999-06-24','Nam','Dong Nai','2021-10-20','Nhan vien ban hang');
insert into NHANVIEN(TenNV, SoDienThoai, NgaySinh, GioiTinh, DiaChi, NgayVaoLam, ChucVu) values (N'Trần Lê Phương','0752326665','1995-08-25','Nam',N'Long An','2021-10-20','Nhan vien ban hang');
```  
__-----------------------------------BẢNG VOUCHER----------------------------------__
```insert into VOUCHER(TenVoucher, TiLeGiamGia, NgayBatDau, NgayKetThuc) values(N'Giáng Sinh Giảm Đậm Sâu',0.3,'2021-12-01','2021-12-26');
insert into VOUCHER(TenVoucher, TiLeGiamGia, NgayBatDau, NgayKetThuc) values(N'Giảm giá 20% mừng ngày Nhà Giáo Việt Nam',0.2,'2021-11-10','2021-11-20');
insert into VOUCHER(TenVoucher, TiLeGiamGia, NgayBatDau, NgayKetThuc) values(N'Giảm giá 10% cuối tuần ấm áp',0.1,'2021-10-01','2021-10-02');
insert into VOUCHER(TenVoucher, TiLeGiamGia, NgayBatDau, NgayKetThuc) values(N'Trang trí nhà xinh giảm 10%',0.1,'2021-09-12','2021-09-15');
insert into VOUCHER(TenVoucher, TiLeGiamGia, NgayBatDau, NgayKetThuc) values(N'Voucher giảm đồng loạt 15% toàn sàn',0.15,'2021-05-05','2021-05-08');
insert into VOUCHER(TenVoucher, TiLeGiamGia, NgayBatDau, NgayKetThuc) values(N'Voucher giảm giá 5%',0.05,'2021-04-01','2021-04-30');
insert into VOUCHER(TenVoucher, TiLeGiamGia, NgayBatDau, NgayKetThuc) values(N'Voucher giảm giá nhân ngày Quốc Khánh',0.3,'2021-09-02','2021-09-03');
insert into VOUCHER(TenVoucher, TiLeGiamGia, NgayBatDau, NgayKetThuc) values(N'Voucher giảm giá 10% cho bếp xinh',0.1,'2021-02-01','2021-02-08');
insert into VOUCHER(TenVoucher, TiLeGiamGia, NgayBatDau, NgayKetThuc) values(N'Voucher giảm giá 25%',0.25,'2021-01-01','2021-01-14');
insert into VOUCHER(TenVoucher, TiLeGiamGia, NgayBatDau, NgayKetThuc) values(N'Voucher giảm giá 20%',0.2,'2021-03-12','2021-03-19');
insert into VOUCHER(TenVoucher, TiLeGiamGia, NgayBatDau, NgayKetThuc) values(N'Không giảm giá',0,'2020-03-12','2023-03-19');
```  
__-----------------------------------BẢNG DANH MỤC SẢN PHẨM-----------------__
```insert into DANHMUCSP(TenDanhMuc,MoTa) values (N'Đồ trang trí nhà cửa',N'Đồ dùng để trang trí nhà cửa');
insert into DANHMUCSP(TenDanhMuc,MoTa) values (N'Phụ kiện trang trí',N'Phụ kiện trang trí đa dạng');
insert into DANHMUCSP(TenDanhMuc,MoTa) values (N'Khung ảnh và tranh treo tường',N'Khung ảnh và tranh treo tường');
insert into DANHMUCSP(TenDanhMuc,MoTa) values (N'Đèn',N'Đèn trang trí các loại');
insert into DANHMUCSP(TenDanhMuc,MoTa) values (N'Lọ hoa và hoa trang trí',N'Hoa và lọ hoa trang trí');
insert into DANHMUCSP(TenDanhMuc,MoTa) values (N'Ngoài trời và sân vườn',N'Đồ dùng trang trí sân vườn và ngoài trời');
insert into DANHMUCSP(TenDanhMuc,MoTa) values (N'Nhà bếp và phòng ăn',N'Đồ dùng nhà bếp và phòng ăn');
insert into DANHMUCSP(TenDanhMuc,MoTa) values (N'Giấy và decal dán tường',N'Giấy trang trí các loại/Sticker trang trí/Decal dán tường');
insert into DANHMUCSP(TenDanhMuc,MoTa) values (N'Sắc đẹp',N'Các phụ kiện làm đẹp');
insert into DANHMUCSP(TenDanhMuc,MoTa) values (N'Đồ chơi',N'Các đồ chơi cho trẻ em và các đồ chơi để trang trí');
```  
__-----------------------------------BẢNG NHÀ CUNG CẤP-----------------------------------------------------------__
```insert into NHACUNGCAP(TenNCC,DiaChi,Email) values (N'RIBO HOUSE',N'Hà Nội',N'ribohouse.contact@gmail.com');
insert into NHACUNGCAP(TenNCC,DiaChi,Email) values (N'BEYOURS',N'Tp. Hồ Chí Minh',N'beyours.contact@gmail.com');
insert into NHACUNGCAP(TenNCC,DiaChi,Email) values (N'SUNHOUSE',N'Hà Nội',N'sunhouse.contact@gmail.com');
insert into NHACUNGCAP(TenNCC,DiaChi,Email) values (N'Hobby home decor',N'Tp. Hồ Chí Minh',N'hobbyhomedecor@gmail.com');
insert into NHACUNGCAP(TenNCC,DiaChi,Email) values (N'SIB DECOR',N'Đồng Nai',N'info.sibdecor@gmail.com');
insert into NHACUNGCAP(TenNCC,DiaChi,Email) values (N'Doti House',N'Tp. Hồ Chí Minh',N'dotihouse.contact@gmail.com');
insert into NHACUNGCAP(TenNCC,DiaChi,Email) values (N'ongtre',N'Bình Dương',N'ongtre.decor@gmail.com');
insert into NHACUNGCAP(TenNCC,DiaChi,Email) values (N'Mịn Decor',N'Hà Nội',N'mindecor.contact@gmail.com');
insert into NHACUNGCAP(TenNCC,DiaChi,Email) values (N'OCHU',N'Tp. Hồ Chí Minh',N'ochu@gmail.com');
insert into NHACUNGCAP(TenNCC,DiaChi,Email) values (N'DEER',N'Ninh Thuận',N'deer.decor@gmail.com');
```  
__----------------------------------BẢNG SẢN PHẨM-------------------------------------------__
```insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Mô hình trang trí tháp Eiffel decor phòng khách',350000,150,1,N' Tháp Eiffel biểu tượng của kinh đô ánh sáng Paris.
																		Mô hình tháp Eiffel phiên bản mini làm bằng kim loại chi tiết, tỉ mỉ, sống động và được chế tác thủ công',1);
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'SET KHUNG LƯỚI TRANG TRÍ TƯỜNG',200000,120,1,N'Khung lưới trang trí bằng kim loại',1);
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Mô hình decor trang trí máy hát đĩa than loa kèn',999000,120,1,N'Mô hình decor trang trí máy hát đĩa than loa kèn Vintage',1); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Đèn LED TIKTOK RGB 5V',300000,120,4,N'Đèn LED TIKTOK RGB 5V - cảm ứng theo nhạc, dây silicon chống nước, điều khiển bằng APP',4); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Nón len nhí trang trí noel giáng sinh',10000,2000,1,N'Nón len nhí trang trí noel giáng sinh',2); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Set 300 Bông Tuyết Trang Trí Giáng Sinh',20000,120,5,N'Set 300 Bông Tuyết Trang Trí Giáng Sinh',2); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Cây thông noel',1000000,120,5,N'Cây thông noel ',2); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Thang trang trí decor homestay',999000,120,1,N'Thang trang trí decor homestay',3); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Vòng Hoa Treo Trang Trí Giáng Sinh',999000,120,5,N'Vòng Hoa Treo Trang Trí Giáng Sinh',3); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Đèn ngủ hươu nhỏ hoa cẩm tú cầu',320000,120,4,N'Đèn ngủ hươu nhỏ hoa cẩm tú cầu',4); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Tượng trang trí thiên thần nhỏ',275000,120,2,N'Tượng trang trí thiên thần nhỏ',4); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Tượng trang trí hoàng tử công chúa',260000,120,2,N'Tượng trang trí hoàng tử công chúa',4); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Đèn để bàn thông minh',140000,120,4,N'Đèn trang trí để bàn hình quả cầu mây, thiết kế hình học độc đáo, tùy ý đổi màu đèn.',5); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Đèn để bàn 3D',70000,120,4,N'Đèn để bàn hiệu ứng 3D khắc trên bề mặt nhựa mica, chân cắm USB, phụ kiện sạc (mua thêm) giúp cắm được ở cả ổ điện.',5); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Gương để bàn',340000,120,9,N'Gương trang điểm để bàn thiết kế chân đỡ tiện dụng giúp dễ dàng thao tác trong quá trình makeup, dưỡng da, làm tóc,... màu sắc nhẹ nhàng rất nên có trong góc trang điểm của các bạn nữ',6); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Băng Đô Tai Mèo Dễ Thương Phong Cách Hàn Quốc',50000,120,9,N'Băng Đô Tai Mèo Dễ Thương Phong Cách Hàn Quốc',5); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Cài tóc giáng sinh',30000,120,9,N'Chất liệu sợi bông mềm mại, gam màu xanh trắng trendy với nhiều họa tiết và kiểu dáng đa dạng. Thiết kế trẻ trung, năng động, phù hợp với các bạn trẻ',7); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Đèn ngủ nai sừng tấm dễ thương',350000,120,4,N'Thiết kế lồng thủy tinh trong suốt, khắc họa khung cảnh trong rừng, chú nai sừng tấm nghỉ chân dưới tán cây, hiệu ứng đèn đom đóm lấp lánh tạo nên khung cảnh giống như trong giấc mơ.',8); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Tượng trang trí nàng tiên cá ',220000,120,2,N'Tượng trang trí nàng tiên cá ',6); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Dây đèn led búp chanh',999000,120,4,N'Dây đèn led búp chanh',7); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Dây đèn nháy bông tuyết trang trí',100000,120,4,N'Dây đèn nháy bông tuyết trang trí cây thông noel',1);
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Dây Đèn LED Rèm Trăng Sao',200000,120,4,N'Dây Đèn LED Rèm Trăng Sao',2); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Dây đèn LED hình chuông tuần lộc',150000,120,4,N'Dây đèn LED hình chuông tuần lộc trang trí Giáng Sinh',4); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'DÂY ĐÈN LED ông già Noel',50000,1000,4,N'DÂY ĐÈN LED ông già Noel',3); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Đèn dây treo hình họa tiết trang trí NOEL',300000,200,4,N'Đèn dây treo hình họa tiết trang trí NOEL',5); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Đèn cây đứng phong cách châu Âu',650000,100,4,N'Đèn cây đứng phong cách châu Âu - thích hợp trang trí phòng khách và phòng ngủ',5); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Đèn cây đứng Pixar trang trí phòng khách ',350000,100,4,N'Đèn cây đứng Pixar trang trí phòng khách ',6); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Đèn led treo tường bằng gỗ nghệ thuật',430000,100,4,N'Đèn led treo tường bằng gỗ nghệ thuật',7); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Đèn Treo Trần Nhà Phong Cách Bắc Âu Hiện Đại',650000,70,4,N'Đèn Treo Trần Nhà Phong Cách Bắc Âu Hiện Đại',8); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Đèn treo trần nhà hình bướm',600000,100,4,N'Đèn treo trần nhà hình bướm phong cách Bắc Âu',8); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Xe đạp trang trí treo tường phong cách Retro',850000,100,1,N'Xe đạp trang trí treo tường phong cách Retro',9); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Nắp chai bia trang trí treo tường 35cm',50000,120,3,N'Nắp chai bia trang trí treo tường 35cm',10); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Biển số thiếc trang trí tường',50000,120,3,N'Biển số thiếc trang trí tường phong cách Retro Vintage',10); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Tranh mica 3D- Mẫu trứng',250000,40,3,N'Tranh mica 3D- Mẫu trứng',2); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'tranh mica 3D - Cây tình nhân xanh',600000,60,3,N'tranh mica 3D - Cây tình nhân xanh',2); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Tranh vải treo tường họa tiết hoạt hình Meteo Bear',180000,50,3,N'Tranh vải treo tường họa tiết hoạt hình Meteo Bear',3); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Tranh vải treo tường họa tiết hoạt hình Astronaut',180000,20,3,N'Tranh vải treo tường họa tiết hoạt hình Astronaut',3); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Tranh vải treo tường họa tiết hoạt hình Mount Fuji',180000,30,3,N'Tranh vải treo tường họa tiết hoạt hình Mount Fuji',3); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Bộ 5 Tấm Tranh canvas treo tường Mã đáo thành công',2000000,20,3,N'Bộ 5 Tấm Tranh canvas treo tường Mã đáo thành công',4); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Mành macrame lá phối 2 màu treo tường',400000,30,3,N'Mành macrame lá phối 2 màu treo tường',4); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Decal dạ quang hình Mặt trăng và bầu trời',200000,50,8,N'Decal dạ quang hình Mặt trăng và bầu trời ngàn ngôi sao',5); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Tranh dán tường mica 3D Totoro',150000,50,8,N'Tranh dán tường mica 3D Totoro',6); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Giấy Dán Tường Yenhome Tự Dính Họa Tiết Gỗ',100000,100,8,N'Giấy Dán Tường Yenhome Tự Dính 40x300cm Họa Tiết Gỗ',5); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Nhãn Dán Tường Yenhome Họa Tiết Hoa Anh Đào',100000,60,8,N'Nhãn Dán Tường Yenhome Chống Nước Họa Tiết Hoa Anh Đào Từ PVC ',7); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Tượng sư tử để bàn',80000,120,10,N'Tượng sư tử để bàn',8); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Cành hoa baby 63cm trang trí phòng',30000,150,5,N'Cành hoa baby 63cm trang trí phòng',1); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Tượng điêu khắc nghệ thuật phong cách bắc âu',100000,70,10,N'Tượng điêu khắc nghệ thuật phong cách bắc âu',8); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Tượng Trang Trí Để Bàn Hình Phi Hành Gia Mặt Trăng',50000,300,10,N'Tượng Trang Trí Để Bàn Hình Phi Hành Gia Mặt Trăng',8); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'Cây Dương Sỉ Ba Tư Nhân Tạo ',1500000,20,6,N'Cây Dương Sỉ Ba Tư Nhân Tạo ',10); 
insert into sanpham(TenSP,GiaTien,SoLuong,MaDanhMuc,MoTa,MaNCC) values (N'CÂY NGŨ GIA BÌ NHÂN TẠO',1400000,10,6,N'CÂY NGŨ GIA BÌ NHÂN TẠO',10); 
```  
__---------------------------------BẢNG KHÁCH HÀNG-------------------------------------------------------------__
```insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Phạm Minh Nhật','0969871223','1987-12-27','Nam','TPHCM','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Thị Lan Hương','0775432880','1999-03-07','Nu','TPHCM','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Lâm Quốc Thái','0965234110','2000-10-21','Nam','Ha Noi','Bac');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Thị Thu','0522089743','1989-01-20','Nu','Binh Duong','Bac');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Thị Hương Thảo','0356971253','2003-12-09','Nu','Ha Tinh','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Hữu Nhật Tân','0786672110','1998-09-25','Nam','An Giang','Bac');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Phạm Gia Lộc','0965623689','2003-03-16','Nam','Long An','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Lê Trịnh Thảo My','0304994467','1999-12-07','Nu','Ca Mau','Bac');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Thị Thanh Hân','0722514482','1990-02-18','Nu','TPHCM','Vang');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Lê Quốc Trưởng','0977541105','1992-04-19','Nam','Ha Noi','Vang');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Thị Minh Thu','0972713345','1980-12-21','Nu','TPHCM','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Lê Kim Phụng','0798800453','2000-12-06','Nu','Kien Giang','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Vũ Thái Hòa','0977754230','1990-07-01','Nam','Lam Dong','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Trần Thị Thanh Vân','0567843398','1999-10-02','Nu','Nghe An','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Phan Thị Yến Nhi','0568987650','2001-05-16','Nu','Phu Tho','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Hải Đăng','0343876555','1996-03-17','Nam','TPHCM','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Lê Minh Tiến','0722219632','1998-04-20','Nam','Ha Noi','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Trần Hoàng Khang','0563488642','2000-07-12','Nam','Dong Nai','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Trần Mai Thùy Trang','0763987600','1992-08-14','Nu','Quang Ngai','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Ngọc Như Quỳnh','0544789987','2001-10-05','Nu','TPHCM','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Thu Thảo','0544789761','2001-12-06','Nu','TPHCM','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Phước Thiên','0544766587','2000-01-07','Nam','Dong Nai','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Duy','0334567988','1999-10-12','Nam','Long An','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Ngọc Vy','0775345987','2001-11-13','Nu','Lang Son','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Lê Thanh Tú','0346876003','1998-07-28','Nu','Ha Noi','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Trần Nguyễn Thanh Tú','0234876559','2000-04-18','Nu','Ha Noi','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Hưng','0500085478','2000-11-05','Nam','TPHCM','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Lê Chính Tuệ','0234446761','1997-06-24','Nam','Ben Tre','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Phùng Các Các','0875098092','2001-08-26','Nu','Tien Giang','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Thị Yến Linh','0789909094','2000-09-17','Nu','Long An','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Quốc Khải','0798898555','1990-02-09','Nam','Ha Noi','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Trịnh Thị Thu Hà','0257963354','1998-01-10','Nu','Dong Nai','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Thị Khánh Hà','0423356925','1997-11-11','Nu','Khanh Hoa','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Lê Duy Hưng','0222549830','2001-10-28','Nam','TPHCM','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Minh Phát','0532561984','2000-12-27','Nam','Ninh Thuan','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Quốc Hữu Duy','0365240118','1994-09-26','Nam','Binh Thuan','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Lê Thị Kim','0254632002','1992-08-25','Nu','Binh Duong','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Thúy Nga','0563299807','1993-06-24','Nu','Binh Phuoc','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Trần Trung Hiếu','0595720348','1995-06-23','Nam','Nam Dinh','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Trần Ngọc Minh Anh','0620125497','1996-06-22','Nu','TPHCM','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Lê Thị Thúy An','0359668211','2000-03-05','Nu','Quy Nhon','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Ngọc Thùy Anh','0589246785','2001-04-06','Nu','Phu Yen','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Minh Huy','0798523123','2002-05-12','Nam','Dong Thap','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Trần Kim Sang','0953124624','1999-06-14','Nam','An Giang','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Quốc Thịnh','0932568879','1998-07-15','Nam','Tay Ninh','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Tạ Tấn Hoàng','0956238700','1989-08-16','Nam','Ca Mau','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Bùi Minh Trung Trực','0585628812','1997-09-17','Nam','Long An','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Nguyễn Thanh Lam','0523259727','2001-10-18','Nu','TPHCM','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Võ Trọng Danh','0785213644','1996-12-19','Nam','TPHCM','Dong');
insert into KHACHHANG(TenKH,SoDienThoai,NgaySinh,GioiTinh,DiaChi,LoaiKH) values (N'Lê Kim Tiền','0586982871','1995-11-21','Nu','Ha Noi','Dong');
```  
__-----------------------------------BẢNG NHÀ VẬN CHUYỂN---------------------------------------------------------__
```insert into NHAVANCHUYEN (TenNVC,DiaChi,Email)values(N'Viettel Post',N'Tp. Hồ Chí Minh',N'viettelpost@gmail.com')
insert into NHAVANCHUYEN (TenNVC,DiaChi,Email)values(N'VN Post',N'Tp.Hồ Chí Minh',N'vietnampost@gmail.com')
insert into NHAVANCHUYEN (TenNVC,DiaChi,Email)values(N'Giao hàng nhanh - GHN',N'Tp. Hồ Chí Minh',N'giaohangnhanhviennam@gmail.com')
insert into NHAVANCHUYEN (TenNVC,DiaChi,Email)values(N'Giao hàng tiết kiệm - GHTK',N'Hà Nội',N'giaohangtietkiem@gmail.com')
insert into NHAVANCHUYEN (TenNVC,DiaChi,Email)values(N'Kerry Express',N'Hà Nội',N'kerryexpress@gmail.com')
insert into NHAVANCHUYEN (TenNVC,DiaChi,Email)values(N'Sship',N'Đồng Nai',N'sship.info@gmail.com')
insert into NHAVANCHUYEN (TenNVC,DiaChi,Email)values(N'Shipchung',N'Đà Nẵng',N'shipchungvn@gmail.com')
insert into NHAVANCHUYEN (TenNVC,DiaChi,Email)values(N'Best Express',N'Hà Nội',N'best.express@gmail.com')
insert into NHAVANCHUYEN (TenNVC,DiaChi,Email)values(N'Giao hàng Supership',N'Tp. Hồ Chí Minh',N'giaohangsupership@gmail.com')
insert into NHAVANCHUYEN (TenNVC,DiaChi,Email)values(N'247 Express',N'Tp.Hồ Chí Minh',N'express247@gmail.com')
```  
__----------------------------------BẢNG ĐƠN ĐẶT HÀNG-----------------------------------------------------------__
```insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (1,11,1000000,0,1000000,1,'2021-10-22',1);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (2,11,475000,0,475000,1,'2021-10-22',1);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (3,11,140000,0,140000,1,'2021-10-23',2);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (4,11,500000,0,500000,1,'2021-10-24',2);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (5,11,1400000,0,1400000,1,'2021-10-24',2);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (6,11,580000,0,580000,1,'2021-10-24',3);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (7,11,3500000,0,3500000,1,'2021-10-25',3);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (8,11,1220000,0,1220000,1,'2021-10-25',1);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (9,11,1000000,0,1000000,1,'2021-10-25',3);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (10,11,390000,0,390000,1,'2021-10-26',2);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (11,11,999000,0,999000,1,'2021-10-27',2);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (12,11,535000,0,535000,1,'2021-10-28',2);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (13,11,400000,0,400000,1,'2021-10-29',2);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (14,11,600000,0,600000,1,'2021-10-30',2);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (15,11,310000,0,310000,1,'2021-10-31',2);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (16,11,360000,0,360000,1,'2021-10-31',2);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (17,11,550000,0,550000,1,'2021-11-01',2);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (18,11,1149000,0,1149000,1,'2021-11-01',2);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (19,11,100000,0,100000,1,'2021-11-01',2);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (20,11,330000,0,330000,1,'2021-11-02',2);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (21,11,410000,0,410000,2,'2021-11-02',3);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (22,11,999000,0,990000,2,'2021-11-02',3);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (23,11,160000,0,160000,2,'2021-11-03',3);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (24,11,400000,0,400000,3,'2021-11-04',1);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (25,11,400000,0,400000,4,'2021-11-05',1);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (26,11,60000,0,60000,4,'2021-11-06',4);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (27,11,1500000,0,1500000,4,'2021-11-06',4);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (28,11,300000,0,300000,5,'2021-11-06',5);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (29,11,330000,0,330000,5,'2021-11-06',5);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (30,11,200000,0,200000,6,'2021-11-07',3);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (31,11,480000,0,480000,7,'2021-11-07',3);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (32,11,230000,0,230000,8,'2021-11-08',4);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (33,11,200000,0,200000,8,'2021-11-08',5);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (34,11,700000,0,700000,8,'2021-11-08',5);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (35,11,400000,0,400000,9,'2021-11-09',3);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (36,11,800000,0,800000,9,'2021-11-09',6);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (37,11,650000,0,650000,9,'2021-11-09',6);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (38,11,250000,0,250000,10,'2021-11-10',6);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (39,11,540000,0,540000,10,'2021-11-10',7);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (40,11,400000,0,400000,2,'2021-11-11',7);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (41,11,999000,0,999000,2,'2021-11-12',7);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (42,11,20000,0,200000,2,'2021-11-12',6);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (43,11,400000,0,400000,2,'2021-11-13',4);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (44,11,320000,0,320000,3,'2021-11-14',8);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (45,11,550000,0,550000,3,'2021-11-14',8);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (46,11,140000,0,140000,4,'2021-11-15',2);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (47,11,50000,0,50000,4,'2021-11-16',1);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (48,11,650000,0,650000,4,'2021-11-16',1);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (49,11,100000,0,100000,5,'2021-11-17',3);
insert into DONDATHANG(MaKH,MaVoucher,TongDonHang,SoTienGiam,ThanhTien,MaNV,NgayDatHang,MaNVC) values (50,11,600000,0,600000,6,'2021-11-18',4);
```  
__-----------------------------------BẢNG CHI TIẾT ĐƠN HÀNGG-----------------------------------------------------__
```insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(1,1,2,350000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(1,4,1,300000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(2,5,20,10000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(2,11,1,10000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(3,14,2,70000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(4,12,1,260000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(4,13,1,140000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(4,21,1,100000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(5,49,1,1400000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(6,40,2,400000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(6,36,1,180000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(7,26,1,650000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(7,31,1,850000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(7,39,1,2000000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(8,7,1,1000000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(8,6,1,20000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(8,5,10,10000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(8,21,1,100000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(9,7,1,1000000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(10,16,1,50000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(10,15,1,340000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(11,9,1,999000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(12,12,1,260000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(12,11,1,275000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(13,17,2,30000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(13,15,1,340000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(14,27,1,350000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(14,34,1,250000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(15,45,1,80000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(15,48,1,50000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(15,38,1,180000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(16,37,1,180000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(16,36,1,180000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(17,1,1,350000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(17,2,1,200000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(18,8,1,999000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(18,23,1,150000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(19,24,2,50000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(20,25,1,300000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(20,17,1,30000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(21,14,1,70000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(21,15,1,340000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(22,20,1,999000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(23,17,2,30000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(23,21,1,100000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(24,16,1,50000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(24,27,1,350000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(25,25,1,300000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(25,44,1,100000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(26,46,2,30000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(27,49,1,1500000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(28,47,1,100000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(28,41,1,200000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(29,42,1,150000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(29,38,1,180000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(30,2,1,200000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(31,2,2,200000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(31,45,1,80000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(32,38,1,180000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(32,33,1,50000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(33,39,1,2000000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(34,35,1,600000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(34,32,2,50000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(35,40,1,400000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(36,2,1,200000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(36,30,1,600000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(37,29,1,650000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(38,19,1,220000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(38,17,1,30000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(39,15,1,340000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(39,2,1,200000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(40,2,2,200000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(41,3,1,999000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(42,6,1,20000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(43,2,2,200000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(44,10,1,320000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(45,11,2,275000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(46,13,1,140000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(47,24,1,50000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(48,26,2,650000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(49,32,2,50000);
insert into CTDH(MaDDH,MaSP,SoLuong,GiaTien)values(50,30,1,600000);
__---------------------------------BẢNG HÌNH ẢNH------------------------------------------------------------__
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/a6c4d91a88ace408509a9f8a4d13c3c4',1);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/527d72072ecdfca2166b955da401a505',2);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/183748ef0565a93e1fcf69353ff55ca3',3);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/4c51bc5d713a93f46d97d32bf2a9ebe8',4);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/2a206024136a9f3e20acf58b28a1cc3e',5);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/d6983509f33107e00abd28cfd511b8d4',6);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://toplist.vn/images/800px/cach-trang-tri-cay-thong-noel-dep-nhat-trong-dem-giang-sinh-569809.jpg',7);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/286bece70f45a46b037532405798c352',8);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/546a1e619d98c2a31ffeda4334401d89',9);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/f45b6fd01c82d05b54a9af6c01b1558f',10);

insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/6e2069bbe88e628fe61f6e56ec72d39d',11);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/02aa93b2d3224dd522bd3c80f6cbb3db',12);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/60fe857b4b76f29d851c80bb74c203ac',13);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/5c8d27efec5de364b2f6d71107473fb3',14);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/11f1478bbd94e20d38824fb76dfbdaeb',15);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/9128c2a6d78be3544d9fed8aba16d74e',16);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/97b0573d4db8e145c1a1fea6cda8d798',17);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/4e250aeb7e7ec63245028761b88943ec',18);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/e9ab6c7541b7a482bf3b55d5ac4142b2',19);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/bd892c7ad5c0ee571eb62caa1220e854',20);

insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/4e3cc7e9dd4e1a9fc0886dc892086139',21);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/5a21abcc004207fb44e3a694b38b3575',22);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/f9d24b8e3d71774176cd4a733fdc6e8c',23);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/8cd4086b3b6f5206415d78233f7c84a5',24);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/7edea3711f55cb07d2089b58ff8f0e61',25);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/6b4271f76773591469af7de6ecfc534d',26);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/3b36cb7df3c746a98fcc1fad42f0c7bf',27);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/2727000219f569c55bb6d7876aa4a089',28);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/19f4421f5c92a5883de8496a4ee6716e',29);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/1af6a762ee496f66b251f5b088a8c426',30);

insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/ccb4ff5e50a136ceb97cd4b5b6e1b1f0',31);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/03bf3b27cdd523b311e7f39c6e098176',32);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/e3838fe1a817a0510d1d0d0455cdebdf',33);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/2ae8baf6835d83262db2936cbff755bb',34);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/c8d5e05cef30e4f56b5bc79fc37d140e',35);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/32f15d3efcf976ab439a1acb3fd26921',36);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/5bcb4b47fe796237a5398386c023f0e3',37);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/437afe4cbc188c4297b62bed6a400bfc',38);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/d59597f53cfa07811fe3c4b3cf844072',39);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/e123a46552b857d67690a7222f809f1d',40);

insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/442f3b93c95b22dc471901c302218826',41);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/90f58c590fa772c4a2890a7dc734d4dd',42);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/3a26529a76a941ad388b2eb84c1adcec',43);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/3b617d4ea3b04000980d05bc94181f8d',44);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/d2f00866b50a83253a22516a3825b56c',45);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/493fdab1a9da21b21254872b7b6db593',46);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/4eb59400dc88e3c814b069eb6632fed8',47);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/c59490fb20d9d666a928510d96fd5e7b',48);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/b9915d199f6482362df604fe20fa83c1',49);
insert into HINHANH(LinkHinhAnh, MaSP) values('https://cf.shopee.vn/file/37c905565810df0965409ca5f9ad5a38',50);
```  
-----------------------------------------------------
