using System;
using System.Threading;
using System.Threading.Tasks;
using OsuSharp.Interfaces;
using OsuSharp.Misc;

namespace OsuSharp
{
    internal sealed class RateLimiter : IRateLimiter
    {
        private SemaphoreSlim Semaphore { get; }

        private int Requests { get; set; }

        private int MaxRequests { get; }

        private TimeSpan TimeInterval { get; }

        private DateTime Time { get; set; }

        private IOsuSharpLogger Logger { get; }

        private bool ThrowsOnMaxRequests { get; }

        public RateLimiter(int maxRequests, TimeSpan timeInterval, bool throwsOnMaxRequests, IOsuSharpLogger logger)
        {
            MaxRequests = maxRequests;
            TimeInterval = timeInterval;
            ThrowsOnMaxRequests = throwsOnMaxRequests;
            Logger = logger;

            Semaphore = new SemaphoreSlim(1);

            Time = DateTime.Now + timeInterval;
        }

        public async Task HandleAsync()
        {
            await HandleAsync(CancellationToken.None);
        }

        public async Task HandleAsync(CancellationToken cancellationToken)
        {
            await Semaphore.WaitAsync(cancellationToken).ConfigureAwait(false);

            try
            {
                DateTime now = DateTime.Now;

                if (Time - now <= TimeSpan.Zero)
                {
                    Time = now + TimeInterval;
                    Requests = 0;
                }
                else if (Requests > MaxRequests && ThrowsOnMaxRequests)
                {
                    throw new OsuSharpException("OsuSharp Internal rate limit reached.");
                }
                else if (Requests > MaxRequests)
                {
                    Logger.LogMessage(LoggingLevel.Warning, "RateLimiter",
                        $"Rate limit exceeded. Queuing the current request, retrying after {(int) (Time - now).TotalMilliseconds}ms",
                        now);
                    await Task.Delay(Time - now, cancellationToken).ConfigureAwait(false);
                    Time = DateTime.Now + TimeInterval;
                    Requests = 0;
                }
                else
                {
                    Requests++;
                }
            }
            finally
            {
                Semaphore.Release();
            }
        }
    }
}