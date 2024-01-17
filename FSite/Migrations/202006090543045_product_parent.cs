namespace FSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product_parent : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ProductCategories", name: "ProductCategory_Id", newName: "ParentId");
            RenameIndex(table: "dbo.ProductCategories", name: "IX_ProductCategory_Id", newName: "IX_ParentId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.ProductCategories", name: "IX_ParentId", newName: "IX_ProductCategory_Id");
            RenameColumn(table: "dbo.ProductCategories", name: "ParentId", newName: "ProductCategory_Id");
        }
    }
}
