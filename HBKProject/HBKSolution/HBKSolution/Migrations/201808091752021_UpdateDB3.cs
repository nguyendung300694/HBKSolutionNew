namespace HBKSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateDB3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.tblProduct", "Price", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.tblProduct", "Price", c => c.String(maxLength: 50));
        }
    }
}
