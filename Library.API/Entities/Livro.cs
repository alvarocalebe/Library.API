using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Library.API.Entities
{
    public class Livro
    {
        public Livro(Guid id, String titulo, String categoria, String descricao, int anoPublicacao) {

            ID = id;
            Titulo = titulo;
            Categoria = categoria;
            Descricao = descricao;
            AnoPublicacao = anoPublicacao;
        
        }

        public Guid ID { get; set; }
        public String Titulo { get; set; }
        public String Categoria { get; set; }
        public String Descricao { get; set; }
        public int AnoPublicacao { get; set; }


        public void Update(String titulo, String categoria, String descricao, int anoPublicacao)
        {
            Titulo = titulo;
            Categoria = categoria;
            Descricao = descricao;
            AnoPublicacao = anoPublicacao;
        }

    }

    public class LivroDTO
    {
        public String Titulo { get; set; }
        public String Categoria { get; set; }
        public String Descricao { get; set; }
        public int AnoPublicacao { get; set; }
    }

}
