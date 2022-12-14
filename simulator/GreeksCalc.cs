namespace Monte_Carlo_Simulation;

public class GreeksCalc
{
            public Dictionary<string, double> EuropeanGreeks(Int64 N, double T, double mu, double sigma, double s0, double k,
            double r, double[,] price, bool iscall)
        {
            //set up the perturbation first
            var delta_s = s0 / 100; //10% change
            var delta_sigma = sigma / 100; //10% change
            var delta_T = T / 100;
            var delta_r = r / 100;
            // Greeks delta calculation

            //stock simulation with perturbations
            var price_plus_deltas= StockSimulator.StockPath(N, T, mu, sigma, s0+delta_s, RandomNumberGenerator.Generate());
            var price_minus_deltas = StockSimulator.StockPath(N, T, mu, sigma, s0-delta_s, RandomNumberGenerator.Generate());
            
            //euoprean option price calculation

            var optionprice = OptionCalculator.EuropeanOptionCalc(N, T, mu, sigma, s0, k, r, price, iscall);
            var optionprice_plus_deltas = OptionCalculator.EuropeanOptionCalc(N, T, mu, sigma, s0, k, r, price_plus_deltas, iscall);
            var optionprice_minus_deltas = OptionCalculator.EuropeanOptionCalc(N, T, mu, sigma, s0, k, r, price_minus_deltas, iscall);
         
            var delta = (optionprice_plus_deltas - optionprice_minus_deltas) / (2 * delta_s);// Delta      
            var gamma = (optionprice_plus_deltas - 2 * optionprice + optionprice_minus_deltas) / Math.Pow(delta_s, 2); //gamma

            //vega calculation below 
            var price_plus_delta_sigma= StockSimulator.StockPath(N, T, mu, sigma+delta_sigma, s0, RandomNumberGenerator.Generate());
            var price_minus_delta_simga = StockSimulator.StockPath(N, T, mu, sigma-delta_sigma, s0, RandomNumberGenerator.Generate());

            var optionprice_plus_sigma = OptionCalculator.EuropeanOptionCalc(N, T, mu, sigma+delta_sigma, s0, k, r, price_plus_delta_sigma, iscall);
            var optionprice_minus_sigma = OptionCalculator.EuropeanOptionCalc(N, T, mu, sigma-delta_sigma, s0, k, r, price_plus_delta_sigma, iscall);
        
            var vega = (optionprice_plus_sigma - optionprice_plus_sigma) / (2 * delta_sigma);
            
            //theta calculation below 
            var price_plus_delta_T= StockSimulator.StockPath(N, T+delta_T, mu, sigma, s0, RandomNumberGenerator.Generate());
            var price_minus_delta_T = StockSimulator.StockPath(N, T-delta_T, mu, sigma, s0, RandomNumberGenerator.Generate());

            var optionprice_plus_deltaT = OptionCalculator.EuropeanOptionCalc(N, T+delta_T, mu, sigma, s0, k, r, price_plus_delta_T, iscall);
            var optionprice_minus_deltaT = OptionCalculator.EuropeanOptionCalc(N, T-delta_T, mu, sigma, s0, k, r, price_minus_delta_T, iscall);
        
            var theta = (optionprice_plus_sigma - optionprice_plus_sigma) / (2 * delta_T);
            
            //rho calculation below 

            var optionprice_plus_deltar = OptionCalculator.EuropeanOptionCalc(N, T, mu, sigma, s0, k, r+delta_r, price, iscall);
            var optionprice_minus_deltar = OptionCalculator.EuropeanOptionCalc(N, T, mu, sigma, s0, k, r-delta_r, price, iscall);
        
            var rho = (optionprice_plus_deltar - optionprice_minus_deltar) / (2 * delta_r);


            return new Dictionary<string, double>
            {
                {"delta", delta},
                {"theta", theta},
                {"gamma", gamma},
                {"vega", vega},
                {"rho", rho},
            };
        }




public Dictionary<string, double> AisanGreeks(Int64 N, double T, double mu, double sigma, double s0, double k,
            double r, double[,] price, bool iscall, double[,]random_number)
        {
            //set up the perturbation first
            var delta_s = s0 / 100; //10% change
            var delta_sigma = sigma / 100; //10% change
            var delta_T = T / 100;
            var delta_r = r / 100;
            // Greeks delta calculation

            //stock simulation with perturbations
            var price_plus_deltas= StockSimulator.StockPath(N, T, mu, sigma, s0+delta_s, RandomNumberGenerator.Generate());
            var price_minus_deltas = StockSimulator.StockPath(N, T, mu, sigma, s0-delta_s, RandomNumberGenerator.Generate());

            var optionprice = OptionCalculator.AsianOptionCalc(N, T, mu, sigma, s0, k, r, price, iscall);
            var optionprice_plus_deltas = OptionCalculator.AsianOptionCalc(N, T, mu, sigma, s0, k, r, price_plus_deltas, iscall);
            var optionprice_minus_deltas = OptionCalculator.AsianOptionCalc(N, T, mu, sigma, s0, k, r, price_minus_deltas, iscall);
         
            var delta = (optionprice_plus_deltas - optionprice_minus_deltas) / (2 * delta_s);// Delta      
            var gamma = (optionprice_plus_deltas - 2 * optionprice + optionprice_minus_deltas) / Math.Pow(delta_s, 2); //gamma

            //vega calculation below 
            var price_plus_delta_sigma= StockSimulator.StockPath(N, T, mu, sigma+delta_sigma, s0, RandomNumberGenerator.Generate());
            var price_minus_delta_simga = StockSimulator.StockPath(N, T, mu, sigma-delta_sigma, s0, RandomNumberGenerator.Generate());

            var optionprice_plus_sigma = OptionCalculator.AsianOptionCalc(N, T, mu, sigma+delta_sigma, s0, k, r, price_plus_delta_sigma, iscall);
            var optionprice_minus_sigma = OptionCalculator.AsianOptionCalc(N, T, mu, sigma-delta_sigma, s0, k, r, price_plus_delta_sigma, iscall);
        
            var vega = (optionprice_plus_sigma - optionprice_plus_sigma) / (2 * delta_sigma);
            
            //theta calculation below 
            var price_plus_delta_T= StockSimulator.StockPath(N, T+delta_T, mu, sigma, s0, RandomNumberGenerator.Generate());
            var price_minus_delta_T = StockSimulator.StockPath(N, T-delta_T, mu, sigma, s0, RandomNumberGenerator.Generate());

            var optionprice_plus_deltaT = OptionCalculator.AsianOptionCalc(N, T+delta_T, mu, sigma, s0, k, r, price_plus_delta_T, iscall);
            var optionprice_minus_deltaT = OptionCalculator.AsianOptionCalc(N, T-delta_T, mu, sigma, s0, k, r, price_minus_delta_T, iscall);
        
            var theta = (optionprice_plus_sigma - optionprice_plus_sigma) / (2 * delta_T);
            
            //rho calculation below 

            var optionprice_plus_deltar = OptionCalculator.AsianOptionCalc(N, T, mu, sigma, s0, k, r+delta_r, price, iscall);
            var optionprice_minus_deltar = OptionCalculator.AsianOptionCalc(N, T, mu, sigma, s0, k, r-delta_r, price, iscall);
        
            var rho = (optionprice_plus_deltar - optionprice_minus_deltar) / (2 * delta_r);


            return new Dictionary<string, double>
            {
                {"delta", delta},
                {"gamma", gamma},
                {"vega", vega},
                {"rho", rho},
            };
        }

public Dictionary<string, double> DigitalGreeks(Int64 N, double T, double mu, double sigma, double s0, double k,
            double r, double fixed_payoff, double[,] price, bool iscall, double[,]random_number)
        {
            //set up the perturbation first
            var delta_s = s0 / 100; //10% change
            var delta_sigma = sigma / 100; //10% change
            var delta_T = T / 100;
            var delta_r = r / 100;
            // Greeks delta calculation

            //stock simulation with perturbations
            var price_plus_deltas= StockSimulator.StockPath(N, T, mu, sigma, s0+delta_s, RandomNumberGenerator.Generate());
            var price_minus_deltas = StockSimulator.StockPath(N, T, mu, sigma, s0-delta_s, RandomNumberGenerator.Generate());

            var optionprice = OptionCalculator.DigitalOptionCalc(N, T, mu, sigma, s0, k, r, fixed_payoff,price, iscall);
            var optionprice_plus_deltas = OptionCalculator.DigitalOptionCalc(N, T, mu, sigma, s0, k, r, fixed_payoff,price_plus_deltas, iscall);
            var optionprice_minus_deltas = OptionCalculator.DigitalOptionCalc(N, T, mu, sigma, s0, k, r, fixed_payoff,price_minus_deltas, iscall);
         
            var delta = (optionprice_plus_deltas - optionprice_minus_deltas) / (2 * delta_s);// Delta      
            var gamma = (optionprice_plus_deltas - 2 * optionprice + optionprice_minus_deltas) / Math.Pow(delta_s, 2); //gamma

            //vega calculation below 
            var price_plus_delta_sigma= StockSimulator.StockPath(N, T, mu, sigma+delta_sigma, s0, RandomNumberGenerator.Generate());
            var price_minus_delta_simga = StockSimulator.StockPath(N, T, mu, sigma-delta_sigma, s0, RandomNumberGenerator.Generate());

            var optionprice_plus_sigma = OptionCalculator.DigitalOptionCalc(N, T, mu, sigma+delta_sigma, s0, k, r, fixed_payoff,price_plus_delta_sigma, iscall);
            var optionprice_minus_sigma = OptionCalculator.DigitalOptionCalc(N, T, mu, sigma-delta_sigma, s0, k, r, fixed_payoff,price_plus_delta_sigma, iscall);
        
            var vega = (optionprice_plus_sigma - optionprice_plus_sigma) / (2 * delta_sigma);
            
            //theta calculation below 
            var price_plus_delta_T= StockSimulator.StockPath(N, T+delta_T, mu, sigma, s0, RandomNumberGenerator.Generate());
            var price_minus_delta_T = StockSimulator.StockPath(N, T-delta_T, mu, sigma, s0, RandomNumberGenerator.Generate());

            var optionprice_plus_deltaT = OptionCalculator.DigitalOptionCalc(N, T+delta_T, mu, sigma, s0, k, r,fixed_payoff, price_plus_delta_T, iscall);
            var optionprice_minus_deltaT = OptionCalculator.DigitalOptionCalc(N, T-delta_T, mu, sigma, s0, k, r, fixed_payoff,price_minus_delta_T, iscall);
        
            var theta = (optionprice_plus_sigma - optionprice_plus_sigma) / (2 * delta_T);
            
            //rho calculation below 

            var optionprice_plus_deltar = OptionCalculator.DigitalOptionCalc(N, T, mu, sigma, s0, k, r+delta_r, fixed_payoff,price, iscall);
            var optionprice_minus_deltar = OptionCalculator.DigitalOptionCalc(N, T, mu, sigma, s0, k, r-delta_r, fixed_payoff,price, iscall);
        
            var rho = (optionprice_plus_deltar - optionprice_minus_deltar) / (2 * delta_r);


            return new Dictionary<string, double>
            {
                {"delta", delta},
                {"gamma", gamma},
                {"vega", vega},
                {"rho", rho},
            };
        }

public Dictionary<string, double> LookBackGreeks(Int64 N, double T, double mu, double sigma, double s0, double k,
            double r, double[,] price, bool iscall, double[,]random_number)
        {
            //set up the perturbation first
            var delta_s = s0 / 100; //10% change
            var delta_sigma = sigma / 100; //10% change
            var delta_T = T / 100;
            var delta_r = r / 100;
            // Greeks delta calculation

            //stock simulation with perturbations
            var price_plus_deltas= StockSimulator.StockPath(N, T, mu, sigma, s0+delta_s, RandomNumberGenerator.Generate());
            var price_minus_deltas = StockSimulator.StockPath(N, T, mu, sigma, s0-delta_s, RandomNumberGenerator.Generate());

            var optionprice = OptionCalculator.LookbackOptionCalc(N, T, mu, sigma, s0, k, r, price, iscall);
            var optionprice_plus_deltas = OptionCalculator.LookbackOptionCalc(N, T, mu, sigma, s0, k, r, price_plus_deltas, iscall);
            var optionprice_minus_deltas = OptionCalculator.LookbackOptionCalc(N, T, mu, sigma, s0, k, r, price_minus_deltas, iscall);
         
            var delta = (optionprice_plus_deltas - optionprice_minus_deltas) / (2 * delta_s);// Delta      
            var gamma = (optionprice_plus_deltas - 2 * optionprice + optionprice_minus_deltas) / Math.Pow(delta_s, 2); //gamma

            //vega calculation below 
            var price_plus_delta_sigma= StockSimulator.StockPath(N, T, mu, sigma+delta_sigma, s0, RandomNumberGenerator.Generate());
            var price_minus_delta_simga = StockSimulator.StockPath(N, T, mu, sigma-delta_sigma, s0, RandomNumberGenerator.Generate());

            var optionprice_plus_sigma = OptionCalculator.LookbackOptionCalc(N, T, mu, sigma+delta_sigma, s0, k, r, price_plus_delta_sigma, iscall);
            var optionprice_minus_sigma = OptionCalculator.LookbackOptionCalc(N, T, mu, sigma-delta_sigma, s0, k, r, price_plus_delta_sigma, iscall);
        
            var vega = (optionprice_plus_sigma - optionprice_plus_sigma) / (2 * delta_sigma);
            
            //theta calculation below 
            var price_plus_delta_T= StockSimulator.StockPath(N, T+delta_T, mu, sigma, s0, RandomNumberGenerator.Generate());
            var price_minus_delta_T = StockSimulator.StockPath(N, T-delta_T, mu, sigma, s0, RandomNumberGenerator.Generate());

            var optionprice_plus_deltaT = OptionCalculator.LookbackOptionCalc(N, T+delta_T, mu, sigma, s0, k, r, price_plus_delta_T, iscall);
            var optionprice_minus_deltaT = OptionCalculator.LookbackOptionCalc(N, T-delta_T, mu, sigma, s0, k, r, price_minus_delta_T, iscall);
        
            var theta = (optionprice_plus_sigma - optionprice_plus_sigma) / (2 * delta_T);
            
            //rho calculation below 

            var optionprice_plus_deltar = OptionCalculator.LookbackOptionCalc(N, T, mu, sigma, s0, k, r+delta_r, price, iscall);
            var optionprice_minus_deltar = OptionCalculator.LookbackOptionCalc(N, T, mu, sigma, s0, k, r-delta_r, price, iscall);
        
            var rho = (optionprice_plus_deltar - optionprice_minus_deltar) / (2 * delta_r);


            return new Dictionary<string, double>
            {
                {"delta", delta},
                {"gamma", gamma},
                {"vega", vega},
                {"rho", rho},
            };
        }
        public Dictionary<string, double> RangeGreeks(Int64 N, double T, double mu, double sigma, double s0, double k,
            double r, double[,] price, double[,]random_number)
        {
            //set up the perturbation first
            var delta_s = s0 / 100; //10% change
            var delta_sigma = sigma / 100; //10% change
            var delta_T = T / 100;
            var delta_r = r / 100;
            // Greeks delta calculation

            //stock simulation with perturbations
            var price_plus_deltas= StockSimulator.StockPath(N, T, mu, sigma, s0+delta_s, RandomNumberGenerator.Generate());
            var price_minus_deltas = StockSimulator.StockPath(N, T, mu, sigma, s0-delta_s, RandomNumberGenerator.Generate());
            
            //euoprean option price calculation

            var optionprice = OptionCalculator.RangeOptionCalc(N, T, mu, sigma, s0, k, r, price);
            var optionprice_plus_deltas = OptionCalculator.RangeOptionCalc(N, T, mu, sigma, s0, k, r, price_plus_deltas);
            var optionprice_minus_deltas = OptionCalculator.RangeOptionCalc(N, T, mu, sigma, s0, k, r, price_minus_deltas);
         
            var delta = (optionprice_plus_deltas - optionprice_minus_deltas) / (2 * delta_s);// Delta      
            var gamma = (optionprice_plus_deltas - 2 * optionprice + optionprice_minus_deltas) / Math.Pow(delta_s, 2); //gamma

            //vega calculation below 
            var price_plus_delta_sigma= StockSimulator.StockPath(N, T, mu, sigma+delta_sigma, s0, RandomNumberGenerator.Generate());
            var price_minus_delta_simga = StockSimulator.StockPath(N, T, mu, sigma-delta_sigma, s0, RandomNumberGenerator.Generate());

            var optionprice_plus_sigma = OptionCalculator.RangeOptionCalc(N, T, mu, sigma+delta_sigma, s0, k, r, price_plus_delta_sigma);
            var optionprice_minus_sigma = OptionCalculator.RangeOptionCalc(N, T, mu, sigma-delta_sigma, s0, k, r, price_plus_delta_sigma);
        
            var vega = (optionprice_plus_sigma - optionprice_plus_sigma) / (2 * delta_sigma);
            
            //theta calculation below 
            var price_plus_delta_T= StockSimulator.StockPath(N, T+delta_T, mu, sigma, s0, RandomNumberGenerator.Generate());
            var price_minus_delta_T = StockSimulator.StockPath(N, T-delta_T, mu, sigma, s0, RandomNumberGenerator.Generate());

            var optionprice_plus_deltaT = OptionCalculator.RangeOptionCalc(N, T+delta_T, mu, sigma, s0, k, r, price_plus_delta_T);
            var optionprice_minus_deltaT = OptionCalculator.RangeOptionCalc(N, T-delta_T, mu, sigma, s0, k, r, price_minus_delta_T);
        
            var theta = (optionprice_plus_sigma - optionprice_plus_sigma) / (2 * delta_T);
            
            //rho calculation below 

            var optionprice_plus_deltar = OptionCalculator.RangeOptionCalc(N, T, mu, sigma, s0, k, r+delta_r, price);
            var optionprice_minus_deltar = OptionCalculator.RangeOptionCalc(N, T, mu, sigma, s0, k, r-delta_r, price);
        
            var rho = (optionprice_plus_deltar - optionprice_minus_deltar) / (2 * delta_r);


            return new Dictionary<string, double>
            {
                {"delta", delta},
                {"gamma", gamma},
                {"vega", vega},
                {"rho", rho},
            };
        }
}