using CH.Combinations;
using MathNet;
using MathNet.Numerics;

namespace Tester;
public class AnnealingMethodSolver : Solver
{

    public AnnealingMethodSolver(Config config, Model model) : base(config, model)
    {

    }
    public override ResultModel Solve()
    {
        var s_asterix = FindBadSolution();
        var tk = (double)(Config.T_Max);
        List<Z> reached_solutions = new List<Z>();
        reached_solutions.Add(s_asterix.Z);
        int all_possible_solution_count = (int)SpecialFunctions.Factorial(Model.Stores_Count) / (int)((SpecialFunctions.Factorial(Model.Stores_Count - Model.Count_Max)) * SpecialFunctions.Factorial(Model.Count_Max));
        while (tk > Config.T_Min)
        {
            Console.WriteLine(tk);
            if (reached_solutions.Count() == all_possible_solution_count)
            {
                return s_asterix.Convert();
            }
            foreach (var itteration in Enumerable.Range(0, Config.L))
            {
                var s_apos = s_asterix.Swap(Model).Copy();
                if (!reached_solutions.Contains(s_apos.Z))
                {
                    reached_solutions.Add(s_apos.Z);
                }
                if (s_apos.Ro > s_asterix.Ro)
                {
                    s_asterix = s_apos.Copy();
                }
                else
                {
                    var p = Math.Exp((s_apos.Ro - s_asterix.Ro) / tk);
                    if (new Random().NextDouble() > p)
                    {
                        s_asterix = s_apos.Copy();
                    }
                }
            }
            tk *= Config.Phi;
            //Console.WriteLine(tk);
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


