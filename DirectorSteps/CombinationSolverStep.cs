namespace Tester.DirectorSteps;

public class CombinationSolverStep : AlgSolverStep<CombinationMethodSolver>
{
    public CombinationSolverStep(Config config, List<Model> models) : base("Combiantion Solver", config, models)
    {
    }

    public override CombinationMethodSolver GetInstance(int index)
    {
        return new CombinationMethodSolver(Config,Models[index]);
    }
}