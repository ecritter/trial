using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Xm.Trial.Models;
using Xm.Trial.Models.Data;

namespace Xm.Trial.Controllers
{
    public class PostViewsController : Controller
    {
        private readonly DataContext _db = new DataContext();

        public ActionResult Index()
        {
            //var postInfos = _db.Posts
            //    .GroupJoin(_db.PostViews,
            //    p => p.Id,
            //    pv => pv.PostId,
            //    (p, pve) => 
            //        new
            //        {
            //            p = p,
            //            pve = pve
            //        }
            //    )
            //    .SelectMany(
            //    temp => temp.pve.DefaultIfEmpty(),
            //    (temp, pv) =>
            //        new
            //        {
            //            p = temp.p,
            //            pv = pv
            //        }
            //    );

            var postViewsQuery = from p in _db.Posts
                                 join c in _db.PostViews on p.Id equals c.PostId into j1
                                 from j2 in j1.DefaultIfEmpty()
                                 group j2 by new { p.Id, p.Likes, p.Title } into grouped
                                 select new PostViewsRow { PostId = grouped.Key.Id, Title = grouped.Key.Title, Likes = grouped.Key.Likes, Views = grouped.Count(t => t.PostId != null) };

            var viewModel = new PostViewsViewModel
            {
                Title = "Post Views",
                PostViews = postViewsQuery.ToList()
            };

            return View(viewModel);
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

            var postViews = await _db.PostViews
                .Select(pv => new PostViewsModel
                {
                    Id = pv.Id,
                    PostId = pv.PostId,
                    RefUrl = pv.RefUrl,
                    Timestamp = pv.Timestamp
                })
                .Where(pv => pv.PostId == id)
                .ToListAsync();
            
            var viewModel = new PostViewDetailsViewModel
            {
                Post = post,
                PostViews = postViews
            };

            return View(viewModel);
        }
    }
}