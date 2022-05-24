namespace Tester
{
    public interface IResultViewer
    {
        public SolverDirector Director{get;}
        public void View();
    }
}