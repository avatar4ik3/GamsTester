namespace Tester;

public class Results
{
    public List<ResultsElements> values { get; set; } = new();

    public Results()
    {
    }
}

public class ResultsElements
{
    public string ResultName { get; set; }
    public List<ResultElement> Elements { get; set; } = new List<ResultElement>();

    public ResultsElements(string resultName)
    {
        ResultName = resultName;
    }
}


public class ResultElement
{
    public int Number { get; set; }
    public ResultModel Model { get; set; }

    public ResultElement(int number, ResultModel model)
    {
        Number = number;
        Model = model;
    }
}
