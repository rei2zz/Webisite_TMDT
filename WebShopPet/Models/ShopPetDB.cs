namespace WebShopPet.Models
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class ShopPetDB : DbContext
    {
        public ShopPetDB()
            : base("name=ShopPetDB")
        {
        }

        public virtual DbSet<BRAND> BRANDs { get; set; }
        public virtual DbSet<CATEGORy> CATEGORIES { get; set; }
        public virtual DbSet<ORDER_DETAILS> ORDER_DETAILS { get; set; }
        public virtual DbSet<ORDER> ORDERS { get; set; }
        public virtual DbSet<PRODUCT> PRODUCTS { get; set; }
        public virtual DbSet<USER> USERS { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BRAND>()
                .Property(e => e.PHONE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<BRAND>()
                .HasMany(e => e.PRODUCTS)
                .WithOptional(e => e.BRAND)
                .HasForeignKey(e => e.BRAND_ID);

            modelBuilder.Entity<CATEGORy>()
                .HasMany(e => e.PRODUCTS)
                .WithOptional(e => e.CATEGORy)
                .HasForeignKey(e => e.CATEGORY_ID);

            modelBuilder.Entity<ORDER>()
                .Property(e => e.PHONE)
                .IsFixedLength()
                .IsUnicode(false);

            modelBuilder.Entity<ORDER>()
                .HasMany(e => e.ORDER_DETAILS)
                .WithOptional(e => e.ORDER)
                .HasForeignKey(e => e.ORDER_ID);

            modelBuilder.Entity<PRODUCT>()
                .HasMany(e => e.ORDER_DETAILS)
                .WithOptional(e => e.PRODUCT);

            modelBuilder.Entity<USER>()
                .HasIndex(p => new { p.EMAIL })
                .IsUnique(true);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.ORDERS)
                .WithOptional(e => e.USER)
                .HasForeignKey(e => e.USER_ID);

            modelBuilder.Entity<USER>()
                .HasMany(e => e.PRODUCTS)
                .WithOptional(e => e.USER)
                .HasForeignKey(e => e.USER_ID);
        }
    }
}
