using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionEval.Model;

public class Market
{
    public string? Name { get; set; } //Option or corn market
    public string? Exchange { get; set; } //which exchange market it's on, e.g NYE or CQG
    public string? Symbol  { get; set; }
    public string? Units { get; set; }
}
