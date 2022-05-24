using GAMS;

namespace Tester;

public class CSolver : Solver
{
    public CSolver(Config config, Model model, GAMSWorkspace ws,GAMSJob job) : base(config, model)
    {
        this.WS = ws;
        Job = job;
        db = WS.AddDatabase();

        GAMSSet g_i = db.AddSet("i", 1, "a");
        GAMSSet g_j = db.AddSet("j", 1, "a");
        GAMSParameter g_count_max = db.AddParameter("count_max", "a");
        GAMSParameter g_t = db.AddParameter("t", 2, "a");
        GAMSParameter g_w = db.AddParameter("w", 1, "a");
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

    }

    public GAMSDatabase db {get;private set;}
    public GAMSWorkspace WS { get; private set; }
    public GAMSJob Job { get; }

    public override ResultModel Solve()
    {
        GAMSOptions gOptions = WS.AddOptions();
        gOptions.Defines.Add("gdxincname", db.Name);
        Job.Run(gOptions, db);

        var result = new ResultModel(customers_Count: Model.Customers_Count, stores_Count: Model.Stores_Count);
        foreach(GAMSVariableRecord elem in Job.OutDB.GetVariable("c")){
            result.C = (int) elem.Level;
        }

        return result;
    }
}