namespace Monte_Carlo_Simulation;

class StockSimulator
{
    public static double[,] StockPath(Int64 N, double T, double mu, double sigma, double s0,
        double[,] inputRandomNumber) //mu and sigma are assumed annualized values here
    {
        var dailymu = mu / 252;
        var dailysigma = sigma / (Math.Pow(252, 0.5));
        double[,] stockpriceArray = new double[N, (int)(T * 252) + 1];
        // assign s0 to the first column of simulated stock price metric
        var index = 1;
        while (index <= N)
        {
            stockpriceArray[index - 1, 0] = s0;
            index++;
        }

        const double dt = 0.00396825; //set daily timestep simulation
        var row = 1;
        while (row <= N)
        {
            var col = 2;
            while (col <= (int)(T * 252) + 1)
            {
                stockpriceArray[row - 1, col - 1] =
                    stockpriceArray[row - 1, col - 2] *
                    Math.Exp((mu - 0.5 * Math.Pow(sigma, 2)) * dt +
                             sigma * Math.Pow(dt, 0.5) *
                             inputRandomNumber[row - 1, col - 2]);
                col++;
            }

            row++;
        }
        return stockpriceArray;

    }

public static void StockPath(object? obj)
{
    var o = (Param)obj!;
    StockPath(o.n, o.t, o.mu, o.sigma, o.s0, RandomNumberGenerator.Generate(o.n, o.t));
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