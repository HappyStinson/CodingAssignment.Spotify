using System.Threading.Tasks;
using System.Web.Mvc;
using CodingAssignment.Spotify.ApiClient;
using WebClient.DAL;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class BasisController : Controller
    {
        private RecommendationContext db = new RecommendationContext();

        // GET: Basis
        public ActionResult Index()
        {
            ViewBag.ImageUrl = "/images/note.png";

            return View("Create");
        }

        // GET: Basis/Create
        public ActionResult Create()
        {
            ViewBag.ImageUrl = "/images/note.png";

            return View();
        }

        // POST: Basis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Artist")] RecommendationBasis recommendationBasis)
        {
            if (ModelState.IsValid)
            {
                var client = new SpotifyApiClient();

                var response = await client.SearchArtistsAsync(recommendationBasis.Artist);
                var artists = response.Artists;

                if (artists.Total > 0)
                {
                    var artist = artists.Items[0];
                    var recommendations = await client.GetRecommendationsAsync(artist.Id);

                    return View("Details", recommendations.Tracks);
                }

                ViewBag.Message = "No artists match your search";
                ViewBag.ImageUrl = "/images/note.png";

                return View();
            }

            return View(recommendationBasis);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
