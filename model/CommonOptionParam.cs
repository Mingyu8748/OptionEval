using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionEval.Model;

public class OptionParam
{
    public bool? IsCall { get; set; } 
    public double UnderlyingPrice { get; set; }
    public double Strike { get; set; }
    public double Tenor { get; set; }
    public double Rate { get; set; }
    public double Payoff { get; set; }

}