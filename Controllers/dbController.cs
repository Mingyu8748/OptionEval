using Microsoft.AspNetCore.Mvc;

namespace OptionDatabase.Controllers;

[ApiController]
[Route("[controller]")]

public class dbController : ControllerBase{
    FinanceContext db = new FinanceContext();

    [HttpGet]
    [Route("/Exchange")]
    public IEnumerable <Exchange> GetExchanges(){
        return db.Exchanges.ToArray();
    }

    [HttpGet]
    [Route("/Exchange/{id}")]
    public ActionResult<Exchange> GetExchanges(int id){
        return db.Exchanges.Find(id);
    }

    [HttpPut]
    [Route("/Exchange")]
    public ActionResult <Exchange> UpdateExchanges([FromBody] Exchange exchange){
        var existingExchange = db.Exchanges.Find(exchange.Id);
        if (existingExchange == null) {
            db.Exchanges.Add(exchange);
        } else {
            existingExchange.Name = exchange.Name;
            existingExchange.Symbol = exchange.Symbol;
            exchange = null;
            db.Exchanges.Update(existingExchange); 
        }

        db.SaveChanges();
        return Ok(exchange);
    }

    [HttpDelete("/Exchange/delete/{id}")]
    public ActionResult <Exchange> DeleteExchange(int id){
        var exchange = db.Exchanges.Find(id);
        db.Exchanges.Remove(exchange);
        db.SaveChanges();
        return NoContent();
    }
}