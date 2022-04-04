using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace OsuSharp.Legacy;

internal sealed class RateLimiter
{
    internal RateLimiterConfiguration Configuration { get; }

    internal int RequestCount { get; set; }

    private SemaphoreSlim Semaphore { get; }

    private DateTimeOffset TimeReference { get; set; }

    internal RateLimiter(RateLimiterConfiguration rateLimiterConfiguration)
    {
        Configuration = rateLimiterConfiguration;

        Semaphore = new SemaphoreSlim(1);

        RequestCount = 0;

        TimeReference = DateTimeOffset.Now + Configuration.Interval;
    }

    internal async Task HandleAsync(CancellationToken token = default)
    {
        await Semaphore.WaitAsync(token).ConfigureAwait(false);

        try
        {
            var now = DateTimeOffset.Now;

            if (TimeReference - now <= TimeSpan.Zero)
            {
                TimeReference = now + Configuration.Interval;
                RequestCount = 0;
            }
            else if (RequestCount > Configuration.MaxRequest)
            {
                if (Configuration.ThrowOnRatelimitHit)
                {
                    throw new OsuSharpException("Internal rate limit reached.", HttpStatusCode.BadRequest);
                }

                await Task.Delay(TimeReference - now, token).ConfigureAwait(false);

                TimeReference = DateTimeOffset.Now + Configuration.Interval;
                RequestCount = 0;
            }
        }
        finally
        {
            Semaphore.Release();
        }
    }

    internal void IncrementRequestCount()
    {
        RequestCount++;
    }
}