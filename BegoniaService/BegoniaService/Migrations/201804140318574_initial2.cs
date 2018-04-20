namespace BegoniaService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookReviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        BookId = c.Int(nullable: false),
                        Review = c.String(maxLength: 4000),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookReviews", "UserId", "dbo.Users");
            DropForeignKey("dbo.BookReviews", "BookId", "dbo.Books");
            DropIndex("dbo.BookReviews", new[] { "BookId" });
            DropIndex("dbo.BookReviews", new[] { "UserId" });
            DropTable("dbo.BookReviews");
        }
    }
}
