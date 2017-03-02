using System.Threading.Tasks;
using System.Web.Mvc;
using CodingAssignment.Spotify.ApiClient;
using WebClient.DAL;
using WebClient.Models;
using CodingAssignment.Spotify.ApiClient.Models;

namespace WebClient.Controllers
{
    public class BasisController : Controller
    {
        private RecommendationContext db = new RecommendationContext();

        // GET: Basis
        public ActionResult Index()
        {
            return RedirectToAction("Create");
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
        public async Task<ActionResult> Create([Bind(Include = "Id, Artist, Tempo, Energy, Danceability, Mode")] RecommendationBasis basis)
        {
            if (ModelState.IsValid)
            {
                var client = new SpotifyApiClient();

                var response = await client.SearchArtistsAsync(basis.Artist);
                var artists = response.Artists;

                GetRecommendationsResponse recommendations = null;
                if (artists.Total > 0)
                {
                    var artist = artists.Items[0];
                    var energy = basis.Energy * 0.01f;
                    var danceability = 0.0f;
                    var mode = 0;

                    if (basis.Danceability)
                        danceability = 1.0f;
                    if (basis.Mode)
                        mode = 1;

                    recommendations = await client.GetRecommendationsAsync(artist.Id, basis.Tempo, energy, danceability, mode);
                }
                else
                {
                    recommendations = await client.GetRecommendationsAsync("0OdUWJ0sBjDrqHygGUXeCF");
                }

                return View("Details", recommendations.Tracks);
            }

            return View(basis);
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
