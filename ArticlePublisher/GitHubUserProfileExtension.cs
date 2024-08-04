using Markdig.Renderers;
using Markdig;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticlePublisher;

public class GitHubUserProfileExtension : IMarkdownExtension
{
    public void Setup(MarkdownPipelineBuilder pipeline)
    {
        if (!pipeline.InlineParsers.Contains<GitHubUserProfileParser>())
        {
            pipeline.InlineParsers.Insert(0, new GitHubUserProfileParser());
        }
    }

    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
    }
}