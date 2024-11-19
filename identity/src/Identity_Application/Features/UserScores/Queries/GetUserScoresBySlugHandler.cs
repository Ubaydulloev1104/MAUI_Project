
using AutoMapper;
using Identity_Application.Common.Exceptions;
using Identity_Application.Common.Interfaces.DbContexts;
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

        public GetUserScoresBySlugHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<UserScoresResponse> Handle(GetUserScoresByUsernameQuery request, CancellationToken cancellationToken)
        {
            var userscores = await _context.UserScores.SingleOrDefaultAsync(s => s.User.UserName == request.UserName.Trim().ToLower(),
                 cancellationToken);
            _ = userscores ?? throw new NotFoundException("userscores not found");
            var result = _mapper.Map<UserScoresResponse>(userscores);
            return result;

       
        }
    }
}
