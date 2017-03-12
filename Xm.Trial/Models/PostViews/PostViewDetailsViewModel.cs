using System.Collections.Generic;

namespace Xm.Trial.Models
{
    public class PostViewDetailsViewModel
    {
        public PostViewModel Post { get; set; }
        public IEnumerable<PostViewsModel> PostViews { get; set; }
    }
}