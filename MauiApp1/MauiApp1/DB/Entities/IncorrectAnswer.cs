namespace MauiApp1.DB.Entities
{
    public class IncorrectAnswer
    {
        public Guid Id { get; set; }
        public string Problem { get; set; }
        public int CorrectAnswer { get; set; }
        public int UserAnswer { get; set; }
    }

}
