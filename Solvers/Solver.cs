namespace Tester;
public abstract class Solver : ISolver
{
    public Solver(Config config,Model model){
        Config = config;
        Model = model;
    }
    public Config Config { get; }
    public Model Model { get; }

    public abstract ResultModel Solve();
}