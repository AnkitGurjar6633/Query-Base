using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueryBase.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class changedVotesType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VoteValue",
                table: "VoteQuestions");

            migrationBuilder.DropColumn(
                name: "VoteValue",
                table: "VoteAnswers");

            migrationBuilder.AddColumn<bool>(
                name: "isUpvote",
                table: "VoteQuestions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isUpvote",
                table: "VoteAnswers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isUpvote",
                table: "VoteQuestions");

            migrationBuilder.DropColumn(
                name: "isUpvote",
                table: "VoteAnswers");

            migrationBuilder.AddColumn<int>(
                name: "VoteValue",
                table: "VoteQuestions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "VoteValue",
                table: "VoteAnswers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
