namespace MSIdentity.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Id : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Product", "CategoryId", "dbo.Category");
            DropIndex("dbo.Product", new[] { "CategoryId" });
            DropPrimaryKey("dbo.Category");
            DropPrimaryKey("dbo.Product");
            AlterColumn("dbo.Category", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Product", "Id", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.Product", "CategoryId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Category", "Id");
            AddPrimaryKey("dbo.Product", "Id");
            CreateIndex("dbo.Product", "CategoryId");
            AddForeignKey("dbo.Product", "CategoryId", "dbo.Category", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Product", "CategoryId", "dbo.Category");
            DropIndex("dbo.Product", new[] { "CategoryId" });
            DropPrimaryKey("dbo.Product");
            DropPrimaryKey("dbo.Category");
            AlterColumn("dbo.Product", "CategoryId", c => c.String(maxLength: 128));
            AlterColumn("dbo.Product", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Category", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Product", "Id");
            AddPrimaryKey("dbo.Category", "Id");
            CreateIndex("dbo.Product", "CategoryId");
            AddForeignKey("dbo.Product", "CategoryId", "dbo.Category", "Id");
        }
    }
}
