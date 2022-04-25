namespace Tester;
public class ConcreteTGenerator : IGenerator
{
    public Model Model { get ; init ; }
    public int[][] Values { get; }

    public ConcreteTGenerator(Model model,params int[][] values)
    {
        Model = model;
        Values = values;
    }


    public void Generate()
    {
        Model.T = new int[Model.Stores_Count][];
        for(int i =0; i< Model.Stores_Count;++i){
            Model.T[i] = new int[Model.Customers_Count];
            Array.Copy(Values[i],Model.T[i],Model.Customers_Count);
        }
    }
}