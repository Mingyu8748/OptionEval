using Microsoft.AspNetCore.Mvc;
using OptionEval.Model;
using System.Text.Json;
using Monte_Carlo_Simulation;
using OptionEval.Model;

namespace OptionEval.Controllers;

[ApiController]
[Route("[controller]")]
public class OptionEvalController : ControllerBase
{
    private readonly ILogger<OptionEvalController> _logger;

    public OptionEvalController(ILogger<OptionEvalController> logger)
    {
        _logger = logger;
    }

    [Route("/EvaluateOption")]
    [HttpPost]
    public ActionResult<string> Get([FromBody] OptionRequest request)
    {
        // OptionRequest? request = JsonSerializer.Deserialize<OptionRequest>(requestString);
        ValuationResult result = new ValuationResult();

        double[,] price = StockSimulator.StockPath(
           request.numberOfPath,
           request.optionParam.Tenor,
           request.mu,
           request.sigma,
           request.optionParam.UnderlyingPrice,
           request.setting.Antithetic,
           RandomNumberGenerator.Generate(request.numberOfPath, request.optionParam.Tenor));



        var option = new Option();
        switch (request.setting.OptionType)
        {
            case "AsianCall":
                result.OptionValue = option.AsianCall(
                    request.numberOfPath,
                    request.optionParam.Tenor,
                    request.mu,
                    request.sigma,
                    request.optionParam.UnderlyingPrice,
                    request.optionParam.Strike,
                    request.optionParam.Rate,
                    price);
                break;
            case "AsianPut":
                result.OptionValue = option.AsianPut(
                   request.numberOfPath,
                   request.optionParam.Tenor,
                   request.mu,
                   request.sigma,
                   request.optionParam.UnderlyingPrice,
                   request.optionParam.Strike,
                   request.optionParam.Rate,
                   price);
                break;
            case "DigitalCall":
                result.OptionValue = option.DigitalCall(
                   request.numberOfPath,
                   request.optionParam.Tenor,
                   request.mu,
                   request.sigma,
                   request.optionParam.UnderlyingPrice,
                   request.optionParam.Strike,
                   request.optionParam.Rate,
                   request.optionParam.Payoff,
                   price);
                break;
            case "DigitalPut":
                result.OptionValue = option.DigitalCall(
                   request.numberOfPath,
                   request.optionParam.Tenor,
                   request.mu,
                   request.sigma,
                   request.optionParam.UnderlyingPrice,
                   request.optionParam.Strike,
                   request.optionParam.Rate,
                   request.optionParam.Payoff,
                   price);
                break;
            case "LookbackCall":
                result.OptionValue = option.LookbackCall(
                   request.numberOfPath,
                   request.optionParam.Tenor,
                   request.mu,
                   request.sigma,
                   request.optionParam.UnderlyingPrice,
                   request.optionParam.Strike,
                   request.optionParam.Rate,
                   price);
                break;
            case "LookbackPut":
                result.OptionValue = option.LookbackPut(
                   request.numberOfPath,
                   request.optionParam.Tenor,
                   request.mu,
                   request.sigma,
                   request.optionParam.UnderlyingPrice,
                   request.optionParam.Strike,
                   request.optionParam.Rate,
                   price);
                break;
            case "RangeOption":
                result.OptionValue = option.RangeOption(
                   request.numberOfPath,
                   request.optionParam.Tenor,
                   request.mu,
                   request.sigma,
                   request.optionParam.UnderlyingPrice,
                   request.optionParam.Strike,
                   request.optionParam.Rate,
                   price);
                break;
            default:
                break;
        }




        return Ok(JsonSerializer.Serialize(result));
    }
}
