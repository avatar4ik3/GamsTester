namespace Tester;

public class X
{
    public bool[][] values { get; private set; } = null!;
    public int N { get; }
    public int M { get; }

    private void CreateArray()
    {
        values = new bool[N][];
        for (int i = 0; i < N; ++i)
        {
            values[i] = new bool[M];
            Array.Fill(values[i],false);
        }
    }
    public X(int n, int m)
    {
        this.N = n;
        this.M = m;
        CreateArray();
    }

    public bool this[int i, int j]
    {
        get { return values[i][j]; }
        set { values[i][j] = value; }
    }

    public X Copy(){
        var res = new X(N,M);
        for(int  i =0 ;i < N;++i){
            Array.Copy(values[i],res.values[i],M);
        }
        return res;
    }

    
}

public class Z
{
    public bool[] values { get; private set; } = null!;
    public int N {get;}

    private void CreateArray(){
        values = new bool[N];
        Array.Fill(values,false);
    }
    public Z(int n)
    {
       this.N = n;
       CreateArray();
    }

    public bool this[int i]
    {
        get { return values[i]; }
        set { values[i] = value; }
    }

    public Z Copy(){
        var res = new Z(N);
        Array.Copy(values,res.values,N);
        return res;
    }

   
}

public class S
{
    public S(int n, int m)
    {
        X = new X(n, m);
        Z = new Z(n);
    }

    public X X { get; set; }

    public Z Z { get; set; }

    public double Ro { get; set; }

    public S Copy(){
        var res = new S(X.N,X.M);
        res.X = X.Copy();
        res.Z = Z.Copy();
        res.Ro = Ro;
        return res;
    }


}
