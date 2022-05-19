using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Project_IPET.Models.EF
{
    public partial class MyProjectContext : DbContext
    {
        public MyProjectContext()
        {
        }

        public MyProjectContext(DbContextOptions<MyProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Banner> Banners { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Coupon> Coupons { get; set; }
        public virtual DbSet<CouponDetail> CouponDetails { get; set; }
        public virtual DbSet<CouponDiscountType> CouponDiscountTypes { get; set; }
        public virtual DbSet<CustomerContact> CustomerContacts { get; set; }
        public virtual DbSet<DeliveryType> DeliveryTypes { get; set; }
        public virtual DbSet<DonationDetail> DonationDetails { get; set; }
        public virtual DbSet<Foundation> Foundations { get; set; }
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<MemberRole> MemberRoles { get; set; }
        public virtual DbSet<MyFavorite> MyFavorites { get; set; }
        public virtual DbSet<NotifiesType> NotifiesTypes { get; set; }
        public virtual DbSet<Notify> Notifies { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<OrderStatus> OrderStatuses { get; set; }
        public virtual DbSet<PaymentType> PaymentTypes { get; set; }
        public virtual DbSet<Pet> Pets { get; set; }
        public virtual DbSet<PetImagePath> PetImagePaths { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostLiked> PostLikeds { get; set; }
        public virtual DbSet<PostType> PostTypes { get; set; }
        public virtual DbSet<PrjConnect> PrjConnects { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductImagePath> ProductImagePaths { get; set; }
        public virtual DbSet<ProjectDetail> ProjectDetails { get; set; }
        public virtual DbSet<Region> Regions { get; set; }
        public virtual DbSet<SubCategory> SubCategories { get; set; }
        public virtual DbSet<TransactionType> TransactionTypes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=MyProject;Integrated Security=True");
//            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_Taiwan_Stroke_CI_AS");

            modelBuilder.Entity<Banner>(entity =>
            {
                entity.Property(e => e.BannerId).HasColumnName("BannerID");

                entity.Property(e => e.BannerImage).IsRequired();

                entity.Property(e => e.BannerName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.StartDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("Brand");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.BrandName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.BannedContent).IsRequired();

                entity.Property(e => e.CommentContent).HasMaxLength(100);

                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.OrderDetail)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.OrderDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_OrderDetails");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Products");
            });

            modelBuilder.Entity<Coupon>(entity =>
            {
                entity.Property(e => e.CouponId).HasColumnName("CouponID");

                entity.Property(e => e.CouponCode)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CouponDiscountTypeId).HasColumnName("CouponDiscountTypeID");

                entity.Property(e => e.CouponEndDate).HasColumnType("datetime");

                entity.Property(e => e.CouponName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CouponStartDate).HasColumnType("datetime");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.CouponDiscountType)
                    .WithMany(p => p.Coupons)
                    .HasForeignKey(d => d.CouponDiscountTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Coupons_CouponDiscountType");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Coupons)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Coupons_Products");
            });

            modelBuilder.Entity<CouponDetail>(entity =>
            {
                entity.HasKey(e => e.CouponDetailsId);

                entity.Property(e => e.CouponDetailsId).HasColumnName("CouponDetailsID");

                entity.Property(e => e.CouponId).HasColumnName("CouponID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.CouponDetails)
                    .HasForeignKey(d => d.CouponId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CouponDetails_Coupons");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.CouponDetails)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CouponDetails_Members");
            });

            modelBuilder.Entity<CouponDiscountType>(entity =>
            {
                entity.ToTable("CouponDiscountType");

                entity.Property(e => e.CouponDiscountTypeId).HasColumnName("CouponDiscountTypeID");

                entity.Property(e => e.CouponDiscountTypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<CustomerContact>(entity =>
            {
                entity.HasKey(e => e.ContactId);

                entity.ToTable("CustomerContact");

                entity.Property(e => e.ContactId).HasColumnName("ContactID");

                entity.Property(e => e.ContactDate).HasColumnType("date");

                entity.Property(e => e.ContactMail).HasMaxLength(50);

                entity.Property(e => e.ContactName).HasMaxLength(50);

                entity.Property(e => e.ContactSubject).HasMaxLength(100);
            });

            modelBuilder.Entity<DeliveryType>(entity =>
            {
                entity.ToTable("DeliveryType");

                entity.Property(e => e.DeliveryTypeId).HasColumnName("DeliveryTypeID");

                entity.Property(e => e.DeliveryTypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<DonationDetail>(entity =>
            {
                entity.HasKey(e => e.DonationlDetailId);

                entity.Property(e => e.DonationlDetailId).HasColumnName("DonationlDetailID");

                entity.Property(e => e.FoundationId).HasColumnName("FoundationID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Foundation)
                    .WithMany(p => p.DonationDetails)
                    .HasForeignKey(d => d.FoundationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DonationDetails_Foundation");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.DonationDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DonationDetails_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.DonationDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DonationDetails_Products");
            });

            modelBuilder.Entity<Foundation>(entity =>
            {
                entity.ToTable("Foundation");

                entity.Property(e => e.FoundationId).HasColumnName("FoundationID");

                entity.Property(e => e.FoundationAddress).HasMaxLength(50);

                entity.Property(e => e.FoundationName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Member>(entity =>
            {
                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.Avatar).HasMaxLength(150);

                entity.Property(e => e.BirthDate).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.RegisteredDate).HasColumnType("datetime");

                entity.Property(e => e.RoleId)
                    .HasColumnName("RoleID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserId)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("UserID");

                entity.HasOne(d => d.Region)
                    .WithMany(p => p.Members)
                    .HasForeignKey(d => d.RegionId)
                    .HasConstraintName("FK_Members_Region");
            });

            modelBuilder.Entity<MemberRole>(entity =>
            {
                entity.HasKey(e => e.RoleId)
                    .HasName("PK_LevelType");

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<MyFavorite>(entity =>
            {
                entity.HasKey(e => e.FavoriteId)
                    .HasName("PK_Favorite");

                entity.Property(e => e.FavoriteId).HasColumnName("FavoriteID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.MyFavorites)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MyFavorites_Members");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.MyFavorites)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MyFavorites_Products");
            });

            modelBuilder.Entity<NotifiesType>(entity =>
            {
                entity.HasKey(e => e.NotifyTypeId)
                    .HasName("PK_NotifyType");

                entity.ToTable("NotifiesType");

                entity.Property(e => e.NotifyTypeId).HasColumnName("NotifyTypeID");

                entity.Property(e => e.NotifyName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Notify>(entity =>
            {
                entity.Property(e => e.NotifyId).HasColumnName("NotifyID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.NotifyContent)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.NotifyTypeId).HasColumnName("NotifyTypeID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Notifies)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notifies_Members");

                entity.HasOne(d => d.NotifyType)
                    .WithMany(p => p.Notifies)
                    .HasForeignKey(d => d.NotifyTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notifies_NotifiesType");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.CouponId).HasColumnName("CouponID");

                entity.Property(e => e.DeliveryTypeId).HasColumnName("DeliveryTypeID");

                entity.Property(e => e.Frieght).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.OrderName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OrderPhone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.OrderStatusId).HasColumnName("OrderStatusID");

                entity.Property(e => e.PayDate).HasMaxLength(50);

                entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentTypeID");

                entity.Property(e => e.RequiredDate).HasMaxLength(50);

                entity.Property(e => e.ShippedDate).HasMaxLength(50);

                entity.Property(e => e.ShippedTo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.TransactionTypeId).HasColumnName("TransactionTypeID");

                entity.HasOne(d => d.Coupon)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.CouponId)
                    .HasConstraintName("FK_Orders_Coupons");

                entity.HasOne(d => d.DeliveryType)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.DeliveryTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_DeliveryType1");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_Members");

                entity.HasOne(d => d.OrderStatus)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_OrderStatus");

                entity.HasOne(d => d.PaymentType)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PaymentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_PaymentType");

                entity.HasOne(d => d.TransactionType)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.TransactionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Orders_TransactionType");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.Property(e => e.OrderDetailId).HasColumnName("OrderDetailID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Orders");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderDetails_Products");
            });

            modelBuilder.Entity<OrderStatus>(entity =>
            {
                entity.ToTable("OrderStatus");

                entity.Property(e => e.OrderStatusId).HasColumnName("OrderStatusID");

                entity.Property(e => e.OrderStatusName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<PaymentType>(entity =>
            {
                entity.ToTable("PaymentType");

                entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentTypeID");

                entity.Property(e => e.PaymentTypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Pet>(entity =>
            {
                entity.Property(e => e.PetId).HasColumnName("PetID");

                entity.Property(e => e.PetAge)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PetCategory)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PetCityId).HasColumnName("PetCityID");

                entity.Property(e => e.PetColor)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PetContact)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PetContactPhone)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PetFix)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PetGender)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PetName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PetRegionId).HasColumnName("PetRegionID");

                entity.Property(e => e.PetSize)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PetVariety)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.PublishedDate)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.PetCity)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.PetCityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pet_Cities");

                entity.HasOne(d => d.PetRegion)
                    .WithMany(p => p.Pets)
                    .HasForeignKey(d => d.PetRegionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pet_Region");
            });

            modelBuilder.Entity<PetImagePath>(entity =>
            {
                entity.ToTable("PetImagePath");

                entity.Property(e => e.PetImagePathId).HasColumnName("PetImagePathID");

                entity.Property(e => e.PetId).HasColumnName("PetID");

                entity.Property(e => e.PetImagePath1).HasColumnName("PetImagePath");

                entity.HasOne(d => d.Pet)
                    .WithMany(p => p.PetImagePaths)
                    .HasForeignKey(d => d.PetId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PetImagePath_Pet");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.Property(e => e.BannedContent).IsRequired();

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.PostTypeId).HasColumnName("PostTypeID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_Members");

                entity.HasOne(d => d.PostType)
                    .WithMany(p => p.Posts)
                    .HasForeignKey(d => d.PostTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Post_PostType");

                entity.HasOne(d => d.ReplyToPostNavigation)
                    .WithMany(p => p.InverseReplyToPostNavigation)
                    .HasForeignKey(d => d.ReplyToPost)
                    .HasConstraintName("FK_Post_Post");
            });

            modelBuilder.Entity<PostLiked>(entity =>
            {
                entity.ToTable("PostLiked");

                entity.Property(e => e.PostLikedId).HasColumnName("PostLikedID");

                entity.Property(e => e.MemberId).HasColumnName("MemberID");

                entity.Property(e => e.PostId).HasColumnName("PostID");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.PostLikeds)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostLiked_Members");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.PostLikeds)
                    .HasForeignKey(d => d.PostId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PostLiked_Post");
            });

            modelBuilder.Entity<PostType>(entity =>
            {
                entity.ToTable("PostType");

                entity.Property(e => e.PostTypeId).HasColumnName("PostTypeID");
            });

            modelBuilder.Entity<PrjConnect>(entity =>
            {
                entity.HasKey(e => e.FId);

                entity.ToTable("PrjConnect");

                entity.Property(e => e.FId).HasColumnName("fId");

                entity.Property(e => e.PrjId).HasColumnName("PrjID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Prj)
                    .WithMany(p => p.PrjConnects)
                    .HasForeignKey(d => d.PrjId)
                    .HasConstraintName("FK_PrjConnect_ProjectDetail");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.PrjConnects)
                    .HasForeignKey(d => d.ProductId)
                    .HasConstraintName("FK_PrjConnect_Products");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.BrandId).HasColumnName("BrandID");

                entity.Property(e => e.CostPrice).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(400);

                entity.Property(e => e.ProductAvailable)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

                entity.Property(e => e.UnitPrice).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.BrandId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Brand");

                entity.HasOne(d => d.SubCategory)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.SubCategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_SubCategories");
            });

            modelBuilder.Entity<ProductImagePath>(entity =>
            {
                entity.ToTable("ProductImagePath");

                entity.Property(e => e.ProductImagePathId).HasColumnName("ProductImagePathID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.ProductImagePaths)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductImagePath_Products");
            });

            modelBuilder.Entity<ProjectDetail>(entity =>
            {
                entity.HasKey(e => e.PrjId);

                entity.ToTable("ProjectDetail");

                entity.Property(e => e.PrjId).HasColumnName("PrjID");

                entity.Property(e => e.Description).HasMaxLength(50);

                entity.Property(e => e.Endtime).HasColumnType("date");

                entity.Property(e => e.FoundationId).HasColumnName("FoundationID");

                entity.Property(e => e.Goal).HasColumnType("money");

                entity.Property(e => e.Starttime).HasColumnType("date");

                entity.Property(e => e.Title).HasMaxLength(50);

                entity.HasOne(d => d.Foundation)
                    .WithMany(p => p.ProjectDetails)
                    .HasForeignKey(d => d.FoundationId)
                    .HasConstraintName("FK_ProjectDetail_Foundation");
            });

            modelBuilder.Entity<Region>(entity =>
            {
                entity.ToTable("Region");

                entity.Property(e => e.RegionId).HasColumnName("RegionID");

                entity.Property(e => e.CityId).HasColumnName("CityID");

                entity.Property(e => e.RegionName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.City)
                    .WithMany(p => p.Regions)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Region_Cities");
            });

            modelBuilder.Entity<SubCategory>(entity =>
            {
                entity.Property(e => e.SubCategoryId).HasColumnName("SubCategoryID");

                entity.Property(e => e.CategoryId).HasColumnName("CategoryID");

                entity.Property(e => e.SubCategoryName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.SubCategories)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubCategories_Categories");
            });

            modelBuilder.Entity<TransactionType>(entity =>
            {
                entity.ToTable("TransactionType");

                entity.Property(e => e.TransactionTypeId).HasColumnName("TransactionTypeID");

                entity.Property(e => e.TransactionTypeName)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
