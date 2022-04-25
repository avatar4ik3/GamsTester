using GAMS;

namespace Tester;

//устаревший класс 
public class GamsTester
{
    public GamsTester(int itterationCount,
                      Model model)
    {
        this.ItterationCount = itterationCount;
        this.Model = model;
        this.WS = new GAMSWorkspace("C:\\Users\\cross\\OneDrive\\Documents\\GAMS\\Studio\\workspace\\", "C:\\GAMS\\35\\");
        this.Rnd = new Random(123456789);
    }

    private ResultModel Itteration()
    {

        // ws.GamsLib("");

        GAMSDatabase db = WS.AddDatabase();

        GAMSSet g_i = db.AddSet("i", 1, "a");
        GAMSSet g_j = db.AddSet("j", 1, "a");
        GAMSParameter g_count_max = db.AddParameter("count_max", "a");
        GAMSParameter g_C = db.AddParameter("C", "a");
        // GAMSParameter g_l = db.AddParameter("l",1, "a");
        GAMSParameter g_t = db.AddParameter("t", 2, "a");
        GAMSParameter g_w = db.AddParameter("w", 1, "a");

        //пусть будет 100 
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

        // foreach(var i in Enumerable.Range(1,Store_Count)){
        //         g_l.AddRecord(i.ToString()).Value = Rnd.Next(0,3);
        // }


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


        GAMSJob t0 = WS.AddJobFromFile("new8.gms");
        GAMSOptions gOptions = WS.AddOptions();
        gOptions.Defines.Add("gdxincname", db.Name);
        t0.Run(gOptions, db);

        var result = new ResultModel(customers_Count: Model.Customers_Count, stores_Count: Model.Stores_Count);
        // foreach(GAMSVariableRecord elem in t0.OutDB.GetVariable("p")){
        //     result.Ro = elem.Level;
        // }

        // foreach(GAMSVariableRecord elem in t0.OutDB.GetVariable("x")){
        //     result.X[int.Parse(elem.Keys[0])][int.Parse(elem.Keys[1])] = IsDoubleEqual(elem.Level)? true : false;
        // }

        // foreach(GAMSVariableRecord elem in t0.OutDB.GetVariable("z")){
        //     result.Z[int.Parse(elem.Keys[0])] = IsDoubleEqual(elem.Level)? true : false;
        // }
        return result;
    }

    private bool IsDoubleEqual(double v1){
        return Math.Abs(v1 - 1) <= 1e-6;
    }
    public void Start()
    {
        Parallel.ForEach(Enumerable.Range(0, ItterationCount), it => Itteration());
        foreach (var i in Enumerable.Range(0, ItterationCount))
        {
            var now = DateTime.Now.ToFileTimeUtc();
            var filename = $"Test\\{now}_i.lst";
            // if(File.Exists(filename)){
            //     File.Delete(filename);
            // }
            // File.Create(filename);
            // File.Open(filename,FileMode.OpenOrCreate);
            File.Copy($"C:\\Users\\cross\\OneDrive\\Documents\\GAMS\\Studio\\workspace\\_gams_net_gjo{i}.lst", filename);

        }
    }
    public int ItterationCount { get; }
    public Model Model { get; }
    public GAMSWorkspace WS { get; }
    public Random Rnd { get; }
}