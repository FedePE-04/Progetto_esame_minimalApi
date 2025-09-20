


using Libro_Progetto.Classi;

namespace Libro_Progetto
{
    public class Libro
    {
        public int Id { get; set; }
        public string? Titolo { get; set; }
        public int Anno { get; set; }

        public Autore? Autore { get; set; }

    }
}
