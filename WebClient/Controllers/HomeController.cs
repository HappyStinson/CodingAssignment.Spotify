using System.Threading.Tasks;
using System.Web.Mvc;
using CodingAssignment.Spotify.ApiClient;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        public async Task<ActionResult> Index()
        {
            var client = new SpotifyApiClient();

            var response = await client.SearchArtistsAsync("kissing candice");
            ViewBag.Response = response;
            var artists = response.Artists;
            var artist = artists.Items[0];

            ViewBag.Message = $"{artist.Name}, popularity: {artist.Popularity}";

            if (artist.Images.Count != 0)
                ViewBag.ImageUrl = artist.Images[0].Url;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}