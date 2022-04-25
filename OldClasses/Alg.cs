namespace Tester;
public class Alg
{

    public Alg(Input input, Model model)
    {
        Input = input;
        Model = model;
        Rnd = new Random();
    }

    public Input Input { get; }
    public Model Model { get; }
    public Random Rnd { get; }

    public Output Solve()
    {
        var s = FindBadSolution();
        Console.WriteLine(s);
        var s_asterix = s;
        var record = s.ro;
        var tk = Input.t_max;
        Console.WriteLine(tk);

        while (tk > Input.t_min)
        {
            foreach (var itteration in Enumerable.Range(0, Input.l))
            {
                Console.WriteLine($"Current L : {itteration}");

                var n_1 = Swap(s);

                Console.WriteLine($"Swaped : {n_1}");
                //создать множество решений с перестановкой параметров, пока что так 
                //возможно это и не нужно поскольку это эквивалентно
                var s_apos = n_1;

                if (s_apos.ro >= record)
                {
                    //переход состояния. 
                    Console.WriteLine("Changed");

                    s_asterix = s_apos;
                    record = s_apos.ro;
                }
                else
                {
                    Console.WriteLine("Not Changed. Finding probability");
                    var p = Math.Exp((s_apos.ro - record) / tk);
                    var rand = Rnd.NextDouble();
                    if (rand < p)
                    {
                        Console.WriteLine("Not proq");
                        //ничего не делаем
                    }
                    else
                    {
                        // тут переход состояния
                        Console.WriteLine("proq");
                        s_asterix = s_apos;
                        record = s_apos.ro;
                    }
                }
            }
            tk = NextTemperature(tk);
            Console.WriteLine(tk);
        }
        return s_asterix;
    }

    private Output Swap(Output o)
    {
        var z = new bool[Model.Stores_Count];
        Array.Copy(o.z, z, Model.Stores_Count);
        var x = new bool[Model.Stores_Count][];
        for (int i = 0; i < Model.Stores_Count; ++i)
        {
            x[i] = new bool[Model.Customers_Count];
            Array.Copy(o.x[i], x[i], Model.Customers_Count);
        }


        var trues = new (int index, bool value)[Model.Stores_Count];
        for (int i = 0; i < Model.Stores_Count; ++i)
        {
            trues[i] = (i, z[i]);
        }
        var z_true = trues.AsQueryable().Where(x => x.value == true).Select(x => x.index).ToArray();
        var z_false = trues.AsQueryable().Where(x => x.value == false).Select(x => x.index).ToArray();

        var i_true = Rnd.Next(0, z_true.Count());
        var i_false = Rnd.Next(0, z_false.Count());
        z[i_true] = false;
        z[i_false] = true;
        Array.Fill(x[i_true], false);
        Array.Fill(x[i_false], false);

        fillX(x, z);

        double ro = FindRo(x, z);
        return new(z, x, ro);
    }

    private double FindRo(bool[][] x, bool[] z)
    {
        return Math.Max(((Model.C - MulSumOfTwoArrays(x, Model.T)) / z.Count(x => x == true)), 0);
    }

    private double MulSumOfTwoArrays(bool[][] x, int[][] t)
    {
        double res = 0;
        for (int i = 0; i < Model.Stores_Count; ++i)
        {
            for (int j = 0; j < Model.Customers_Count; ++j)
            {
                if (x[i][j] == true)
                {
                    res += t[i][j];
                }
            }
        }
        return res;
    }
    private Output FindBadSolution()
    {
        var z = new bool[Model.Stores_Count];
        var x = new bool[Model.Stores_Count][];
        for (int i = 0; i < Model.Stores_Count; ++i)
        {
            x[i] = new bool[Model.Customers_Count];
        }
        double ro = 0;

        for (int i = 0; i < z.Count(); i++)
        {
            if (z.Count(x => x == true) == Model.Count_Max)
            {
                z[i] = false;
            }
            z[i] = Rnd.Next(0, 2) == 0 ? false : true;
        }

        fillX(x, z);
        ro = FindRo(x, z);
        return new(z, x, ro);
    }
    private void fillX(bool[][] x, bool[] z)
    {
        for (int j = 0; j < Model.Customers_Count; j++)
        {
            for (int i = 0; i < Model.Stores_Count; i++)
            {
                if (z[i] == true)
                {
                    if (isBadXContainsTrue(x, j) == false)
                    {
                        x[i][j] = Rnd.Next(0, 2) == 0 ? false : true;
                    }
                    else
                    {
                        x[i][j] = false;
                    }
                }
            }
        }
    }
    private bool isBadXContainsTrue(bool[][] x, int j)
    {
        for (int i = 0; i < Model.Stores_Count; i++)
        {
            if (x[i][j] == true) return true;
        }
        return false;
    }
    private double NextTemperature(double current)
    {
        return current * Input.phi;
    }
}

public record Output(
    bool[] z,
    bool[][] x,
    double ro
);

public record Input(double t_max, double t_min, double phi, int l);