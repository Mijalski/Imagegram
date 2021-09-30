using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Mijalski.Imagegram.Domain.Accounts;
using Mijalski.Imagegram.Server.Infrastructures.Databases;
using Mijalski.Imagegram.Server.Infrastructures.Generics;
using Mijalski.Imagegram.Server.Modules.Accounts.Databases;
using Mijalski.Imagegram.Server.Modules.Accounts.Mappers;

namespace Mijalski.Imagegram.Server.Infrastructures.Services;

class CurrentAccountService : ICurrentAccountService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly ApplicationDbContext _dbContext;
    private readonly IAccountMapper _mapper;

    public CurrentAccountService(IHttpContextAccessor accessor,
        ApplicationDbContext dbContext, 
        IAccountMapper mapper)
    {
        _httpContextAccessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    public async Task<Account> GetCurrentAccountAsync(CancellationToken cancellationToken = default)
    {
        var name = _httpContextAccessor?.HttpContext?.User.FindFirstValue("Name");
        if (string.IsNullOrEmpty(name))
        {
            throw new InvalidOperationException("User is not logged into application!");
        }

        var dbAccount = await _dbContext.Set<DbAccount>().SingleAsync(a => a.Name == name, cancellationToken);
        return _mapper.Map(dbAccount);
    }
}