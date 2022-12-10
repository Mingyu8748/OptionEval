
using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.ComponentModel.DataAnnotations.Schema; 
using System.Linq;
using Npgsql; 

public class FinanceContext : DbContext{
    public DbSet<Exchange> Exchanges { get; set;}
    public DbSet<TradingMarket> Markets {get;set;}
    public DbSet<Unit> Units {get;set;}
    public DbSet<FinancialInstrument> FinancialInstruments {get;set;}
    public DbSet<RatePoint> RatePoints {get;set;}
    public DbSet<Ratecurve> RateCurves {get;set;}
    public DbSet<Historical_price> HistoricalPrices {get;set;}
    public DbSet<Underlying> Underlyings {get; set;}
    public DbSet<Option> Options {get;set;}
    public DbSet<European> EuropeanOptions {get;set;}
    public DbSet<Digital> DigitalOptions {get;set;}
    public DbSet<Asian> AsianOptions {get;set;}
    public DbSet<Lookback> LookbackOptions {get;set;}
    public DbSet<Range> RangeOptions {get;set;}
    public DbSet<Barrier> BarrierOptions{get;set;}
    public DbSet<Trade> Trades{get;set;}
    public DbSet<Option_Trade_Evaluation> OptionTradeEvaluations{get;set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                    =>optionsBuilder.UseNpgsql("host=localhost;Database=ourdb;Username=postgres;Password=");
}

[Table("Exchange")]
public class Exchange
{
    public int Id {get;set;}
    public string Name {get;set;}
    public string Symbol {get;set;}

    //public List<Market> markets {get; set;}
}

[Table("Market")]
public class TradingMarket
{
    public int Id{get;set;}
    public string Name {get;set;}
    public int UnitId {get;set;}
    public Unit Unit {get;set;}
    public int ExchangeId {get;set;}
    public Exchange Exchange {get;set;}
}

[Table("Unit")]
public class Unit
{   public int Id{get;set;}
    public string typeUnit {get;set;}
    public double sizeUnit {get;set;}
}

[Table("FinancialInstrument")]
public class FinancialInstrument
{   
    public int Id{get;set;}
    public int marketid {get;set;}
    public TradingMarket Market {get;set;}
    // add symbol here
    // make id separate
    // exchange finexchange {get;set;}
}

// if you have the market, you have the exchange.
[Table("RatePoint")]
public class RatePoint
{
    public int Id{get;set;}
    public DateTime measure_date {get;set;} // put into historical price?
    public double term_in_years {get;set;}
    public double rate {get;set;}
    public int ratecurve_id {get;set;} //this identifies which rate curve it belongs to
    
}

[Table("RateCurve")]
public class Ratecurve
{
    public int Id{get;set;}
    public string name {get;set;}
    public string symbol {get;set;}
    public List<RatePoint> ratepoints {get;set;}
}

[Table("HistoricalPrice")] //I honestly dont think we'll be using this much, I cant truly think of a reason to
public class Historical_price
{
    public int Id{get;set;}
    public List<double> prices {get;set;} // this means each of our prices need to be linked... does this need to exist?
    public DateTime dateofobservation {get;set;}
    public string price_of {get;set;} // this is either underlying, option, or future
    public int price_of_id {get;set;} // price_of to go to the proper table and price_of_id to go to proper option, underlying, or future
    // id, refers to inst, date of obser, price(s)
}

//ratepoint = new ratepoint{}
[Table("Underlying")]
public class Underlying : FinancialInstrument
{
    // market underlying_market{get;set;}
    public int Id{get;set;}
    public string Name {get;set;}
    public string Symbol {get;set;}
    //historical_close_prices prices {get;set;}
}

[Table("Option")]
public class Option: FinancialInstrument
{
    public int underlyingid {get;set;}
    public Underlying Op_Under {get;set;}
    // double imp_volatility {get;set;}
    public DateTime Expiration_Date {get; set;}

    //internal double EuropeanCall(long numberOfPath, double tenor, double mu, double sigma, double underlyingPrice, double strike, double rate, double[,] price)
    //{
    //    throw new NotImplementedException();
    //}
    // historical_close_prices prices {get;set;}
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
    // Nothing here, dawg.
}

[Table("Lookback")]
public class Lookback : Option
{
    public double Strike {get; set;}
    public Boolean Is_Call {get; set;}
}

// enum KnockType
// {
//     DownAndOut, // 0
//     DownAndIn, // 1
//     UpandOut, // 2
//     UpAndIn // 3
// }

[Table("Barrier")]
public class Barrier : Option
{
    public double Strike {get; set;}
    public Boolean Is_Call {get; set;}
    public double Barrier_Level {get; set;}
    public int Knock_Type {get; set;}
}

[Table("Trade")]
public class Trade
{  
    public int Id {get;set;}
    public double quantity {get;set;} // change to quantity, dont need is buy
    public int underlyingid {get;set;}
    public string underlying_type {get;set;} // option, future, underlying, etc.
    public double Trade_Price {get;set;} // price it was traded at
}

[Table("OptionTradeEvaluation")]
public class Option_Trade_Evaluation
{
    public int Id {get;set;}
    
    // double standard_error {get; set;}
    // Monte Carlo evals. an instrument; not a trade.
    public double Unrealized_Pnl {get; set;} // different from market value; refer to trade price
    public double Delta {get; set;}
    public double Gamma {get; set;}
    public double Vega {get; set;}
    public double Rho {get; set;}
    public double Theta {get; set;}

    // ===============================
    // Team Winning!
    // Chris Prouty's All-Star Students
    // Ryne owes Vathana some Pringles.
    // ===============================

}

