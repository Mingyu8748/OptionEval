using Microsoft.AspNetCore.Mvc;

namespace OptionDatabase.Controllers;

[ApiController]
[Route("[controller]")]

public class dbController : ControllerBase{
    [HttpGet]
    [Route("/Exchange")]
    public IEnumerable <Exchange> GetExchanges(){
        FinanceContext db = new FinanceContext();
        return db.Exchanges.ToArray();
    }
}