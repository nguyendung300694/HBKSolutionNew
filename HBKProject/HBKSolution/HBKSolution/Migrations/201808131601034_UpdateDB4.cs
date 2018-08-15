namespace HBKSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB4 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.tblProductCategory", "FolderName", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropColumn("dbo.tblProductCategory", "FolderName");
        }
    }
}
