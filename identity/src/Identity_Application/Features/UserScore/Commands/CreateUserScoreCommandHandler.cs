using Identity_Application.Common.Exceptions;
using Identity_Application.Common.Interfaces.DbContexts;
using Identity_Application.Common.Interfaces.Services;
using Identity_Application.Contracts.UserScores.Commands.Create;
using Identity_Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity_Application.Features.UserScore.Commands
{
    public class CreateUserScoreCommandHandler : IRequestHandler<CreateUserScoreDetailCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserHttpContextAccessor _userHttpContextAccessor;

        public CreateUserScoreCommandHandler(IApplicationDbContext context,
            IUserHttpContextAccessor userHttpContextAccessor)
        {
            _context = context;
            _userHttpContextAccessor = userHttpContextAccessor;
        }
        public async Task<Guid> Handle(CreateUserScoreDetailCommand request, CancellationToken cancellationToken)
        {
            var userId = _userHttpContextAccessor.GetUserId();
            var user = await _context.Users
                .Include(u => u.UserScores)
                .FirstOrDefaultAsync(u => u.Id.Equals(userId));
            _ = user ?? throw new NotFoundException("user is not found");

            var textInfo = System.Globalization.CultureInfo.CurrentCulture.TextInfo;


            var education = new UserScores()
            {
                University = university,
                Speciality = speciality,
                StartDate = request.StartDate.HasValue ? request.StartDate.Value : default(DateTime),
                EndDate = request.EndDate.HasValue ? request.EndDate.Value : default(DateTime),
                UntilNow = request.UntilNow
            };

            user.UserScores.Add(education);
            await _context.SaveChangesAsync();
            return education.Id;
        }
    }
}
