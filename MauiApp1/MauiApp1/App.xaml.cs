using MauiApp1.DB;

namespace MauiApp1
{
    public partial class App : Application
    {
        public static LocalDatabase Database { get; private set; }

        public App(LocalDatabase database)
        {
            InitializeComponent();
            Database = database;
            MainPage = new MainPage();
        }
    }
}
