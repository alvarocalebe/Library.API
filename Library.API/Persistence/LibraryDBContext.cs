using Library.API.Entities;
namespace Library.API.Persistence
{
    public class LibraryDBContext
    {
        public List<Usuario> Usuarios { get; set; }
        public List<Livro> Livros { get; set; }
        public List<Autor> Autores { get; set; }
        public List<Reserva> Reservas { get; set; }

        public LibraryDBContext()
        {
            Usuarios = new List<Usuario>();
            Livros = new List<Livro>();
            Autores = new List<Autor>();
            Reservas = new List<Reserva>();
        }
    }
 }
