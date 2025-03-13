using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;




namespace api_music.Model
{

    public class CancionService 
    {

        // Cliente HTTP para realizar solicitudes a la API
        private HttpClient Client;
        private const string LastFmApiKey = "ca6bfd1c64c4ffe3160f1260f18b2975";

        /// Constructor de la clase CancionService.
        /// Inicializa una nueva instancia de HttpClient.
        public CancionService()
        {
            Client = new HttpClient();
        }


        /// Obtiene la letra de una canción desde la API lyrics.ovh.
        public async Task<Cancion> GetCancionAsync(string titulo, string artista)
        {
            try
            {
                // Obtener la letra de la canción desde Lyrics.ovh
                string lyricsUrl = $"https://api.lyrics.ovh/v1/{artista}/{titulo}";
                HttpResponseMessage lyricsResponse = await Client.GetAsync(lyricsUrl);
                lyricsResponse.EnsureSuccessStatusCode();
                string lyricsJson = await lyricsResponse.Content.ReadAsStringAsync();
                var lyricsData = JsonConvert.DeserializeObject<dynamic>(lyricsJson);

                // Obtener la imagen del álbum desde Last.fm
                string imageUrl = await GetAlbumImageAsync(titulo, artista);

                // Si encontramos la letra, retornamos el objeto Cancion
                if (lyricsData["lyrics"] != null)
                {
                    return new Cancion
                    {
                        Titulo = titulo,
                        Artista = artista,
                        Letra = lyricsData["lyrics"].ToString(),
                        ImagenUrl = imageUrl
                    };
                }

                return null;
            }
            catch
            {
                return null;
            }
        }

        private async Task<string> GetAlbumImageAsync(string titulo, string artista)
        {
            try
            {
                string lastFmUrl = $"http://ws.audioscrobbler.com/2.0/?method=track.getInfo&api_key={LastFmApiKey}&artist={artista}&track={titulo}&format=json";
                HttpResponseMessage response = await Client.GetAsync(lastFmUrl);
                response.EnsureSuccessStatusCode();
                string jsonResponse = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<dynamic>(jsonResponse);

                // Extraer la URL de la imagen (tamaño grande)
                string imageUrl = data["track"]["album"]["image"][2]["#text"];
                return string.IsNullOrEmpty(imageUrl) ? "https://via.placeholder.com/150" : imageUrl;
            }
            catch
            {
                return "https://via.placeholder.com/150"; // Imagen por defecto en caso de error
            }
        }
    }
}