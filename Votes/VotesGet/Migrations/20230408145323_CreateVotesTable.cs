using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VotesGet.Migrations
{
    public partial class CreateVotesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Votes",
                columns: table => new
                {
                    UserID = table.Column<long>(type: "bigint", precision: 10, nullable: false),
                    ReviewID = table.Column<long>(type: "bigint", precision: 10, nullable: false),
                    Vote = table.Column<string>(type: "text", unicode: false, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VOTES", x => new { x.ReviewID, x.UserID });
                });

            migrationBuilder.CreateIndex(
                name: "IDX_REVIEWID",
                table: "Votes",
                column: "ReviewID");

            migrationBuilder.CreateIndex(
                name: "IDX_USERID",
                table: "Votes",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Votes");
        }
    }
}
