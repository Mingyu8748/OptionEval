using Microsoft.AspNetCore.Mvc;
using OptionEval.Model;
using System.Text.Json;
using Monte_Carlo_Simulation;
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



        var optioncalc = new OptionCalculator();
        switch (request.setting.OptionType)
        {
            case "EuropeanCall":
                result.OptionValue = optioncalc.EuropeanCall(
                    request.numberOfPath,
                    request.optionParam.Tenor,
                    request.mu,
                    request.sigma,
                    request.optionParam.UnderlyingPrice,
                    request.optionParam.Strike,
                    request.optionParam.Rate,
                    price

                );
                break;

            case "EuropeanPut":
                result.OptionValue = optioncalc.EuropeanPut(
                    request.numberOfPath,
                    request.optionParam.Tenor,
                    request.mu,
                    request.sigma,
                    request.optionParam.UnderlyingPrice,
                    request.optionParam.Strike,
                    request.optionParam.Rate,
                    price);
                break;

            case "AsianCall":
                result.OptionValue = optioncalc.AsianCall(
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
                result.OptionValue = optioncalc.AsianPut(
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
                result.OptionValue = optioncalc.DigitalCall(
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
                result.OptionValue = optioncalc.DigitalCall(
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
                result.OptionValue = optioncalc.LookbackCall(
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
                result.OptionValue = optioncalc.LookbackPut(
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
                result.OptionValue = optioncalc.RangeOption(
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
