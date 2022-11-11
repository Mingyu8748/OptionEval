using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptionEval.Model;

public class Setting
{
    public string? OptionType { get; set; } 
    public bool EnableControlVariate { get; set; }

    public bool Antithetic { get; set; }
    public bool EnabelMultithreading { get; set; }
}


