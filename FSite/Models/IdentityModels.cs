using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using FSite.Models.Data;
using FSite.Models.Data.Attributes;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FSite.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Add(new DecimalPrecisionAttributeConvention());
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<PostCategory> PostCategories { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<PostItem> PostItems { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogCategory> BlogCategories { get; set; }
        public DbSet<BlogCategoryItem> BlogCategoryItems { get; set; }
        public DbSet<BlogItem> BlogItems { get; set; }
      
        public DbSet<FaqCategory> FaqCategories { get; set; }
        public DbSet<FaqCategoryItem> FaqCategoryItems { get; set; }
        public DbSet<Faq> Faqs { get; set; }


        public DbSet<Service> Services { get; set; }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
        public DbSet<ServiceCategoryItem> ServiceCategoryItems { get; set; }

        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductCategoryItem> ProductCategoryItems { get; set; }

        public DbSet<ProductBrand> ProductBrands { get; set; }
        public DbSet<ProductItem> ProductItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ServiceItem> ServiceItems { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Content> Contents { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderItem> SliderItems { get; set; }

    }
}