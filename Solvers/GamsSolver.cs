using GAMS;

namespace Tester;

public class GamsSolver : Solver
{
    public GamsSolver(Config config, Model model) : base(config, model)
    {
        this.WS = new GAMSWorkspace(Config.GamsWorkspaceFolder, Config.GamsFolder);
    }

    public GAMSWorkspace WS { get; private set; }

    public override ResultModel Solve()
    {
        
        GAMSDatabase db = WS.AddDatabase();

        GAMSSet g_i = db.AddSet("i", 1, "a");
        GAMSSet g_j = db.AddSet("j", 1, "a");
        GAMSParameter g_count_max = db.AddParameter("count_max", "a");
        GAMSParameter g_C = db.AddParameter("C", "a");
        GAMSParameter g_t = db.AddParameter("t", 2, "a");
        GAMSParameter g_w = db.AddParameter("w", 1, "a");

        g_C.AddRecord().Value = Model.C;
        g_count_max.AddRecord().Value = Model.Count_Max;

        foreach (var i in Enumerable.Range(1, Model.Stores_Count))
        {
            g_i.AddRecord(i.ToString() + "s");
        }

        foreach (var i in Enumerable.Range(1, Model.Customers_Count))
        {
            g_j.AddRecord(i.ToString() + "c");
        }

        foreach (var i in Enumerable.Range(1, Model.Customers_Count))
        {
            g_w.AddRecord(i.ToString() + "c").Value = Model.W[i - 1];
        }

        foreach (var i in Enumerable.Range(1, Model.Stores_Count))
        {
            foreach (var j in Enumerable.Range(1, Model.Customers_Count))
            {
                g_t.AddRecord(i.ToString() + "s", j.ToString() + "c").Value = Model.T[i - 1][j - 1];
            }
        }


        GAMSJob t0 = WS.AddJobFromFile(Config.FileName);
        GAMSOptions gOptions = WS.AddOptions();
        gOptions.Defines.Add("gdxincname", db.Name);
        t0.Run(gOptions, db);

        var result = new ResultModel(customers_Count: Model.Customers_Count, stores_Count: Model.Stores_Count);
        foreach(GAMSVariableRecord elem in t0.OutDB.GetVariable("p")){
            result.Ro = elem.Level;
        }

        int GamsSetElementToIntegerValue(string setElement){
            var str = setElement.Substring(0,setElement.Length - 1);
            return int.Parse(str) - 1;
        }

        bool ConvertDoubleToBool(double value){
            return Math.Abs(value - 1) <= 1e-5 ? true : false;
        }
        foreach(GAMSVariableRecord elem in t0.OutDB.GetVariable("x")){
            result.X[GamsSetElementToIntegerValue(elem.Keys[0])][GamsSetElementToIntegerValue(elem.Keys[1])] = ConvertDoubleToBool(elem.Level);
        }

        foreach(GAMSVariableRecord elem in t0.OutDB.GetVariable("z")){
            result.Z[GamsSetElementToIntegerValue(elem.Keys[0])] = ConvertDoubleToBool(elem.Level);
        }
        return result;
    }
}