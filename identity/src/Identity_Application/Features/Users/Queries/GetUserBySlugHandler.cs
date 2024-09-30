using AutoMapper;
using Identity_Application.Contracts.User.Queries;
using Identity_Application.Contracts.User.Responses;
using Identity_Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



namespace Identity_Application.Features.Users.Queries;

public class GetUserBySlugHandler : IRequestHandler<GetUserByUsernameQuery, UserResponse>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IMapper _mapper;

    public GetUserBySlugHandler(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }
    public async Task<UserResponse> Handle(GetUserByUsernameQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);
        var result = _mapper.Map<UserResponse>(user);

        return result;
    }
}
