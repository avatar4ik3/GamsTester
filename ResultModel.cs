using System.Text;

namespace Tester;
public class ResultModel{
    public bool[][] X {get;set;} = null!;
    public bool[] Z {get;set;} = null!;

    public double Ro {get;set;}
    public int Customers_Count { get; }
    public int Stores_Count { get; }

    public ResultModel(int customers_Count, int stores_Count){
        Z = new bool[stores_Count];
        X = new bool[stores_Count][];
        for(int i = 0; i < stores_Count;++i){
            X[i] = new bool[customers_Count];
        }
        Customers_Count = customers_Count;
        Stores_Count = stores_Count;
    }

    public override string? ToString()
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine($"Z:");
        foreach(var z_i in Z){
            sb.Append($"{z_i} ");
        }
        sb.AppendLine();
        sb.AppendLine("X:");

        for(int i = 0;i < Stores_Count; ++i){
            for(int j = 0;j < Customers_Count;++j){
                sb.Append($"{X[i][j]} ");
            }
            sb.AppendLine();
        }
        return sb.ToString();
    }
}