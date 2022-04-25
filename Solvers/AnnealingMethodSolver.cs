namespace Tester;
public class AnnealingMethodSolver : Solver
{

    public AnnealingMethodSolver(Config config, Model model) : base(config, model)
    {

    }

    public override ResultModel Solve()
    {
        var s_asterix = FindBadSolution();
        var record = s_asterix.Ro;
        var tk = (double)(Config.T_Max);
        while (tk > Config.T_Min)
        {
            foreach (var itteration in Enumerable.Range(0, Config.L))
            {
                //создать множество решений с перестановкой параметров, пока что так 
                //возможно это и не нужно поскольку это эквивалентно
                var s_apos = s_asterix.Swap().Copy();
                s_apos.Ro = s_apos.FindRo(Model);

                if (s_apos.Ro >= record)
                {
                    //переход состояния. 
                    s_asterix = s_apos.Copy();
                    record = s_apos.Ro;
                }
                else
                {
                    var p = Math.Exp((s_apos.Ro - record) / tk);
                    var rand = new Random().NextDouble();
                    if (rand < p)
                    {
                        //ничего не делаем
                    }
                    else
                    {
                        // тут переход состояния
                        s_asterix = s_apos.Copy();
                        record = s_apos.Ro;
                    }
                }
            }
            tk *= Config.Phi;
            Console.WriteLine(tk);
        }
        return s_asterix.Convert();
    }

    private S FindBadSolution()
    {
        var result = new S(Config.Stores_Count,Config.Customers_Count);

        result.Z.FillWithRandomElements(Config.Count_max);

        result.FillWithRandomElements();

        result.Ro = result.FindRo(Model);
        return result;
    }
}


