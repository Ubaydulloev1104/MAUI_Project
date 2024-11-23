using AutoMapper;
using Identity_Application.Common.Interfaces.DbContexts;
using Identity_Application.Contracts.User.Queries;
using Identity_Application.Contracts.User.Responses;
using Identity_Application.Contracts.UserScores.Queries;
using Identity_Application.Contracts.UserScores.Response;
using Identity_Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity_Application.Features.UserScore.Queries;

public class GetAllUserScoresQueryHandler : IRequestHandler<GetAllUsersScoresQuery, List<UserScoresResponse>>
{
    private readonly IApplicationDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllUserScoresQueryHandler(IApplicationDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    public async Task<List<UserScoresResponse>> Handle(GetAllUsersScoresQuery request, CancellationToken cancellationToken)
    {
        var educations = await _dbContext.UserScores
             .ToListAsync();
        var response = educations.
                 Select(e => _mapper.Map<UserScoresResponse>(e)).ToList();
        return response;
    }
}
