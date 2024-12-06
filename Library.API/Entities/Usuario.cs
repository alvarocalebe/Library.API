using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.API.Entities
{
    public class Usuario
    {   
     public Usuario(Guid ID, String userName, String email){
            ID = ID;
            UserName = userName;
            Email = email;
            Reservas = new List<Reserva>();
            
        }
        public Guid ID { get; set; }
        public String UserName { get; set; }
        public String Email { get; set; }

        // Relacionamento com reservas
        public List<Reserva> Reservas { get; set; }

        public void Update(String userName, String email)
        {
            UserName = userName;
            Email = email;
        }


    }

    public class UsuarioDTO
    {
        public String UserName { get; set; }
        public String Email { get; set; }
    }
}
