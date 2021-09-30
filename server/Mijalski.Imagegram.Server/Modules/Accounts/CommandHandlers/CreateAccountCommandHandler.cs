using Mijalski.Imagegram.Domain.Accounts;
using Mijalski.Imagegram.Server.Infrastructures.Databases;
using Mijalski.Imagegram.Server.Infrastructures.Generics;
using Mijalski.Imagegram.Server.Modules.Accounts.Databases;
using Mijalski.Imagegram.Server.Modules.Accounts.Jwts;

namespace Mijalski.Imagegram.Server.Modules.Accounts.CommandHandlers;

public record CreateAccountCommand(string Name, string Password);

class CreateAccountCommandHandler
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IAccountManager _accountManager;
    private readonly IJwtTokenGeneratorService _jwtTokenGeneratorService;
    private readonly IMapper<Account, DbAccount> _mapper;

    public CreateAccountCommandHandler(ApplicationDbContext dbContext, 
        IMapper<Account, DbAccount> mapper, 
        IAccountManager accountManager, 
        IJwtTokenGeneratorService jwtTokenGeneratorService)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _accountManager = accountManager ?? throw new ArgumentNullException(nameof(accountManager));
        _jwtTokenGeneratorService = jwtTokenGeneratorService ?? throw new ArgumentNullException(nameof(jwtTokenGeneratorService));
    }

    public async Task CreateAsync(CreateAccountCommand command, CancellationToken cancellationToken = default)
    {
        var account = await _accountManager.CreateAccountAsync(command.Name, cancellationToken);

        var dbAccount = _mapper.Map(account);
        dbAccount.PasswordHash = await _jwtTokenGeneratorService.GenerateTokenAsync(account, cancellationToken);

        await _dbContext.Set<DbAccount>().AddAsync(dbAccount, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}