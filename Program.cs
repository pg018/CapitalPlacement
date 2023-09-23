﻿using CapitalPlacement.Controllers;
using CapitalPlacement.CoreLevel.ServiceContracts;
using CapitalPlacement.CoreLevel.Services;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Net;
using System.Text.Json;

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
        services.AddScoped<IApplicationFormService, ApplicationFormService>();

        // controller services...
        services.AddScoped<ProgramDetailsController>();
        services.AddScoped<AppFormController>();
    });

var host = builder.Build();

var listener = new HttpListener();
listener.Prefixes.Add("http://localhost:5000/");
listener.Start();

Console.Write("Listening to Incoming Requests...");

while (true)
{
    var _commonService = host.Services.GetRequiredService<ICommonService>();
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
            case "/applicationform":
                Console.WriteLine("Entering Application Form Controller");
                var appInfoController = host.Services.GetRequiredService<AppFormController>();
                await appInfoController.HandleRequest(request, response);
                break;
        }
    }
    catch (KeyNotFoundException ex)
    {
        Console.WriteLine("Key Not Found");
        Console.WriteLine(ex.ToString());
        await _commonService.SendResponse(
            HttpStatusCode.BadRequest,
            "Properties in JSON are Missing", response, true);
    }
    catch (JsonException ex) when (ex.InnerException is FormatException)
    {
        Console.WriteLine("Format Exception");
        Console.WriteLine(ex.ToString());
        await _commonService.SendResponse(HttpStatusCode.BadRequest, "Invalid JSON Format",response, true);
    }
    catch (JsonException ex)
    {
        Console.WriteLine("JSON Exception");
        Console.WriteLine(ex.ToString());
        await _commonService.SendResponse(HttpStatusCode.BadRequest, "Please Check JSON Format", response, true);
    }
    catch (Exception ex) 
    {
        Console.WriteLine("General Exception");
        Console.WriteLine(ex.ToString());
        await _commonService.SendResponse(
            HttpStatusCode.InternalServerError,
            "Internal Server Error", response, true);
    }

    response.Close();
}