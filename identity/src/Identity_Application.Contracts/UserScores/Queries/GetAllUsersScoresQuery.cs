using Identity_Application.Contracts.User.Responses;
using Identity_Application.Contracts.UserScores.Response;
using MediatR;

namespace Identity_Application.Contracts.UserScores.Queries
{
    public class GetAllUsersScoresQuery : IRequest<List<UserScoresResponse>>
    {

    }
}
