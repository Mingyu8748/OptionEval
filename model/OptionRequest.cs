using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionEval.Model;

public class OptionRequest
{
    public OptionParam? optionParam { get; set; } 
    public Market? market { get; set; } 
    public Setting? setting { get; set; }

    public Int64 numberOfPath {get; set;}
    public double mu {get; set;}
    public double sigma {get; set;}
}