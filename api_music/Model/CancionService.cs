using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;




namespace api_music.Model
{

    public class CancionService 
    {

        // Cliente HTTP para realizar solicitudes a la API
        private HttpClient Client;


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
                // Construye la URL de la solicitud a la API
                string url = $"https://api.lyrics.ovh/v1/{artista}/{titulo}";


                // Realiza una solicitud GET a la API
                HttpResponseMessage response = await Client.GetAsync(url);

                // Verifica si la respuesta es exitosa, de lo contrario lanza una excepción
                response.EnsureSuccessStatusCode();


                // Lee el contenido de la respuesta como una cadena JSON
                string responseJson = await response.Content.ReadAsStringAsync();


                // Deserializa el JSON de respuesta a un objeto dinámico
                var jsonResponse = JsonConvert.DeserializeObject<dynamic>(responseJson);


                // Verifica si la respuesta contiene la propiedad "lyrics"
                if (jsonResponse["lyrics"] != null)
                {

                    // Retorna un objeto Cancion con los datos obtenidos
                    return new Cancion
                    {
                        Titulo = titulo,
                        Artista = artista,
                        Letra = jsonResponse["lyrics"].ToString()
                    };
                }

                // Retorna null si no se encuentra la letra de la canción
                return null;
            }
            catch
            {

                // Retorna null en caso de error (por ejemplo, si la canción no se encuentra o hay un problema con la API)
                return null;
            }
        }
    }
}