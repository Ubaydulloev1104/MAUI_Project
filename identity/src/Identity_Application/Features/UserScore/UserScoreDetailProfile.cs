using AutoMapper;
using Identity_Application.Contracts.UserScores.Response;
using Identity_Application.Features.UserScore.Commands;
using Identity_Domain.Entities;

namespace Identity_Application.Features.UserScore;

public class UserScoreDetailProfile : Profile
{
    public UserScoreDetailProfile()
    {
        CreateMap<UpdateUserScoreDetailCommandHandler, UserScores> ();
        CreateMap<UserScores, UserScoresResponse>();
    }
}
