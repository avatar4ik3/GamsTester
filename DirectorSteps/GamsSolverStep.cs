using GAMS;

namespace Tester.DirectorSteps;
public abstract class GamsSolverStep<T> : SolverStep
{
    public GamsSolverStep(Config config,List<Model> models,GAMSWorkspace ws,string name) : base(0,name,config,models)
    {
        this.WS = ws;
    }

    public GAMSWorkspace WS { get; }
}