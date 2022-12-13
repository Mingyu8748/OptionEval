using Microsoft.AspNetCore.Mvc;

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
        public IEnumerable <FinancialInstrument> GetInstruments(){
            return db.FinancialInstruments.ToArray();
        }

        [HttpGet]
        [Route("/FinancialInstrument/{id}")]
        public ActionResult<FinancialInstrument> GetInstruments(int id){
            return db.FinancialInstruments.Find(id);}

        [HttpPut]
        [Route("/FinancialInstrument")]
        public ActionResult <FinancialInstrument> UpdateInstruments([FromBody] FinancialInstrument instruments){
            var existingInstruments = db.FinancialInstruments.Find(instruments.FinancialInstrumentID);
            if (existingInstruments == null) {
                db.FinancialInstruments.Add(instruments);
            } else {
                existingInstruments.FinancialInstrumentID = instruments.FinancialInstrumentID;
                instruments = null;
                db.FinancialInstruments.Update(existingInstruments); 
            }
            db.SaveChanges();
            return Ok(instruments);
        }
      [HttpDelete("/FinancialInstrument/delete/{id}")]
        public ActionResult <FinancialInstrument> DeleteInstruments(int id){
            var instrument = db.FinancialInstruments.Find(id);
            db.FinancialInstruments.Remove(instrument);
            db.SaveChanges();
            return NoContent();
        }


        [HttpDelete("/FinancialInstrument/delete-all")]
        public ActionResult DeleteInstruments(){
            var instruments = db.FinancialInstruments.ToArray();

            foreach (FinancialInstrument instrument in instruments)
            {
                if(instrument != null) {
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
            existingEuropeans.Expiration_Date = europeans.Expiration_Date;
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


    [HttpGet]
    [Route("/Load")]
    public ActionResult Load()
    {   

        // exchanges
        db.Exchanges.Add(new Exchange { Id = 1, Name = "my exchange1", Symbol = "EE1"});
        db.Exchanges.Add(new Exchange { Id = 2, Name = "my exchange2", Symbol = "EE2"});

        // markets
        db.Markets.Add(new TradingMarket { Id = 1, Name = "my market1-1", ExchangeId = 1 });
        db.Markets.Add(new TradingMarket { Id = 2, Name = "my market1-2", ExchangeId = 1 });
        db.Markets.Add(new TradingMarket { Id = 3, Name = "my market1-3", ExchangeId = 1 });
        db.Markets.Add(new TradingMarket { Id = 4, Name = "my market2-1", ExchangeId = 2 });
        db.Markets.Add(new TradingMarket { Id = 5, Name = "my market2-2", ExchangeId = 2 });
        db.Markets.Add(new TradingMarket { Id = 6, Name = "my market2-3", ExchangeId = 2 });



        // underlyings
        db.Underlyings.Add(new Underlying { FinancialInstrumentID = 1, Name= "my underlying1", Symbol = "UL1",  price = 50, TradingMarketId = 1 });
        db.Underlyings.Add(new Underlying { FinancialInstrumentID = 2, Name= "my underlying2", Symbol = "UL2",  price = 100, TradingMarketId = 2 });
        db.Underlyings.Add(new Underlying { FinancialInstrumentID = 3, Name= "my underlying3", Symbol = "UL3",  price = 20, TradingMarketId = 3 });
        db.Underlyings.Add(new Underlying { FinancialInstrumentID = 4, Name= "my underlying4", Symbol = "UL4",  price = 40, TradingMarketId = 1 });

        // European optiosn
        db.Europeans.Add(new European { FinancialInstrumentID = 5, Name = "European option 1", Symbol = "EO1", Expiration_Date = "11/1/2022", Strike = 50, Is_Call = true, volatility = 0.2, underlyingId = 1, TradingMarketId = 1 });
        db.Europeans.Add(new European { FinancialInstrumentID = 6, Name = "European option 2", Symbol = "EO2", Expiration_Date = "1/2/2020", Strike = 50, Is_Call = true, volatility = 0.3, underlyingId = 2, TradingMarketId = 2 });
        db.Europeans.Add(new European { FinancialInstrumentID = 7, Name = "European option 3", Symbol = "EO3", Expiration_Date = "10/14/2022", Strike = 50, Is_Call = true, volatility = 0.25, underlyingId = 3, TradingMarketId = 3 });
        db.Europeans.Add(new European { FinancialInstrumentID = 8, Name = "European option 4", Symbol = "EO4", Expiration_Date = "11/12/2021", Strike = 50, Is_Call = true, volatility = 0.19, underlyingId = 1, TradingMarketId = 1 });

        db.SaveChanges();
        return Ok();
    }
}
