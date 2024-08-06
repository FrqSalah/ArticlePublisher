using ArticlePublisher;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;

const string IMediumHost = "https://api.medium.com";


var host = new HostBuilder()
    .ConfigureFunctionsWebApplication()
    .ConfigureServices(services =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();      
        services.AddHttpClient("HttpMedium", (provider, client) =>
        {
            client.BaseAddress = new System.Uri(IMediumHost);
            client.DefaultRequestHeaders.Add("Accept", "application/json");
        })
        .AddTypedClient(c => RestService.For<IMedium>(c));
    })
    .Build();

host.Run();
