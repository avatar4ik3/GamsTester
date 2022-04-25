namespace Tester;

public class ConsoleResultViewer : IResultViewer
{
    public ConsoleResultViewer()
    {
    }

    public void View(Results results)
    {
        List<(string name ,double ro)> average = new List<(string name ,double ro)>();
        foreach(var result in results.values){
            foreach(var elem in result.list){
                Console.WriteLine(result.resultName);
                Console.WriteLine(elem);
            }
            average.Add(new(result.resultName,result.list.Average(x => x.Ro)));
        }
        foreach(var elem in average){
            Console.WriteLine($"{elem.name} : ro : {elem.ro}");
        }
    }
}