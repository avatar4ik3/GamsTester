using GAMS;

namespace Tester.DirectorSteps;
public class CSolverStep : GamsSolverStep<CSolver>
{
    public CSolverStep(Config config, List<Model> models, GAMSWorkspace ws) : base(config, models, ws, "Gams C-Only Solver")
    {
    }

    public override ResultsElements Step()
    {
        var list = new List<ResultElement>();
        var resultModels = new List<(int ModelIndex,ResultModel Result)>();
        Parallel.ForEach(Enumerable.Range(0, Models.Count() - 1), i => {
            Console.WriteLine($"{Name} : {i} solving.");
            var solver = new CSolver(Config,Models[i],WS, WS.AddJobFromFile("new12.gms"));
            var result = solver.Solve();
            resultModels.Add(new(i,result));
            list.Add(new(i,result));
            Console.WriteLine($"{Name} : {i} solved.");
        });
        foreach(var resultModel in resultModels){
            Models[resultModel.ModelIndex].C = resultModel.Result.C * 2;
        }
        var output = new ResultsElements(Name);
        output.Elements.AddRange(list);
        return output;
    }
}