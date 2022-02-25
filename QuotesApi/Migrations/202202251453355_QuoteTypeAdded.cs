namespace QuotesApi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class QuoteTypeAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quotes", "Title", c => c.String());
            AddColumn("dbo.Quotes", "Type", c => c.String());
            DropColumn("dbo.Quotes", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Quotes", "Name", c => c.String());
            DropColumn("dbo.Quotes", "Type");
            DropColumn("dbo.Quotes", "Title");
        }
    }
}
