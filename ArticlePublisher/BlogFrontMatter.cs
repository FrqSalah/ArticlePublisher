﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YamlDotNet.Serialization;

namespace ArticlePublisher;

public class BlogFrontMatter
{
    [YamlMember(Alias = "layout")]
    public string layout { get; set; }

    [YamlMember(Alias = "tags")]
    public string Tags { get; set; }

    [YamlMember(Alias = "title")]
    public string Title { get; set; }

    [YamlMember(Alias = "image")]
    public string Image { get; set; }

    [YamlMember(Alias = "image_credit_name")]
    public string ImageCreditName { get; set; }

    [YamlMember(Alias = "image_credit_url")]
    public string ImageCreditUrl { get; set; }

    [YamlMember(Alias = "image_alt")]
    public string ImageAlt { get; set; }

    [YamlMember(Alias = "redirect_from")]
    public string[] RedirectFrom { get; set; }

    [YamlIgnore]
    public IList<string> GetTags => Tags?
        .Split(",", StringSplitOptions.RemoveEmptyEntries)
        .Select(x => x.Trim())
        .ToArray();
}
