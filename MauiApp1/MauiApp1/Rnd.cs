namespace MauiApp1;

public class Rnd
{

    Random r = new Random();
    public int r_for()
    {
        return r.Next(1, 5);
    }
    public int rr(int p)
    {
        if (p <= 3)
        {
            return r.Next(1, 10);
        }
        else if (p <= 5)
        {
            return r.Next(1, 30);
        }
        else if (p <= 7)
        {
            return r.Next(10, 60);
        }
        else if (p <= 10)
        {
            return r.Next(50, 100);
        }
        else if (p <= 13)
        {
            return r.Next(100, 150);
        }
        else if (p <= 15)
        {
            return r.Next(100, 200);
        }
        else if (p <= 20)
        {
            return r.Next(100, 500);
        }
        else if (p >= 20)
        {
            int a;
            int b;
            a = r.Next(100, 1000);
            b = r.Next(100, 1000);
            return r.Next(a, b);
        }
        return 0;

    }


}
