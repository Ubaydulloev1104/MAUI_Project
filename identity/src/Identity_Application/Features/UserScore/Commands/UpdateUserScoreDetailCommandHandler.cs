
using AutoMapper;
using Identity_Application.Common.Exceptions;
using Identity_Application.Common.Interfaces.DbContexts;
using Identity_Application.Common.Interfaces.Services;
using Identity_Application.Contracts.UserScores.Commands.Update;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity_Application.Features.UserScore.Commands;

public class UpdateUserScoreDetailCommandHandler : IRequestHandler<UpdateUserScoreDetailCommand, Guid>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserHttpContextAccessor _userHttpContextAccessor;
    private readonly IMapper _mapper;

    public UpdateUserScoreDetailCommandHandler(IApplicationDbContext context,
        IUserHttpContextAccessor userHttpContextAccessor,
        IMapper mapper)
    {
        _context = context;
        _userHttpContextAccessor = userHttpContextAccessor;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(UpdateUserScoreDetailCommand request, CancellationToken cancellationToken)
    {
        var userId = _userHttpContextAccessor.GetUserId();
        var user = await _context.Users
            .Include(u => u.UserScores)
            .FirstOrDefaultAsync(u => u.Id.Equals(userId));
        _ = user ?? throw new NotFoundException("user is not found");

        var UserScore = user.UserScores.FirstOrDefault(e => e.Id.Equals(request.Id));
        _ = UserScore ?? throw new NotFoundException("UserScores not exits");

        UserScore.Score = request.Score;
        UserScore.IncorrectQuestion = request.IncorrectQuestion;
        UserScore.LastUpdated = request.LastUpdated;
        await _context.SaveChangesAsync();
        return UserScore.Id;
    }
}

