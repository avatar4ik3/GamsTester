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

    public static void PlaceTruesInMostSuitablePlaces(this S s,Model model)
    {
        var trues = new (int index, bool value)[s.X.N];
        for (int i = 0; i <s.X.N; ++i)
        {
            trues[i] = (i, s.Z[i]);
        }
        var opened_index = trues.Where(x => x.value == true).Select(x => x.index).ToArray();
        for(int j = 0 ; j < s.X.M ; ++j){
            if(s.X.IsColumnContainsTrues(j)) continue;
            int index = GetColumnMinValueIndex(model.T,opened_index,j);
            s.X.values[index][j] = true;
        }
    }
    public static int GetColumnMinValueIndex(int[][] t,int[] indexies,int j){
        (int index,int value) min = new(0,int.MaxValue);
        
        for (int i = 0; i < indexies.Count(); ++i)
        {
            if(t[indexies[i]][j] < min.value){
                min.value = t[indexies[i]][j];
                min.index = indexies[i];
            }
        }
        return min.index;
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
        return Math.Max(((double)(model.C - MulSumOfArrays(s.X, model.T,model.W)) / (double)model.Count_Max), 0);
    }

    private static int MulSumOfArrays(this X x, int[][] t,int [] w)
    {
        var res = 0;
        for (int i = 0; i < x.N; ++i)
        {
            for(int j = 0; j < x.M;++j)
            {
                if (x[i, j] == true)
                {
                    res += t[i][j] * w[j];
                }
            }
        }
        return res;
    }

    public static S Swap(this S s,Model model)
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

        res.X.ClearX();

        res.PlaceTruesInMostSuitablePlaces(model);


        res.Ro = res.FindRo(model);
        
        ///
        return res;
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