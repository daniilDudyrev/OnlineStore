using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Exceptions;
using OnlineStore.Domain.RepositoryInterfaces;

namespace OnlineStore.Domain.Services;

public class AccountService
{
    private readonly IAccountRepository _accountRepository;
    private readonly IPasswordHasherService _passwordHasherService;

    public AccountService(IAccountRepository accountRepository, IPasswordHasherService passwordHasherService)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        _passwordHasherService = passwordHasherService;
    }

    public virtual async Task<Account> Register(string name, string email, string password, CancellationToken cts)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (email == null)
        {
            throw new ArgumentNullException(nameof(email));
        }

        if (password == null)
        {
            throw new ArgumentNullException(nameof(password));
        }

        var existedAccount = await _accountRepository.FindByEmail(email, cts);
        var emailRegistered = existedAccount is not null;
        if (emailRegistered)
        {
            throw new EmailExistsException("Email уже зарегистрирован");
        }

        var hashedPassword = _passwordHasherService.HashPassword(password);
        var account = new Account(Guid.NewGuid(), name, email, hashedPassword);
        await _accountRepository.Add(account, cts);
        return account;
    }

    public virtual async Task<Account> Authentication(string email, string password, CancellationToken cts)
    {
        if (email == null)
        {
            throw new ArgumentNullException(nameof(email));
        }

        if (password == null)
        {
            throw new ArgumentNullException(nameof(password));
        }

        var account = await _accountRepository.FindByEmail(email, cts);
        if (account == null)
        {
            throw new EmailNotFoundException("Такого аккаунта не существует");
        }

        if (!_passwordHasherService.VerifyPassword(account.PasswordHash, password))
        {
            throw new InvalidPasswordException("Неверный пароль");
        }

        return account;
    }
}