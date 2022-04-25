namespace Tester;

public class TMinMaxGenerator : MinMaxGenerator<int>
{

    public TMinMaxGenerator( Random random,Model model, int min, int max) : base(random, model, min, max)
    {
    }

    public override void Generate()
    {
        if (Model.Customers_Count == 0 || Model.Stores_Count == 0)
        {
            throw new ArgumentException("Unable to generate without boundries");
        }
        if (Model.T is null)
        {
            Model.T = new int[Model.Stores_Count][];
            for (int i = 0; i < Model.Stores_Count; ++i)
            {
                Model.T[i] = new int[Model.Customers_Count];
            }
        }
        for (int i = 0; i < Model.Stores_Count; ++i)
        {
            for (int j = 0; j < Model.Customers_Count; ++j)
            {
                Model.T[i][j] = Random.Next(Min, Max);
            }
        }
    }
}