# Markdown to Medium Azure Function (C#)

This Azure Function, implemented in C# (.NET 8.0), takes a Markdown file as input, converts it to HTML, and publishes the content to Medium.

## Features

- Accepts Markdown input
- Converts Markdown to HTML using Markdig
- Publishes the HTML content to Medium
- Runs as an Azure Function
- Utilizes Application Insights for monitoring

## Prerequisites

- Azure account
- .NET 8.0 SDK
- Azure Functions Core Tools
- Visual Studio 2022 or Visual Studio Code with C# extension

## Dependencies

This project uses the following NuGet packages:

```xml
<PackageReference Include="Markdig" Version="0.37.0" />
<PackageReference Include="Microsoft.Azure.Functions.Worker" Version="1.21.0" />
<PackageReference Include="Microsoft.Azure.Functions.Worker.Extensions.Http" Version="3.1.0" />
<PackageReference Include="Microsoft.Azure.Functions.Worker.Extensions.Http.AspNetCore" Version="1.2.1" />
<PackageReference Include="Microsoft.Azure.Functions.Worker.Sdk" Version="1.17.0" />
<PackageReference Include="Microsoft.ApplicationInsights.WorkerService" Version="2.22.0" />
<PackageReference Include="Microsoft.Azure.Functions.Worker.ApplicationInsights" Version="1.2.0" />
<PackageReference Include="Refit.HttpClientFactory" Version="7.1.2" />
<PackageReference Include="System.Net.Http" Version="4.3.4" />
<PackageReference Include="YamlDotNet" Version="16.0.0" />
```

## Setup

1. Clone this repository
2. Open the solution in Visual Studio 2022 or Visual Studio Code
3. Restore NuGet packages
4. Create a `local.settings.json` file in the project root:
   ```json
   {
     "IsEncrypted": false,
     "Values": {
       "AzureWebJobsStorage": "UseDevelopmentStorage=true",
       "FUNCTIONS_WORKER_RUNTIME": "dotnet-isolated",
       "MEDIUM_TOKEN": "your-medium-api-token",
       "MEDIUM_USERID": "your-medium-user-id"
     }
   }
   ```
5. Replace `your-medium-api-token` with your actual Medium API token and `your-medium-user-id` with your Medium user ID

## Usage

To run the function locally:

1. Press F5 in Visual Studio 2022 or use the Azure Functions Core Tools:
   ```
   func start
   ```

2. To trigger the function, send a POST request to the function URL. Here's an example using curl:

   ```
   curl -X POST http://localhost:7099/api/Publisher \
   -H "Content-Type: text/markdown" \
   -d "# Your Markdown Content Here"
   ```

   Or, if you're using a tool like Postman:
   
   ```
   POST http://localhost:7099/api/Publisher
   Content-Type: text/markdown

   # Your Markdown Content Here
   ```

   Replace `http://localhost:7099` with your actual function URL when deployed.

## Deployment

Deploy to Azure using Visual Studio 2022 or Azure Functions Core Tools:

```
func azure functionapp publish <YourFunctionAppName>
```

## Configuration

Set the following environment variables in your Azure Function App settings:

- `MEDIUM_TOKEN`: Your Medium API access token
- `MEDIUM_USERID`: Your Medium user ID

## Monitoring

This function uses Application Insights for monitoring. You can view logs, performance data, and other metrics in the Azure portal.

## License

[MIT License](LICENSE)
