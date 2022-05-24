using Tester.Solvers;

namespace Tester.DirectorSteps;

public class TwoSwapDoubleAnnealingSolverStep : AlgSolverStep<TwoSwapDoubleAnnealingMethodSolver>
{
    public TwoSwapDoubleAnnealingSolverStep(Config config, List<Model> models) : base( "2 Swap Pre Heating", config, models)
    {
    }

    public override TwoSwapDoubleAnnealingMethodSolver GetInstance(int index)
    {
        return new TwoSwapDoubleAnnealingMethodSolver(Config,Models[index]);
    }
}