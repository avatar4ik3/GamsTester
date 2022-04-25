namespace Tester;

public abstract class ValueFillGenarator<T> : RandomGenerator
{
    protected ValueFillGenarator(Random random, Model model,T Value) : base(random, model)
    {
        this.Value = Value;
    }

    public T Value { get; init;}
}