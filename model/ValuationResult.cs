using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionEval.Model;

public class ValuationResult
{
    public double OptionValue { get; set; }
    public double StandardError{ get; set; }
    public double GreeksDelta{ get; set; }
    public double GreeksGamma{ get; set; }
    public double GreeksTheta{ get; set; }
    public double GreeksVega{ get; set; }
    public double Greeksrho{ get; set; }

}