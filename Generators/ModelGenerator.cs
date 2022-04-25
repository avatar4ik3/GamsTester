namespace Tester;

public class ModelGenerator : IGenerator
{
    public Model Model { get ; init ; }

    public List<IGenerator> Generators = new List<IGenerator>();

    public ModelGenerator(Model model){
        this.Model = model;
    }
    public void Generate()
    {
        foreach(var generator in Generators){
            generator.Generate();
        }
    }
}

public interface IGenerator {
    public Model Model{get;init;}

    public void Generate();
}
