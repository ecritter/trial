using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Xm.Trial.Models.Data
{
    public class PostLike
    {
        [Key]
        public int Id { get; set; }

        public int PostId { get; set; }
        
        [MaxLength(254)]
        public string Email { get; set; }

        public DateTimeOffset Timestamp { get; set; }

        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; }
    }
}