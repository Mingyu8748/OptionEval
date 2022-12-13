namespace OptionDatabase;
using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Npgsql;

public class FinanceContext : DbContext
{
    public DbSet<Exchange> Exchanges { get; set; }
    public DbSet<TradingMarket> Markets { get; set; }
    // public DbSet<Unit> Unit {get;set;}
    public DbSet<FinancialInstrument> FinancialInstruments {get;set;}
    // public DbSet<RatePoint> RatePoints {get;set;}
    // public DbSet<Ratecurve> RateCurves {get;set;}
    // public DbSet<HistoricalPrice> HistoricalPrices {get;set;}
    public DbSet<Underlying> Underlyings {get; set;}
    public DbSet<Option> Options {get;set;}
    public DbSet<European> Europeans {get;set;}
    public DbSet<Digital> DigitalOptions {get;set;}
    public DbSet<Asian> AsianOptions {get;set;}
    public DbSet<Lookback> LookbackOptions {get;set;}
    public DbSet<Range> RangeOptions {get;set;}
    public DbSet<Barrier> BarrierOptions{get;set;}
    public DbSet<Trade> Trades{get;set;}
    public DbSet<Option_Trade_Evaluation> OptionTradeEvaluations{get;set;}

    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                    => optionsBuilder.UseNpgsql("host=localhost;Database=OptionEval;Username=root;Password=root");
}

// [Table("Exchange")]
public class Exchange
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Symbol { get; set; }
    public virtual ICollection<TradingMarket>? Markets { get; set; }

}

// [Table("Market")]
public class TradingMarket
{
    public int Id { get; set; }
    public string Name { get; set; }
    // public int UnitId {get;set;}
    // public Unit Unit {get;set;}

    public int ExchangeId { get; set; }
    public virtual Exchange? Exchange { get; set; }
    // public ICollection<FinancialInstrument> financialInstruments {get;set;} 
}

// [Table("Unit")]
// public class Unit
// {   public int UnitID{get;set;}
//     public string typeUnit {get;set;}
//     public double sizeUnit {get;set;}
// }

[Table("FinancialInstrument")]
public class FinancialInstrument
{   
    public int FinancialInstrumentID{get;set;}
    public string Name {get;set;}
    public string Symbol {get;set;}
    public int TradingMarketId {get;set;}
    public TradingMarket? market {get;set;}
    public double price {get;set;}
    // add symbol here
    // make id separate
    // exchange finexchange {get;set;}
    // public ICollection<HistoricalPrice> historicalPrices {get;set;}
}

// // if you have the market, you have the exchange.
// [Table("RatePoint")]
// public class RatePoint
// {
//     public int RatePointID{get;set;}
//     public DateTime measure_date {get;set;} // put into historical price?
//     public double term_in_years {get;set;}
//     public double rate {get;set;}
//     // public int ratecurve_id {get;set;}
// }

// [Table("RateCurve")]
// public class Ratecurve
// {
//     public int RatecurveID{get;set;}
//     public string name {get;set;}
//     public string symbol {get;set;}
//     public ICollection<RatePoint> ratepoints {get;set;}
// }

// [Table("HistoricalPrice")] //I honestly dont thixnk we'll be using this much, I cant truly think of a reason to
// public class HistoricalPrice
// {
//     public int HistoricalPriceId{get;set;}
//     public double prices {get;set;} // this means each of our prices need to be linked... does this need to exist?
//     public FinancialInstrument financialInstrument {get;set;}
//     public DateTime dateofobservation {get;set;}
// }

// //ratepoint = new ratepoint{}
[Table("Underlying")]
public class Underlying : FinancialInstrument
{
    // market underlying_market{get;set;}
    //historical_close_prices prices {get;set;}
}

[Table("Option")]
public class Option: FinancialInstrument
{
    public double volatility {get;set;}
    public string Expiration_Date {get; set;}
    public int underlyingId {get; set;}
    public Underlying? underlying {get; set;}

    //internal double EuropeanCall(long numberOfPath, double tenor, double mu, double sigma, double underlyingPrice, double strike, double rate, double[,] price)
    //{
    //    throw new NotImplementedException();
    //}
}

[Table("European")]

public class European : Option
{
    public double Strike {get; set;}
    public bool Is_Call {get; set;}
}

[Table("Digital")]

public class Digital : Option
{
    public double Strike {get; set;}
    public Boolean Is_Call {get; set;}
    public double Payout {get; set;}
}

[Table("Asian")]
public class Asian : Option
{
    public double Strike {get; set;}
    public Boolean Is_Call {get; set;}
}
[Table("Range")]
public class Range : Option
{
}

[Table("Lookback")]
public class Lookback : Option
{
    public double Strike {get; set;}
    public Boolean Is_Call {get; set;}
}

[Table("Barrier")]
public class Barrier : Option
{
    public double Strike {get; set;}
    public Boolean Is_Call {get; set;}
    public double Barrier_Level {get; set;}
    public string Knock_Type {get; set;}
}

[Table("Trade")]
public class Trade
{  
    public int Id {get;set;}
    public double quantity {get;set;} // change to quantity, dont need is buy
    public int FinancialInstrumentId {get;set;}
    public FinancialInstrument financialInstrument {get;set;}
    public double Trade_Price {get;set;} // price it was traded at
}

[Table("OptionTradeEvaluation")]
public class Option_Trade_Evaluation
{
    public int Id {get;set;}
    public double Unrealized_Pnl {get; set;} // different from market value; refer to trade price
    public double Delta {get; set;}
    public double Gamma {get; set;}
    public double Vega {get; set;}
    public double Rho {get; set;}
    public double Theta {get; set;}
}

