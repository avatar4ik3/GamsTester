namespace Tester;
public class ConcreteWGenerator : IGenerator
{
    public Model Model { get ; init ; }
    public int[] Values { get; }

    public ConcreteWGenerator(Model model,params int[] values)
    {
        Model = model;
        Values = values;
    }


    public void Generate()
    {
        Model.W = new int[Model.Stores_Count];
        Array.Copy(Values,Model.W,Model.Stores_Count);
    }
}