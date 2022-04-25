namespace Tester;

public abstract class RandomGenerator : IGenerator{
    public Random Random {get;init;}
    public Model Model { get; init; }

    protected RandomGenerator(Random random, Model model)
    {
        Random = random;
        Model = model;
    }

    public abstract void Generate();
}