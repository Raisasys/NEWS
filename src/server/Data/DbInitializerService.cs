using Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

/*public class DbInitializerService : IDbInitializerService
{
	private const int PhoneNumberConfirmTicketExpirationMinutes = 30;
	private const int EmailConfirmTicketExpirationHours = 24;

	private readonly IServiceScopeFactory _scopeFactory;
    //private readonly ISecurityService _securityService;

    public DbInitializerService(IServiceScopeFactory scopeFactory/*, ISecurityService securityService#1#)
    {
        _scopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
        //_securityService = securityService ?? throw new ArgumentNullException(nameof(securityService));
    }

    public void Initialize()
    {
        using var serviceScope = _scopeFactory.CreateScope();
        using var context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
        context.Database.Migrate();
    }

    public void SeedData()
    {
        
    }
}*/