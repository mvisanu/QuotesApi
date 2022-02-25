namespace QuotesApi.Authen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUserId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotes", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quotes", "UserId");
        }
    }
}
