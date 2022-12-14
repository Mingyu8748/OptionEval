namespace Monte_Carlo_Simulation;
using System.Linq;
public class OptionCalculator
{
    public static double EuropeanOptionCalc(Int64 N, double T, double mu, double sigma, double s0, double k,
        double r, double[,] price, bool iscall)
       
    {
        if (iscall == true)
        {
            var sum = 0.0;
            for (int i = 0; i < N; i++)
            {
                var payoff = price[i, (int)(T * 252)] - k;
                if (payoff > 0)
                {
                    sum += payoff;
                }
            }
            var mean = sum / N;
            var europrice = mean * Math.Exp(-r * T);
            return europrice;
        }
        else
        {
            var sum = 0.0;
            for (int i = 0; i < N; i++)
            {
                var payoff = k - price[i, (int)(T * 252)];
                if (payoff > 0)
                {
                    sum += payoff;
                }
            }
            var mean = sum / N;
            var europrice = mean * Math.Exp(-r * T);
            return europrice;
        }
        // payoff & option price calculation
        // var sum = 0.0;
        // for (int i = 0; i < N; i++)
        // {
        //     var payoff = price[i, (int)(T * 252)] - k;
        //     if (payoff > 0)
        //     {
        //         sum += payoff;
        //     }
        // }
        // var mean = sum / N;
        // var callprice = mean * Math.Exp(-r * T);
        //Control variate method 
        //disabled this in the final project implementation
        // var K_2 = 50;
        // var sum2 = 0.0;
        // for (int i = 0; i < N; i++)
        // {
        //     var payoff2 = price[i, (int) (T * 252)] - K_2;
        //     if (payoff2 > 0)
        //     {
        //         sum2 += payoff2;
        //     }
        // }
        // var bnum = (sum2 - sum2 / N) * (sum - mean);
        // var bdenom = Math.Pow((sum - mean), 2);
        // var b = bnum / bdenom;
        // // standard error of the call price
        // var diffsum = 0.0;
        // for (int i = 0; i < N; i++)
        // {
        //     var payoff = price[i, (int)(T * 252)] - k;
        //     var eachprice = payoff * Math.Exp(-r * T);
        //     var diff = Math.Pow((eachprice - callprice), 2);
        //     diffsum += diff;
        // }
        // var std = diffsum / (N - 1);
        // var std_error = std / Math.Pow(N, 0.5);
    }
    //     public double EuropeanPut(Int64 N, double T, double mu, double sigma, double s0, double k,
    //         double r, double[,] price)
    //     {
    //         var sum = 0.0;
    //         for (int i = 0; i < N; i++)
    //         {
    //             var payoff = k - price[i, (int)(T * 252)];
    //             if (payoff > 0)
    //             {
    //                 sum += payoff;
    //             }
    //         }
    //         var putprice = (sum / N) * Math.Exp(-r * T);
    //         // standard error of the call price
    //         var diffsum = 0.0;
    //         for (int i = 0; i < N; i++)
    //         {
    //             var payoff = price[i, (int)(T * 252)] - k;
    //             var eachprice = payoff * Math.Exp(-r * T);
    //             var diff = Math.Pow((eachprice - putprice), 2);
    //             diffsum += diff;
    //         }
    //         var std = diffsum / (N - 1);
    //         var std_error = std / Math.Pow(N, 0.5);
    //         return putprice;
    //     }
    public static double AsianOptionCalc(Int64 N, double T, double mu, double sigma, double s0, double k,
        double r, double[,] price, bool iscall)
    {
        double[,] AveragePrice = new double[N, 1];
        var sum = 0.0;
        for (int j = 0; j < N; j++)
        {
            for (int i = 0; i < (int)(T * 252) + 1; i++)
            {
                var price_path = price[j, i];
                sum += price_path;
            }
            AveragePrice[j, 0] = sum / (T * 252 + 1);
        }
        var sum2 = 0.0;
        if (iscall == true)
        {
            for (int i = 0; i < N; i++)
            {
                var payoff = Math.Max(AveragePrice[i, 0] - k, 0);
                sum2 += payoff;
            }
            var asianprice = (sum2 / N) * Math.Exp(-r * T);
            return asianprice;
        }
        else
        {
            for (int i = 0; i < N; i++)
            {
                var payoff = Math.Max(k - AveragePrice[i, 0], 0);
                sum2 += payoff;
            }
            var asianprice = (sum2 / N) * Math.Exp(-r * T);
            return asianprice;
        }
    }
    //     public double AsianPut(Int64 N, double T, double mu, double sigma, double s0, double k,
    //         double r, double[,] price)
    //     {
    //         double[,] AveragePrice = new double[N, 1];
    //         var sum = 0.0;
    //         for (int j = 0; j < N; j++)
    //         {
    //             for (int i = 0; i < (int)(T * 252) + 1; i++)
    //             {
    //                 var price_path = price[j, i];
    //                 sum += price_path;
    //             }
    //             AveragePrice[j, 0] = sum / (T * 252 + 1);
    //         }
    //         var sum2 = 0.0;
    //         for (int i = 0; i < N; i++)
    //         {
    //             var payoff = k - AveragePrice[i, 0];
    //             if (payoff > 0)
    //             {
    //                 sum2 += payoff;
    //             }
    //         }
    //         var putprice = (sum2 / N) * Math.Exp(-r * T);
    //         return putprice;
    //     }
        public static double DigitalOptionCalc(Int64 N, double T, double mu, double sigma, double s0, double k,
            double r, double fixedpayoff, double[,] price, bool iscall)
        {
            //Find the average price at node S_T
            var sum = 0.0;
            for (int i = 0; i < N; i++)
            {
                sum += price[i, (int)(T * 252)];
            }
            var avg_st = sum / N;
            if(iscall == true)
            {
                if(avg_st>k)
                {
                    var digitalprice = fixedpayoff * Math.Exp(-r * T);
                    return digitalprice;
                }
                else{
                    var digitalprice = 0;
                    return digitalprice;
                }
            }
            else
            {    if(avg_st<k)
                {
                    var digitalprice = fixedpayoff * Math.Exp(-r * T);
                    return digitalprice;
                }
                else{
                    var digitalprice = 0;
                    return digitalprice;
                }
            }
        }

    public static double LookbackOptionCalc(Int64 N, double T, double mu, double sigma, double s0, double k,
        double r, double[,] price, bool iscall)
    {
        var sum = 0.0;
        if (iscall == true)
        {
            for (int i = 0; i < N; i++)
            {
                var pricepath = new double[] { price[i, (int)(T * 252)] };
                var payoff = pricepath.Max() - k;
                if (payoff > 0)
                {
                    sum += payoff;
                }
            }
            var lookbackprice = (sum / N) * Math.Exp(-r * T);
            return lookbackprice;
        }
        else
        {
            for (int i = 0; i < N; i++)
            {
                var pricepath = new double[] { price[i, (int)(T * 252)] };
                var payoff = k - pricepath.Min();
                if (payoff > 0)
                {
                    sum += payoff;
                }
            }
            var lookbackprice = (sum / N) * Math.Exp(-r * T);
            return lookbackprice;
        }
    }
    public static double RangeOptionCalc(Int64 N, double T, double mu, double sigma, double s0, double k,
        double r, double[,] price)
    {
        var sum = 0.0;
        for (int i = 0; i < N; i++)
        {
            var pricepath = new double[] { price[i, (int)(T * 252)] };
            var payoff = pricepath.Max() - pricepath.Min();
            if (payoff > 0)
            {
                sum += payoff;
            }
        }
        var optionprice = (sum / N) * Math.Exp(-r * T);
        return optionprice;
    }
}
    // public double  Greeks_delta (Int64 N, double T, double mu, double sigma, double s0, double k, 
    // double r, double price, bool iscall, double perturbation)
    // {
    //     var OptionValueup = EuropeanOptionCalc(N,T,mu,sigma)
    // }
//         public Dictionary<string, double> CallGreeks(Int64 N, double T, double mu, double sigma, double s0, double k,
//             double r, double price)
//         {
//             var deltaS = s0 / 10; //10% change
//             var deltaSigma = sigma / 10; //10% change
//             //var deltaT = T / 10;
//             var deltaR = r / 10;
//             var price = StockSimulator.StockPath(N, T, mu, sigma, s0, RandomNumberGenerator.Generate(N, T));
//             var originalPrice = EuropeanCall(N, T, mu, sigma, s0, k, r, price);
//             // Delta
//             price = StockSimulator.StockPath(N, T, mu, sigma, s0, antithetic, RandomNumberGenerator.Generate(N, T));
//             var priceplusS0 = EuropeanCall(N, T, mu, sigma, s0 + deltaS, k, r, price);
//             price = StockSimulator.StockPath(N, T, mu, sigma, s0, antithetic, RandomNumberGenerator.Generate(N, T));
//             var priceminusS0 = EuropeanCall(N, T, mu, sigma, s0 - deltaS, k, r, price);
//             var delta = (priceplusS0 - priceminusS0) / (2 * deltaS);
//             //gamma
//             var gamma = (priceplusS0 - 2 * originalPrice + priceminusS0) / Math.Pow(deltaS, 2);
//             // vega
//             price = StockSimulator.StockPath(N, T, mu, sigma, s0, antithetic, RandomNumberGenerator.Generate(N, T));
//             var priceplusSigma = EuropeanCall(N, T, mu, sigma + deltaSigma, s0, k, r, price);
//             price = StockSimulator.StockPath(N, T, mu, sigma, s0, antithetic, RandomNumberGenerator.Generate(N, T));
//             var priceminusSigma = EuropeanCall(N, T, mu, sigma - deltaSigma, s0, k, r, price);
//             var vega = (priceplusSigma - priceminusSigma) / (2 * deltaSigma);
//             // Theta
//             // //price = StockSimulator.StockPath(N, T, mu, sigma, s0, antithetic, RandomNumberGenerator.Generate(N, T));
//             // var priceplusT = EuropeanCall(N, T + deltaT, mu, sigma, s0, k, r, price)["call option price"];
//             //var theta = (priceplusT - originalPrice) / deltaT;
//             // Rho 
//             price = StockSimulator.StockPath(N, T, mu, sigma, s0, antithetic, RandomNumberGenerator.Generate(N, T));
//             var priceplusR = EuropeanCall(N, T, mu, sigma, s0, k, r + deltaR, price);
//             price = StockSimulator.StockPath(N, T, mu, sigma, s0, antithetic, RandomNumberGenerator.Generate(N, T));
//             var priceminusR = EuropeanCall(N, T, mu, sigma, s0, k, r - deltaR, price);
//             var rho = (priceplusR - priceminusR) / (2 * deltaR);
//             return new Dictionary<string, double>
//             {
//                 {"delta", delta},
//                 {"gamma", gamma},
//                 {"vega", vega},
//                 {"rho", rho},
//             };
//         }
//         public Dictionary<string, double> PutGreeks(long N, double T, double mu, double sigma, double s0, double k,
//             double r, bool antithetic)
//         {
//             var deltaS = s0 / 10; //10% change
//             var deltaSigma = sigma / 10; //10% change
//             var deltaT = T / 10;
//             var deltaR = r / 10;
//             var price = StockSimulator.StockPath(N, T, mu, sigma, s0, antithetic, RandomNumberGenerator.Generate(N, T));
//             var originalPrice = EuropeanPut(N, T, mu, sigma, s0, k, r, price);
//             // Delta
//             price = StockSimulator.StockPath(N, T, mu, sigma, s0, antithetic, RandomNumberGenerator.Generate(N, T));
//             var priceplusS0 = EuropeanPut(N, T, mu, sigma, s0 + deltaS, k, r, price);
//             price = StockSimulator.StockPath(N, T, mu, sigma, s0, antithetic, RandomNumberGenerator.Generate(N, T));
//             var priceminusS0 = EuropeanPut(N, T, mu, sigma, s0 - deltaS, k, r, price);
//             var delta = (priceplusS0 - priceminusS0) / (2 * deltaS);
//             //gamma
//             var gamma = (priceplusS0 - 2 * originalPrice + priceminusS0) / Math.Pow(deltaS, 2);
//             // vega
//             price = StockSimulator.StockPath(N, T, mu, sigma, s0, antithetic, RandomNumberGenerator.Generate(N, T));
//             var priceplusSigma = EuropeanPut(N, T, mu, sigma + deltaSigma, s0, k, r, price);
//             price = StockSimulator.StockPath(N, T, mu, sigma, s0, antithetic, RandomNumberGenerator.Generate(N, T));
//             var priceminusSigma = EuropeanPut(N, T, mu, sigma - deltaSigma, s0, k, r, price);
//             var vega = (priceplusSigma - priceminusSigma) / (2 * deltaSigma);
//             // Theta
//             price = StockSimulator.StockPath(N, T, mu, sigma, s0, antithetic, RandomNumberGenerator.Generate(N, T));
//             var priceplusT = EuropeanPut(N, T + deltaT, mu, sigma, s0, k, r, price);
//             var theta = (priceplusT - originalPrice) / deltaT;
//             // Rho
//             price = StockSimulator.StockPath(N, T, mu, sigma, s0, antithetic, RandomNumberGenerator.Generate(N, T));
//             var priceplusR = EuropeanPut(N, T, mu, sigma, s0, k, r + deltaR, price);
//             price = StockSimulator.StockPath(N, T, mu, sigma, s0, antithetic, RandomNumberGenerator.Generate(N, T));
//             var priceminusR = EuropeanPut(N, T, mu, sigma, s0, k, r - deltaR, price);
//             var rho = (priceplusR - priceminusR) / (2 * deltaR);
//             return new Dictionary<string, double>
//             {
//                 {"delta", delta},
//                 {"gamma", gamma},
//                 {"vega", vega},
//                 {"theta", theta},
//                 {"rho", rho},
//             };
//         }
//     }
    // //     public double DigitalCall(Int64 N, double T, double mu, double sigma, double s0, double k,
    // //         double r, double p, double[,] price)
    // //     {
    // //         var sum = 0.0;
    // //         for (int i = 0; i < N; i++)
    // //         {
    // //             var payoff = price[i, (int)(T * 252)] - k;
    // //             if (payoff > 0)
    // //             {
    // //                 sum += p;
    // //             }
    // //         }
    // //         var optionprice = (sum / N) * Math.Exp(-r * T);
    // //         return optionprice;
    // //     }
    //     public double LookbackOptionCalc(Int64 N, double T, double mu, double sigma, double s0, double k,
    //         double r, double[,] price, bool iscall)
    //     {
    //         if(iscall == true)
    //         {
    //             var sum = 0.0;
    //             double[,] maxprice = new double [N,1];
    //             double[] eachpath = new double[N];
    //             for (int i = 0; i < N; i++)
    //             {
    //                 for (int j=0; j < (int)(T * 252) + 1; j++)
    //                 {
    //                     eachpath[j] = price [i,j] ;
    //                 }
    //                 maxprice[]
    //                 eachpath [i,0] = price.Max();
    //                 Maxprice[i,0] = Math.Max(price[i, (int)(T * 252)];
    //             var payoff = pricepath.Max() - k;
    //             if (payoff > 0)
    //             {
    //                 sum += payoff;
    //             }
    //             }
    //         }
    //         for (int i = 0; i < N; i++)
    //         {
    //             var pricepath = new double[] { price[i, (int)(T * 252)] };
    //             var payoff = pricepath.Max() - k;
    //             if (payoff > 0)
    //             {
    //                 sum += payoff;
    //             }
    //         }
    //         var optionprice = (sum / N) * Math.Exp(-r * T);
    //         return optionprice;
    //     }
