using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Exceptions;
using OnlineStore.Domain.RepositoryInterfaces;

namespace OnlineStore.Domain.Services;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;

    public AccountService(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
    }

    public virtual async Task<Account> Register(string name, string email, string password, CancellationToken cts)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));
        if (email == null) throw new ArgumentNullException(nameof(email));
        if (password == null) throw new ArgumentNullException(nameof(password));
        var existedAccount = await _accountRepository.FindByEmail(email, cts);
        var emailRegistered = existedAccount is not null;
        if (emailRegistered)
            throw new EmailExistsException("Email уже зарегистрирован");
        var account = new Account(Guid.NewGuid(), name, email, password);
        await _accountRepository.Add(account, cts);
        return account;
    }
}
