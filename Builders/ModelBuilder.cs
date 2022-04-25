namespace Tester;

public class ModelBuilder
{
    public ModelGenerator Generator { get; private set; }

    public Model Build
    {
        get
        {
            Generator.Generate();
            var model = Generator.Model;
            Generator = new ModelGenerator(new Model()); 
            return model;
        }
    }
    public ModelBuilder()
    {
        Generator = new ModelGenerator(new Model());
    }

    public ModelBuilder AddGenerator(IGenerator generator)
    {
        Generator.Generators.Add(generator);
        return this;
    }


}