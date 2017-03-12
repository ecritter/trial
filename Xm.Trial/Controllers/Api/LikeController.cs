using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Xm.Trial.Models;
using Xm.Trial.Models.Data;

namespace Xm.Trial.Controllers.Api
{
    public class LikeController : ApiController
    {
        private readonly DataContext _db = new DataContext();

        [HttpPost]
        public async Task<IHttpActionResult> Like(LikeRequest likeRequest)
        {
            if (string.IsNullOrEmpty(likeRequest.UserEmail)) return BadRequest();

            var post = await _db.Posts.FindAsync(likeRequest.PostId);

            if (post == null)
                return NotFound();

            if (_db.PostLikes.SingleOrDefault(pl => pl.PostId == likeRequest.PostId && pl.Email == likeRequest.UserEmail) == null)
            {
                post.Likes++;
                _db.PostLikes.Add(new PostLike { PostId = likeRequest.PostId, Email = likeRequest.UserEmail, Timestamp = DateTime.Now });
                await _db.SaveChangesAsync();
            }
            
            var viewModel = new LikeViewModel
            {
                Id = post.Id,
                Count = post.Likes
            };

            return Ok(viewModel);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}