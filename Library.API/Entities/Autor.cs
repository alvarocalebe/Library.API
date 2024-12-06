using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.API.Entities
{
    public class Autor
    {

        public Autor(Guid ID, String nome, String sobreNome, String nacionalidade, int idade)
        {
            ID = ID;
            Nome = nome;
            SobreNome = sobreNome;
            Nacionalidade = nacionalidade;
            Idade = idade;
            Livros = new List<Livro>();
        }
        public Guid ID { get; set; } 
        public String Nome { get; set; }
        public String SobreNome { get; set; }
        public String Nacionalidade { get; set; }
        public int Idade { get; set; }
        public List<Livro> Livros { get; set; }


        public void Update(String none, String sobreNome, String nacionalidade, int idade)
        {
            Nome = none;
            SobreNome = sobreNome;
            Nacionalidade = nacionalidade;
            Idade = idade;
        }

    }

    public class AutorDTO
    {
        public String Nome { get; set; }
        public string SobreNome { get; set; }
        public String Nacionalidade { get; set; }
        public int Idade { get; set; }
    }
}
