using Microsoft.EntityFrameworkCore.Migrations;

namespace CancunHotel.Repository.Migrations
{
    public partial class AddLoginToTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Login (Id, UserName, Password) VALUES ('24F5E0C1-CDCF-4E2C-8785-4D39748CBC90','admin', 'admin')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
