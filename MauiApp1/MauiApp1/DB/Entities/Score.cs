using SQLite;

namespace MauiApp1.DB.Entities
{
    public class Score
    {
        [PrimaryKey, AutoIncrement]
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public int Value { get; set; }
        public int Level { get; set; }
        public DateTime Date { get; set; }

        [Ignore]
        public List<IncorrectAnswer> IncorrectAnswer { get; set; }
    }
}
