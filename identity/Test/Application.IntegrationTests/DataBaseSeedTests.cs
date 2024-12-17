using FluentAssertions;
using Identity_Domain.Entities;
using Identity_Infrastructure.Identity;
using System.Security.Claims;


namespace Application.IntegrationTests;

public class DataBaseSeedTests : BaseTest
{
    #region SuperAdmin

    private async Task<ApplicationUser> GetSuperAdmin() =>
    await GetEntity<ApplicationUser>(s => s.UserName == "SuperAdmin");

    [Test]
    public async Task SuperAdminRole()
    {
        var superAdminRole = await GetEntity<ApplicationRole>(s =>
            s.NormalizedName == ApplicationClaimValues.SuperAdministrator.ToUpper());
        superAdminRole.Should().NotBeNull();
    }

    [Test]
    public async Task SuperAdminUser()
    {
        (await GetSuperAdmin()).Should().NotBeNull();
    }

    [Test]
    public async Task SuperAdminClaims()
    {
        var superAdmin = await GetSuperAdmin();

        var roleClaim = await GetEntity<ApplicationUserClaim>(s =>
            s.UserId == superAdmin.Id &&
            s.ClaimType == ClaimTypes.Role);

        roleClaim.Should().NotBeNull();
    }

    #endregion

    #region Au007Admin

    private async Task<ApplicationUser> GetMathAAdmin() =>
        await GetEntity<ApplicationUser>(s => s.UserName == "Au007Admin");

    [Test]
    public async Task MAthA_AdminRole()
    {
        var superAdmin = await GetMathAAdmin();
        var superAdminRole = await GetEntity<ApplicationRole>(s =>
            s.NormalizedName == ApplicationClaimValues.Administrator.ToUpper());
        superAdminRole.Should().NotBeNull();

        var userRole = await GetEntity<ApplicationUserRole>(s =>
            s.RoleId == superAdminRole.Id &&
            s.UserId == superAdmin.Id);

        userRole.Should().NotBeNull();
    }

    [Test]
    public async Task MathAAdminUser()
    {
        (await GetMathAAdmin()).Should().NotBeNull();
    }

    [Test]
    public async Task MAthAAdminClaims()
    {
        var superAdmin = await GetMathAAdmin();

        var roleClaim = await GetEntity<ApplicationUserClaim>(s =>
            s.UserId == superAdmin.Id &&
            s.ClaimType == ClaimTypes.Role);

        roleClaim.Should().NotBeNull();
    }

    #endregion
}
