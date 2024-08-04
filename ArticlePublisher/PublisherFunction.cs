using Markdig;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json.Serialization;
using FromBodyAttribute = Microsoft.Azure.Functions.Worker.Http.FromBodyAttribute;

using static System.Net.Mime.MediaTypeNames;
using Microsoft.Azure.Functions.Worker.Http;

namespace ArticlePublisher
{
    public class PublisherFunction
    {
        private readonly ILogger<PublisherFunction> _logger;

        public PublisherFunction(ILogger<PublisherFunction> logger)
        {
            _logger = logger;
        }

        [Function("Publisher")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            // read body
            var content = await new StreamReader(req.Body).ReadToEndAsync();

            var frontMatter = content.GetFrontMatter<BlogFrontMatter>();

            var pipeline = new MarkdownPipelineBuilder()
            .UseYamlFrontMatter()
            .Use<GitHubUserProfileExtension>()
                    .Build();

            var html = Markdown
                .ToHtml(content!, pipeline);

            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult(html);
        }
    }
}
