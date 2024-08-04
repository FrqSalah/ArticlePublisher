using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArticlePublisher
{
    public interface IMedium
    {
        [Post("/v1/users/{userId}/posts")]
        Task CreatePost(string userId, [Body] MediumPost mediumPostJson, [Header("Authorization")] string authorization);
    }
}
