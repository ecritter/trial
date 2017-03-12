using System;

namespace Xm.Trial.Models
{
    public class PostViewsModel
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public string RefUrl { get; set; }

        public DateTimeOffset Timestamp { get; set; }   
    }
}