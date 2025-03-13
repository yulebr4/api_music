using api_music.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace api_music.View
{

    /// Interfaz que define los métodos que debe implementar la vista en el patrón MVP.
    /// Se encarga de mostrar la información relacionada con las canciones en la interfaz de usuario.
    public interface ICancionView
    {

        /// Muestra la letra de una canción en la interfaz de usuario.
        void MostrarLetra(Cancion cancion);

        /// Muestra una lista de canciones en la interfaz de usuario.
        void MostrarCanciones(List<Cancion> canciones);

        /// Muestra un mensaje en la interfaz de usuario, por ejemplo, errores o notificaciones.
        void MostrarMensaje(string mensaje);
    }
}
