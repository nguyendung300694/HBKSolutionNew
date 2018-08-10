namespace HBKSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblProduct", "Price", c => c.Int(nullable: false));
            AlterColumn("dbo.tblProduct", "View", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblProduct", "View", c => c.Int());
            AlterColumn("dbo.tblProduct", "Price", c => c.Int());
        }
    }
}
