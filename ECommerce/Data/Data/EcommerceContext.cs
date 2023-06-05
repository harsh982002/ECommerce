using Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Data.Data;

public partial class EcommerceContext : DbContext
{
    public EcommerceContext()
    {
    }

    public EcommerceContext(DbContextOptions<EcommerceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblCart> TblCarts { get; set; }

    public virtual DbSet<TblCategory> TblCategories { get; set; }

    public virtual DbSet<TblCity> TblCities { get; set; }

    public virtual DbSet<TblCompany> TblCompanies { get; set; }

    public virtual DbSet<TblCountry> TblCountries { get; set; }

    public virtual DbSet<TblOrder> TblOrders { get; set; }

    public virtual DbSet<TblPaymentMethod> TblPaymentMethods { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblProductMedia> TblProductMedias { get; set; }

    public virtual DbSet<TblProductRating> TblProductRatings { get; set; }

    public virtual DbSet<TblProductReview> TblProductReviews { get; set; }

    public virtual DbSet<TblReviewMedia> TblReviewMedias { get; set; }

    public virtual DbSet<TblSubCategory> TblSubCategories { get; set; }

    public virtual DbSet<TblSupplier> TblSuppliers { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblUserAddress> TblUserAddresses { get; set; }

    public virtual DbSet<TblUserRole> TblUserRoles { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=PCA118\\SQL2017;DataBase=ECommerce;User ID=sa;Password=Tatva@123;encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblCart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__tblCarts__51BCD7B7EAD2D6AA");

            entity.ToTable("tblCarts");

            entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");
            entity.Property(e => e.Removed).HasDefaultValueSql("((0))");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(12, 2)");

            entity.HasOne(d => d.Product).WithMany(p => p.TblCarts)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__tblCarts__Produc__1F98B2C1");

            entity.HasOne(d => d.User).WithMany(p => p.TblCarts)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__tblCarts__UserId__1EA48E88");
        });

        modelBuilder.Entity<TblCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__tblCateg__19093A0BB6ABBF48");

            entity.ToTable("tblCategories");

            entity.Property(e => e.CategoryName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<TblCity>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__tblCitie__F2D21B76BFA11F34");

            entity.ToTable("tblCities");

            entity.Property(e => e.CityName)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Country).WithMany(p => p.TblCities)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__tblCities__Count__5070F446");
        });

        modelBuilder.Entity<TblCompany>(entity =>
        {
            entity.HasKey(e => e.CompanyId).HasName("PK__tblCompa__2D971CAC6643181F");

            entity.ToTable("tblCompanies");

            entity.Property(e => e.CompanyAddress)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.CompanyName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ContactNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Country).WithMany(p => p.TblCompanies)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__tblCompan__Count__5BE2A6F2");
        });

        modelBuilder.Entity<TblCountry>(entity =>
        {
            entity.HasKey(e => e.CountryId).HasName("PK__tblCount__10D1609FE69226FA");

            entity.ToTable("tblCountries");

            entity.Property(e => e.CountryId).ValueGeneratedOnAdd();
            entity.Property(e => e.CountryName)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TblOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__tblOrder__C3905BCF68166D1A");

            entity.ToTable("tblOrders");

            entity.Property(e => e.CancledAt).HasColumnType("datetime");
            entity.Property(e => e.OrderAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Quantity).HasDefaultValueSql("((1))");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");

            entity.HasOne(d => d.PaymentMethodNavigation).WithMany(p => p.TblOrders)
                .HasForeignKey(d => d.PaymentMethod)
                .HasConstraintName("FK__tblOrders__Payme__30C33EC3");

            entity.HasOne(d => d.Product).WithMany(p => p.TblOrders)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__tblOrders__Produ__2DE6D218");

            entity.HasOne(d => d.UserAddressNavigation).WithMany(p => p.TblOrders)
                .HasForeignKey(d => d.UserAddress)
                .HasConstraintName("FK__tblOrders__UserA__31B762FC");

            entity.HasOne(d => d.User).WithMany(p => p.TblOrders)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__tblOrders__UserI__2EDAF651");
        });

        modelBuilder.Entity<TblPaymentMethod>(entity =>
        {
            entity.HasKey(e => e.MethodId).HasName("PK__tblPayme__FC68185184E0B798");

            entity.ToTable("tblPaymentMethods");

            entity.Property(e => e.MethodId).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.MethodName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__tblProdu__B40CC6CD80E5A209");

            entity.ToTable("tblProducts");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Discount)
                .HasDefaultValueSql("((0))")
                .HasColumnType("decimal(18, 0)");
            entity.Property(e => e.IsFree).HasDefaultValueSql("((0))");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ProductName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StokeKeepingUnit)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.TblProducts)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__tblProduc__Categ__76969D2E");

            entity.HasOne(d => d.Company).WithMany(p => p.TblProducts)
                .HasForeignKey(d => d.CompanyId)
                .HasConstraintName("FK__tblProduc__Compa__787EE5A0");

            entity.HasOne(d => d.SubCategory).WithMany(p => p.TblProducts)
                .HasForeignKey(d => d.SubCategoryid)
                .HasConstraintName("FK__tblProduc__SubCa__4E53A1AA");

            entity.HasOne(d => d.Supplier).WithMany(p => p.TblProducts)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblProduc__Suppl__607251E5");
        });

        modelBuilder.Entity<TblProductMedia>(entity =>
        {
            entity.HasKey(e => e.MediaId).HasName("PK__tblProdu__B2C2B5CFC9AA24E0");

            entity.ToTable("tblProductMedias");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Default).HasDefaultValueSql("((0))");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.MediaName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.MediaPath)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.MediaType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Product).WithMany(p => p.TblProductMedia)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__tblProduc__Produ__0C85DE4D");
        });

        modelBuilder.Entity<TblProductRating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("PK__tblProdu__FCCDF87C654B131C");

            entity.ToTable("tblProductRatings");

            entity.Property(e => e.RatingId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Rating).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Product).WithMany(p => p.TblProductRatings)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__tblProduc__Produ__114A936A");

            entity.HasOne(d => d.User).WithMany(p => p.TblProductRatings)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__tblProduc__UserI__10566F31");
        });

        modelBuilder.Entity<TblProductReview>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__tblProdu__74BC79CE3CF5598F");

            entity.ToTable("tblProductReviews");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Product).WithMany(p => p.TblProductReviews)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK__tblProduc__Produ__160F4887");

            entity.HasOne(d => d.User).WithMany(p => p.TblProductReviews)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__tblProduc__UserI__151B244E");
        });

        modelBuilder.Entity<TblReviewMedia>(entity =>
        {
            entity.HasKey(e => e.ReviewMediaId).HasName("PK__tblRevie__5C018B1C090B4559");

            entity.ToTable("tblReviewMedias");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.ReviewMediaName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ReviewMediaPath)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ReviewMediaType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Review).WithMany(p => p.TblReviewMedia)
                .HasForeignKey(d => d.ReviewId)
                .HasConstraintName("FK__tblReview__Revie__19DFD96B");
        });

        modelBuilder.Entity<TblSubCategory>(entity =>
        {
            entity.HasKey(e => e.SubcategoryId).HasName("PK__tblSubCa__9C4E705DF3903820");

            entity.ToTable("tblSubCategories");

            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.SubCategoryName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Category).WithMany(p => p.TblSubCategories)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK__tblSubCat__Categ__3C34F16F");
        });

        modelBuilder.Entity<TblSupplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__tblSuppl__4BE666B49CB09305");

            entity.ToTable("tblSuppliers");

            entity.Property(e => e.ContactNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.SupplierName)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.Company).WithMany(p => p.TblSuppliers)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__tblSuppli__Compa__6166761E");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__tblUsers__1788CC4CAAD0F78F");

            entity.ToTable("tblUsers");

            entity.Property(e => e.Avatar).IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Firstname)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.Lastname)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Role).HasDefaultValueSql("((1))");
            entity.Property(e => e.Status).HasDefaultValueSql("((1))");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.City).WithMany(p => p.TblUsers)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__tblUsers__CityId__5812160E");

            entity.HasOne(d => d.Country).WithMany(p => p.TblUsers)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__tblUsers__Countr__571DF1D5");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.TblUsers)
                .HasForeignKey(d => d.Role)
                .HasConstraintName("FK__tblUsers__Role__5441852A");
        });

        modelBuilder.Entity<TblUserAddress>(entity =>
        {
            entity.HasKey(e => e.AddressId).HasName("PK__tblUserA__091C2AFB74157F0B");

            entity.ToTable("tblUserAddress");

            entity.Property(e => e.AddressLine1)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.AddressLine2)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");

            entity.HasOne(d => d.City).WithMany(p => p.TblUserAddresses)
                .HasForeignKey(d => d.CityId)
                .HasConstraintName("FK__tblUserAd__CityI__2A164134");

            entity.HasOne(d => d.Country).WithMany(p => p.TblUserAddresses)
                .HasForeignKey(d => d.CountryId)
                .HasConstraintName("FK__tblUserAd__Count__29221CFB");

            entity.HasOne(d => d.User).WithMany(p => p.TblUserAddresses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__tblUserAd__UserI__282DF8C2");
        });

        modelBuilder.Entity<TblUserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__tblUserR__8AFACE1A1F7EC887");

            entity.ToTable("tblUserRoles");

            entity.Property(e => e.RoleId).ValueGeneratedOnAdd();
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.DeletedAt).HasColumnType("datetime");
            entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");
            entity.Property(e => e.RoleName)
                .HasMaxLength(16)
                .IsUnicode(false);
            entity.Property(e => e.UpdatedAt).HasColumnType("datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
