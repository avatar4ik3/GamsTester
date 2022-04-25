namespace Tester;

public class WFillGenerator : ValueFillGenarator<int>
{
    public WFillGenerator(Random random, Model model, int Value) : base(random, model, Value)
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
            Model.W[i] = Value;
        }
    }
}