using System.Linq;
using System.Threading.Tasks;
using GigaFix.Data;
using Microsoft.EntityFrameworkCore;

namespace GigaFix.Services;

public class AuthService
{
    public string AuthenticatedUserName { get; set; }
    
    private readonly AppDbContext _dbContext;
    public AuthService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        
    }

    public async Task<bool> Login(string login, string password)
    {
        var res =  await _dbContext.Users
            .Where(u => u.Login == login && u.Password == password)
            .FirstOrDefaultAsync();
        if (res != null)
        {
            AuthenticatedUserName = res.FullnameUser ?? "Гость";
            return true;
        }
        else
            return false;
    }
}