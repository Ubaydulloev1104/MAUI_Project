using Identity_Application.Common.Exceptions;
using Identity_Application.Common.Interfaces.DbContexts;
using Identity_Application.Common.Interfaces.Services;
using Identity_Application.Contracts.UserScores.Commands.Delete;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Identity_Application.Features.UserScore.Commands;

public class DeleteUserScoreDetailHandler: IRequestHandler<DeleteUserScoreCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IUserHttpContextAccessor _userHttpContextAccessor;

    public DeleteUserScoreDetailHandler(IApplicationDbContext context, IUserHttpContextAccessor userHttpContextAccessor)
    {
        _context = context;
        _userHttpContextAccessor = userHttpContextAccessor;
    }

    public async Task<bool> Handle(DeleteUserScoreCommand request,
        CancellationToken cancellationToken)
    {
        var userId = _userHttpContextAccessor.GetUserId();
        var user = await _context.Users
            .Include(u => u.UserScores)
            .FirstOrDefaultAsync(u => u.Id == userId);
        _ = user ?? throw new NotFoundException("user is not found");

        var education = user.UserScores.FirstOrDefault(e => e.Id == request.Id);
        _ = education ?? throw new NotFoundException("This User has not such education");

        user.UserScores.Remove(education);

        await _context.SaveChangesAsync();

        return true;
    }
}
