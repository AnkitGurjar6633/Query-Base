using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QueryBase.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class updatedVotesAnswerTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "QuestionId",
                table: "VoteAnswers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_VoteAnswers_QuestionId",
                table: "VoteAnswers",
                column: "QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_VoteAnswers_Questions_QuestionId",
                table: "VoteAnswers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VoteAnswers_Questions_QuestionId",
                table: "VoteAnswers");

            migrationBuilder.DropIndex(
                name: "IX_VoteAnswers_QuestionId",
                table: "VoteAnswers");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "VoteAnswers");
        }
    }
}
