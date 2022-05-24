using GAMS;
using Tester.DirectorSteps;

namespace Tester;

public class StepsProvider{
    private List<SolverStep> _steps = new List<SolverStep>(); 

    public StepsProvider(GAMSWorkspace ws,Config config, RandomModelDataProvider dataProvider)
    {
        var models = dataProvider.Provide(config.Itterations);
        _steps.Add(new CSolverStep(config,models,ws));
        _steps.Add(new AllGamsSolverStep(config,models,ws));
        _steps.Add(new CombinationSolverStep(config,models));
        _steps.Add(new AnnealingSolverStep(config,models));
        _steps.Add(new TwoSwapDoubleAnnealingSolverStep(config,models));
        _steps.Add(new OneSwapOneSwapStep(config,models));
    }

    public List<SolverStep> GetSteps(string[] names){
        return _steps.Where(step => names.Contains(step.Name)).ToList();
    }
}