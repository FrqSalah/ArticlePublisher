using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticlePublisher;

public class MediumPost
{
    public string Title { get; set; }
    public string ContentFormat { get; set; }
    public string Content { get; set; }
    public string CanonicalUrl { get; set; }
    public string[] Tags { get; set; }
    public string PublishStatus { get; set; }
}
