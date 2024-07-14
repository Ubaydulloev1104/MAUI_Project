namespace MauiApp1;

public partial class NewPage2 : ContentPage
{
    public int shotchik = 0;
    public int pravelni_otvet = 0;
    public int rnd_ot = 0;
    public int nepravelni_otvet = 0;

    public NewPage1()
	{
		InitializeComponent();
        Label_1.Text = "0";
    }
    public int p1 = 0;
    public int p2 = 0;
    public int otvet = 0;

    private void Botton_1_Clicked(object sender, EventArgs e)
    {
        formula formula = new formula();
        try
        {
            otvet = int.Parse(Textbox_1.Text);
            int ot_p_p = formula.fd(p1, p2);
            if (otvet != ot_p_p)
            {
                nepravelni_otvet += 1;
                rnd_ot -= 1;
                DisplayAlert("System", $"Неправилно😕,правилний ответ {ot_p_p}!", "ok");
            }
            else if (otvet == ot_p_p)
            {
                if (shotchik != 0)
                {
                    pravelni_otvet += 1;
                    rnd_ot += 1;
                }
            }
        }
        catch (Exception)
        {
            otvet = 0;
        }
        Botton_1.Text = "ok";
        Rnd rnd = new Rnd();
        p1 = rnd.rr(rnd_ot);
        p2 = rnd.rr(rnd_ot);
        Label_1_pra_nepra.Text = $"Правилный ответ={pravelni_otvet}  Не правилный ответ= {nepravelni_otvet}";
        Label_1.Text = $"{formula.f_text(p1, p2)} ";
        shotchik += 1;
        Textbox_1.Text = "";

    }
}