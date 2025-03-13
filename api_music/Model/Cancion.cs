using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace api_music.Model
{
    public class Cancion
    {
        
        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Artista { get; set; }
        public string Letra { get; set; }

        public string ImagenUrl { get; set; }

    }
}

