using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MuralDeRecados.Models
{
    [Serializable]
    public class Usuario
    {
        public int UsuarioId { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        public string Telefone { get; set; }

        public string LoginRedeSocial { get; set; }

        public virtual ICollection<Mural> Murais { get; set; }
    }
}