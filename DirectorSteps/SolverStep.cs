using GAMS;

namespace Tester.DirectorSteps;

public abstract class SolverStep{
    public int Number {get;init;}

    public string Name {get;init;}
    public Config Config { get; }
    public List<Model> Models { get; }

    public SolverStep(int number, string name,Config config,List<Model> models)
    {
        Number = number;
        Name = name;
        Config = config;
        Models = models;
    }

    public abstract ResultsElements Step();
}
