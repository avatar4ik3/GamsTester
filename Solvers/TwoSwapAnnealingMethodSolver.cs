using CH.Combinations;
using MathNet;
using MathNet.Numerics;

namespace Tester;
public class TwoSwapAnnealingMethodSolver : Solver
{
    public S S_asterix;
    public TwoSwapAnnealingMethodSolver(Config config, Model model) : base(config, model)
    {
        S_asterix = FindBadSolution();
    }
    public override ResultModel Solve()
    {
        var tk = (double)(Config.T_Max);
        List<Z> reached_solutions = new List<Z>();
        reached_solutions.Add(S_asterix.Z);
        int all_possible_solution_count = (int)SpecialFunctions.Factorial(Model.Stores_Count) / (int)((SpecialFunctions.Factorial(Model.Stores_Count - Model.Count_Max)) * SpecialFunctions.Factorial(Model.Count_Max));
       
        while (tk > Config.T_Min)
        {
            if (reached_solutions.Count() == all_possible_solution_count)
            {
                var result = S_asterix.Convert();
                result.C = Model.C;
                return result;
            }
            foreach (var itteration in Enumerable.Range(0, Config.L))
            {
                var s_apos = S_asterix.Swap2(Model).Copy();
                if (!reached_solutions.Contains(s_apos.Z))
                {
                    reached_solutions.Add(s_apos.Z);
                }
                if (s_apos.Ro > S_asterix.Ro)
                {
                    S_asterix = s_apos.Copy();
                }
                else
                {
                    var p = Math.Exp((s_apos.Ro - S_asterix.Ro) / tk);
                    if (new Random().NextDouble() > p)
                    {
                        S_asterix = s_apos.Copy();
                    }
                }
            }
            tk *= Config.Phi;
            //Console.WriteLine(tk);
        }
        var res = S_asterix.Convert();
        res.C = Model.C; 
        return res;
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


