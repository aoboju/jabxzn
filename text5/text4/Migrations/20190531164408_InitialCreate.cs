using Microsoft.EntityFrameworkCore.Migrations;

namespace text4.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Blogs",
                columns: table => new
                {
                    num = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    siteId = table.Column<string>(nullable: true),
                    siteProperty = table.Column<string>(nullable: true),
                    Sitevalue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Blogs", x => x.num);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Blogs");
        }
    }
}
