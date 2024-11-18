namespace Identity_Application.Contracts.UserScores.Response
{
    public record class UserScoresResponse
    {
        public Guid Id { get; init; }
        public string UserName { get; init; }
        public string PhoneNumber { get; init; }
        public int Score { get; set; }
        public string IncorrectQuestion { get; set; }
    }
}
