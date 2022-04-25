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
        Builder.AddGenerator(new TMinMaxGenerator(Random, Builder.Generator.Model, Config.T_Min_Model, Config.T_Max_Model));

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

    public Model BuildConcreteModel()
    {
        Builder.AddGenerator(new CGenerator(Random, Builder.Generator.Model, 26));
        Builder.AddGenerator(new CountMaxGenerator(Random, Builder.Generator.Model, 2));
        Builder.AddGenerator(new StoresCountGenerator(Random, Builder.Generator.Model, 3));
        Builder.AddGenerator(new CustomersCountGenerator(Random, Builder.Generator.Model, 3));
        Builder.AddGenerator(new ConcreteTGenerator(Builder.Generator.Model, new int[3][]{new int[3]{4,7,11},new int[3]{6,2,2},new int[3]{8,1,9}}));

        Builder.AddGenerator(new ConcreteWGenerator(Builder.Generator.Model, new int[3]{1,5,7}));


        return Builder.Build;
    }
}