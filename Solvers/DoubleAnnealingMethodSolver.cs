namespace Tester.Solvers;

public class TwoSwapDoubleAnnealingMethodSolver : Solver{
    private AnnealingMethodSolver _solverSwap1;
    private TwoSwapAnnealingMethodSolver _solverSwap2;

    public TwoSwapDoubleAnnealingMethodSolver(Config config, Model model) : base(config, model)
    {
        _solverSwap1 = new AnnealingMethodSolver(config,model);
        _solverSwap2 = new TwoSwapAnnealingMethodSolver(config,model);
    }

    public override ResultModel Solve()
    {
        var res1 = _solverSwap1.Solve();
        _solverSwap2.S_asterix = res1.Convert();
        return _solverSwap2.Solve();
    }
}