namespace Tester;

public class CountMaxGenerator : ValueFillGenarator<int>
{
    public CountMaxGenerator(Random random, Model model, int Value) : base(random, model, Value)
    {
    }

    public override void Generate()
    {
        Model.Count_Max = Value;
    }
}