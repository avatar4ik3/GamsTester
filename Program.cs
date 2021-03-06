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
        using  FileStream filestream = new FileStream("out.txt", FileMode.Create);
        using var streamwriter = new StreamWriter(filestream);
        streamwriter.AutoFlush = true;
        Console.SetOut(streamwriter);
        Console.SetError(streamwriter);

        IServiceProvider provider = BuildServiceProvider();
        var dir = provider.GetService<ConsoleResultViewer>();
        dir!.View();

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
        collection.AddSingleton<ConsoleResultViewer>();
        collection.AddSingleton<GAMSWorkspace>(new GAMSWorkspace(config.GamsWorkspaceFolder, config.GamsFolder));
        collection.AddSingleton<RandomModelDataProvider>();
        collection.AddSingleton<StepsProvider>();
        collection.AddSingleton<string[]>(config.Solvers);
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

    public string[] Solvers { get; set; }
}



