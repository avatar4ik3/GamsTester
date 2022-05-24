namespace Tester.DirectorSteps;

public abstract class AlgSolverStep<T> : SolverStep where T : ISolver
{
    protected AlgSolverStep(string name, Config config, List<Model> models) : base(1, name, config, models)
    {
    }

    public abstract T GetInstance(int index);

    public override ResultsElements Step()
    {
        var list = new List<ResultElement>();
        Parallel.ForEach(Enumerable.Range(0, Models.Count() - 1), i => {
            Console.WriteLine($"{Name} : {i} solving.");
            var solver = GetInstance(i);
            var result = solver.Solve();
            list.Add(new(i,result));
            Console.WriteLine($"{Name} : {i} solved.");

        });
        var output = new ResultsElements(Name);
        output.Elements.AddRange(list);
        return output;
    }
}