namespace FSite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class product_sku : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Sku", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Sku");
        }
    }
}
