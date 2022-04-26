namespace Tester;

public class SolverDirector
{
    public SolverDirector(Config config, ModelBuilderDirector builderDirector, Random random, IResultViewer viewer)
    {
        Config = config;
        BuilderDirector = builderDirector;
        Random = random;
        Viewer = viewer;
    }

    public Config Config { get; }
    public ModelBuilderDirector BuilderDirector { get; }
    public Random Random { get; }
    public IResultViewer Viewer { get; }

    public void Solve()
    {
        Results results = new Results();
        List<ResultModel> gamsResults = new List<ResultModel>();
        List<ResultModel> algResults = new List<ResultModel>();
        List<Model> models = new List<Model>();
        foreach (var i in Enumerable.Range(1, Config.Itterations))
        {
            models.Add(BuilderDirector.BuildRandomModel());
        }

        // foreach (var i in Enumerable.Range(1, Config.Itterations))
        // {
        //     Console.WriteLine($"Solving gams #{i}");
        //     var solver = new GamsSolver(Config, models[i - 1]);
        //     gamsResults.Add(solver.Solve());
        // }
        // foreach (var i in Enumerable.Range(1, Config.Itterations))
        // {
        //     Console.WriteLine($"Solving alg #{i}");
        //     var solver = new AnnealingMethodSolver(Config, models[i - 1]);
        //     algResults.Add(solver.Solve());
        // }
        Parallel.ForEach(Enumerable.Range(1,Config.Itterations),(i) =>{
            Console.WriteLine($"Solving gams #{i}");
            var gamsSolver = new GamsSolver(Config,models[i - 1]);
            gamsResults.Add(gamsSolver.Solve());
            Console.WriteLine($"Solving gams #{i}... done");

        } );
        Parallel.ForEach(Enumerable.Range(1,Config.Itterations),(i) =>{
            Console.WriteLine($"Solving alg #{i}");
            var algSolver = new CombinationMethodSolver(Config,models[i - 1]);
            algResults.Add(algSolver.Solve());
            Console.WriteLine($"Solving alg #{i}... done");
        } );

        results.values.Add(new("gams", gamsResults));
        results.values.Add(new("alg", algResults));

        Viewer.View(results);

    }
}