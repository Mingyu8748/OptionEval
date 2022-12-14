namespace Monte_Carlo_Simulation;

class RandomNumberGenerator
{
    public static double Generate()
    {
        var rand = new Random();
        return  Math.Sqrt(-2.0 * Math.Log(rand.NextDouble())) * Math.Sin(2.0 * Math.PI * rand.NextDouble());
    }

    internal static void Generate(int v, double? tenor)
    {
        throw new NotImplementedException();
    }
}