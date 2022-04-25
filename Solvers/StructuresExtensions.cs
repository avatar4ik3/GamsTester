using System.Linq;
namespace Tester;

public static class StructuresExtensions
{
    public static void FillWithRandomElements(this Z z, int count_max)
    {
        List<int> indexies = Enumerable.Range(0,z.N - 1).ToList();
        for(int i = 0 ; i < count_max ; ++i){
            var picked = indexies[new Random().Next(0,indexies.Count())];
            z[picked] = true;
            indexies.Remove(picked);
        }

    }

    public static void FillWithRandomElements(this S s)
    {
        var trues = new (int index, bool value)[s.X.N];
        for (int i = 0; i <s.X.N; ++i)
        {
            trues[i] = (i, s.Z[i]);
        }
        var opened_index = trues.Where(x => x.value == true).ToArray();
        for(int j = 0 ; j < s.X.M ; ++j){
            if(s.X.IsColumnContainsTrues(j)) continue;
            int index = new Random().Next(0,opened_index.Count());
            s.X.values[opened_index[index].index][j] = true;
        }
        // for(int i  =0,j = 0; i < s.X.N && j < s.X.M; ++i,j++){
        //     if(s.Z[i] == false){
        //         continue;
        //     }
        //     var index = new Random().Next(0,s.X.N);

        // }
        // for (int j = 0 ;j < s.X.M ; j ++ )
        // {
        //     var index = new Random().Next(0,s.X.N);
        //     bool fl = true;
        //     for(int i = 0 ; i < s.X.N; ++i)
        //     {
        //         if (s.X.IsColumnContainsTrues(j) || s.Z[i] == false)
        //         {
        //             s.X.values[i][j] = false;
        //             fl = false;
        //         }
        //     }
        //     if(fl){
        //         s.X.values[index][j] = true;
        //     }
        // }
    }

    public static bool IsColumnContainsTrues(this X x, int j)
    {
        for (int i = 0; i < x.N; ++i)
        {
            if (x[i, j] == true)
            {
                return true;
            }
        }
        return false;
    }

    public static double FindRo(this S s, Model model)
    {
        return Math.Max(((double)(model.C - MulSumOfArrays(s.X, model.T,model.W)) / model.Count_Max), 0);
    }

    private static int MulSumOfArrays(this X x, int[][] t,int [] w)
    {
        var res = 0;
        for (int j = 0, i = 0; j < x.M && i < x.N; j = (i == x.N - 1) ? j + 1 : j, ++i)
        {
            {
                if (x[i, j] == true)
                {
                    res += t[i][j] * w[i];
                }
            }
        }
        return res;
    }

    public static S Swap(this S s)
    {
        var res = s.Copy();

        ///

        var trues = new (int index, bool value)[res.X.N];
        for (int i = 0; i <res.X.N; ++i)
        {
            trues[i] = (i, res.Z[i]);
        }
        var z_true = trues.AsQueryable().Where(x => x.value == true).Select(x => x.index).ToArray();
        var z_false = trues.AsQueryable().Where(x => x.value == false).Select(x => x.index).ToArray();

        var i_true = new Random().Next(0, z_true.Count());
        var i_false = new Random().Next(0, z_false.Count());
        res.Z.values[z_true[i_true]] = false;
        res.Z.values[z_false[i_false]] = true;

        res.X.FillRowWithFalse(z_true[i_true]);
        res.X.FillRowWithFalse(z_false[i_false]);

        res.FillWithRandomElements();

        
        ///
        return res;
    }

    public static void FillRowWithFalse(this X x ,int i){
        for (int j = 0; j < x.M; ++j)
        {
            x[i,j] = false;
        }
    }

    public static void FillColumnWithFalse(this X x ,int j){
        for (int i = 0; i < x.N; ++i)
        {
            x[i,j] = false;
        }
    }

    public static ResultModel Convert(this S s){
        var res = new ResultModel(s.X.N,s.X.M);
        Array.Copy(s.Z.values,res.Z,s.X.N);
        for(int i = 0 ; i < s.X.N ; ++i ){
            Array.Copy(s.X.values[i],res.X[i],s.X.M);
        }
        res.Ro = s.Ro;
        return res;
    }

    public static void ClearX(this X x){
        for(int i =0 ;i < x.N;++i){
            Array.Fill(x.values[i],false);
        }
    }
}