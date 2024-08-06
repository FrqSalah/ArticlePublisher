using Markdig;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Configuration;

namespace ArticlePublisher
{
    public class PublisherFunction
    {
        private readonly ILogger<PublisherFunction> _logger;
        private readonly IMedium _mediumClient;

        public PublisherFunction(ILogger<PublisherFunction> logger, IMedium mediumClient)
        {
            _logger = logger;
            _mediumClient = mediumClient;
        }

        [Function("Publisher")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post")] HttpRequestData req)
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

            await PublishToMediumAsync(frontMatter, html);

            return new OkObjectResult(html);
        }

        public async Task PublishToMediumAsync(BlogFrontMatter blogFrontMatter, string html)
        {
            MediumPost mediumPost = new MediumPost
            {
                Title = blogFrontMatter.Title,
                ContentFormat = "html",
                Content = html,
                PublishStatus = "draft"
            };

            var mediumToken = Environment.GetEnvironmentVariable("MEDIUM_TOKEN");
            var mediumUserId = Environment.GetEnvironmentVariable("MEDIUM_USERID");

            await _mediumClient.CreatePost(mediumUserId, mediumPost, mediumToken);

        }
    }
}
