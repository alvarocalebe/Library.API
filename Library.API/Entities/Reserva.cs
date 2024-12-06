namespace Library.API.Entities
{
    public class Reserva
    {   
        public Reserva(Guid id, Livro livro) {

            ID = id;
            Livro = livro;
            dataEmprestimo = DateTime.Now;
            dataDevolucao = dataEmprestimo.AddDays(7);
        }

        public Guid ID { get; set; }
        public Livro Livro { get; set; }
        public DateTime dataEmprestimo { get; set; }
        public DateTime dataDevolucao { get; set; }
    }

    public class ReservaDTO
    {
        public Guid LivroID { get; set; }
 
    }

    
}
