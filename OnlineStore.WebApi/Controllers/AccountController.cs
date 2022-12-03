using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data.Repositories;
using OnlineStore.Domain;
using OnlineStore.Models.Requests;

namespace OnlineStore.WebApi.Controllers;

[ApiController]
[Route("account")]
public class AccountController : ControllerBase
{
    private readonly IAccountRepository _accountRepository;

    public AccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
    }

    [HttpPost("register")]
    public async Task<ActionResult<Account>> Register(RegisterRequest request, CancellationToken cts)
    {
        if (request == null) throw new ArgumentNullException(nameof(request));
        if (_accountRepository.GetAll(cts).Result.Any(it => it.Email == request.Email))
            return Conflict(new { message = "Такой email уже зарегестрирован" });
        var account = new Account(Guid.NewGuid(), request.Name, request.Email, request.Password);
        await _accountRepository.Add(account, cts);
        return account;
    }

    [HttpGet("get_all")]
    public async Task<IReadOnlyList<Account>> GetAll()
    {
        var accounts = await _accountRepository.GetAll();
        return accounts;
    }
}