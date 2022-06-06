namespace QuirkyBookRental.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMembershipTypeToDatabase : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MembershipTypes", "SignUpFee", c => c.Byte(nullable: false));
            DropColumn("dbo.MembershipTypes", "SingUpFee");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MembershipTypes", "SingUpFee", c => c.Byte(nullable: false));
            DropColumn("dbo.MembershipTypes", "SignUpFee");
        }
    }
}
