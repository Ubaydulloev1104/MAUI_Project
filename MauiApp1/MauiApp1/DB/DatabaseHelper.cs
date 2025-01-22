using System.IO;
namespace MauiApp1.DB
{
    public static class DatabaseHelper
    {
        public static string GetDatabasePath()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(folderPath, "Math_Adventure.db");
        }
    }
}
