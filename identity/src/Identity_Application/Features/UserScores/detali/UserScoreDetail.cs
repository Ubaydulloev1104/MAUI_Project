using AutoMapper;
using Identity_Application.Contracts.UserScores.Commands.Update;
using Identity_Application.Contracts.UserScores.Response;
using Identity_Domain.Entities;

namespace Identity_Application.Features.UserScores;

public class UserScoreDetail : Profile
{
    public UserScoreDetail()
    {
        CreateMap<UpdateUserScoreDetailCommand, UserScores>();
        CreateMap<UserScores, UserScoresResponse>();
    }
}
