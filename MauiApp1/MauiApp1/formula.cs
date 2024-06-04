namespace MauiApp1;

public class formula
{
    public formula()
    {

    }
    public static void rr()
    {
        Rnd rnd = new Rnd();
        s = rnd.r_for();
        r = s;
    }

    public static int s;
    public static int r;
    public int fd(int p1, int p2)
    {

        if (r == 1)
        {
            return p1 + p2;
        }
        else if (r == 2)
        {
            return p1 - p2;
        }
        else if (r == 3)
        {

            return p1 / p2;
        }
        else
        {
            return p1 * p2;
        }

    }
    public string f_text(int p1_t, int p2_t)
    {
        rr();
        if (s == 1)
        {
            return $"{p1_t} + {p2_t}=?";
        }
        else if (s == 2)
        {
            return $"{p1_t} - {p2_t}=?";
        }
        else if (s == 3)
        {
            return $"{p1_t} / {p2_t}=?";
        }
        else
        {
            return $"{p1_t} * {p2_t}=?";
        }

    }
}