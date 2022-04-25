namespace Tester;

public class ModelBuilderDirector
{
    public ModelBuilder Builder { get; private set; }
    public Config Config { get; }
    public Random Random { get; }

    public ModelBuilderDirector(Config config, Random random)
    {
        Builder = new ModelBuilder();
        Config = config;
        Random = random;
    }

    public Model BuildRandomModel()
    {
        Builder.AddGenerator(new CGenerator(Random, Builder.Generator.Model, Config.C));
        Builder.AddGenerator(new CountMaxGenerator(Random, Builder.Generator.Model, Config.Count_max));
        Builder.AddGenerator(new StoresCountGenerator(Random, Builder.Generator.Model, Config.Stores_Count));
        Builder.AddGenerator(new CustomersCountGenerator(Random, Builder.Generator.Model, Config.Customers_Count));
        Builder.AddGenerator(new TMinMaxGenerator(Random, Builder.Generator.Model, Config.T_Min, Config.T_Max));

        if (Config.W_Value_Model == 0)
        {
            Builder.AddGenerator(new WMinMaxGenerator(Random, Builder.Generator.Model, Config.W_Min, Config.W_Max));

        }
        else
        {
            Builder.AddGenerator(new WFillGenerator(Random, Builder.Generator.Model, Config.W_Value_Model));

        }
        return Builder.Build;
    }
}