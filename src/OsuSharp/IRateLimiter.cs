using System.Threading;
using System.Threading.Tasks;

namespace OsuSharp
{
    public interface IRateLimiter
    {
        Task HandleAsync();

        Task HandleAsync(CancellationToken cancellationToken);
    }
}
