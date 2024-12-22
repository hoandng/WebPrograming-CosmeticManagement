using System;
using Microsoft.EntityFrameworkCore;
using QuanLyBanMyPham.Models;

namespace QuanLyBanMyPham.Data
{
    public partial class QuanLyBanMyPhamContext : DbContext
    {
        public QuanLyBanMyPhamContext()
        {
        }

        public QuanLyBanMyPhamContext(DbContextOptions<QuanLyBanMyPhamContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
            => optionsBuilder.UseSqlServer("Data Source=HOANDANG\\THANHDANGHOAN;Initial Catalog=QuanLyBanMyPham;Persist Security Info=True;User ID=sa;Password=123456789;TrustServerCertificate=True");

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.CategoryId).HasName("PK__categori__D54EE9B4E3519ED9");
                entity.ToTable("categories");

                entity.Property(e => e.CategoryId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("category_id");
                entity.Property(e => e.CategoryName)
                    .HasMaxLength(100)
                    .IsUnicode(true)
                    .HasColumnName("category_name");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.OrderId).HasName("PK__orders__465962296371DFDF");
                entity.ToTable("orders");

                entity.Property(e => e.OrderId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("order_id");
                entity.Property(e => e.OrderDate).HasColumnName("order_date");
                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(true)
                    .HasColumnName("status");
                entity.Property(e => e.TotalAmount)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("total_amount");
                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__orders__user_id__45F365D3");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.OrderDetailId).HasName("PK__order_de__3C5A4080B706BC0A");
                entity.ToTable("order_details");

                entity.Property(e => e.OrderDetailId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("order_detail_id");
                entity.Property(e => e.OrderId).HasColumnName("order_id");
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");
                entity.Property(e => e.ProductId).HasColumnName("product_id");
                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__order_det__order__48CFD27E");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__order_det__produ__49C3F6B7");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.ProductId).HasName("PK__products__47027DF58063257B");
                entity.ToTable("products");

                entity.Property(e => e.ProductId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("product_id");
                entity.Property(e => e.CategoryId).HasColumnName("category_id");
                entity.Property(e => e.Price)
                    .HasColumnType("decimal(10, 2)")
                    .HasColumnName("price");
                entity.Property(e => e.ProductName)
                    .HasMaxLength(100)
                    .IsUnicode(true)
                    .HasColumnName("product_name");
                entity.Property(e => e.Quantity).HasColumnName("quantity");
                entity.Property(e => e.SupplierId).HasColumnName("supplier_id");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__products__catego__4222D4EF");

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SupplierId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__products__suppli__4316F928");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.RoleId).HasName("PK__roles__760965CC654D6F2F");
                entity.ToTable("roles");

                entity.HasIndex(e => e.RoleName, "UQ__roles__783254B1D51AC7E9").IsUnique();

                entity.Property(e => e.RoleId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("role_id");
                entity.Property(e => e.RoleName)
                    .HasMaxLength(50)
                    .IsUnicode(true)
                    .HasColumnName("role_name");
            });

            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.HasKey(e => e.SupplierId).HasName("PK__supplier__6EE594E8C059C1B5");
                entity.ToTable("suppliers");

                entity.Property(e => e.SupplierId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("supplier_id");
                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(true)
                    .HasColumnName("address");
                entity.Property(e => e.ContactName)
                    .HasMaxLength(100)
                    .IsUnicode(true)
                    .HasColumnName("contact_name");
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(true)
                    .HasColumnName("email");
                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(true)
                    .HasColumnName("phone");
                entity.Property(e => e.SupplierName)
                    .HasMaxLength(100)
                    .IsUnicode(true)
                    .HasColumnName("supplier_name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId).HasName("PK__users__B9BE370FC797F05D");
                entity.ToTable("users");

                entity.HasIndex(e => e.Username, "UQ__users__F3DBC572462868DE").IsUnique();

                entity.Property(e => e.UserId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("user_id");
                entity.Property(e => e.Email)
                    .HasMaxLength(100)
                    .IsUnicode(true)
                    .HasColumnName("email");
                entity.Property(e => e.FullName)
                    .HasMaxLength(100)
                    .IsUnicode(true)
                    .HasColumnName("full_name");
                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(true)
                    .HasColumnName("password");
                entity.Property(e => e.Phone)
                    .HasMaxLength(20)
                    .IsUnicode(true)
                    .HasColumnName("phone");
                entity.Property(e => e.RoleId).HasColumnName("role_id");
                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .IsUnicode(true)
                    .HasColumnName("username");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.SetNull)
                    .HasConstraintName("FK__users__role_id__3B75D760");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
