namespace Tester;
public class StoresCountGenerator : ValueFillGenarator<int>
{
    public StoresCountGenerator(Random random, Model model, int Value) : base(random, model, Value)
    {
    }

    public override void Generate()
    {
        Model.Stores_Count = Value;
    }
}