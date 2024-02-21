using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Votes.Migrations
{
    public partial class CreateVotesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
            migrationBuilder.DropIndex(
                name: "IDX_REVIEWID",
                table: "Votes");

            migrationBuilder.DropIndex(
                name: "IDX_USERID",
                table: "Votes");
        }
    }
}
