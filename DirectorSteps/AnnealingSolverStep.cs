namespace Tester.DirectorSteps;

public class AnnealingSolverStep : AlgSolverStep<AnnealingMethodSolver>
{
    public AnnealingSolverStep(Config config, List<Model> models) : base( "Annealing Solver", config, models)
    {
    }

    public override AnnealingMethodSolver GetInstance(int index)
    {
        return new AnnealingMethodSolver(Config,Models[index]);
    }
}