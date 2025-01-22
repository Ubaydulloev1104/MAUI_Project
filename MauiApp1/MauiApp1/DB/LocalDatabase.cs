using MauiApp1.DB.Entities;
using SQLite;

namespace MauiApp1.DB
{
    public class LocalDatabase
    {
        private readonly SQLiteConnection _database;

        public LocalDatabase(string dbPath)
        {
            _database = new SQLiteConnection(dbPath);
            _database.CreateTable<Score>();
            _database.CreateTable<IncorrectAnswer>();
        }

        public void AddScore(Score score)
        {
            _database.Insert(score);
            if (score.IncorrectAnswer != null && score.IncorrectAnswer.Count > 0)
            {
                foreach (var answer in score.IncorrectAnswer)
                {
                    _database.Insert(answer);
                }
            }
        }

        public List<Score> GetAllScores() => _database.Table<Score>().ToList();

        public List<IncorrectAnswer> GetIncorrectAnswers(Guid scoreId)
        {
            return _database.Table<IncorrectAnswer>().Where(a => a.Id == scoreId).ToList();
        }
    }
}
