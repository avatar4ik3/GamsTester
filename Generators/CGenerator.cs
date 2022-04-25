namespace Tester;

public class CGenerator : ValueFillGenarator<int>
{
    public CGenerator(Random random, Model model, int Value) : base(random, model, Value)
    {
    }

    public override void Generate()
    {
        Model.C = Value;
    }
}