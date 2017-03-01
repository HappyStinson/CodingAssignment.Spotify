using System.Data.Entity;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using CodingAssignment.Spotify.ApiClient;
using CodingAssignment.Spotify.ApiClient.Models;
using WebClient.DAL;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class BasisController : Controller
    {
        private RecommendationContext db = new RecommendationContext();

        // GET: Basis
        public async Task<ActionResult> Index()
        {
            return View(await db.Basis.ToListAsync());
        }

        // GET: Basis/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecommendationBasis recommendationBasis = await db.Basis.FindAsync(id);
            if (recommendationBasis == null)
            {
                return HttpNotFound();
            }
            return View(recommendationBasis);
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
                    ViewBag.Message = $"{artist.Name}, popularity: {artist.Popularity}";

                    if (artist.Images.Count > 0)
                        ViewBag.ImageUrl = artist.Images[0].Url;
                }
                else
                {
                    ViewBag.Message = "No artists match your search";
                    ViewBag.ImageUrl = "/images/note.png";
                }

                // fulhack med ViewBag
                return View();

                //db.Basis.Add(recommendationBasis);
                //await db.SaveChangesAsync();
                //return RedirectToAction("Index"); // Recommendation skicka med viewmodel?
            }

            return View(recommendationBasis);
        }

        // GET: Basis/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecommendationBasis recommendationBasis = await db.Basis.FindAsync(id);
            if (recommendationBasis == null)
            {
                return HttpNotFound();
            }
            return View(recommendationBasis);
        }

        // POST: Basis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Artist")] RecommendationBasis recommendationBasis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(recommendationBasis).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(recommendationBasis);
        }

        // GET: Basis/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RecommendationBasis recommendationBasis = await db.Basis.FindAsync(id);
            if (recommendationBasis == null)
            {
                return HttpNotFound();
            }
            return View(recommendationBasis);
        }

        // POST: Basis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RecommendationBasis recommendationBasis = await db.Basis.FindAsync(id);
            db.Basis.Remove(recommendationBasis);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
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
