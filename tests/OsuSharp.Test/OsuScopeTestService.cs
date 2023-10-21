using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OsuSharp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OsuSharp.Test;

public class OsuScopeTestService : BackgroundService
{
    private readonly IServiceProvider _services;
    private readonly ILogger<OsuScopeTestService> _logger;

    public OsuScopeTestService(IServiceProvider services, ILogger<OsuScopeTestService> logger)
    {
        _services = services;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var scope1 = _services.CreateScope();
        var os1 = scope1.ServiceProvider.GetRequiredService<IOsuClient>();
        os1.UpdateAccessToken("A", null, 86400);

        var scope2 = _services.CreateScope();
        var os2 = scope2.ServiceProvider.GetRequiredService<IOsuClient>();
        os2.UpdateAccessToken("B", null, 86400);

        var scope3 = _services.CreateScope();
        var os3 = scope3.ServiceProvider.GetRequiredService<IOsuClient>();
        os3.UpdateAccessToken("C", null, 86400);

        var tk1 = await os1.GetOrUpdateAccessTokenAsync(stoppingToken);
        var tk2 = await os2.GetOrUpdateAccessTokenAsync(stoppingToken);
        var tk3 = await os3.GetOrUpdateAccessTokenAsync(stoppingToken);

        _logger.LogInformation("OsuSharp1: {Tk1}; OsuSharp2: {Tk2}; OsuSharp3: {Tk3}", tk1.AccessToken, tk2.AccessToken, tk3.AccessToken);
    }
}
