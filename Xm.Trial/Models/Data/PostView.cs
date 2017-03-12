using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xm.Trial.Models.Data
{
    public class PostView
    {
        [Key]
        public int Id { get; set; }

        public int PostId { get; set; }
        
        public string RefUrl { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }
    }
}