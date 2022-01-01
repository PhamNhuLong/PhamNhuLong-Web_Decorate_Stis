using System; 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace doan.Models
{
    public partial class CuaHangDecorateContext : DbContext
    {
        public CuaHangDecorateContext()
        {
        }

        public CuaHangDecorateContext(DbContextOptions<CuaHangDecorateContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ctdh> Ctdh { get; set; }
        public virtual DbSet<Danhmucsp> Danhmucsp { get; set; }
        public virtual DbSet<Dondathang> Dondathang { get; set; }
        public virtual DbSet<Hinhanh> Hinhanh { get; set; }
        public virtual DbSet<Khachhang> Khachhang { get; set; }
        public virtual DbSet<Nhacungcap> Nhacungcap { get; set; }
        public virtual DbSet<Nhanvien> Nhanvien { get; set; }
        public virtual DbSet<Nhavanchuyen> Nhavanchuyen { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }
        public virtual DbSet<Sanpham> Sanpham { get; set; }
        public virtual DbSet<Taikhoan> Taikhoan { get; set; }
        public virtual DbSet<Voucher> Voucher { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=localhost;Database=CuaHangDecorate;Integrated Security=true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ctdh>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("CTDH");

                entity.Property(e => e.GiaTien).HasColumnType("money");

                entity.Property(e => e.MaDdh).HasColumnName("MaDDH");

                entity.Property(e => e.MaSp).HasColumnName("MaSP");

                entity.HasOne(d => d.MaDdhNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaDdh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DONDATHANG");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany()
                    .HasForeignKey(d => d.MaSp)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SANPHAM");
            });

            modelBuilder.Entity<Danhmucsp>(entity =>
            {
                entity.HasKey(e => e.MaDanhMuc)
                    .HasName("PK__DANHMUCS__B3750887117D2737");

                entity.ToTable("DANHMUCSP");

                entity.Property(e => e.MoTa).HasMaxLength(200);

                entity.Property(e => e.TenDanhMuc)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Dondathang>(entity =>
            {
                entity.HasKey(e => e.MaDdh)
                    .HasName("PK__DONDATHA__3D88C804D64D54C7");

                entity.ToTable("DONDATHANG");

                entity.Property(e => e.MaDdh).HasColumnName("MaDDH");

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.MaNv).HasColumnName("MaNV");

                entity.Property(e => e.MaNvc).HasColumnName("MaNVC");

                entity.Property(e => e.NgayDatHang).HasColumnType("date");

                entity.Property(e => e.SoTienGiam).HasColumnType("money");

                entity.Property(e => e.ThanhTien).HasColumnType("money");

                entity.Property(e => e.TongDonHang).HasColumnType("money");

                entity.HasOne(d => d.MaKhNavigation)
                    .WithMany(p => p.Dondathang)
                    .HasForeignKey(d => d.MaKh)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_KHACHHANG");

                entity.HasOne(d => d.MaNvNavigation)
                    .WithMany(p => p.Dondathang)
                    .HasForeignKey(d => d.MaNv)
                    .HasConstraintName("FK_NHANVIEN");

                entity.HasOne(d => d.MaNvcNavigation)
                    .WithMany(p => p.Dondathang)
                    .HasForeignKey(d => d.MaNvc)
                    .HasConstraintName("FK_NHAVANCHUYEN");

                entity.HasOne(d => d.MaVoucherNavigation)
                    .WithMany(p => p.Dondathang)
                    .HasForeignKey(d => d.MaVoucher)
                    .HasConstraintName("FK_VOUCHER");
            });

            modelBuilder.Entity<Hinhanh>(entity =>
            {
                entity.HasKey(e => e.MaHinhAnh)
                    .HasName("PK__HINHANH__A9C37A9BF389F9CA");

                entity.ToTable("HINHANH");

                entity.Property(e => e.LinkHinhAnh)
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.Property(e => e.MaSp).HasColumnName("MaSP");

                entity.HasOne(d => d.MaSpNavigation)
                    .WithMany(p => p.Hinhanh)
                    .HasForeignKey(d => d.MaSp)
                    .HasConstraintName("FK_SANPHAMM");
            });

            modelBuilder.Entity<Khachhang>(entity =>
            {
                entity.HasKey(e => e.MaKh)
                    .HasName("PK__KHACHHAN__2725CF1EDD6E207B");

                entity.ToTable("KHACHHANG");

                entity.Property(e => e.MaKh).HasColumnName("MaKH");

                entity.Property(e => e.DiaChi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.GioiTinh)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.LoaiKh)
                    .IsRequired()
                    .HasColumnName("LoaiKH")
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.SoDienThoai)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TenKh)
                    .IsRequired()
                    .HasColumnName("TenKH")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Nhacungcap>(entity =>
            {
                entity.HasKey(e => e.MaNcc)
                    .HasName("PK__NHACUNGC__3A185DEB7013EA99");

                entity.ToTable("NHACUNGCAP");

                entity.Property(e => e.MaNcc).HasColumnName("MaNCC");

                entity.Property(e => e.DiaChi).HasMaxLength(200);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.TenNcc)
                    .IsRequired()
                    .HasColumnName("TenNCC")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Nhanvien>(entity =>
            {
                entity.HasKey(e => e.MaNv)
                    .HasName("PK__NHANVIEN__2725D70AEEE18081");

                entity.ToTable("NHANVIEN");

                entity.Property(e => e.MaNv).HasColumnName("MaNV");

                entity.Property(e => e.ChucVu)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.DiaChi)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.GioiTinh)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.NgaySinh).HasColumnType("date");

                entity.Property(e => e.NgayVaoLam).HasColumnType("date");

                entity.Property(e => e.SoDienThoai)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.TenNv)
                    .IsRequired()
                    .HasColumnName("TenNV")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Nhavanchuyen>(entity =>
            {
                entity.HasKey(e => e.MaNvc)
                    .HasName("PK__NHAVANCH__3A19786F79D016A1");

                entity.ToTable("NHAVANCHUYEN");

                entity.Property(e => e.MaNvc).HasColumnName("MaNVC");

                entity.Property(e => e.DiaChi).HasMaxLength(200);

                entity.Property(e => e.Email).HasMaxLength(200);

                entity.Property(e => e.TenNvc)
                    .HasColumnName("TenNVC")
                    .HasMaxLength(200);
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK__Roles__8AFACE3A7B91FC56");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.MoTa).HasMaxLength(200);

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<Sanpham>(entity =>
            {
                entity.HasKey(e => e.MaSp)
                    .HasName("PK__SANPHAM__2725081C1270B337");

                entity.ToTable("SANPHAM");

                entity.Property(e => e.MaSp).HasColumnName("MaSP");

                entity.Property(e => e.GiaTien).HasColumnType("money");

                entity.Property(e => e.MaNcc).HasColumnName("MaNCC");

                entity.Property(e => e.MoTa).HasMaxLength(200);

                entity.Property(e => e.TenSp)
                    .IsRequired()
                    .HasColumnName("TenSP")
                    .HasMaxLength(50);

                entity.HasOne(d => d.MaDanhMucNavigation)
                    .WithMany(p => p.Sanpham)
                    .HasForeignKey(d => d.MaDanhMuc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DANHMUCSP");

                entity.HasOne(d => d.MaNccNavigation)
                    .WithMany(p => p.Sanpham)
                    .HasForeignKey(d => d.MaNcc)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_NHACUNGCAP");
            });

            modelBuilder.Entity<Taikhoan>(entity =>
            {
                entity.HasKey(e => e.MaTk)
                    .HasName("PK__TAIKHOAN__2725007046CDCD7C");

                entity.ToTable("TAIKHOAN");

                entity.Property(e => e.MaTk).HasColumnName("MaTK");

                entity.Property(e => e.MatKhau)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.SoDienThoai)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Taikhoan)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK_TAIKHOAN");
            });

            modelBuilder.Entity<Voucher>(entity =>
            {
                entity.HasKey(e => e.MaVoucher)
                    .HasName("PK__VOUCHER__0AAC5B11B192AA6B");

                entity.ToTable("VOUCHER");

                entity.Property(e => e.NgayBatDau).HasColumnType("date");

                entity.Property(e => e.NgayKetThuc).HasColumnType("date");

                entity.Property(e => e.TenVoucher)
                    .IsRequired()
                    .HasMaxLength(40);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
