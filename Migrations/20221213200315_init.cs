using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace OptionEval.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Exchanges",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Symbol = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exchanges", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OptionTradeEvaluation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UnrealizedPnl = table.Column<double>(name: "Unrealized_Pnl", type: "double precision", nullable: false),
                    Delta = table.Column<double>(type: "double precision", nullable: false),
                    Gamma = table.Column<double>(type: "double precision", nullable: false),
                    Vega = table.Column<double>(type: "double precision", nullable: false),
                    Rho = table.Column<double>(type: "double precision", nullable: false),
                    Theta = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OptionTradeEvaluation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Markets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    ExchangeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Markets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Markets_Exchanges_ExchangeId",
                        column: x => x.ExchangeId,
                        principalTable: "Exchanges",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FinancialInstrument",
                columns: table => new
                {
                    FinancialInstrumentID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Symbol = table.Column<string>(type: "text", nullable: false),
                    TradingMarketId = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialInstrument", x => x.FinancialInstrumentID);
                    table.ForeignKey(
                        name: "FK_FinancialInstrument_Markets_TradingMarketId",
                        column: x => x.TradingMarketId,
                        principalTable: "Markets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trade",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    quantity = table.Column<double>(type: "double precision", nullable: false),
                    FinancialInstrumentId = table.Column<int>(type: "integer", nullable: false),
                    TradePrice = table.Column<double>(name: "Trade_Price", type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trade", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trade_FinancialInstrument_FinancialInstrumentId",
                        column: x => x.FinancialInstrumentId,
                        principalTable: "FinancialInstrument",
                        principalColumn: "FinancialInstrumentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Underlying",
                columns: table => new
                {
                    FinancialInstrumentID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Underlying", x => x.FinancialInstrumentID);
                    table.ForeignKey(
                        name: "FK_Underlying_FinancialInstrument_FinancialInstrumentID",
                        column: x => x.FinancialInstrumentID,
                        principalTable: "FinancialInstrument",
                        principalColumn: "FinancialInstrumentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Option",
                columns: table => new
                {
                    FinancialInstrumentID = table.Column<int>(type: "integer", nullable: false),
                    volatility = table.Column<double>(type: "double precision", nullable: false),
                    ExpirationDate = table.Column<string>(name: "Expiration_Date", type: "text", nullable: false),
                    underlyingId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Option", x => x.FinancialInstrumentID);
                    table.ForeignKey(
                        name: "FK_Option_FinancialInstrument_FinancialInstrumentID",
                        column: x => x.FinancialInstrumentID,
                        principalTable: "FinancialInstrument",
                        principalColumn: "FinancialInstrumentID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Option_Underlying_underlyingId",
                        column: x => x.underlyingId,
                        principalTable: "Underlying",
                        principalColumn: "FinancialInstrumentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Asian",
                columns: table => new
                {
                    FinancialInstrumentID = table.Column<int>(type: "integer", nullable: false),
                    Strike = table.Column<double>(type: "double precision", nullable: false),
                    IsCall = table.Column<bool>(name: "Is_Call", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asian", x => x.FinancialInstrumentID);
                    table.ForeignKey(
                        name: "FK_Asian_Option_FinancialInstrumentID",
                        column: x => x.FinancialInstrumentID,
                        principalTable: "Option",
                        principalColumn: "FinancialInstrumentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Barrier",
                columns: table => new
                {
                    FinancialInstrumentID = table.Column<int>(type: "integer", nullable: false),
                    Strike = table.Column<double>(type: "double precision", nullable: false),
                    IsCall = table.Column<bool>(name: "Is_Call", type: "boolean", nullable: false),
                    BarrierLevel = table.Column<double>(name: "Barrier_Level", type: "double precision", nullable: false),
                    KnockType = table.Column<string>(name: "Knock_Type", type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Barrier", x => x.FinancialInstrumentID);
                    table.ForeignKey(
                        name: "FK_Barrier_Option_FinancialInstrumentID",
                        column: x => x.FinancialInstrumentID,
                        principalTable: "Option",
                        principalColumn: "FinancialInstrumentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Digital",
                columns: table => new
                {
                    FinancialInstrumentID = table.Column<int>(type: "integer", nullable: false),
                    Strike = table.Column<double>(type: "double precision", nullable: false),
                    IsCall = table.Column<bool>(name: "Is_Call", type: "boolean", nullable: false),
                    Payout = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Digital", x => x.FinancialInstrumentID);
                    table.ForeignKey(
                        name: "FK_Digital_Option_FinancialInstrumentID",
                        column: x => x.FinancialInstrumentID,
                        principalTable: "Option",
                        principalColumn: "FinancialInstrumentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "European",
                columns: table => new
                {
                    FinancialInstrumentID = table.Column<int>(type: "integer", nullable: false),
                    Strike = table.Column<double>(type: "double precision", nullable: false),
                    IsCall = table.Column<bool>(name: "Is_Call", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_European", x => x.FinancialInstrumentID);
                    table.ForeignKey(
                        name: "FK_European_Option_FinancialInstrumentID",
                        column: x => x.FinancialInstrumentID,
                        principalTable: "Option",
                        principalColumn: "FinancialInstrumentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lookback",
                columns: table => new
                {
                    FinancialInstrumentID = table.Column<int>(type: "integer", nullable: false),
                    Strike = table.Column<double>(type: "double precision", nullable: false),
                    IsCall = table.Column<bool>(name: "Is_Call", type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lookback", x => x.FinancialInstrumentID);
                    table.ForeignKey(
                        name: "FK_Lookback_Option_FinancialInstrumentID",
                        column: x => x.FinancialInstrumentID,
                        principalTable: "Option",
                        principalColumn: "FinancialInstrumentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Range",
                columns: table => new
                {
                    FinancialInstrumentID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Range", x => x.FinancialInstrumentID);
                    table.ForeignKey(
                        name: "FK_Range_Option_FinancialInstrumentID",
                        column: x => x.FinancialInstrumentID,
                        principalTable: "Option",
                        principalColumn: "FinancialInstrumentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FinancialInstrument_TradingMarketId",
                table: "FinancialInstrument",
                column: "TradingMarketId");

            migrationBuilder.CreateIndex(
                name: "IX_Markets_ExchangeId",
                table: "Markets",
                column: "ExchangeId");

            migrationBuilder.CreateIndex(
                name: "IX_Option_underlyingId",
                table: "Option",
                column: "underlyingId");

            migrationBuilder.CreateIndex(
                name: "IX_Trade_FinancialInstrumentId",
                table: "Trade",
                column: "FinancialInstrumentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Asian");

            migrationBuilder.DropTable(
                name: "Barrier");

            migrationBuilder.DropTable(
                name: "Digital");

            migrationBuilder.DropTable(
                name: "European");

            migrationBuilder.DropTable(
                name: "Lookback");

            migrationBuilder.DropTable(
                name: "OptionTradeEvaluation");

            migrationBuilder.DropTable(
                name: "Range");

            migrationBuilder.DropTable(
                name: "Trade");

            migrationBuilder.DropTable(
                name: "Option");

            migrationBuilder.DropTable(
                name: "Underlying");

            migrationBuilder.DropTable(
                name: "FinancialInstrument");

            migrationBuilder.DropTable(
                name: "Markets");

            migrationBuilder.DropTable(
                name: "Exchanges");
        }
    }
}
