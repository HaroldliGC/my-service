namespace BegoniaService.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Isbn = c.String(nullable: false, maxLength: 20),
                        Name = c.String(nullable: false, maxLength: 100),
                        Type = c.String(maxLength: 200),
                        Press = c.String(maxLength: 200),
                        Author = c.String(maxLength: 100),
                        Info = c.String(maxLength: 4000),
                        Price = c.Double(nullable: false),
                        Number = c.Int(nullable: false),
                        BorrowNumber = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Books");
        }
    }
}
