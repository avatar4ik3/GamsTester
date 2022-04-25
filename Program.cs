using CommandLine;
using Microsoft.Extensions.DependencyInjection;
using GAMS;
using System;
using System.Collections;
using Tester;
using Microsoft.Extensions.Configuration;

namespace Tester;

public class Programm
{
    public static void Main(string[] args)
    {
        IServiceProvider provider = BuildServiceProvider();
        var dir = provider.GetService<SolverDirector>();
        dir!.Solve();
    }

    private static IServiceProvider BuildServiceProvider()
    {
        IServiceCollection collection = new ServiceCollection();
        IConfiguration configuration = new ConfigurationBuilder()
        .AddJsonFile("appSettings.json", optional: false)
        .Build();
        Config config = configuration.Get<Config>();
        collection.AddSingleton<Config>(config);
        collection.AddSingleton<ConsoleResultViewer>();
        Random rnd = null!;
        if (config.RandomSeed == -1)
        {
            rnd = new Random();
        }
        else
        {
            rnd = new Random(config.RandomSeed);
        }
        collection.AddSingleton<Random>(rnd);
        collection.AddSingleton<SolverDirector>();
        collection.AddSingleton<ModelBuilderDirector>();
        collection.AddSingleton<IResultViewer>(new ConsoleResultViewer());

        return collection.BuildServiceProvider();
    }
}


public class Config
{
    public String GamsWorkspaceFolder { get; set; }

    public String GamsFolder { get; set; }

    public int RandomSeed { get; set; }

    public int C { get; set; }

    public int Count_max { get; set; }

    public int Customers_Count { get; set; }
    public int Stores_Count { get; set; }

    public int W_Value_Model { get; set; }
    public int W_Min { get; set; }
    public int W_Max { get; set; }
    public int T_Min_Model { get; set; }
    public int T_Max_Model { get; set; }
    public int T_Min { get; set; }
    public int T_Max { get; set; }
    public int L { get; set; }

    public double Phi { get; set; }

    public int Itterations { get; set; }

    public string FileName { get; set; }
}



// var RND = new Random(1234567890);
// Model m = new Model();
// m.C = 10000;
// m.Count_Max = 3;
// m.Customers_Count = 10;
// m.Stores_Count = 10;
// int[][] t = new int[m.Stores_Count][];
// for (int i = 0; i < m.Stores_Count; ++i)
// {
//     t[i] = new int[m.Customers_Count];
// }
// for (int i = 0; i < m.Customers_Count; ++i)
// {
//     for (int j = 0; j < m.Stores_Count; ++j)
//     {
//         t[i][j] = RND.Next(20, 140);
//     }
// }
// m.T = t;
// int[] w = new int[m.Customers_Count];
// for (int i = 0; i < m.Customers_Count; ++i)
// {
//     w[i] = RND.Next(10, 30);
// }
// m.W = w;

// var tester = new GamsTester(2, m);
// //tester.Start();
// Console.WriteLine("Alg Started");
// var alg = new Alg(new Input(200,5,0.95,1000),m);
// var result = alg.Solve();
// Console.WriteLine("Alg ended");
// File.Open("/Test/outputalg.txt",FileMode.OpenOrCreate);
// using(var writer = new StreamWriter("/Test/outputalg.txt")){
//     writer.WriteLine("Z:");
//     for(int i = 0 ; i < m.Stores_Count; ++i){
//         writer.Write($"{result.z[i]} ");
//     }
//     writer.WriteLine("X:");
//     for(int i = 0 ; i < m.Stores_Count; ++i){
//         for(int j = 0; j < m.Customers_Count; ++j){
//             writer.Write($"{result.x[i][j]} ");
//         }
//         writer.WriteLine("");
//     }
//     writer.WriteLine($"Ro : {result.ro}");
// };




