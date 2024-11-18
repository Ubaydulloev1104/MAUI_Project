using Identity_Application.Contracts.UserScores.Response;
using MediatR;

namespace Identity_Application.Contracts.UserScores.Queries
{
    public class GetUserScoresByUsernameQuery : IRequest<UserScoresResponse>
    {
        public string UserName { get; set; }
    }
}
