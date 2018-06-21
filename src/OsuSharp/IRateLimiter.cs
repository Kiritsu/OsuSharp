using System.Threading.Tasks;

namespace OsuSharp
{
    public interface IRateLimiter
    {
        Task HandleAsync();
    }
}
