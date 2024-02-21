using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Votes.Migrations
{
    public partial class ReviewTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameIndex(
                name: "IDX_REVIEWID",
                table: "Votes",
                newName: "IDX_REVIEWID1");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewID = table.Column<long>(type: "bigint", precision: 10, nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_REVIEW", x => x.ReviewID);
                });

            migrationBuilder.CreateIndex(
                name: "IDX_REVIEWID",
                table: "Reviews",
                column: "ReviewID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.RenameIndex(
                name: "IDX_REVIEWID1",
                table: "Votes",
                newName: "IDX_REVIEWID");
        }
    }
}
