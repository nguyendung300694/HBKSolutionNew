namespace HBKSolution.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Init_HBKSolutionDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tblProductCategoryExtend",
                c => new
                    {
                        ProductCategoryId = c.Int(nullable: false),
                        FileName = c.String(maxLength: 400),
                        FileSize = c.Int(),
                        FilePath = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.ProductCategoryId)
                .ForeignKey("dbo.tblProductCategory", t => t.ProductCategoryId)
                .Index(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.tblProductCategory",
                c => new
                    {
                        ProductCategoryId = c.Int(nullable: false, identity: true),
                        ProductCategoryName = c.String(nullable: false, maxLength: 200),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.tblProduct",
                c => new
                    {
                        ProductId = c.Int(nullable: false, identity: true),
                        ProductCategoryId = c.Int(nullable: false),
                        ProductName = c.String(nullable: false, maxLength: 200),
                        ProductContent = c.String(maxLength: 4000),
                        CreatedDate = c.DateTime(nullable: false),
                        View = c.Int(),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.tblProductCategory", t => t.ProductCategoryId, cascadeDelete: true)
                .Index(t => t.ProductCategoryId);
            
            CreateTable(
                "dbo.tblProductExtend",
                c => new
                    {
                        ProductId = c.Int(nullable: false),
                        FileName = c.String(maxLength: 400),
                        FileSize = c.Int(),
                        FilePath = c.String(maxLength: 1000),
                    })
                .PrimaryKey(t => t.ProductId)
                .ForeignKey("dbo.tblProduct", t => t.ProductId)
                .Index(t => t.ProductId);
            
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
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.tblProductCategoryExtend", "ProductCategoryId", "dbo.tblProductCategory");
            DropForeignKey("dbo.tblProductExtend", "ProductId", "dbo.tblProduct");
            DropForeignKey("dbo.tblProduct", "ProductCategoryId", "dbo.tblProductCategory");
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.tblProductExtend", new[] { "ProductId" });
            DropIndex("dbo.tblProduct", new[] { "ProductCategoryId" });
            DropIndex("dbo.tblProductCategoryExtend", new[] { "ProductCategoryId" });
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.tblProductExtend");
            DropTable("dbo.tblProduct");
            DropTable("dbo.tblProductCategory");
            DropTable("dbo.tblProductCategoryExtend");
        }
    }
}
