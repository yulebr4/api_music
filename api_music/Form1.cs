using api_music.Model;
using api_music.Presenter;
using api_music.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace api_music
{

    /// Formulario principal de la aplicación que implementa la interfaz ICancionView en el patrón MVP.
    /// Permite al usuario buscar la letra de una canción mediante el ingreso del título y el artista.
    public partial class Form1 : Form, ICancionView
    {

        // Instancia del presentador que manejará la lógica de la búsqueda de canciones
        private CancionPresenter _presenter;


        //Constructor
        public Form1()
        {
            InitializeComponent();
            _presenter = new CancionPresenter(this);


        }


        /// Evento que se ejecuta cuando el formulario se carga.
        /// Busca la letra de la canción ingresada por el usuario.
        private async void Form1_Load(object sender, EventArgs e)
        {

            // Obtiene el título y el artista ingresados por el usuario
            string titulo = txtTitulo.Text.Trim();
            string artista = txtArtista.Text.Trim();


            // Validar que ambos campos no estén vacíos
            if (string.IsNullOrEmpty(titulo) || string.IsNullOrEmpty(artista))
            {
                MessageBox.Show("Por favor, ingrese el título y el artista de la cancion que busca.");
                return;
            }


            // Llamar al presentador para buscar la canción de manera asíncrona
            await _presenter.BuscarCancionAsync(titulo, artista);


            // Limpiar los TextBox después de la búsqueda
            txtTitulo.Clear();
            txtArtista.Clear();

            // Opcional: Mover el foco al primer TextBox
            txtTitulo.Focus();
        }


        /// Muestra la letra de la canción en un MessageBox.
        public void MostrarLetra(Cancion cancion)
        {
            MessageBox.Show($"Letra de {cancion.Titulo}:\n\n{cancion.Letra}", "Letra", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MostrarCanciones(List<Cancion> canciones)
        {

        }


        /// Muestra un mensaje informativo en la interfaz mediante un MessageBox.
        public void MostrarMensaje(string mensaje)
        {
            MessageBox.Show(mensaje, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void label5_Click(object sender, EventArgs e)
        {

        }

        public void MostrarImagen(string imageUrl)
        {
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    byte[] imageBytes = webClient.DownloadData(imageUrl);
                    using (var ms = new System.IO.MemoryStream(imageBytes))
                    {
                        picAlbum.Image = Image.FromStream(ms);
                    }
                }
            }
            catch
            {
                MessageBox.Show("No se pudo cargar la imagen del álbum.");
            }
        }
    }
}
    






