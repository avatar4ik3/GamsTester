namespace Tester;
public abstract class MinMaxGenerator<T> : RandomGenerator
{
    public T Min {get;init;}
    public T Max {get;init;}


    public MinMaxGenerator(Random random, Model model,T min, T max) : base(random, model)
    {
        Min = min;
        Max = max;
    }
}