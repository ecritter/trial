using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xm.Trial.Models;
using Xm.Trial.Models.Data;
using System;

namespace Xm.Trial.Controllers
{
    public class BlogController : Controller
    {
        private readonly DataContext _db = new DataContext();

        public ActionResult Index()
        {
            ViewBag.Title = "Posts";
            return View();
        }
        
        public async Task<ActionResult> Posts(int offset)
        {
            int pageSize = 5;

            var posts = await _db.Posts
                                 .Select(p => new PostSnippetViewModel
                                 {
                                     Id = p.Id,
                                     Created = p.Created,
                                     Title = p.Title,
                                     Picture = p.Picture,
                                     PictureCaption = p.PictureCaption,
                                     Snippet = p.Snippet,
                                     Author = p.Author
                                 })
                                 .OrderBy(p => p.Created)
                                 .Skip(offset * pageSize)
                                 .Take(pageSize)
                                 .ToArrayAsync();
            
            return PartialView("_Post", posts);
        }

        public async Task<ActionResult> Details(int id)
        {
            var post = await _db.Posts
                                .Select(p => new PostViewModel
                                             {
                                                 Id = p.Id,
                                                 Created = p.Created,
                                                 Title = p.Title,
                                                 Picture = p.Picture,
                                                 PictureCaption = p.PictureCaption,
                                                 Text = p.Text,
                                                 Likes = p.Likes,
                                                 Author = p.Author,
                                                 Tags = p.Tags
                                             })
                                .FirstOrDefaultAsync(p => p.Id == id);

            if (post == null)
                return new HttpNotFoundResult();

            string refUrl = (Request.UrlReferrer == null) ? "" : Request.UrlReferrer.ToString();

            _db.PostViews.Add(new PostView { PostId = id, RefUrl = refUrl, Timestamp = DateTime.Now });
            await _db.SaveChangesAsync();

            ViewBag.Title = post.Title;
            ViewBag.UserEmail = User.Identity.IsAuthenticated ? 
                ClaimsPrincipal.Current.Claims.Where(c => c.Type == ClaimTypes.Email).Select(c => c.Value).SingleOrDefault() : string.Empty;

            return View(post);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _db.Dispose();

            base.Dispose(disposing);
        }
    }
}