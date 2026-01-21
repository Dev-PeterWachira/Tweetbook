using Tweetbook.Domain;
using System.Threading.Tasks;

namespace Tweetbook.Services
   
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string email, string password);
        
        Task<AuthenticationResult> LoginAsync(string ermail, string password);
    }
}

