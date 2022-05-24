using Tester.Solvers;

namespace Tester.DirectorSteps;

public class OneSwapDoubleAnnealingSolverStep : AlgSolverStep<TwoSwapDoubleAnnealingMethodSolver>
{
    public OneSwapDoubleAnnealingSolverStep(Config config, List<Model> models) : base( "1 Swap Pre Heating", config, models)
    {
    }

    public override TwoSwapDoubleAnnealingMethodSolver GetInstance(int index)
    {
        return new TwoSwapDoubleAnnealingMethodSolver(Config,Models[index]);
    }
}