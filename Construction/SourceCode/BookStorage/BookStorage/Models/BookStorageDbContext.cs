namespace BookStorage.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class BookStorageDbContext : DbContext
    {
        public BookStorageDbContext()
            : base("name=BookStorageDbContext")
        {
        }

        public virtual DbSet<Book> Books { get; set; }
        public virtual DbSet<BookCategory> BookCategories { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<GoodsIssue> GoodsIssues { get; set; }
        public virtual DbSet<GoodsIssueInfo> GoodsIssueInfoes { get; set; }
        public virtual DbSet<GoodsReceipt> GoodsReceipts { get; set; }
        public virtual DbSet<GoodsReceiptInfo> GoodsReceiptInfoes { get; set; }
        public virtual DbSet<Supplier> Suppliers { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Book>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Image)
                .IsUnicode(false);

            modelBuilder.Entity<Book>()
                .Property(e => e.Price)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Customer>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<GoodsIssue>()
                .Property(e => e.Debit)
                .IsUnicode(false);

            modelBuilder.Entity<GoodsIssue>()
                .Property(e => e.Credit)
                .IsUnicode(false);

            modelBuilder.Entity<GoodsIssue>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<GoodsIssueInfo>()
                .Property(e => e.TotalPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<GoodsReceipt>()
                .Property(e => e.Debit)
                .IsUnicode(false);

            modelBuilder.Entity<GoodsReceipt>()
                .Property(e => e.Credit)
                .IsUnicode(false);

            modelBuilder.Entity<GoodsReceipt>()
                .Property(e => e.Code)
                .IsUnicode(false);

            modelBuilder.Entity<GoodsReceiptInfo>()
                .Property(e => e.TotalPrice)
                .HasPrecision(18, 0);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Username)
                .IsUnicode(false);

            modelBuilder.Entity<User>()
                .Property(e => e.Password)
                .IsUnicode(false);
        }
    }
}
