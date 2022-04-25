namespace Tester;

public class CustomersCountGenerator : ValueFillGenarator<int>
{
    public CustomersCountGenerator(Random random, Model model, int Value) : base(random, model, Value)
    {
    }

    public override void Generate()
    {
        Model.Customers_Count = Value;
    }
}