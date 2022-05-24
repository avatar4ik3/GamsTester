using GAMS;
using Tester.DirectorSteps;
using System.Linq;

namespace Tester;

public class SolverDirector
{
    
    public SolverDirector(StepsProvider provider,string[] solvers)
    {
        var steps = provider.GetSteps(solvers);
        this.Itterations = steps.AsQueryable().GroupBy(x => x.Number).ToList(); 
    }

    public List<IGrouping<int, SolverStep>> Itterations { get; }

    public Results Solve()
    {
        Results results = new Results();
        
        foreach (var itteration in Itterations)
        {
            Parallel.ForEach(itteration,i => results.values.Add(i.Step()));
        }

        return results;
    }
}