
using AutoMapper;
using Identity_Application.Common.Exceptions;
using Identity_Application.Common.Interfaces.DbContexts;
using Identity_Application.Common.Interfaces.Services;
using Identity_Application.Contracts.Claim.Responses;
using Identity_Application.Contracts.User.Responses;
using Identity_Application.Contracts.UserScores.Queries;
using Identity_Application.Contracts.UserScores.Response;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Identity_Application.Features.UserScores.Queries
{
    public class GetUserScoresBySlugHandler : IRequestHandler<GetUserScoresByUsernameQuery, UserScoresResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IUserHttpContextAccessor _userHttpContextAccessor;

        public GetUserScoresBySlugHandler(IApplicationDbContext context, IMapper mapper, IUserHttpContextAccessor userHttpContextAccessor)
        {
            _context = context;
            _mapper = mapper;
            _userHttpContextAccessor = userHttpContextAccessor;
        }
        public async Task<UserScoresResponse> Handle(GetUserScoresByUsernameQuery request, CancellationToken cancellationToken)
        {
            var roles = _userHttpContextAccessor.GetUserRoles();
            var userName = _userHttpContextAccessor.GetUserName();
            if (request.UserName != null && roles.Any(role => role == "Applicant") && userName != request.UserName)
                throw new ForbiddenAccessException("Access is denied");

            if (request.UserName != null)
                userName = request.UserName;


            var user = await _context.Users
                .Include(u => u.UserScores)
                .FirstOrDefaultAsync(u => u.UserName == userName);
            _ = user ?? throw new NotFoundException("user is not found");

            var userEducationResponses = user.UserScores;
            var result = _mapper.Map<UserScoresResponse>(userEducationResponses);
            return result;

       
        }
    }
}
