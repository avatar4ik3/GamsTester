using Tester.Solvers;

namespace Tester.DirectorSteps;

public class OneSwapOneSwapStep : AlgSolverStep<OneSwapOneSwapSolver>
{
    public OneSwapOneSwapStep(Config config, List<Model> models) : base( "1 Swap - 1 Swap", config, models)
    {
    }

    public override OneSwapOneSwapSolver GetInstance(int index)
    {
        return new OneSwapOneSwapSolver(Config,Models[index]);
    }
}