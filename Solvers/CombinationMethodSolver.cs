using CH.Combinations;
using MathNet;
using MathNet.Numerics;

namespace Tester;
public class CombinationMethodSolver : Solver
{

    public CombinationMethodSolver(Config config, Model model) : base(config, model)
    {

    }

    public IEnumerable<S> GetAllCombinations()
    {
        Combinations<int> c = new Combinations<int>(Enumerable.Range(0, Model.Stores_Count).ToArray(), Model.Count_Max);
        foreach (var elem in c)
        {
            yield return CreateSolutionOfCombination(elem);
        }
    }

    public S CreateSolutionOfCombination(int[] c)
    {
        var result = new S(Config.Stores_Count, Config.Customers_Count);
        result.Z.FillZCombinations(c);
        result.PlaceTruesInMostSuitablePlaces(Model);

        result.Ro = result.FindRo(Model);
        return result;
    }
    public override ResultModel Solve()
    {
        var s_asterix = FindBadSolution();
        foreach (var combination in GetAllCombinations())
        {
            var s_apos = combination.Copy();
            if (s_apos.Ro > s_asterix.Ro)
            {
                s_asterix = s_apos.Copy();
            }

        }
        return s_asterix.Convert();
    }

    private S FindBadSolution()
    {
        var result = new S(Config.Stores_Count, Config.Customers_Count);

        result.Z.FillWithRandomElements(Config.Count_max);

        result.PlaceTruesInMostSuitablePlaces(Model);

        result.Ro = result.FindRo(Model);
        return result;
    }
}


