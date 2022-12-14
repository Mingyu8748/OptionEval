namespace Monte_Carlo_Simulation;

class StockSimulator
{
    public static double[,] StockPath(Int64 N, double T, double mu, double sigma, double s0,
        double inputRandomNumber) //mu and sigma are assumed annualized values here
    {
        var dailymu = mu / 252;
        var dailysigma = sigma / (Math.Pow(252, 0.5));
        var columnNumber = (int)(T * 252) + 1;
        double[,] stockpriceArray = new double[N, columnNumber];

        for (var i = 0; i < N; i++)
        {
            const double dt = 0.00396825; //set daily timestep simulation
            var current = s0;
            stockpriceArray[i, 0] = current;
            for (var j = 1; j < columnNumber; j++)
            {
                var next = current * Math.Exp((mu - 0.5 * Math.Pow(sigma, 2)) * dt +
                                 sigma * Math.Pow(dt, 0.5) * RandomNumberGenerator.Generate());
                
                stockpriceArray[i, j] = next;
                current = next;

            }
        }

        return stockpriceArray;

    }

    public static void StockPath(object? obj)
    {
        var o = (Param)obj!;
        StockPath(o.n, o.t, o.mu, o.sigma, o.s0, RandomNumberGenerator.Generate());
    }
}

class Param
{
    public Int64 n { get; set; }
    public double t { get; set; }
    public double mu { get; set; }
    public double sigma { get; set; }
    public double s0 { get; set; }
    public bool varianceReduction { get; set; }

    public Param(long n, double t, double mu, double sigma, double s0, bool varianceReduction)
    {
        this.n = n;
        this.t = t;
        this.mu = mu;
        this.sigma = sigma;
        this.s0 = s0;
        this.varianceReduction = varianceReduction;
    }
}