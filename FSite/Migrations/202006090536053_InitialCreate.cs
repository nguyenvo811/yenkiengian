namespace FSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BlogCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 150),
                        Title = c.String(nullable: false, maxLength: 150),
                        Title2 = c.String(maxLength: 100),
                        MetaTitle = c.String(maxLength: 100),
                        MetaKeyword = c.String(maxLength: 2000),
                        MetaDescription = c.String(maxLength: 2000),
                        Description = c.String(maxLength: 2000),
                        Link = c.String(maxLength: 250),
                        Index = c.Int(),
                        IsActive = c.Boolean(),
                        IsFeature = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.BlogCategoryItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(maxLength: 100),
                        ImageUrl = c.String(nullable: false, maxLength: 250),
                        Index = c.Int(),
                        IsActive = c.Boolean(),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogCategories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.BlogItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(maxLength: 100),
                        ImageUrl = c.String(nullable: false, maxLength: 250),
                        Index = c.Int(),
                        IsActive = c.Boolean(),
                        BlogId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Blogs", t => t.BlogId)
                .Index(t => t.BlogId);
            
            CreateTable(
                "dbo.Blogs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 200),
                        Title = c.String(nullable: false, maxLength: 200),
                        Title2 = c.String(maxLength: 200),
                        MetaTitle = c.String(maxLength: 200),
                        MetaKeyword = c.String(maxLength: 2000),
                        MetaDescription = c.String(maxLength: 2000),
                        Description = c.String(maxLength: 2000),
                        CategoryId = c.Int(),
                        Detail = c.String(),
                        Link = c.String(maxLength: 250),
                        ImageUrl = c.String(maxLength: 250),
                        IsActive = c.Boolean(),
                        IsFeature = c.Boolean(),
                        Viewed = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedById = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedById = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.BlogCategories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Contents",
                c => new
                    {
                        Id = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Code = c.String(maxLength: 50),
                        Title = c.String(nullable: false, maxLength: 500),
                        Note = c.String(),
                        MetaType = c.String(maxLength: 100),
                        Meta = c.String(),
                        Status = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedById = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedById = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FaqCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 150),
                        Title = c.String(nullable: false, maxLength: 150),
                        Title2 = c.String(maxLength: 100),
                        MetaTitle = c.String(maxLength: 100),
                        MetaKeyword = c.String(maxLength: 2000),
                        MetaDescription = c.String(maxLength: 2000),
                        Description = c.String(maxLength: 2000),
                        Link = c.String(maxLength: 250),
                        Index = c.Int(),
                        IsActive = c.Boolean(),
                        IsFeature = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FaqCategoryItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(maxLength: 100),
                        ImageUrl = c.String(nullable: false, maxLength: 250),
                        Index = c.Int(),
                        IsActive = c.Boolean(),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FaqCategories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Faqs",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 200),
                        Title = c.String(nullable: false, maxLength: 200),
                        Title2 = c.String(maxLength: 200),
                        MetaTitle = c.String(maxLength: 200),
                        MetaKeyword = c.String(maxLength: 2000),
                        MetaDescription = c.String(maxLength: 2000),
                        Description = c.String(maxLength: 2000),
                        CategoryId = c.Int(),
                        Detail = c.String(),
                        Link = c.String(maxLength: 250),
                        IsActive = c.Boolean(),
                        IsFeature = c.Boolean(),
                        Viewed = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedById = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedById = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FaqCategories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.Menus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 100),
                        Title = c.String(nullable: false, maxLength: 100),
                        Icon = c.String(maxLength: 250),
                        Link = c.String(maxLength: 250),
                        Description = c.String(maxLength: 2000),
                        IsActive = c.Boolean(),
                        ParentId = c.Int(),
                        Index = c.Int(),
                        TypeKey = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedById = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedById = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Menus", t => t.ParentId)
                .Index(t => t.ParentId);
            
            CreateTable(
                "dbo.PostCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(maxLength: 150),
                        Title = c.String(nullable: false, maxLength: 150),
                        Title2 = c.String(maxLength: 100),
                        MetaTitle = c.String(maxLength: 100),
                        MetaKeyword = c.String(maxLength: 2000),
                        MetaDescription = c.String(maxLength: 2000),
                        Description = c.String(maxLength: 2000),
                        Link = c.String(maxLength: 250),
                        Index = c.Int(),
                        IsActive = c.Boolean(),
                        IsFeature = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PostItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(maxLength: 100),
                        ImageUrl = c.String(nullable: false, maxLength: 250),
                        Index = c.Int(),
                        IsActive = c.Boolean(),
                        PostId = c.Long(),
                        Post_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.Post_Id)
                .Index(t => t.Post_Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 200),
                        Title = c.String(nullable: false, maxLength: 200),
                        Title2 = c.String(maxLength: 200),
                        MetaTitle = c.String(maxLength: 200),
                        MetaKeyword = c.String(maxLength: 2000),
                        MetaDescription = c.String(maxLength: 2000),
                        Description = c.String(maxLength: 2000),
                        CategoryId = c.Int(),
                        Detail = c.String(),
                        Link = c.String(maxLength: 250),
                        ImageUrl = c.String(maxLength: 250),
                        IsActive = c.Boolean(),
                        IsFeature = c.Boolean(),
                        Viewed = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedById = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedById = c.String(),
                        Template = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PostCategories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.ProductBrands",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 150),
                        Title = c.String(nullable: false, maxLength: 150),
                        Title2 = c.String(maxLength: 100),
                        MetaTitle = c.String(maxLength: 100),
                        MetaKeyword = c.String(maxLength: 2000),
                        MetaDescription = c.String(maxLength: 2000),
                        Description = c.String(maxLength: 2000),
                        Link = c.String(maxLength: 250),
                        ImageUrl = c.String(maxLength: 250),
                        Index = c.Int(),
                        IsActive = c.Boolean(),
                        IsFeature = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 150),
                        Title = c.String(nullable: false, maxLength: 150),
                        Title2 = c.String(maxLength: 100),
                        MetaTitle = c.String(maxLength: 100),
                        MetaKeyword = c.String(maxLength: 2000),
                        MetaDescription = c.String(maxLength: 2000),
                        Description = c.String(maxLength: 2000),
                        Link = c.String(maxLength: 250),
                        Index = c.Int(),
                        IsActive = c.Boolean(),
                        IsFeature = c.Boolean(),
                        ProductCategory_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategories", t => t.ProductCategory_Id)
                .Index(t => t.ProductCategory_Id);
            
            CreateTable(
                "dbo.ProductCategoryItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(maxLength: 100),
                        ImageUrl = c.String(nullable: false, maxLength: 250),
                        Index = c.Int(),
                        IsActive = c.Boolean(),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductCategories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.ProductItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(maxLength: 100),
                        ImageUrl = c.String(nullable: false, maxLength: 250),
                        Index = c.Int(),
                        IsActive = c.Boolean(),
                        ProductId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId)
                .Index(t => t.ProductId);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 200),
                        Title = c.String(nullable: false, maxLength: 200),
                        Title2 = c.String(maxLength: 200),
                        MetaTitle = c.String(maxLength: 200),
                        MetaKeyword = c.String(maxLength: 2000),
                        MetaDescription = c.String(maxLength: 2000),
                        Description = c.String(maxLength: 2000),
                        Price = c.Decimal(precision: 18, scale: 2),
                        PriceDiscount = c.Decimal(precision: 18, scale: 2),
                        DiscountFromDate = c.DateTime(),
                        DiscountToDate = c.DateTime(),
                        CategoryId = c.Int(),
                        BrandId = c.Int(),
                        Detail = c.String(),
                        Link = c.String(maxLength: 250),
                        ImageUrl = c.String(maxLength: 250),
                        IsActive = c.Boolean(),
                        IsFeature = c.Boolean(),
                        Viewed = c.Int(),
                        Meta = c.String(),
                        Meta_Template = c.String(),
                        CreatedDate = c.DateTime(),
                        CreatedById = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedById = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ProductBrands", t => t.BrandId)
                .ForeignKey("dbo.ProductCategories", t => t.CategoryId)
                .Index(t => t.CategoryId)
                .Index(t => t.BrandId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.ServiceCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 150),
                        Title = c.String(nullable: false, maxLength: 150),
                        Title2 = c.String(maxLength: 100),
                        MetaTitle = c.String(maxLength: 100),
                        MetaKeyword = c.String(maxLength: 2000),
                        MetaDescription = c.String(maxLength: 2000),
                        Description = c.String(maxLength: 2000),
                        Link = c.String(maxLength: 250),
                        Index = c.Int(),
                        IsActive = c.Boolean(),
                        IsFeature = c.Boolean(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ServiceCategoryItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(maxLength: 100),
                        ImageUrl = c.String(nullable: false, maxLength: 250),
                        Index = c.Int(),
                        IsActive = c.Boolean(),
                        CategoryId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceCategories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.ServiceItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(maxLength: 100),
                        ImageUrl = c.String(nullable: false, maxLength: 250),
                        Index = c.Int(),
                        IsActive = c.Boolean(),
                        ServiceId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Services", t => t.ServiceId)
                .Index(t => t.ServiceId);
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 200),
                        Title = c.String(nullable: false, maxLength: 200),
                        Title2 = c.String(maxLength: 200),
                        MetaTitle = c.String(maxLength: 200),
                        MetaKeyword = c.String(maxLength: 2000),
                        MetaDescription = c.String(maxLength: 2000),
                        Description = c.String(maxLength: 2000),
                        CategoryId = c.Int(),
                        Detail = c.String(),
                        Link = c.String(maxLength: 250),
                        ImageUrl = c.String(maxLength: 250),
                        IsActive = c.Boolean(),
                        IsFeature = c.Boolean(),
                        Viewed = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedById = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedById = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ServiceCategories", t => t.CategoryId)
                .Index(t => t.CategoryId);
            
            CreateTable(
                "dbo.SliderItems",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Title = c.String(maxLength: 100),
                        ImageUrl = c.String(nullable: false, maxLength: 250),
                        Index = c.Int(),
                        IsActive = c.Boolean(),
                        SliderId = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sliders", t => t.SliderId)
                .Index(t => t.SliderId);
            
            CreateTable(
                "dbo.Sliders",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Key = c.String(nullable: false, maxLength: 200),
                        Title = c.String(nullable: false, maxLength: 200),
                        Title2 = c.String(maxLength: 200),
                        MetaTitle = c.String(maxLength: 200),
                        MetaKeyword = c.String(maxLength: 2000),
                        MetaDescription = c.String(maxLength: 2000),
                        Description = c.String(maxLength: 2000),
                        Type = c.Int(),
                        Detail = c.String(),
                        Link = c.String(maxLength: 250),
                        ImageUrl = c.String(maxLength: 250),
                        IsActive = c.Boolean(),
                        IsFeature = c.Boolean(),
                        Viewed = c.Int(),
                        CreatedDate = c.DateTime(),
                        CreatedById = c.String(),
                        ModifiedDate = c.DateTime(),
                        ModifiedById = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.SliderItems", "SliderId", "dbo.Sliders");
            DropForeignKey("dbo.ServiceItems", "ServiceId", "dbo.Services");
            DropForeignKey("dbo.Services", "CategoryId", "dbo.ServiceCategories");
            DropForeignKey("dbo.ServiceCategoryItems", "CategoryId", "dbo.ServiceCategories");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.ProductItems", "ProductId", "dbo.Products");
            DropForeignKey("dbo.Products", "CategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.Products", "BrandId", "dbo.ProductBrands");
            DropForeignKey("dbo.ProductCategories", "ProductCategory_Id", "dbo.ProductCategories");
            DropForeignKey("dbo.ProductCategoryItems", "CategoryId", "dbo.ProductCategories");
            DropForeignKey("dbo.PostItems", "Post_Id", "dbo.Posts");
            DropForeignKey("dbo.Posts", "CategoryId", "dbo.PostCategories");
            DropForeignKey("dbo.Menus", "ParentId", "dbo.Menus");
            DropForeignKey("dbo.Faqs", "CategoryId", "dbo.FaqCategories");
            DropForeignKey("dbo.FaqCategoryItems", "CategoryId", "dbo.FaqCategories");
            DropForeignKey("dbo.Blogs", "CategoryId", "dbo.BlogCategories");
            DropForeignKey("dbo.BlogItems", "BlogId", "dbo.Blogs");
            DropForeignKey("dbo.BlogCategoryItems", "CategoryId", "dbo.BlogCategories");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.SliderItems", new[] { "SliderId" });
            DropIndex("dbo.Services", new[] { "CategoryId" });
            DropIndex("dbo.ServiceItems", new[] { "ServiceId" });
            DropIndex("dbo.ServiceCategoryItems", new[] { "CategoryId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Products", new[] { "BrandId" });
            DropIndex("dbo.Products", new[] { "CategoryId" });
            DropIndex("dbo.ProductItems", new[] { "ProductId" });
            DropIndex("dbo.ProductCategoryItems", new[] { "CategoryId" });
            DropIndex("dbo.ProductCategories", new[] { "ProductCategory_Id" });
            DropIndex("dbo.Posts", new[] { "CategoryId" });
            DropIndex("dbo.PostItems", new[] { "Post_Id" });
            DropIndex("dbo.Menus", new[] { "ParentId" });
            DropIndex("dbo.Faqs", new[] { "CategoryId" });
            DropIndex("dbo.FaqCategoryItems", new[] { "CategoryId" });
            DropIndex("dbo.Blogs", new[] { "CategoryId" });
            DropIndex("dbo.BlogItems", new[] { "BlogId" });
            DropIndex("dbo.BlogCategoryItems", new[] { "CategoryId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Sliders");
            DropTable("dbo.SliderItems");
            DropTable("dbo.Services");
            DropTable("dbo.ServiceItems");
            DropTable("dbo.ServiceCategoryItems");
            DropTable("dbo.ServiceCategories");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.Products");
            DropTable("dbo.ProductItems");
            DropTable("dbo.ProductCategoryItems");
            DropTable("dbo.ProductCategories");
            DropTable("dbo.ProductBrands");
            DropTable("dbo.Posts");
            DropTable("dbo.PostItems");
            DropTable("dbo.PostCategories");
            DropTable("dbo.Menus");
            DropTable("dbo.Faqs");
            DropTable("dbo.FaqCategoryItems");
            DropTable("dbo.FaqCategories");
            DropTable("dbo.Contents");
            DropTable("dbo.Blogs");
            DropTable("dbo.BlogItems");
            DropTable("dbo.BlogCategoryItems");
            DropTable("dbo.BlogCategories");
        }
    }
}
