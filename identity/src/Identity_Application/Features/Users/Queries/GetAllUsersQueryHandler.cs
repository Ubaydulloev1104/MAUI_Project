using AutoMapper;
using Identity_Application.Contracts.User.Responses;
using Identity_Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TSR_Accoun_Application.Contracts.User.Queries;

namespace Identity_Application.Features.Users.Queries;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<UserResponse>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }
    public async Task<List<UserResponse>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userManager.Users.ToListAsync(cancellationToken);
        var result = _mapper.Map<List<UserResponse>>(users);

        return result;
    }
}
