using CapitalPlacement.Controllers;
using CapitalPlacement.CoreLevel.ServiceContracts;
using CapitalPlacement.CoreLevel.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((_, services) =>
    {
        var configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();

        services.AddSingleton<IConfiguration>(configuration);
        services.AddSingleton(serviceProvider =>
        {
            return new CosmosClient(configuration["CosmosDB:EndpointUrl"], configuration["CosmosDB:PrimaryKey"]);
        });

        services.AddScoped(serviceProvider =>
        {
            var cosmosClient = serviceProvider.GetRequiredService<CosmosClient>();
            var database = cosmosClient.GetDatabase(configuration["CosmosDB:DatabaseName"]);
            return database.GetContainer(configuration["CosmosDB:ContainerName"]);
        });

        // All application wide services
        services.AddScoped(typeof(ICosmosService<>), typeof(CosmosService<>));
        services.AddScoped<ICommonService, CommonService>();

        // controller services...
        services.AddScoped<ProgramDetailsController>();
    });

var host = builder.Build();

var listener = new HttpListener();
listener.Prefixes.Add("http://localhost:5000/");
listener.Start();

Console.Write("Listening to Incoming Requests...");

while (true)
{
    var context = await listener.GetContextAsync();
    var request = context.Request;
    var response = context.Response;
    // handling the requests based on the url
    try
    {
        switch (request?.Url?.AbsolutePath.ToLower())
        {
            case "/programdetails":
                Console.WriteLine("Entering Program Details Controller");
                var programDetailsController = host.Services.GetRequiredService<ProgramDetailsController>();
                await programDetailsController.HandleRequest(request, response);
                break;
        }
    } catch (Exception ex) 
    {
        Console.WriteLine(ex.ToString());
    }

    response.Close();
}