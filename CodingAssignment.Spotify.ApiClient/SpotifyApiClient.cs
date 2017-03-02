using System;
using System.Net.Http;
using System.Threading.Tasks;
using CodingAssignment.Spotify.ApiClient.Models;
using Flurl;
using Newtonsoft.Json;

namespace CodingAssignment.Spotify.ApiClient
{
    public class SpotifyApiClient
    {
        private const string ClientId = "996d0037680544c987287a9b0470fdbb";
        private const string ClientSecret = "5a3c92099a324b8f9e45d77e919fec13";

        protected const string BaseUrl = "https://api.spotify.com/";
        private HttpClient GetDefaultClient()
        {
            var authHandler = new SpotifyAuthClientCredentialsHttpMessageHandler(
                ClientId,
                ClientSecret,
                new HttpClientHandler());

            var client = new HttpClient(authHandler)
            {
                BaseAddress = new Uri(BaseUrl)
            };

            return client;
        }

        public async Task<SearchArtistResponse> SearchArtistsAsync(string artistName, int? limit = null, int? offset = null)
        {
            var client = GetDefaultClient();

            var url = new Url("/v1/search");
            url = url.SetQueryParam("q", artistName);
            url = url.SetQueryParam("type", "artist");

            if (limit != null)
                url = url.SetQueryParam("limit", limit);

            if (offset != null)
                url = url.SetQueryParam("offset", offset);

            var response = await client.GetStringAsync(url);

            var artistResponse = JsonConvert.DeserializeObject<SearchArtistResponse>(response);
            return artistResponse;
        }

        public async Task<GetRecommendationsResponse> GetRecommendationsAsync(string artistId,
                                                                                float? tempo = null,
                                                                                float? energy = null,
                                                                                float? danceability = null,
                                                                                int? mode = null,
                                                                                int? limit = null)
        {
            var client = GetDefaultClient();

            var url = new Url("/v1/recommendations");
            url = url.SetQueryParam("seed_artists", artistId);

            if (tempo != null)
                url = url.SetQueryParam("target_tempo", tempo);
            if (energy != null)
                url = url.SetQueryParam("target_energy", energy);
            if (danceability != null)
                url = url.SetQueryParam("target_danceability", danceability);
            if (mode != null)
                url = url.SetQueryParam("target_mode", mode);
            if (limit != null)
                url = url.SetQueryParam("limit", limit);

            var response = await client.GetStringAsync(url);

            var recommendationsResponse = JsonConvert.DeserializeObject<GetRecommendationsResponse>(response);
            return recommendationsResponse;
        }
    }
}
