using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Votes.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    UserID = table.Column<long>(type: "bigint", precision: 10, nullable: false),
                    ReviewID = table.Column<long>(type: "bigint", precision: 10, nullable: false),
                    Vote = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VOTES", x => new { x.ReviewID, x.UserID });
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes");
        }
    }
}
