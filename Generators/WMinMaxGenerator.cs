namespace Tester;

public class WMinMaxGenerator : MinMaxGenerator<int>
{
    public WMinMaxGenerator(Random random, Model model, int min, int max) : base(random, model, min, max)
    {
    }

    public override void Generate()
    {
        if (Model.Customers_Count == 0)
        {
            throw new ArgumentException($"Cannot Generator without {nameof(Model.Customers_Count)}");
        }
        if (Model.W is null)
        {
            Model.W = new int[Model.Customers_Count];
        }
        for (int i = 0; i < Model.Customers_Count; ++i)
        {
            Model.W[i] = Random.Next(Min, Max);
        }
    }
}