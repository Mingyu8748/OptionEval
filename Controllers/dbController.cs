using Microsoft.AspNetCore.Mvc;
using Monte_Carlo_Simulation;

namespace OptionDatabase.Controllers;

[ApiController]
[Route("[controller]")]

public class dbController : ControllerBase
{
    FinanceContext db = new FinanceContext();

    //Echange Table Controller
    [HttpGet]
    [Route("/Exchange")]
    public IEnumerable<Exchange> GetExchanges()
    {
        return db.Exchanges.ToArray();
    }

    [HttpGet]
    [Route("/Exchange/{id}")]
    public ActionResult<Exchange> GetExchanges(int id)
    {
        return db.Exchanges.Find(id);
    }

    [HttpPut]
    [Route("/Exchange")]
    public ActionResult<Exchange> UpdateExchanges([FromBody] Exchange exchange)
    {
        var existingExchange = db.Exchanges.Find(exchange.Id);
        if (existingExchange == null)
        {
            db.Exchanges.Add(exchange);
        }
        else
        {
            existingExchange.Name = exchange.Name;
            existingExchange.Symbol = exchange.Symbol;
            exchange = null;
            db.Exchanges.Update(existingExchange);
        }
        db.SaveChanges();
        return Ok(exchange);
    }

    [HttpDelete("/Exchange/delete/{id}")]
    public ActionResult<Exchange> DeleteExchange(int id)
    {
        var exchange = db.Exchanges.Find(id);
        db.Exchanges.Remove(exchange);
        db.SaveChanges();
        return NoContent();
    }


    [HttpDelete("/Exchange/delete-all")]
    public ActionResult DeleteAllExchange()
    {
        var exchanges = db.Exchanges.ToArray();

        foreach (Exchange exchange in exchanges)
        {
            if (exchange != null)
            {
                db.Exchanges.Remove(exchange);
            }
        }
        db.SaveChanges();
        return Ok();
    }

    //Market Table Controller
    [HttpGet]
    [Route("/Market")]
    public IEnumerable<TradingMarket> GetMarkets()
    {
        return db.Markets.ToArray();
    }

    [HttpGet]
    [Route("/Market/{id}")]
    public ActionResult<TradingMarket> GetMarkets(int id)
    {
        return db.Markets.Find(id);
    }

    [HttpPut]
    [Route("/Market")]
    public ActionResult<TradingMarket> UpdateMarkets([FromBody] TradingMarket market)
    {
        var existingMarkets = db.Markets.Find(market.Id);
        if (existingMarkets == null)
        {
            db.Markets.Add(market);
        }
        else
        {
            existingMarkets.Name = market.Name;
            // existingMarkets.Unit = market.Unit;
            existingMarkets.ExchangeId = market.ExchangeId;

            market = null;
            db.Markets.Update(existingMarkets);
        }
        db.SaveChanges();
        return Ok(market);
    }

    //    [HttpDelete("/Market/delete/{id}")]
    //     public ActionResult <TradingMarket> DeleteMarkets(int id){
    //         var market = db.Markets.Find(id);
    //         db.Markets.Remove(market);
    //         db.SaveChanges();
    //         return NoContent();
    //     }


    //     [HttpDelete("/Market/delete-all")]
    //     public ActionResult DeleteAllMarkets(){
    //         var markets = db.Markets.ToArray();

    //         foreach (TradingMarket market in markets)
    //         {
    //             if(market != null) {
    //                 db.Markets.Remove(market);
    //             }
    //         }
    //         db.SaveChanges();
    //         return Ok();
    //     }  

    //     //Unit Table Controllers

    //     [HttpGet]
    //     [Route("Unit")]
    //     public IEnumerable <Unit> GetUnits(){
    //         return db.Unit.ToArray();
    //     }

    //     [HttpGet]
    //     [Route("/Unit/{id}")]
    //     public ActionResult<Unit> GetUnits(int id){
    //         return db.Unit.Find(id);}

    //     [HttpPut]
    //     [Route("/Unit")]
    //     public ActionResult <Unit> UpdateUnits([FromBody] Unit unit){
    //         var existingUnits = db.Unit.Find(unit.Id);
    //         if (existingUnits == null) {
    //             db.Unit.Add(unit);
    //         } else {
    //             existingUnits.typeUnit = unit.typeUnit;
    //             existingUnits.sizeUnit = unit.sizeUnit;
    //             unit = null;
    //             db.Unit.Update(existingUnits); 
    //         }
    //         db.SaveChanges();
    //         return Ok(unit);
    //     }
    //   [HttpDelete("/Unit/delete/{id}")]
    //     public ActionResult <Unit> DeleteUnits(int id){
    //         var unit = db.Unit.Find(id);
    //         db.Unit.Remove(unit);
    //         db.SaveChanges();
    //         return NoContent();
    //     }


    //     [HttpDelete("/Unit/delete-all")]
    //     public ActionResult DeleteAllUnits(){
    //         var units = db.Unit.ToArray();

    //         foreach (Unit unit in units)
    //         {
    //             if(unit != null) {
    //                 db.Unit.Remove(unit);
    //             }
    //         }
    //         db.SaveChanges();
    //         return Ok();
    //     }

    // httpget all options 

    [HttpGet]
    [Route("/Option")]
    public IEnumerable<Option> GetOptions()
    {
        return db.Options.ToArray();
    }

    [HttpGet]
    [Route("/Option/{id}")]
    public ActionResult<Option> GetOptions(int id)
    {
        return db.Options.Find(id);
    }

    [HttpPut]
    [Route("/Option")]
    public ActionResult<Option> UpdateOptions([FromBody] Option option)
    {
        var existingOptions = db.Options.Find(option.FinancialInstrumentID);
        if (existingOptions == null)
        {
            db.Options.Add(option);
        }
        else
        {
            existingOptions = null;
            db.Options.Update(option);
        }
        db.SaveChanges();
        return Ok(option);
    }

    [HttpDelete("/Option/delete/{id}")]
    public ActionResult<Option> DeleteOptions(int id)
    {
        var option = db.Options.Find(id);
        db.Options.Remove(option);
        db.SaveChanges();
        return NoContent();
    }

    [HttpDelete("/Option/delete-all")]
    public ActionResult DeleteAllOptions()
    {
        var options = db.Options.ToArray();
        foreach (Option option in options)
        {
            if (option != null)
            {
                db.Options.Remove(option);
            }
        }
        db.SaveChanges();
        return Ok();
    }


    //FinancialInstrumetn controllers

    [HttpGet]
    [Route("/FinancialInstrument")]
    public IEnumerable<FinancialInstrument> GetInstruments()
    {
        return db.FinancialInstruments.ToArray();
    }

    [HttpGet]
    [Route("/FinancialInstrument/{id}")]
    public ActionResult<FinancialInstrument> GetInstruments(int id)
    {
        return db.FinancialInstruments.Find(id);
    }

    [HttpPut]
    [Route("/FinancialInstrument")]
    public ActionResult<FinancialInstrument> UpdateInstruments([FromBody] FinancialInstrument instruments)
    {
        var existingInstruments = db.FinancialInstruments.Find(instruments.FinancialInstrumentID);
        if (existingInstruments == null)
        {
            db.FinancialInstruments.Add(instruments);
        }
        else
        {
            existingInstruments.FinancialInstrumentID = instruments.FinancialInstrumentID;
            instruments = null;
            db.FinancialInstruments.Update(existingInstruments);
        }
        db.SaveChanges();
        return Ok(instruments);
    }
    [HttpDelete("/FinancialInstrument/delete/{id}")]
    public ActionResult<FinancialInstrument> DeleteInstruments(int id)
    {
        var instrument = db.FinancialInstruments.Find(id);
        db.FinancialInstruments.Remove(instrument);
        db.SaveChanges();
        return NoContent();
    }


    [HttpDelete("/FinancialInstrument/delete-all")]
    public ActionResult DeleteInstruments()
    {
        var instruments = db.FinancialInstruments.ToArray();

        foreach (FinancialInstrument instrument in instruments)
        {
            if (instrument != null)
            {
                db.FinancialInstruments.Remove(instrument);
            }
        }
        db.SaveChanges();
        return Ok();
    }

    //     //RatePoints Controllers
    //     [HttpGet]
    //     [Route("Ratepoint")]
    //     public IEnumerable <RatePoint> GetRates(){
    //         return db.RatePoints.ToArray();
    //     }

    //     [HttpGet]
    //     [Route("/Ratepoint/{id}")]
    //     public ActionResult<RatePoint> GetRates(int id){
    //         return db.RatePoints.Find(id);}


    //     [HttpPut]
    //     [Route("/Ratepoint")]
    //     public ActionResult <RatePoint> UpdateRates([FromBody] RatePoint rates){
    //         var existingRates = db.RatePoints.Find(rates.Id);
    //         if (existingRates == null) {
    //             db.RatePoints.Add(rates);
    //         } else {
    //             existingRates.measure_date = rates.measure_date;
    //             existingRates.term_in_years = rates.term_in_years;
    //             existingRates.rate = rates.rate;
    //             existingRates.ratecurve_id = rates.ratecurve_id;
    //             rates = null;
    //             db.RatePoints.Update(existingRates); 
    //         }
    //         db.SaveChanges();
    //         return Ok(rates);
    //     }
    //   [HttpDelete("/Ratepoint/delete/{id}")]
    //     public ActionResult <RatePoint> DeleteRates(int id){
    //         var rate = db.RatePoints.Find(id);
    //         db.RatePoints.Remove(rate);
    //         db.SaveChanges();
    //         return NoContent();
    //     }
    //     //RateCurve Controllers

    //     [HttpGet]
    //     [Route("RateCurve")]
    //     public IEnumerable <Ratecurve> GetRateCurves(){
    //         return db.RateCurves.ToArray();
    //     }
    //     [HttpPut]
    //     [Route("/RateCurve")]
    //     public ActionResult <Ratecurve> UpdateCurves([FromBody] Ratecurve curves){
    //         var existingCurves = db.RateCurves.Find(curves.Id);
    //         if (existingCurves == null) {
    //             db.RateCurves.Add(curves);
    //         } else {
    //             existingCurves.name = curves.name;
    //             existingCurves.symbol = curves.symbol;
    //             curves = null;
    //             db.RateCurves.Update(existingCurves); 
    //         }
    //         db.SaveChanges();
    //         return Ok(curves);
    //     }
    //   [HttpDelete("/RateCurve/delete/{id}")]
    //     public ActionResult <Ratecurve> DeleteCurves(int id){
    //         var curve = db.RateCurves.Find(id);
    //         db.RateCurves.Remove(curve);
    //         db.SaveChanges();
    //         return NoContent();
    //     }

    //     //Historical Price Controllers
    //     [HttpGet]
    //     [Route("HistoricalPrice")]
    //     public IEnumerable <Historical_price> GetPrices(){
    //         return db.HistoricalPrices.ToArray();
    //     }

    //     [HttpGet]
    //     [Route("/HistoricalPrice/{id}")]
    //     public ActionResult<Historical_price> GetPrices(int id){
    //         return db.HistoricalPrices.Find(id);}

    //     [HttpPut]
    //     [Route("/HistoricalPrice")]
    //     public ActionResult <Historical_price> UpdatePrices([FromBody] Historical_price prices){
    //         var existingPrices = db.HistoricalPrices.Find(prices.Id);
    //         if (existingPrices == null) {
    //             db.HistoricalPrices.Add(prices);
    //         } else {
    //             existingPrices.prices = prices.prices;
    //             existingPrices.dateofobservation = prices.dateofobservation;
    //             existingPrices.price_of = prices.price_of;
    //             existingPrices.price_of_id = prices.price_of_id;
    //             prices = null;
    //             db.HistoricalPrices.Update(existingPrices); 
    //         }
    //         db.SaveChanges();
    //         return Ok(prices);
    //     }
    //   [HttpDelete("/HistoricalPrice/delete/{id}")]
    //     public ActionResult <Historical_price> DeletePrices(int id){
    //         var price = db.HistoricalPrices.Find(id);
    //         db.HistoricalPrices.Remove(price);
    //         db.SaveChanges();
    //         return NoContent();
    //     }

    //     [HttpDelete("/HistoricalPrice/delete-all")]
    //     public ActionResult DeletePrices(){
    //         var prices = db.HistoricalPrices.ToArray();

    //         foreach (Historical_price price in prices)
    //         {
    //             if(price != null) {
    //                 db.HistoricalPrices.Remove(price);
    //             }
    //         }
    //         db.SaveChanges();
    //         return Ok();
    //     }

    //Underlying Controllers
    [HttpGet]
    [Route("/Underlying")]
    public IEnumerable<Underlying> GetUnderlying()
    {
        return db.Underlyings.ToArray();
    }

    [HttpGet]
    [Route("/Underlying/{id}")]
    public ActionResult<Underlying> GetUnderlyings(int id)
    {
        return db.Underlyings.Find(id);
    }

    [HttpPut]
    [Route("/Underlying")]
    public ActionResult<Underlying> UpdatePrices([FromBody] Underlying underlyings)
    {
        var existingUnderlyings = db.Underlyings.Find(underlyings.FinancialInstrumentID);
        if (existingUnderlyings == null)
        {
            db.Underlyings.Add(underlyings);
        }
        else
        {
            existingUnderlyings.Name = underlyings.Name;
            existingUnderlyings.Symbol = underlyings.Symbol;
            underlyings = null;
            db.Underlyings.Update(existingUnderlyings);
        }
        db.SaveChanges();
        return Ok(underlyings);
    }
    [HttpDelete("/Underlying/delete/{id}")]
    public ActionResult<Underlying> DeleteUnderlyings(int id)
    {
        var underlying = db.Underlyings.Find(id);
        db.Underlyings.Remove(underlying);
        db.SaveChanges();
        return NoContent();
    }

    [HttpDelete("/Underlying/delete-all")]
    public ActionResult DeleteUnderlyings()
    {
        var underlyings = db.Underlyings.ToArray();

        foreach (Underlying underlying in underlyings)
        {
            if (underlying != null)
            {
                db.Underlyings.Remove(underlying);
            }
        }
        db.SaveChanges();
        return Ok();
    }

    //     //Option Controllers (combine different otpion classes into same table and display them?)

    //     //Trade Controllers 

    [HttpGet]
    [Route("/Trade")]
    public IEnumerable<Trade> GetTrades()
    {
        return db.Trades.ToArray();
    }

    [HttpGet]
    [Route("/Trade/{id}")]
    public ActionResult<Trade> GetTrades(int id)
    {
        return db.Trades.Find(id);
    }

    [HttpPut]
    [Route("/Trade")]
    public ActionResult<Trade> UpdateTrades([FromBody] Trade trades)
    {
        var existingTrades = db.Trades.Find(trades.Id);
        if (existingTrades == null)
        {
            db.Trades.Add(trades);
        }
        else
        {
            existingTrades.quantity = trades.quantity;
            existingTrades.Trade_Price = trades.Trade_Price;
            trades = null;
            db.Trades.Update(existingTrades);
        }
        db.SaveChanges();
        return Ok(trades);
    }
    [HttpDelete("/Trade/delete/{id}")]
    public ActionResult<Trade> DeleteTrades(int id)
    {
        var trade = db.Trades.Find(id);
        db.Trades.Remove(trade);
        db.SaveChanges();
        return NoContent();
    }

    [HttpDelete("/Trade/delete-all")]
    public ActionResult DeleteTrades()
    {
        var trades = db.Trades.ToArray();

        foreach (Trade trade in trades)
        {
            if (trade != null)
            {
                db.Trades.Remove(trade);
            }
        }
        db.SaveChanges();
        return Ok();
    }


    //European Controllers
    [HttpGet]
    [Route("/European")]
    public IEnumerable<European> GetEuropean()
    {
        return db.Europeans.ToArray();
    }

    [HttpGet]
    [Route("/European/{id}")]
    public ActionResult<European> GetEuropeans(int id)
    {
        return db.Europeans.Find(id);
    }

    [HttpPut]
    [Route("/European")]
    public ActionResult<European> UpdateEuropeans([FromBody] European europeans)
    {
        var existingEuropeans = db.Europeans.Find(europeans.FinancialInstrumentID);
        if (existingEuropeans == null)
        {
            db.Europeans.Add(europeans);
        }
        else
        {
            existingEuropeans.expireIn = europeans.expireIn;
            existingEuropeans.Strike = europeans.Strike;
            existingEuropeans.Is_Call = europeans.Is_Call;
            existingEuropeans.volatility = europeans.volatility;
            existingEuropeans.underlyingId = europeans.underlyingId;
            europeans = null;
            db.Europeans.Update(existingEuropeans);
        }
        db.SaveChanges();
        return Ok(europeans);
    }
    [HttpDelete("/European/delete/{id}")]
    public ActionResult<European> DeleteEuropeans(int id)
    {
        var european = db.Europeans.Find(id);
        db.Europeans.Remove(european);
        db.SaveChanges();
        return NoContent();
    }

    [HttpDelete("/European/delete-all")]
    public ActionResult DeleteEuropeans()
    {
        var europeans = db.Europeans.ToArray();

        foreach (European european in europeans)
        {
            if (european != null)
            {
                db.Europeans.Remove(european);
            }
        }
        db.SaveChanges();
        return Ok();
    }

    // controllers for Option_Trade_Evaluation
    [HttpGet]
    [Route("/OptionTradeEvaluation")]
    public IEnumerable<Option_Trade_Evaluation> GetOption_Trade_Evaluation()
    {
        return db.OptionTradeEvaluations.ToArray();
    }

    [HttpGet]
    [Route("/OptionTradeEvaluation/{id}")]
    public ActionResult<Option_Trade_Evaluation> GetOption_Trade_Evaluation(int id)
    {
        return db.OptionTradeEvaluations.Find(id);
    }

    [HttpPut]
    [Route("/OptionTradeEvaluation")]
    public ActionResult<Option_Trade_Evaluation> UpdateOption_Trade_Evaluation([FromBody] Option_Trade_Evaluation option_Trade_Evaluations)
    {
        var existingOption_Trade_Evaluations = db.OptionTradeEvaluations.Find(option_Trade_Evaluations.Id);
        if (existingOption_Trade_Evaluations == null)
        {
            db.OptionTradeEvaluations.Add(option_Trade_Evaluations);
        }
        else
        {
            existingOption_Trade_Evaluations.Id = option_Trade_Evaluations.Id;
            existingOption_Trade_Evaluations.Unrealized_Pnl = option_Trade_Evaluations.Unrealized_Pnl;
            existingOption_Trade_Evaluations.Delta = option_Trade_Evaluations.Delta;
            existingOption_Trade_Evaluations.Gamma = option_Trade_Evaluations.Gamma;
            existingOption_Trade_Evaluations.Vega = option_Trade_Evaluations.Vega;
            existingOption_Trade_Evaluations.Rho = option_Trade_Evaluations.Rho;
            existingOption_Trade_Evaluations.Theta = option_Trade_Evaluations.Theta;
            option_Trade_Evaluations = null;
            db.OptionTradeEvaluations.Update(existingOption_Trade_Evaluations);
        }
        db.SaveChanges();
        return Ok(option_Trade_Evaluations);
    }
    [HttpDelete("/OptionTradeEvaluation/delete/{id}")]
    public ActionResult<Option_Trade_Evaluation> DeleteOption_Trade_Evaluation(int id)
    {
        var option_Trade_Evaluation = db.OptionTradeEvaluations.Find(id);
        db.OptionTradeEvaluations.Remove(option_Trade_Evaluation);
        db.SaveChanges();
        return NoContent();
    }

    [HttpDelete("/OptionTradeEvaluation/delete-all")]
    public ActionResult DeleteOption_Trade_Evaluation()
    {
        var option_Trade_Evaluations = db.OptionTradeEvaluations.ToArray();

        foreach (Option_Trade_Evaluation option_Trade_Evaluation in option_Trade_Evaluations)
        {
            if (option_Trade_Evaluation != null)
            {
                db.OptionTradeEvaluations.Remove(option_Trade_Evaluation);
            }
        }
        db.SaveChanges();
        return Ok();
    }



    public class GreekCalcParam
    {
        public string Type { get; set; }
        public double Quantity { get; set; }
        public double TradePrice { get; set; }
        public int FinancialInstrumentId { get; set; }
    }

    [HttpPost]
    [Route("/Simulate")]
    public ActionResult Simulate([FromBody] GreekCalcParam request)
    {
        var evaluation = new Option_Trade_Evaluation();

        long N = 10000;
        double mu = 0.05;
        double r = 0.03;
        // T : by year, Option table_ expiration date
        //sigma: Option table_volatility
        //s0: Option table_underlying price
        //k:option table_strike
        //r: ratepoint table rate (if this is too complicated, we can just use 0.03)
        //price: output of method StockSimulator.StockPath (this method need the output of method RandomNumberGenerator.Generate)
        //iscall: Option table iscall
        //radom number : output of method RandomNumberGenerator.Generate (Input N & T into this method, same as the N & T here)

        var greeksCalc = new GreeksCalc();
        var european = db.Europeans.Find(request.FinancialInstrumentId);

        if (european != null)
        {
            var underlying = db.Underlyings.Find(european.underlyingId);
            var price = StockSimulator.StockPath(N, european.expireIn, mu, european.volatility, underlying.price, RandomNumberGenerator.Generate());
            var greekResult = greeksCalc.EuropeanGreeks(N, european.expireIn, mu, european.volatility, underlying.price, european.Strike, r, price, european.Is_Call);

            var optionResult = OptionCalculator.EuropeanOptionCalc(N, european.expireIn, mu, european.volatility, underlying.price, european.Strike, r, price, european.Is_Call);

            evaluation = new Option_Trade_Evaluation
            {
                Unrealized_Pnl = (optionResult - request.TradePrice) * request.Quantity,
                Delta = greekResult["delta"],
                Gamma = greekResult["gamma"],
                Vega = greekResult["vega"],
                Rho = greekResult["rho"],
                Theta = greekResult["theta"]
            };
            var savedeval = db.OptionTradeEvaluations.Add(evaluation);
            evaluation.Id = savedeval.Entity.Id;
        }
        db.SaveChanges();

        db.Trades.Add(new Trade
        {
            quantity = request.Quantity,
            Trade_Price = request.TradePrice,
            FinancialInstrumentId = request.FinancialInstrumentId,
            EvaluationId = evaluation.Id
        });

        db.SaveChanges();
        return Ok();
    }




    [HttpGet]
    [Route("/Load")]
    public ActionResult Load()
    {

        // exchanges
        var exchagne1 = db.Exchanges.Add(new Exchange { Name = "my exchange1", Symbol = "EE1" });
        var exchagne2 = db.Exchanges.Add(new Exchange { Name = "my exchange2", Symbol = "EE2" });
        db.SaveChanges();

        // markets
        var m1 = db.Markets.Add(new TradingMarket {Name = "my market1", ExchangeId = exchagne1.Entity.Id });
        var m2 = db.Markets.Add(new TradingMarket {Name = "my market2", ExchangeId = exchagne2.Entity.Id });
        db.SaveChanges();

        // underlyings
        var u1 = db.Underlyings.Add(new Underlying { Name = "my underlying1", Symbol = "UL1", price = 50, TradingMarketId = m1.Entity.Id });
        var u2 = db.Underlyings.Add(new Underlying { Name = "my underlying2", Symbol = "UL2", price = 100, TradingMarketId = m2.Entity.Id });
        db.SaveChanges();

        // European optiosn
        db.Europeans.Add(new European { Name = "European option 3", Symbol = "EO3", expireIn = 8, Strike = 50, Is_Call = true, price = 150, volatility = 0.25, underlyingId = u1.Entity.FinancialInstrumentID, TradingMarketId = m1.Entity.Id });
        db.Europeans.Add(new European {  Name = "European option 4", Symbol = "EO4", expireIn = 2, Strike = 50, Is_Call = true, price = 200, volatility = 0.19, underlyingId = u2.Entity.FinancialInstrumentID, TradingMarketId = m2.Entity.Id });

        db.SaveChanges();
        return Ok();
    }
}

