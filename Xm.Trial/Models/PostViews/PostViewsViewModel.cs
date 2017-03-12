using System.Collections.Generic;

namespace Xm.Trial.Models
{
    public class PostViewsViewModel
    {
        public string Title { get; set; }
        public IEnumerable<PostViewsRow> PostViews { get; set; }
    }
}