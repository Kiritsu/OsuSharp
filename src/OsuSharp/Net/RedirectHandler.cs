using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace OsuSharp.Net;

internal class RedirectHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken)
    {
        var response = await base.SendAsync(request, cancellationToken);
        if (response.StatusCode != HttpStatusCode.Found)
        {
            return response;
        }

        var newRoute = response.Headers.Location;
        if (newRoute == null)
        {
            return response;
        }

        request.RequestUri = newRoute;
        return await base.SendAsync(request, cancellationToken);
    }
}