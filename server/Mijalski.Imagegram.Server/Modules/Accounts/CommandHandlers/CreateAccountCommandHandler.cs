using Mijalski.Imagegram.Domain.Accounts;
using Mijalski.Imagegram.Server.Infrastructures.Databases;
using Mijalski.Imagegram.Server.Infrastructures.Generics;
using Mijalski.Imagegram.Server.Modules.Accounts.Databases;
using Mijalski.Imagegram.Server.Modules.Accounts.Passwords;

namespace Mijalski.Imagegram.Server.Modules.Accounts.CommandHandlers;

public record CreateAccountCommand(string Name, string Password);

class CreateAccountCommandHandler
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IAccountManager _accountManager;
    private readonly IAccountPasswordService _accountPasswordService;
    private readonly IMapper<Account, DbAccount> _mapper;

    public CreateAccountCommandHandler(ApplicationDbContext dbContext, 
        IMapper<Account, DbAccount> mapper, 
        IAccountManager accountManager,
        IAccountPasswordService accountPasswordService)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        _accountManager = accountManager ?? throw new ArgumentNullException(nameof(accountManager));
        _accountPasswordService = accountPasswordService ?? throw new ArgumentNullException(nameof(accountPasswordService));
    }

    public async Task CreateAsync(CreateAccountCommand command, CancellationToken cancellationToken = default)
    {
        var account = await _accountManager.CreateAccountAsync(command.Name, cancellationToken);

        var dbAccount = _mapper.Map(account);
        dbAccount.PasswordHash = _accountPasswordService.CreatePasswordHash(command.Password);

        await _dbContext.Set<DbAccount>().AddAsync(dbAccount, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}