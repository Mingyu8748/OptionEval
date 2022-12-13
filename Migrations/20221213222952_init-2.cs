using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OptionEval.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EvaluationId",
                table: "Trade",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Trade_EvaluationId",
                table: "Trade",
                column: "EvaluationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trade_OptionTradeEvaluation_EvaluationId",
                table: "Trade",
                column: "EvaluationId",
                principalTable: "OptionTradeEvaluation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trade_OptionTradeEvaluation_EvaluationId",
                table: "Trade");

            migrationBuilder.DropIndex(
                name: "IX_Trade_EvaluationId",
                table: "Trade");

            migrationBuilder.DropColumn(
                name: "EvaluationId",
                table: "Trade");
        }
    }
}
