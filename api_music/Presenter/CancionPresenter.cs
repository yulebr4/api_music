using api_music.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using api_music.View;

namespace api_music.Presenter
{
    public class CancionPresenter
    {

        // Referencia a la vista para actualizar la UI con los resultados
        private readonly ICancionView _view;

        // Instancia del servicio que obtiene la información de la canción
        private readonly CancionService _service;

        public CancionPresenter(ICancionView view)
        {
            _view = view;
            _service = new CancionService();
        }


        // Busca la letra de una canción de manera asíncrona.
        public async Task BuscarCancionAsync(string titulo, string artista)
        {

            // Llama al servicio para obtener la canción
            var cancion = await _service.GetCancionAsync(titulo, artista);

            if (cancion != null)

            {

                // Si la canción se encuentra, muestra la letra en la vista
                _view.MostrarLetra(cancion);
                _view.MostrarImagen(cancion.ImagenUrl);


            }

            else

                // Si la canción no se encuentra, muestra un mensaje de error en la vista
                _view.MostrarMensaje("Letra no encontrada.");
        }
    }
}